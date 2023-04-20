using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO.Compression;
using Nerosoft.Insights.Framework;
using Nerosoft.Insights.Storage.Application;

namespace Nerosoft.Insights.Storage;

public class MessageBus
{
    private static readonly Dictionary<Type, Type> _typeMaps = new()
    {
        [typeof(Models.EventLog)] = typeof(Domain.Event),
        [typeof(Models.HandledErrorLog)] = typeof(Domain.Error),
        [typeof(Models.ManagedErrorLog)] = typeof(Domain.Error),
        [typeof(Models.StartSessionLog)] = typeof(Domain.Session)
    };

    private readonly MessageBusOptions _options;
    private readonly IServiceProvider _provider;
    private readonly IObjectMapper _mapper;

    public MessageBus(IOptions<MessageBusOptions> options, IServiceProvider provider, IObjectMapper mapper)
    {
        _provider = provider;
        _mapper = mapper;
        _options = options.Value;
    }

    public void Run()
    {
        var factory = new ConnectionFactory { Uri = new Uri(_options.Connection) };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(_options.Queue, true, false, false, null);
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += HandleMessageReceived;

        channel.BasicConsume(_options.Queue, true, consumer);
    }

    private async void HandleMessageReceived(object _, BasicDeliverEventArgs args)
    {
        using (var scope = _provider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var service = scope.ServiceProvider.GetService<IStorageService>();

            if (service == null)
            {
                return;
            }

            var body = args.Body.ToArray();
            var message = await DecompressAsync(body);
            var jsonObject = JsonConvert.DeserializeObject<JObject>(message);
            var array = (JArray)jsonObject.GetValue("logs");
            if (array == null)
            {
                return;
            }

            foreach (var token in array)
            {
                try
                {
                    var log = LogSerializer.Deserialize<Models.Log>(token.ToString());

                    var modelType = log.GetType();
                    if (!_typeMaps.TryGetValue(modelType, out var entityType))
                    {
                        continue;
                    }

                    var entity = _mapper.ProjectAs(log, entityType);
                    if (entity is Domain.Session session)
                    {
                        session.AppSecret = GetHeader(args, "AppSecret");
                        session.InstallId = GetHeader(args, "InstallId");
                    }

                    await service.InsertAsync(entity);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                    Debug.WriteLine(token);
                }
            }
        }
    }

    private static string GetHeader(BasicDeliverEventArgs args, string name)
    {
        if (args?.BasicProperties?.Headers?.ContainsKey(name) != true)
        {
            return string.Empty;
        }

        var bytes = (byte[])args.BasicProperties.Headers[name];
        if (bytes == null)
        {
            return string.Empty;
        }

        return Encoding.UTF8.GetString(bytes);
    }

    private static async Task<string> DecompressAsync(byte[] buffer)
    {
        try
        {
            using var source = new MemoryStream(buffer);

            using (var zipStream = new GZipStream(source, CompressionMode.Decompress, false))
            using (var reader = new StreamReader(zipStream))
            {
                string data = await reader.ReadToEndAsync();
                return data;
            }
        }
        catch
        {
            return Encoding.UTF8.GetString(buffer);
        }

        //using var stream = new GZipStream(source, CompressionMode.Decompress, false);
        //using var memory = new MemoryStream();
        ////int count = 0;
        ////do
        ////{
        ////    count = await stream.ReadAsync(bytes.AsMemory(0, 1024));
        ////    if (count > 0)
        ////    {
        ////        memory.Write(bytes, 0, count);
        ////    }
        ////}
        ////while (count > 0);
        ////return memory.ToArray();

        //await stream.CopyToAsync(memory);
        //var bytes = memory.ToArray();
        //return Encoding.UTF8.GetString(bytes);
    }
}

public class MessageBusOptions
{
    public string Connection { get; set; }

    public string Queue { get; set; }
}
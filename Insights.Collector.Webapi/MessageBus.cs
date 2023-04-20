using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Nerosoft.Insights.Collector;

public class MessageBus
{
    private readonly IModel _channel;
    private readonly MessageBusOptions _options;

    public MessageBus(IOptions<MessageBusOptions> options)
    {
        _options = options.Value;
        var factory = new ConnectionFactory { Uri = new Uri(_options.Connection) };

        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
    }

    public void Send<T>(T message, string installId, string appSecret)
        where T : class
    {
        var content = JsonConvert.SerializeObject(message);
        Send(Encoding.UTF8.GetBytes(content), installId, appSecret);
    }

    public void Send(byte[] message, string installId, string appSecret)
    {
        //var factory = new ConnectionFactory { Uri = new Uri(_options.Connection) };
        //using var connection = factory.CreateConnection();
        //using var channel = connection.CreateModel();

        // channel.QueueDeclare(queue: _options.Queue,
        //     durable: true,
        //     exclusive: false,
        //     autoDelete: false,
        //     arguments: null);

        var props = _channel.CreateBasicProperties();
        props.Headers ??= new Dictionary<string, object>();

        if (!string.IsNullOrEmpty(appSecret))
        {
            props.Headers["AppSecret"] = appSecret;
        }

        if (!string.IsNullOrEmpty(installId))
        {
            props.Headers["InstallId"] = installId;
        }

        _channel.BasicPublish("", _options.Queue, props, message);
    }
}

public class MessageBusOptions
{
    public string Connection { get; set; }

    public string Queue { get; set; }
}
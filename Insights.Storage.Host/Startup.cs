using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nerosoft.Insights.Framework;
using Nerosoft.Insights.Storage.Mapping;

namespace Nerosoft.Insights.Storage;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        LogSerializer.RegisterType(typeof(ILog).Assembly);

        services.Configure<MessageBusOptions>(Configuration.GetSection("MessageBusOptions"));
        services.AddSingleton<MessageBus>();

        services.AddObjectMapper<DefaultObjectMapper>(config =>
        {
            config.AddProfile<ModelMappingProfile>();
        });

        var connectionString = Configuration.GetConnectionString("DefaultConnection");

        var storage = Configuration.GetSection("Provider").Value;

        switch (storage)
        {
            case "mysql":
                services.AddMysqlStorage(connectionString);
                break;
            case "mssql":
                services.AddMssqlStorage(connectionString);
                break;
            case "mongo":
                services.AddMongoStorage(connectionString);
                break;
            case "pgsql":
                services.AddPgsqlStorage(connectionString);
                break;
        }

        services.AddService();

        // services.AddMassTransit(bus =>
        // {
        //     bus.UsingRabbitMq((context, config) =>
        //     {
        //         
        //         config.Host("localhost");
        //         config.ConfigureEndpoints(context);
        //         
        //         config.ReceiveEndpoint("queue:insights.collect", endpoint =>
        //         {
        //             endpoint.Consumer<LoggingHandler>();
        //         });
        //     });
        //
        //     //bus.AddConsumer<LoggingHandler>(context => { context.})
        //     
        // });

        services.AddGrpc(options =>
        {
            options.MaxReceiveMessageSize = null;
            options.EnableDetailedErrors = true;
            //options.Interceptors.Add<ExceptionHandlingInterceptor>();
        });
        services.AddGrpcReflection();
        services.AddHostedService<MessageService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        app.UseRouting();
        app.UseEndpoints(endpoint =>
        {
            endpoint.MapGrpcReflectionService();
            endpoint.MapGrpcService<HealthService>();
            endpoint.MapGrpcService<SessionGrpcService>();
            endpoint.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
        });
    }
}
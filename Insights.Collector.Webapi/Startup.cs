using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Nerosoft.Insights.Collector;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddRequestDecompression();
        services.AddProblemDetails();
        services.AddEndpointsApiExplorer();
        services.AddControllers()
                .AddNewtonsoftJson();

        services.Configure<MessageBusOptions>(Configuration.GetSection("MessageBusOptions"));
        services.AddSingleton<MessageBus>();

        //services.AddMassTransit(bus =>
        //{
        //    bus.UsingRabbitMq((context, config) =>
        //    {
        //        config.Host("localhost");
        //    });
        //});

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler();
        //app.UseRequestDecompression();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
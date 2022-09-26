using Microsoft.Extensions.Configuration;
using MkopaSMS.Consumer.RabbitMQConsumer;
using RabbitMQ.Client;
using System;
using System.IO;
using Serilog;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using static MkopaSMS.Consumer.RabbitMQConsumer.SMSFanoutExchangeConsumer;

namespace MkopaSMS.Consumer
{
    //Uses DI , Serilog and Settings
    class Program
    {
        static void Main(string[] args)
        {
            //Declare the connection factory to listen a channel 

            var builder = new ConfigurationBuilder();
            BuildConfig((IConfigurationBuilder)builder);
            Log.Logger = (Serilog.ILogger)new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console().CreateLogger();

            var host = Host.CreateDefaultBuilder()
                     .ConfigureServices((context, services) =>
                     {
                         services.AddTransient<IMkopaSMSService, MkopaSMSService>();
                         services.AddScoped<IWebSmsSender, WebSmsSender>();
                     })
                     //.UseSerilog()
                     .Build();

            var svc = ActivatorUtilities.CreateInstance<MkopaSMSService>(host.Services);
            
            svc.Run();


     
        }

        //Load configurations
        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Poduction"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}

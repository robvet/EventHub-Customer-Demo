// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using eventhub.producer;
using eventhub.producer.EventGenerator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using eventhub_demo_eventIngester.EventIngestion;

using Microsoft.Extensions.Logging;
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<ISendEvent, SendEvent>();

ILogger logger = LoggerFactory
                 .Create(builder => builder.AddConsole())
                 .CreateLogger("MyProgram");

logger.LogInformation($"EventSender application is starting{Environment.NewLine}");

// Create a new configuration builder
var config = new ConfigurationBuilder()
    //.AddJsonFile("appSettings.json")
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables()
    .Build();

var serviceProvider = new ServiceCollection()
                .AddSingleton<ISendEvent, SendEvent>()
                .BuildServiceProvider();



//var service = serviceProvider.GetService<ISendEvent>();


//    .AddUserSecrets<Program>()
//    .Build();

//builder.AddUserSecrets<Program>();

//// Add user secrets configuration source
//var environment = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
//if (environment == "Development")
//{
//    builder.AddUserSecrets<Program>();
//}
//builder.AddUserSecrets<YourClassName>();

// Build the configuration
//var configuration = builder.Build();

// Get the secret value for eventHubName

//var eventHubName = config["eventHubName"];
//var eventHubConnectionString = config["eventHubConnectionString"];
//var appInsightsConnectionString = config["applicationInsightsInstrumentationKey"];


await new GenerateEvents(logger, config).GenerateEventsAsync();

//await new ProduceEvents().ProduceEventsAsync(eventHubName,
//                                             eventHubConnectionString,
//                                             appInsightsConnectionString);


// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using eventhub.consumer;

// Create a new configuration builder
var config = new ConfigurationBuilder()    
    //.AddJsonFile("appSettings.json")
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables()
    .Build();


//var config = new ConfigurationBuilder()
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

var eventHubName = config["eventHubName"];
var eventHubConnectionString = config["eventHubConnectionString"];
var appInsightsConnectionString = config["applicationInsightsInstrumentationKey"];
var storageContainerConnectionString = config["storageContainerConnectionString"];
var storageContainerName = config["storageContainerName"];

await new ConsumeEvents().ConsumeEventsAsync(eventHubName,
                                             eventHubConnectionString,
                                             appInsightsConnectionString,
                                             storageContainerConnectionString,
                                             storageContainerName);   

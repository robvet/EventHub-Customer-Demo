using System.Threading.Tasks;

// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using eventhub.consumer;
using System.Diagnostics;

// Create a new configuration builder
var config = new ConfigurationBuilder()
    //.AddJsonFile("appSettings.json")
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables()
    .Build();

// Get the secret value for eventHubName
var eventHubName = config["eventHubName"];
var eventHubConnectionString = config["eventHubConnectionString"];
var appInsightsConnectionString = config["applicationInsightsInstrumentationKey"];
var storageContainerConnectionString = config["storageContainerConnectionString"];
var storageContainerName = config["storageContainerName"];


// Create tasks to run each process method
var task1 = Task.Run(() => new ProcessGasolineSalesEvents(eventHubName,
                                     eventHubConnectionString,
                                     appInsightsConnectionString,
                                     storageContainerConnectionString,
                                     storageContainerName,
                                     300)
                                       .ConsumeEventsAsync());

var task2 = Task.Run(() => new ProcessGrocerySalesEvents(eventHubName,
                                     eventHubConnectionString,
                                     appInsightsConnectionString,
                                     storageContainerConnectionString,
                                     storageContainerName,
                                     300)
                                       .ConsumeEventsAsync());

var task3 = Task.Run(() => new ProcessLotterySalesEvents(eventHubName,
                                     eventHubConnectionString,
                                     appInsightsConnectionString,
                                     storageContainerConnectionString,
                                     storageContainerName,
                                     300)
                                       .ConsumeEventsAsync());

//// Wait for all tasks to complete
await Task.WhenAll(task1, task2, task3);



//// Create tasks to run each process method
//var task1 = Task.Run(() =>
//{
//    using var gasolineTrace = new TraceListenerTextWriter(Console.Out);
//    new ProcessGasolineSalesEvents(eventHubName,
//                                   eventHubConnectionString,
//                                   appInsightsConnectionString,
//                                   storageContainerConnectionString,
//                                   storageContainerName,
//                                   300)
//                                       .ConsumeEventsAsync();
//});

//var task2 = Task.Run(() =>
//{
//    using var groceryTrace = new TraceListenerTextWriter(Console.Out);
//    new ProcessGrocerySalesEvents(eventHubName,
//                                   eventHubConnectionString,
//                                   appInsightsConnectionString,
//                                   storageContainerConnectionString,
//                                   storageContainerName,
//                                   300)
//                                       .ConsumeEventsAsync();
//});

//var task3 = Task.Run(() =>
//{
//    using var lotteryTrace = new TraceListenerTextWriter(Console.Out);
//    new ProcessLotterySalesEvents(eventHubName,
//                                   eventHubConnectionString,
//                                   appInsightsConnectionString,
//                                   storageContainerConnectionString,
//                                   storageContainerName,
//                                   300)
//                                       .ConsumeEventsAsync();
//});

//// Wait for all tasks to complete
//await Task.WhenAll(task1, task2, task3);

public class TraceListenerTextWriter : TextWriterTraceListener
{
    public TraceListenerTextWriter(TextWriter writer) : base(writer) { }

    public override void WriteLine(string message)
    {
        lock (this)
        {
            base.WriteLine(message);
            Console.WriteLine(message);
        }
    }
}
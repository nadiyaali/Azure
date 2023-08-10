// See https://aka.ms/new-console-template for more information
//https://learn.microsoft.com/en-us/azure/storage/queues/storage-quickstart-queues-dotnet?tabs=passwordless%2Croles-azure-portal%2Cenvironment-variable-windows%2Csign-in-azure-cli
// Install Nuget package Azure.Storage.Queues
// This project works with basic queues

using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
// All the messages sent should have base64, otherwise Azure Function Queue Trigger cannot read them
// Azure Function Queue Trigger tries 5 times to read a msg and if unsuccessful, adds to poison queue


// Create a unique name for the queue
string queue_name = "demoqueue5";
// get it from Access keys blade
string conn_string = "DefaultEndpointsProtocol=https;AccountName=storagetabledemo111;AccountKey=mQuJ753qq81GRf5WwKmxIT/UyLmmhyZ1Jx+rZUzQqiGc/exb7Qwqrt/6R3DTRa4pn0ecmZi763wE+AStEdczOg==;EndpointSuffix=core.windows.net";


// Instantiate a QueueClient to create and interact with the queue
QueueClient queueClient = new QueueClient(conn_string, queue_name);

Console.WriteLine($"Creating queue: {queue_name}");

// Create the queue
await queueClient.CreateAsync();

Console.WriteLine("\nAdding messages to the queue...");

// Send several messages to the queue
await queueClient.SendMessageAsync("First message",);
await queueClient.SendMessageAsync("Second message");

// Save the receipt so we can update this message later
SendReceipt receipt = await queueClient.SendMessageAsync("Third message");





Console.WriteLine("\nPeek at the messages in the queue...");
// Peek at messages in the queue
PeekedMessage[] peekedMessages = await queueClient.PeekMessagesAsync(maxMessages: 10);

foreach (PeekedMessage peekedMessage in peekedMessages)
{
    // Display the message
    Console.WriteLine($"ID: {peekedMessage.MessageId}");

    Console.WriteLine($"Message: {peekedMessage.MessageText}");

}

Console.WriteLine("\nUpdating the third message in the queue...");

// Update a message using the saved receipt from sending the message
await queueClient.UpdateMessageAsync(receipt.MessageId, receipt.PopReceipt, "Third message has been updated");

Console.ReadKey();

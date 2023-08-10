// See https://aka.ms/new-console-template for more information
//https://learn.microsoft.com/en-us/dotnet/api/overview/azure/data.tables-readme?view=azure-dotnet#create-the-table-service-client


// First install Azure.Data.Tables

using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

string conn_string = "BlobEndpoint=https://storagetabledemo111.blob.core.windows.net/;QueueEndpoint=https://storagetabledemo111.queue.core.windows.net/;FileEndpoint=https://storagetabledemo111.file.core.windows.net/;TableEndpoint=https://storagetabledemo111.table.core.windows.net/;SharedAccessSignature=sv=2022-11-02&ss=t&srt=sco&sp=rwdlacu&se=2023-08-09T08:37:56Z&st=2023-08-09T00:37:56Z&spr=https&sig=Vz52SmBb1KZAEw0ryXm3V4Vlpf3SbO8j5sJ4fE2jW%2B8%3D";
string table_name = "CustomerNew";

//table operations
string _table_name = "mydemotable";
var table_service_client = new TableServiceClient(conn_string);

TableItem table = table_service_client.CreateTableIfNotExists(_table_name);
Console.WriteLine($"The created table's name is {table.Name}.");


//To interact with table entities, we must first construct a TableClient.

TableClient table_client = table_service_client.GetTableClient(_table_name);


// Make a dictionary entity by defining a <see cref="TableEntity">.
// PartitionKey and Rowkeyare like composite primary key
string partitionkey = "Stationary";
var tableEntity = new TableEntity(partitionkey, "Marker Set2")
{
    { "Product", "Marker Set" },
    { "Price", 5.00 },
    { "Quantity", 21 }
};

Console.WriteLine($"{tableEntity.RowKey}: {tableEntity["Product"]} costs ${tableEntity.GetDouble("Price")}.");

var tableEntity2 = new TableEntity(partitionkey, "Pencil Set2")
{
    { "Product", "Pencil Set" },
    { "Price", 3.00 },
    { "Quantity", 29 }
};

Console.WriteLine($"{tableEntity2.RowKey}: {tableEntity2["Product"]} costs ${tableEntity2.GetDouble("Price")}.");

// Add the newly created entity.
table_client.AddEntity(tableEntity);
table_client.AddEntity(tableEntity2);

//  List all the tables in this service
// Use the <see cref="TableServiceClient"> to query the service. Passing in OData filter strings is optional.
Pageable<TableItem> queryTableResults = table_service_client.Query(filter: $"TableName eq '{_table_name}'");
Console.WriteLine("The following are the names of the tables in the query results:");
// Iterate the <see cref="Pageable"> in order to access queried tables.
foreach (TableItem tabletemp in queryTableResults)
{
    Console.WriteLine(tabletemp.Name);
}


//List all the contents of the table
Pageable<TableEntity> queryResultsFilter = table_client.Query<TableEntity>(filter: $"PartitionKey eq '{partitionkey}'");
// Iterate the <see cref="Pageable"> to access all queried entities.
foreach (TableEntity qEntity in queryResultsFilter)
{
    Console.WriteLine($"{qEntity.GetString("Product")}: {qEntity.GetDouble("Price")}");
}

Console.WriteLine($"The query returned {queryResultsFilter.Count()} entities.");



// Deletes the table made previously.
//table_service_client.DeleteTable(_table_name);


Console.ReadKey();


// See https://aka.ms/new-console-template for more information


// Download nuget package Azure.Storage.Blobs

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

string _container_name = "mydatacontainer3";
string _conn_string = "DefaultEndpointsProtocol=https;AccountName=vscodesamplestorage;AccountKey=dZwZVhdOtdqQErAIQFYqhvtydCbgP0ZiqrVXQ36SClPsja5n9euf5jXJtVf8F/mIfGowesu4lWRp+AStWSusdg==;EndpointSuffix=core.windows.net";

// First connect to azure blob service
BlobServiceClient _service_client = new BlobServiceClient(_conn_string);
// Then create container
_service_client.CreateBlobContainer(_container_name);

Console.WriteLine("Creating BlobContainer using BlobClientService");


///////////////////////////////////////////////////////////////////////////////
Console.WriteLine("Container created");
// Within this container, now upload a file in this container

string _local_path = "C:\\Users\\A\\Bootcamp Code GIT\\bootcamp docs(no GIT control)\\Notes\\React.txt";
string _blob_name = "React.txt";
BlobContainerClient _container_client = _service_client.GetBlobContainerClient(_container_name);

BlobClient _blob_client  = _container_client.GetBlobClient(_blob_name);
// upload async can also be used
_blob_client.Upload(_local_path);

Console.WriteLine("File uploaded in container");

/////////////////////////////////////////////////////////////////////////////////
// Now list all the blobs in this container
Console.WriteLine("---Listing all items in container---");
foreach(BlobItem item in _container_client.GetBlobs())
    Console.WriteLine(item.Name);


////////////////////////////////////////////////////////////////////////////////////
//Now download a blob
string _download_path = "C:\\Users\\A\\Bootcamp Code GIT\\bootcamp docs(no GIT control)\\temp\\downloadedReact.txt";

_blob_client.DownloadTo(_download_path);
Console.WriteLine("Blob downloaded");






Console.ReadKey();


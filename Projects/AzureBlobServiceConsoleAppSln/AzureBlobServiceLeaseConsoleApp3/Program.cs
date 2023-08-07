using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;


// Connect to this container using SAS "Shared Access Signature"
string _container_name = "mydatacontainer3";
string _conn_string = "DefaultEndpointsProtocol=https;AccountName=vscodesamplestorage;AccountKey=dZwZVhdOtdqQErAIQFYqhvtydCbgP0ZiqrVXQ36SClPsja5n9euf5jXJtVf8F/mIfGowesu4lWRp+AStWSusdg==;EndpointSuffix=core.windows.net";
string _blob_name = "fileforedit.txt";


// First connect to azure blob service
BlobServiceClient _service_client = new BlobServiceClient(_conn_string);
// Second, get container
BlobContainerClient _container_client = _service_client.GetBlobContainerClient(_container_name);
// Third get blob
BlobClient _blob_client = _container_client.GetBlobClient(_blob_name);

MemoryStream _memory = new MemoryStream();
_blob_client.DownloadTo(_memory);
_memory.Position = 0;

StreamReader _reader = new StreamReader(_memory);
Console.WriteLine(_reader.ReadToEnd());

/////////////////////////////////////////////////////////////////////////////
// Create lease on this file and then upload it
BlobLeaseClient _blob_lease_client = _blob_client.GetBlobLeaseClient();
BlobLease _lease = _blob_lease_client.Acquire(TimeSpan.FromSeconds(30));

Console.WriteLine($"The lease is {_lease.LeaseId}");
// This code will giver error if there is already a lease on the client

BlobUploadOptions _blobUploadOption = new BlobUploadOptions()
{ 
    Conditions = new BlobRequestConditions()
    { 
        LeaseId = _lease.LeaseId,
    }
};


/////////////////////////////////////////////////////////////////////////////

StreamWriter _writer = new StreamWriter(_memory);
_writer.WriteLine("This line has been added in memory using StreamWriter, no need to save the file");
_writer.Flush();

_memory.Position = 0;

_blob_client.Upload(_memory, _blobUploadOption);
// If using lease
_blob_lease_client.Release();


Console.WriteLine("Change made");
Console.ReadKey();




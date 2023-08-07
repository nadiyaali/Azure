// Download nuget package Azure.Storage.Blobs

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

// Connect to this container using SAS "Shared Access Signature"
string _container_name = "mydatacontainer3";
string _conn_string = "DefaultEndpointsProtocol=https;AccountName=vscodesamplestorage;AccountKey=dZwZVhdOtdqQErAIQFYqhvtydCbgP0ZiqrVXQ36SClPsja5n9euf5jXJtVf8F/mIfGowesu4lWRp+AStWSusdg==;EndpointSuffix=core.windows.net";
string _blob_name = "React.txt";
string _blob_local_path = "C:\\Users\\A\\Bootcamp Code GIT\\bootcamp docs(no GIT control)\\temp\\ReactDownloadedSas.txt";

/*
// First connect to azure blob service
BlobServiceClient _service_client = new BlobServiceClient(_conn_string);
// Second, get container
BlobContainerClient _container_client = _service_client.GetBlobContainerClient(_container_name);
// Third get blob
BlobClient _blob_client = _container_client.GetBlobClient(_blob_name);

//Now generate a uri for the blob
BlobSasBuilder _builder = new BlobSasBuilder()
{ 
    BlobContainerName = _container_name,  
    BlobName = _blob_name,
    Resource = "b"
};


_builder.SetPermissions(BlobAccountSasPermissions.Read | BlobAccountSasPermissions.List);
_builder.ExpiresOn = DateTime.UtcNow.AddHours(1);
Uri _blob_uri =  _blob_client.GenerateSasUri(_builder);

// Once the url is generated using SAS, it can be used to create blob client
BlobClient _client = new BlobClient(_blob_uri);
_client.DownloadTo(_blob_local_path);
Console.WriteLine("File Downloaded");
*/

/////////////////////////////////////////////////////////////////////////////////////////
// Read Metedata of the blob

// First connect to azure blob service
BlobServiceClient _service_client2 = new BlobServiceClient(_conn_string);
// Second, get container
BlobContainerClient _container_client2 = _service_client2.GetBlobContainerClient(_container_name);
// Third get blob
BlobClient _blob_client2 = _container_client2.GetBlobClient(_blob_name);

BlobProperties _properties = _blob_client2.GetProperties();

IDictionary<string,string> _metadata = _properties.Metadata;

foreach (var item in _metadata)
{
    Console.WriteLine(item.Key);
    Console.WriteLine(item.Value);

}


//////////////////////////////////////////////////////////////////////////////////////////////
//Write metadata to the blob
_metadata.Add("Filetype", "Text File");
_blob_client2.SetMetadata(_metadata);

Console.WriteLine("Added new metadata");

Console.ReadKey();
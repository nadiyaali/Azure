// See https://aka.ms/new-console-template for more information
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using System.Globalization;

// FROM UDEMY AZ 204 course
// This program uses StartCopyfrom Uri to copy a blob from one storage account to another

// put the connection string from Access key here
string connectionstring = "";
string sourcecontainer = "firstcontainer";
string destcontainer = "secondcontainer";
string sourcefile = "abc.txt";
string destfile = "abc-copy2.txt";

var sourceclient = new BlockBlobClient(connectionstring, sourcecontainer, sourcefile);
var destclient = new BlockBlobClient(connectionstring, destcontainer, destfile);

//destclient.StartCopyFromUri(sourceclient.Uri);

BlobProperties props = sourceclient.GetProperties();
Console.WriteLine(props.AccessTier);

IDictionary<string, string> metadata = new Dictionary<string, string>();
metadata.Add("CreatedBy", "Nadia");
metadata.Add("Environment", "Practice");

sourceclient.SetMetadata(metadata);


Console.ReadKey();

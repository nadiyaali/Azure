using CosmosConsoleApp3;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CosmosDB
{
    class Program
    {
        // This program reads the file QueryResult.json and creates json objects and uploads them to cosmosdb
        private static readonly string _connection_string = "AccountEndpoint=https://cosmosdemo222.documents.azure.com:443/;AccountKey=wVIUm9x5thMtcsVlPlcK5NHuHSfCbaXg3kKMV133Nuvz8CX1jga2VJTREOIg2VWSLIPhmhMeTIGfACDbblWcoA==;";
        private static readonly string _database_name = "appdb";
        private static readonly string _container_name = "activity";

        static async Task Main(string[] args)
        {

            FileStream _fs = new FileStream(System.Environment.CurrentDirectory + @"\data\QueryResult.json", FileMode.Open, FileAccess.Read);
            StreamReader _reader = new StreamReader(_fs);
            JsonTextReader _json_reader = new JsonTextReader(_reader);

            CosmosClient _client = new CosmosClient(_connection_string);
            Container _container = _client.GetContainer(_database_name, _container_name);

            while (_json_reader.Read())
            {
                if (_json_reader.TokenType == JsonToken.StartObject)
                {
                    JObject _object = JObject.Load(_json_reader);
                    Activity _activity = _object.ToObject<Activity>();
                    _activity.id = Guid.NewGuid().ToString();
                    await _container.CreateItemAsync<Activity>(_activity, new PartitionKey(_activity.Operationname));
                    Console.WriteLine($"Adding item {_activity.Correlationid}");
                }
            }

            Console.WriteLine("Written data to Azure Cosmos DB");
            Console.ReadKey();
        }
    }
}

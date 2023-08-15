using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace CosmosdbConsoleApp2
{
    class Program
    {
        private static readonly string _connection_string = "AccountEndpoint=https://cosmosdemo222.documents.azure.com:443/;AccountKey=wVIUm9x5thMtcsVlPlcK5NHuHSfCbaXg3kKMV133Nuvz8CX1jga2VJTREOIg2VWSLIPhmhMeTIGfACDbblWcoA==;";
        //private static readonly string _connection_string = "";
        private static readonly string _database_name = "appdb";
        private static readonly string _container_name = "course";
        private static readonly string _partition_key = "/courseid";

        static async Task Main(string[] args)
        {

            AddRecordCustomerOrder();
            Console.ReadKey();
        }


        public async static void AddRecordCustomerOrder()
        {
            CosmosClient _cosmosclient = new CosmosClient(_connection_string);
            
            Course _course = new Course()
            {
                id = "8",
                courseid = "C00108",
                coursename = "AZ-704",
                rating = 4.3m,
                orders = new List<Order>() { new Order() { orderid="O2",price=50},
                new Order() { orderid="O3",price=80}}
            };

            Container _container = _cosmosclient.GetContainer(_database_name, _container_name);

            await _container.CreateItemAsync<Course>(_course, new PartitionKey(_course.courseid));

            Console.WriteLine("Item with order is created");

        }

        /* Cosmosdb with Table API, 
         * This works the same as Table Storage Account, just the connection string changes
         * 
        private static string connection_string = "DefaultEndpointsProtocol=https;AccountName=apptable100;AccountKey=j4Rnbv92mayF7kHtTzvdl3CKO5Tk0Vodjw65ejrSp4a84KLWpn4rELvRxBjRg7aWDJprYYHbaJWRLb7xmkEkWw==;TableEndpoint=https://apptable100.table.cosmos.azure.com:443/;";
        private static string table_name = "customer";
        static void Main(string[] args)
        {
            CloudStorageAccount _account = CloudStorageAccount.Parse(connection_string);

            CloudTableClient _client = _account.CreateCloudTableClient();

            CloudTable _table = _client.GetTableReference(table_name);

            Customer _customer = new Customer("UserA", "Chicago");
            _customer.customerid = "C1";

            TableOperation _operation = TableOperation.Insert(_customer);

            TableResult _result = _table.Execute(_operation);

            Console.WriteLine("Customer Entity Added");
            Console.ReadKey();
        }
    }
         * 
         * 
         */



    }
}
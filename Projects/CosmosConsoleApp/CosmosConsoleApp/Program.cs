using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace CosmosConsoleApp
{
    class Program
    {
        private static readonly string _connection_string = "";
        private static readonly string _database_name = "appdb";
        private static readonly string _container_name = "course";
        private static readonly string _partition_key = "/courseid";

        static async Task Main(string[] args)
        {

            //CreateCosmosDb();
            //AddRecord();
            //AddRecordBulk();
            //ReadRecord();
            //UpdateRecord();
            //DeleteRecord();
            ExecuteStoredProcedure();
            Console.ReadKey();
        }

        public async static void CreateCosmosDb()
        {
            CosmosClient _cosmosclient = new CosmosClient(_connection_string);
            // Creating a database

            await _cosmosclient.CreateDatabaseAsync(_database_name);
            Console.WriteLine("Database created");
            // Creating a container in the database

            Database _cosmos_db = _cosmosclient.GetDatabase(_database_name);
            await _cosmos_db.CreateContainerAsync(_container_name, _partition_key);
            Console.WriteLine("Container created");

        }

        public async static void AddRecord()
        {
            CosmosClient _cosmosclient = new CosmosClient(_connection_string);

            Course _course = new Course()
            {
                id = "1",
                courseid = "C00102",
                coursename = "AZ-900",
                rating = 4.0m


            };

            // Get the container from db
            Container _container = _cosmosclient.GetContainer(_database_name, _container_name);
            await _container.CreateItemAsync<Course>(_course, new PartitionKey(_course.courseid));
            Console.WriteLine("Record has been created");

        
        }

        public async static void AddRecordBulk()
        {
            CosmosClient _cosmosclient = new CosmosClient(_connection_string, new CosmosClientOptions() { AllowBulkExecution=true });

            List<Course> _courselist = new List<Course>()
            {
            new Course(){
                id = "2",
                courseid = "C00102",
                coursename = "AZ-900",
                rating = 4.0m
            },
            new Course(){
                id = "3",
                courseid = "C00103",
                coursename = "AZ-104",
                rating = 4.4m
            },
            new Course(){
                id = "4",
                courseid = "C00104",
                coursename = "DP-900",
                rating = 3.9m
            },
            new Course(){
                id = "5",
                courseid = "C00105",
                coursename = "DP-100",
                rating = 4.0m
            },

        };
            // Get the container from db
            Container _container = _cosmosclient.GetContainer(_database_name, _container_name);
            List<Task> _tasks = new List<Task>();
            foreach (var _course in _courselist)
                _tasks.Add(_container.CreateItemAsync<Course>(_course, new PartitionKey(_course.courseid)));

            await Task.WhenAll(_tasks);

            Console.WriteLine("Bulk insert of records has been completed");


        }

        public async static void ReadRecord()
        {
            CosmosClient _cosmosclient = new CosmosClient(_connection_string);
            
            // Get the container from db
            Container _container = _cosmosclient.GetContainer(_database_name, _container_name);

            string _query = "Select * from c where c.courseid='C00105'";

            QueryDefinition _definition = new QueryDefinition(_query);
            FeedIterator<Course> _iterator = _container.GetItemQueryIterator<Course>(_definition);

            while (_iterator.HasMoreResults)
            {
               FeedResponse<Course> _response = await _iterator.ReadNextAsync();
                foreach (var _course in _response)
                {
                    Console.WriteLine($"Course id is {_course.id}");
                    Console.WriteLine($"Course name is {_course.coursename}");
                    Console.WriteLine($"Course rating is {_course.rating}");

                }
            }
 
            }



        public async static void UpdateRecord()
        {
            CosmosClient _cosmosclient = new CosmosClient(_connection_string);

            // Get the container from db
            Container _container = _cosmosclient.GetContainer(_database_name, _container_name);

            string _id = "2";
            string _partition_key = "C00102";

            ItemResponse<Course> _response = await _container.ReadItemAsync<Course>(_id, new PartitionKey(_partition_key));
            Course _course = _response.Resource;
            _course.rating = 2.9m;  
            await _container.ReplaceItemAsync<Course>(_course, _id, new PartitionKey(_partition_key));
            Console.WriteLine("Item has been updated");
        
        }

        public async static void DeleteRecord()
        {
            CosmosClient _cosmosclient = new CosmosClient(_connection_string);

            // Get the container from db
            Container _container = _cosmosclient.GetContainer(_database_name, _container_name);

            string _id = "5";
            string _partition_key = "C00105";

            await _container.DeleteItemAsync<Course>(_id, new PartitionKey(_partition_key));
            
            Console.WriteLine("Item has been deleted");

        }

       
        public async static void ExecuteStoredProcedure()
        {
            CosmosClient _cosmosclient = new CosmosClient(_connection_string);

            // Get the container from db
            Container _container = _cosmosclient.GetContainer(_database_name, _container_name);
            string _output = await _container.Scripts.ExecuteStoredProcedureAsync<string>("demo", new PartitionKey(String.Empty), null);

            Console.WriteLine($"Output from stored procedure : {_output}");
        }


        }
    }
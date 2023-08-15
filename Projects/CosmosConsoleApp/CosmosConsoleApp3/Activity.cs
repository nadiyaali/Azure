using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosConsoleApp3
{
    internal class Activity
    {
        public string id { get; set; }
        public string Correlationid { get; set; }
        public string Operationname { get; set; }
        public string Status { get; set; }
        public string Eventcategory { get; set; }
        public string Level { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosdbConsoleApp2
{
    internal class Course
    {
        public string id { get; set; }
        public string courseid { get; set; }
        public string coursename { get; set; }
        public decimal rating { get; set; }
        public List<Order> orders { get; set; }
    }
}

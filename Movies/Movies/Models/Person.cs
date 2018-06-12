using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class Person
    {
        public string id { get; set; }
        public string Name { get; set; }
        public char Sex { get; set; }
        public DateTime DOB { get; set; }
        public string Biography { get; set; }

        public string text { get; set; }
    }
}

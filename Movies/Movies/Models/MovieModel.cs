using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class MovieModel
    {
        public MovieModel()
        {
            Producer = new Person();
            Actors = new List<Person>();
        }

        public string id { get; set; }
        public string Name { get; set; }
        public int ReleaseYear { get; set; }
        public string Plot { get; set; }
        public Person Producer { get; set; }
        public List<Person> Actors { get; set; }

        public List<string> SelectedActors { get; set; }
        public string SelectedProducerOfMovie { get; set; }

    }
}

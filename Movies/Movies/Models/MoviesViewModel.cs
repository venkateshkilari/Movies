using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class MoviesViewModel
    {
        public MoviesViewModel()
        {
            MoviesList = new List<MovieModel>();
            Actors = new List<Person>();
            Producers = new List<Person>();
        }
        public List<MovieModel> MoviesList { get; set; }  
        public List<Person> Actors { get; set; }
        public List<Person> Producers { get; set; }

    }
}

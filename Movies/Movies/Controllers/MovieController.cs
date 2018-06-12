using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Movies.BL;
using Movies.Models;
using Newtonsoft.Json;

namespace Movies.Controllers
{
    public class MovieController : Controller
    {
        private readonly IConfiguration configuration;
        private Movie movie;


        public MovieController(IConfiguration config)
        {
            configuration = config;
            string connection = configuration.GetConnectionString("DatabaseConnection");
            movie = new Movie(connection);
        }
       

        public IActionResult MovieList()
        {

            MoviesViewModel moviesList = movie.GetProducersAndActors();
            moviesList.MoviesList= movie.GetMovieList();
            
            return View(moviesList);
        }
        public JsonResult getProducersAndActors()
        {
            MoviesViewModel moviesList = movie.GetProducersAndActors();
            return Json(moviesList);

        }

     

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string InsertProducer(string name,char sex,string dob,string bio)
        {
            Person producer = new Person();
            producer.Name = name;
            producer.Sex = sex;
            producer.DOB = Convert.ToDateTime(dob);
            producer.Biography = bio;

            return movie.InsertProducer(producer);
        }

        public string InsertActor(string name, char sex, string dob, string bio)
        {
            Person actor = new Person();
            actor.Name = name;
            actor.Sex = sex;
            actor.DOB = Convert.ToDateTime(dob);
            actor.Biography = bio;

            return movie.InsertActor(actor);
        }

        public string InsertMovie(string name, int year, string producerId, List<string> actorIds)
        {
            MovieModel movieModel= new MovieModel();
            movieModel.Name = name;
            movieModel.ReleaseYear = year;
            movieModel.Producer.id = producerId;
            movieModel.Actors = actorIds.Select(x=> new Person() { id=x}).ToList() ;

            return movie.InsertMovie(movieModel);
        }

        public bool UpdateMovie(string id,string name, int year, string producerId, List<string> actorIds)
        {
            MovieModel movieModel = new MovieModel();
            movieModel.id = id;
            movieModel.Name = name;
            movieModel.ReleaseYear = year;
            movieModel.Producer.id = producerId;
            movieModel.Actors = actorIds.Select(x => new Person() { id = x }).ToList();

            return movie.UpdateMovie(movieModel);
        }
    }
}

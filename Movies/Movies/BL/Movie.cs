using Movies.DAL;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.BL
{
    public class Movie
    {
        DALConnection dALConnection;
        public Movie(string connectionString)
        {
            dALConnection=new DALConnection(connectionString);
        }
        public List<MovieModel> GetMovieList()
        {
            List<MovieModel> movies = new List<MovieModel>();
            DataSet dataset = new DataSet();
            List<Person> actors = new List<Person>();
                         
            try
            {

                //movies.Add(new MovieModel()
                //{
                //    id = 1,
                //    Name = "BAN",
                //    ReleaseDate = DateTime.Now,
                //    Plot = "Plot Explained",
                //    Producer = new Person()
                //    {
                //        id = 1,
                //        Name = "person1",
                //        text = "person1",
                //        Sex = 'M',
                //        DOB = DateTime.Now
                //    },
                //    Actors = new List<Person>() {
                //        new Person() {
                //             id = 1,
                //        Name = "actor1",
                //         text = "actor1",
                //        Sex = 'M',
                //        DOB = DateTime.Now
                //        },
                //         new Person() {
                //             id = 2,
                //        Name = "actor2",
                //         text = "actor1",
                //        Sex = 'M',
                //        DOB = DateTime.Now
                //        }
                //    }
                    
                //});
               string query = "select A.movieId,A.name as movieName,A.releaseYear,A.plot,B.producerId,B.name as producerName,B.dob,B.sex,B.bio from movie A inner join producer B  on A.producerId=B.producerId;select A.movieId,B.actorId,B.name as actorName,B.dob,B.sex,B.bio from movieactorlink A inner join actor B  on A.actorId=B.actorId ";
                dataset = dALConnection.selectDatasetFromDatabase(query);
                if (dataset != null && dataset.Tables[0] != null && dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        actors = new List<Person>();
                        foreach(DataRow actorRow in dataset.Tables[1].Select("movieId='"+ Convert.ToString(row["movieId"])+"'"))
                        {
                            actors.Add(new Person()
                            {
                                id = Convert.ToString(actorRow["actorId"]),
                                Name = Convert.ToString(actorRow["actorName"]),
                                Sex = Convert.ToChar(actorRow["sex"]),
                                DOB = Convert.ToDateTime(actorRow["dob"])
                            });
                        }
                       
                       
                        movies.Add(new MovieModel()
                        {
                            id = Convert.ToString(row["movieId"]),
                            Name = Convert.ToString(row["movieName"]),
                            ReleaseYear = Convert.ToInt16(row["releaseYear"]),
                            Plot = Convert.ToString(row["plot"]),
                            Producer = new Person()
                            {
                                id = Convert.ToString(row["producerId"]),
                                Name = Convert.ToString(row["producerName"]),
                                Sex = Convert.ToChar(row["sex"]),
                                DOB = Convert.ToDateTime(row["dob"])
                            },
                           Actors= actors,
                            SelectedActors=actors.Select(x=>x.id).ToList(),
                            SelectedProducerOfMovie = Convert.ToString(row["producerId"])


                        }

                        );
                    }

                }
            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {

            }
            return movies;

        }

        public MoviesViewModel GetProducersAndActors()
        {
            MoviesViewModel moviesViewModel = new MoviesViewModel();
            DataSet dataSet = new DataSet();

            string query = "select * from producer; select * from actor";
            dataSet = dALConnection.selectDatasetFromDatabase(query);
            if (dataSet != null && dataSet.Tables[0]!=null && dataSet.Tables[0].Rows.Count> 0)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    moviesViewModel.Producers.Add(new Person()
                    {
                        id = Convert.ToString(row["producerId"]),
                        Name = Convert.ToString(row["name"]),
                        Sex = Convert.ToChar(row["sex"]),
                        DOB = Convert.ToDateTime(row["dob"]),
                        text= Convert.ToString(row["name"])
                    }

                    );
                }

            }
            if (dataSet != null && dataSet.Tables[0] != null && dataSet.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow row in dataSet.Tables[1].Rows)
                {
                    moviesViewModel.Actors.Add(new Person()
                    {
                        id = Convert.ToString(row["actorId"]),
                        Name = Convert.ToString(row["name"]),
                        Sex = Convert.ToChar(row["sex"]),
                        DOB = Convert.ToDateTime(row["dob"]),
                        text = Convert.ToString(row["name"])
                    }

                    );
                }

            }
         
            return moviesViewModel;
        }

        public string InsertProducer(Person producer)
        {
            string id = Guid.NewGuid().ToString();
            string query = "Insert into producer(producerId,name,sex,dob,bio) values('"+id+"','"+producer.Name+"','"+producer.Sex+"','"+producer.DOB+"','"+producer.Biography+"')";
            if (dALConnection.ExecuteNonQuery(query))
            {
                return id;
            }
            else
            {
                return string.Empty;
            }

                

        }

        public string InsertActor(Person actor)
        {
            string id = Guid.NewGuid().ToString();
            string query = "Insert into actor(actorId,name,sex,dob,bio) values('" + id + "','" + actor.Name + "','" + actor.Sex + "','" + actor.DOB + "','" + actor.Biography + "')";
            if (dALConnection.ExecuteNonQuery(query))
            {
                return id;
            }
            else
            {
                return string.Empty;
            }



        }

        public string InsertMovie(MovieModel movie)
        {
            string id = Guid.NewGuid().ToString();
            string query = "Insert into movie(movieId,name,releaseYear,posterFileName,producerId) values('" + id + "','" + movie.Name + "'," + movie.ReleaseYear + ",null,'" + movie.Producer.id + "')";
            if (dALConnection.ExecuteNonQuery(query))
            {
                foreach (var actor in movie.Actors)
                {
                    query = "Insert into movieActorLink(movieId,actorId,remuneration) values('" + id + "','" + actor.id + "',0)";
                    dALConnection.ExecuteNonQuery(query);
                }
                    return id;

            }
            else
            {
                return string.Empty;
            }



        }

        public bool UpdateMovie(MovieModel movie)
        {
;
            string query = "update movie set name='"+movie.Name+"',releaseYear="+movie.ReleaseYear+",posterFileName='',producerId='"+movie.Producer.id+"' where movieId='"+movie.id+"';";
            if (dALConnection.ExecuteNonQuery(query))
            {
                query = "delete from movieActorLink where movieId='"+movie.id+"';";
                dALConnection.ExecuteNonQuery(query);
                foreach (var actor in movie.Actors)
                {
                    if (!string.IsNullOrWhiteSpace(actor.id))
                    {
                        query = "Insert into movieActorLink(movieId,actorId,remuneration) values('" + movie.id + "','" + actor.id + "',0)";
                        dALConnection.ExecuteNonQuery(query);
                    }
                }
                return true;

            }
            else
            {
                return false;
            }



        }
    }
}

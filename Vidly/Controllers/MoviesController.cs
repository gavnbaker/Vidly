using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.VeiwModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        

        // GET: Movies
        public ActionResult Index()
        {
            return View(getMovies());
        }

        // GET: Movies/Details/1
        public ActionResult Details(int id)
        {
            var movie = getMovies().SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }


        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() {Name = "Shrek!"};
            var customers = new List<Customer>()
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"}
            };

            var veiwModel = new RandomMovieVeiwModel
            {
                Movie = movie,
                Customers = customers
            };
            return View(veiwModel);
        }

        private IEnumerable<Movie> getMovies()
        {
            return new List<Movie>()
            {
                new Movie() {Id = 1, Name = "I-Robot"},
                new Movie() {Id = 2, Name = "Shrek!"},
                new Movie() {Id = 3, Name = "Mafia I"}
            };
        }

    }
}
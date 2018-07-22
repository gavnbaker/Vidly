using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.VeiwModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext context;

        public MoviesController()
        {
            this.context = new ApplicationDbContext();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var movie = context.Movies.Single(m => m.Id == id);
            var genres = context.Genres.ToList();
            var movieViewModel = new MovieFormViewModel
            {
                Genres = genres,
                Movie = movie
            };
            return View("MovieForm", movieViewModel);

        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Today;
                context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = context.Movies.SingleOrDefault(m => m.Id == movie.Id);
                if (movieInDb == null)
                {
                    return HttpNotFound();
                }

                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.Stock = movie.Stock;
            }
            
            context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }


        [HttpGet]
        public ActionResult Create()
        {
            var genres = context.Genres.ToList();
            var movieViewModel = new MovieFormViewModel
            {
                Genres = genres,
            };

            return View("MovieForm", movieViewModel);
        }

        // GET: Movies
        public ActionResult Index()
        {
            var movies = context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        // GET: Movies/Details/1
        public ActionResult Details(int id)
        {
            var movie = context.Movies.Include(m => m.Genre)
                .SingleOrDefault(m => m.Id == id);

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

    }
}
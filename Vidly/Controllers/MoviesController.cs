using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() {Name = "Shrek!"};

            return Content("");

            //return Content("Hello World!");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new {page = 1, sortBy = "name"});
        }

        public ActionResult Edit(int id)
        {
            return Content("id=" + id);
        }

        //movies
        public ActionResult Index()
        {
            var movies = GetMovies();

            return View(movies);


        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")] //[Route("URL {PARAMETER:CONSTRAINTS()}")
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month.ToString("00"));
        }

        private List<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie { Id = 1, Name = "Shrek"},
                new Movie {Id = 2, Name = "Wall-e"}
            };
        }



    }
}
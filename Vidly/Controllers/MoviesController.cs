using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context; //database

        public MoviesController() //constructor initialize database so we can use it
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing) //dispose database
        {
            _context.Dispose();
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movies {Name = "Shrek!"};

            return Content("");

            //return Content("Hello World!");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new {page = 1, sortBy = "name"});
        }

        //movies
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");
            return View("ReadOnlyList");
        }

        //movies/details
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.MovieGenre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")] //[Route("URL {PARAMETER:CONSTRAINTS()}")
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month.ToString("00"));
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var movieGenres = _context.MovieGenres.ToList();
            var viewModel = new MovieFormViewModel
            {
                Movies = new Movies(),
                MovieGenres = movieGenres
            };

            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Bulk()
        {
            var upload = new Upload {Processed = 0};
            return View(upload);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movies movies)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Movies = movies,
                    MovieGenres = _context.MovieGenres.ToList()
                };
                return View("MovieForm", viewModel);
            }

            if (movies.Id == 0) //no Id, so new movie, insert it
            {
                movies.DateAdded = DateTime.Now;
                movies.NumberAvailable = movies.Stock;
                _context.Movies.Add(movies);
            }
            else //Id exists so, update existing movie
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movies.Id);

                movieInDb.Name = movies.Name;
                movieInDb.ReleaseDate = movies.ReleaseDate;
                movieInDb.MovieGenreId = movies.MovieGenreId;
                movieInDb.Stock = movies.Stock;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id); //get movie by id, or null
            var viewModel = new MovieFormViewModel();

            if (movie == null) //if null 404
                return HttpNotFound();

            if (!ModelState.IsValid) //if form doesn't validate
            {
                viewModel.Movies = movie;
                viewModel.MovieGenres = _context.MovieGenres.ToList();
                return View("MovieForm", viewModel);
            }

            viewModel.Movies = movie;
            viewModel.MovieGenres = _context.MovieGenres.ToList();

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult BulkUpload(Upload upload)
        {
            if (!ModelState.IsValid) return View("Bulk");

            var path = Server.MapPath("~/uploads/");

            if (upload.File == null)
                throw new NullReferenceException("File is null.");

            if (upload.File != null)
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

            var uploadId = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            var extension = Path.GetExtension(upload.File.FileName);
            var filePath = path + Path.GetFileNameWithoutExtension(upload.File.FileName) + "-" + uploadId + "-" +
                           extension;

            upload.File.SaveAs(filePath);

            //CSV should have header row with name,stock,releaseDate,Genre

            //read contents of file
            var csvData = System.IO.File.ReadAllText(filePath);

            var itemsProcessed = 0;

            //loop over data
            foreach (var row in csvData.Split('\n'))
            {
                var nameField = row.Split(',')[0];
                var stockField = row.Split(',')[1];
                var releaseDateField = row.Split(',')[2];
                var genreField = row.Split(',')[3];
                genreField = genreField.Replace("\r", "");

                //validate headers
                if (itemsProcessed == 0)
                {
                    if (nameField.ToLower() != "name") upload.ColumnError = "name";
                    if (stockField.ToLower() != "stock") upload.ColumnError = "stock";
                    if (releaseDateField.ToLower() != "releasedate") upload.ColumnError = "releaseDate";
                    if (genreField.ToLower() != "genre") upload.ColumnError = "genre";
                }

                if (!string.IsNullOrWhiteSpace(upload.ColumnError))
                {
                    upload.Processed = 0;
                    return View("Bulk", upload);
                }

                if (row.Split(',')[0].ToLower() == "name" && itemsProcessed == 0) //skip header row
                {
                    itemsProcessed++;
                    continue;
                } 

                if (!string.IsNullOrEmpty(row))
                {
                    var movie = new Movies
                    {
                        Name = nameField,
                        Stock = Convert.ToInt32(stockField),
                        ReleaseDate = DateTime.Parse(releaseDateField),
                        MovieGenreId = Convert.ToInt32(genreField),
                        DateAdded = DateTime.Now
                    };

                    movie.NumberAvailable = movie.Stock;

                    _context.Movies.Add(movie);
                }

                itemsProcessed++;
            }

            _context.SaveChanges();

            upload.Processed = itemsProcessed - 1;

            return View("Bulk", upload);
        }
    }

}
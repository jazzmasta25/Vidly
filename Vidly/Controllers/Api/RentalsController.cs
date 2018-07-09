using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class RentalsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);

            if (customer == null)
                return BadRequest("Customer not found.");

            var movies = _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();
            /*
             * SELECT * FROM Movies WHERE Id IN (1, 2, 3);
            */

            if (movies.Count != newRental.MovieIds.Count)
                return BadRequest("One or more movie IDs are invalid.");

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movies = movie,
                    DateRented = DateTime.Now
                };
                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteRental(int id)
        {
            var rental = _context.Rentals.Include(c => c.Movies).SingleOrDefault(r => r.Id == id);

            if (rental == null)
                return NotFound();

            var movie = rental.Movies;

            movie.NumberAvailable++;

            _context.Movies.AddOrUpdate(movie);
            _context.Rentals.Remove(rental);

            _context.SaveChanges();
            return Ok();
        }

    }
}
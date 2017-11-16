using System;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpPost]
        [Route("api/rentals/add")]
        public IHttpActionResult AddMovieRental(NewRentalDto newRentalDto)
        {
            if (newRentalDto.MovieIds == null || newRentalDto.MovieIds.Count == 0)
                return BadRequest("No Movie Ids were provided");

            var customer = _context.Customers.FirstOrDefault(c => c.Id == newRentalDto.CustomerId);

            if (customer == null)
                return BadRequest("Invalid Customer Id");

            var movies = _context.Movies.Where(m => newRentalDto.MovieIds.Contains(m.Id)).ToList();

            if (newRentalDto.MovieIds.Count != movies.Count)
                return BadRequest("One or more movie ids entered given was invalid or you have subscribed for the same movie more than once");

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest(String.Format("Movie: {0} is out of stock.", movie.Name));

                var rental = new Rentals()
                {
                    Movie = movie,
                    Customer = customer,
                    RentedDate = DateTime.Now
                };

                _context.Rentals.Add(rental);

                movie.NumberAvailable--;
            }

            _context.SaveChanges();

            return Ok(true);
        }
    }
}

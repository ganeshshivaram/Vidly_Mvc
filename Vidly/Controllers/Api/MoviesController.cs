using AutoMapper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [System.Web.Http.Route("api/movies")]
        [HttpGet]
        public IHttpActionResult GetMovie()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return Ok(Mapper.Map<IEnumerable<Movie>, IEnumerable<MovieDto>>(movies));
        }

        [System.Web.Http.Route("api/movie/{id}")]
        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.ToList().FirstOrDefault(c => c.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }


        [Route("api/movie/add")]
        [HttpPost]
        public IHttpActionResult AddMovie(MovieDto movieDto)
        {
            if ((!ModelState.IsValid) || movieDto == null)
            {
                return BadRequest();
            }

            _context.Movies.Add(Mapper.Map<MovieDto, Movie>(movieDto));
            _context.SaveChanges();

            return Ok(true);
        }

        [Route("api/movie/edit/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if ((!ModelState.IsValid) || movieDto == null)
            {
                return BadRequest();
            }

            var movieinDb = _context.Movies.ToList().FirstOrDefault(c => c.Id == id);

            if (movieinDb == null)
            {
                return NotFound();
            }

            Mapper.Map(movieDto, movieinDb);
            _context.SaveChanges();

            return Ok(true);
        }


        [Route("api/movie/delete/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieinDb = _context.Movies.ToList().FirstOrDefault(c => c.Id == id);

            if (movieinDb == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movieinDb);
            _context.SaveChanges();

            return Ok(true);
        }
    }
}

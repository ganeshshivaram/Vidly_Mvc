using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            return View();
        }

        [Route("movies/details/{id}")]
        public ActionResult GetDetails(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).FirstOrDefault(m => m.Id == id);
            return View("Details", movie);
        }

        [Route("movies/add")]
        public ActionResult Add()
        {
            ViewBag.PageMode = "Add";

            var genres = _context.Genres.ToList();
            var movieFormViewModel = new MovieFormViewModel()
            {
                Movie = new Movie(),
                Genres = genres
            };

            return View("MovieForm", movieFormViewModel);
        }

        [Route("movies/modify/{id}")]
        public ActionResult Modify(int? id)
        {
            ViewBag.PageMode = "Edit";

            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var genres = _context.Genres.ToList();

            var movieFormViewModel = new MovieFormViewModel()
            {
                Movie = movie,
                Genres = genres
            };

            return View("MovieForm", movieFormViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel()
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.FirstOrDefault(m => m.Id == movie.Id);

                if (movieInDb != null)
                {
                    movieInDb.Name = movie.Name;
                    movieInDb.GenreId = movie.GenreId;
                    movieInDb.NumberInStock = movie.NumberInStock;
                    movieInDb.DateAdded = movie.DateAdded;
                    movieInDb.ReleaseDate = movie.ReleaseDate;
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }
    }
}
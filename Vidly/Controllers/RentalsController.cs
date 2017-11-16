using System.Web.Mvc;

namespace Vidly.Controllers
{
    public class RentalsController : Controller
    {
        [Route("rentals/new")]
        public ActionResult New()
        {
            return View("NewRentals");
        }
    }
}
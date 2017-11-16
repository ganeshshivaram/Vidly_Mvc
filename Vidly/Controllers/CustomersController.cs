using System.Linq;
using System.Web.Mvc;
using Vidly.Helpers;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ViewResult Index()
        {
            if (User.IsInRole(RoleHelper.CanManageCustomers))
                return View("CustomerList");

            return View("ReadOnlyCustomerList");
        }

        [Route("customers/details/{id}")]
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        [Route("customers/add")]
        [Authorize(Roles = RoleHelper.CanManageCustomers)]
        public ActionResult Add()
        {
            var membershipTypes = _context.MembershipTypes;

            ViewBag.Mode = "Add";

            var customer = new CustomerFormViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", customer);
        }

        [Route("customers/modify/{id}")]
        [Authorize(Roles = RoleHelper.CanManageCustomers)]
        public ActionResult Modify(int id)
        {
            var customer = _context.Customers.FirstOrDefault(m => m.Id == id);

            ViewBag.Mode = "Modify";

            if (customer == null)
                return HttpNotFound();

            var membershipTypes = _context.MembershipTypes;

            var customerViewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", customerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleHelper.CanManageCustomers)]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.FirstOrDefault(c => c.Id == customer.Id);

                if (customerInDb == null)
                    return HttpNotFound();

                customerInDb.Name = customer.Name;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

    }
}
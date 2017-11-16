using AutoMapper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomerController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [System.Web.Http.Route("api/customers")]
        [HttpGet]
        public IHttpActionResult GetCustomer(string query = null)
        {
            var customersQuery = _context.
                                 Customers.
                                 Include(m => m.MembershipType);

            if (!string.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            var customers = customersQuery.ToList();

            return Ok(Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers));
        }

        [System.Web.Http.Route("api/customer/{id}")]
        [HttpGet]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.ToList().FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }


        [Route("api/customer/add")]
        [HttpPost]
        public IHttpActionResult AddCustomer(CustomerDto customerDto)
        {
            if ((!ModelState.IsValid) || customerDto == null)
            {
                return BadRequest();
            }

            _context.Customers.Add(Mapper.Map<CustomerDto, Customer>(customerDto));
            _context.SaveChanges();

            return Ok(true);
        }

        /* Test on Postman:
         * http://localhost:49333/api/customer/edit/5/
         * 
         * Body: Params raw json
         * {
		      "name": "Sudil R",
		      "isSubscribedToNewsletter": true,
		      "membershipTypeId": 1,
		      "birthDate": "1991-11-08T00:00:00"
            }
         * Content-Type: application/json
         * */

        [Route("api/customer/edit/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if ((!ModelState.IsValid) || customerDto == null)
            {
                return BadRequest();
            }

            var customerinDb = _context.Customers.ToList().FirstOrDefault(c => c.Id == id);

            if (customerinDb == null)
            {
                return NotFound();
            }

            Mapper.Map(customerDto, customerinDb);
            _context.SaveChanges();

            return Ok(true);
        }


        [Route("api/customer/delete/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerinDb = _context.Customers.ToList().FirstOrDefault(c => c.Id == id);

            if (customerinDb == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customerinDb);
            _context.SaveChanges();

            return Ok(true);
        }


    }
}

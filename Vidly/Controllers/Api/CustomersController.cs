using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        private static bool AgeVerify(CustomerDto customer)
        {
            if (customer.MembershipTypeId == MembershipType.Unknown ||
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return true;

            if (customer.Birthdate == null)
                return false;

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            return age >= Customer.MinAgeToSubscribe;
        }

        //GET /api/customers/
        [HttpGet]
        public IHttpActionResult GetCustomers()
        {
            return Ok(_context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>));
        }

        //GET /api/customers/1
        [HttpGet]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.Single(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }

        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            var ageCheck = AgeVerify(customerDto);

            if (!ageCheck)
                return BadRequest("Customer should be at least 18 years old to subscribe to a membership. Or birthdate is required.");

            if (!ModelState.IsValid)
                return BadRequest();
            
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            var ageCheck = AgeVerify(customerDto);

            if (!ageCheck)
                return BadRequest("Customer should be at least 18 years old to subscribe to a membership. Or birthdate is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            if (customerInDb == null)
                return NotFound();

            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
        }


    }
}

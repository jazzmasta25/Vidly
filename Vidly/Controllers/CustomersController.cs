using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {

        private ApplicationDbContext _context; //database

        public CustomersController() //constructor initialize database so we can use it
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing) //dispose database
        {
            _context.Dispose();
        }

        public bool AgeVerify(Customer customer)
        {
            if (customer.MembershipTypeId == MembershipType.Unknown ||
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return true;

            if (customer.Birthdate == null)
                return false;

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            return age >= Customer.MinAgeToSubscribe;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id); ;

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }


        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid) //if form doesn't validate
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0) //new customer
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            var viewModel = new CustomerFormViewModel();

            if (customer == null)
                return HttpNotFound();

            if (!ModelState.IsValid) //if form doesn't validate
            {
                viewModel.Customer = customer;
                viewModel.MembershipTypes = _context.MembershipTypes.ToList();
                return View("CustomerForm", viewModel);
            }

            viewModel.Customer = customer;
            viewModel.MembershipTypes = _context.MembershipTypes.ToList();

            return View("CustomerForm", viewModel);

        }
    }
}
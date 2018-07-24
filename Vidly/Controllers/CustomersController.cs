using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Vidly.Models;
using Vidly.VeiwModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext context;

        public CustomersController()
        {
            context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        public ActionResult Edit(int Id)
        {
            var customer = context.Customers.SingleOrDefault(c => c.Id == Id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var customerViewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = context.MembershipTypes.ToList()
                };
                return View("CustomerForm", customerViewModel);
            }
            
            if (customer.Id == 0)
                context.Customers.Add(customer);
            else
            {
                var customerInDb = context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.Name = customer.Name;
                customerInDb.Birthday = customer.Birthday;

                // Alternative way to update our model, warning about storing strings, better to make a DTO with values that will be changed
                // TryUpdateModel(customerInDb, "", new string[] { "Name", "Birthday", "MembershipTypeId", "IsSubscribedToNewsLetter" });
            }
            
            context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }


        public ActionResult New()
        {
            var membershipTypes = context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes,

            };
            return View("CustomerForm", viewModel);
        }

        // GET: Customers
        public ActionResult Index()
        {
            var customers = context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        // GET: Customers/Details/1
        public ActionResult Details(int id)
        {
            var customer = context.Customers.Include(c => c.MembershipType)
                .SingleOrDefault(cus => cus.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            } 

            return View(customer);
        }
        
    }
}
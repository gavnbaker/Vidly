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

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }


        public ActionResult New()
        {
            var membershipTypes = context.MembershipTypes.ToList();

            var viewModel = new NewCustomerViewModel
            {
                MembershipTypes = membershipTypes,

            };
            return View(viewModel);
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
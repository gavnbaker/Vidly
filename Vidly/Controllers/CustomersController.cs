using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            return View(getCustomers());
        }

        // GET: Customers/Details/1
        public ActionResult Details(int id)
        {
            var customer = getCustomers().SingleOrDefault(cus => cus.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            } 

            return View(customer);
        }

        private IEnumerable<Customer> getCustomers()
        {
            return new List<Customer>()
            {
                new Customer() {Id = 1, Name = "Drake Morgan"},
                new Customer() {Id = 2, Name = "Mike Sicario"},
                new Customer() {Id = 3, Name = "John Smith"}
            };
        }
    }
}
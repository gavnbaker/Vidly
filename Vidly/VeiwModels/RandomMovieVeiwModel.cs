using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.VeiwModels
{
    public class RandomMovieVeiwModel
    {
        public Movie Movie { get; set; }
        public  List<Customer> Customers { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace Movies.Web.Models
{
    public class MovieVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }
}
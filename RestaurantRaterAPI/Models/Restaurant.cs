using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class Restaurant
    {
        public string Name { get; set; }
        public double Rating { get; set; }
        public bool IsRecommended // => Rating >= 3.5;   simplified version since only one operation
        {
            get
            {
                return Rating >= 3.5;
                //return (Rating >= 3.5) ? true : false;   Could also be if/else statement
            }
        }


    }
}
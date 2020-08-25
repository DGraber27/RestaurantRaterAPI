using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    //Restaurant entity (Class that gets stored in the database)
    public class Restaurant
    {
        [Key] //attribute
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
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
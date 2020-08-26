using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    //Restaurant entity (Class that gets stored in the database)
    public class Restaurant
    {    //Primary Key
        [Key] //attribute  
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public double Rating
        {
            get
            {
                //return FoodRating + EnvironmentRating + CleanlinessRating /3;

                // Calculate a total avergae score based on Ratings List
                double totalAverageRating = 0;
                // Add all ratings together to get total Average Rating
                foreach (var rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }
                return (Ratings.Count > 0) ? totalAverageRating / Ratings.Count : 0;
            }
        }

        //Average Food Rating
        public double FoodRating
        {
            get
            {
                double totalFoodScore = 0;
                foreach (var rating in Ratings)
                    totalFoodScore += rating.FoodScore;

                return (Ratings.Count > 0) ? totalFoodScore / Ratings.Count : 0;
            }
        }
        //public double AverageFoodRating
        //{
        //    get
        //    {
        //        double averageFoodRating = 0;
        //        foreach (var rating in Ratings)
        //        {
        //            averageFoodRating += rating.FoodScore;
        //        }
        //        return (Ratings.Count > 0) ? averageFoodRating / Ratings.Count : 0;
        //    }
        //}

        //Average Environment Rating
        public double EnvironmentRating
        {
            get
            {
                var scores = Ratings.Select(rating => rating.EnvironmentScore); //var is IEnumerable<double>
                double totalEnvironmenScore = scores.Sum();
                return (Ratings.Count > 0) ? totalEnvironmenScore / Ratings.Count : 0;
            }
        }
        //public double AverageEnvironmentRating
        //{
        //    get
        //    {
        //        double averageEnvironmentRating = 0;
        //        foreach (var rating in Ratings)
        //        {
        //            averageEnvironmentRating += rating.EnvironmentScore;
        //        }
        //        return (Ratings.Count > 0) ? averageEnvironmentRating / Ratings.Count : 0;
        //    }
        //}

        //Average Cleanliness Rating
        public double CleanlinessRating
        {
            get
            {
                var totalScore = Ratings.Select(r => r.CleanlinessScore).Sum();
                return (Ratings.Count > 0) ? totalScore / Ratings.Count : 0;
            }
        }
        //public double AverageCleanlinessRating
        //{
        //    get
        //    {
        //        double averageCleanlinessRating = 0;
        //        foreach (var rating in Ratings)
        //        {
        //            averageCleanlinessRating += rating.CleanlinessScore;
        //        }
        //        return (Ratings.Count > 0) ? averageCleanlinessRating / Ratings.Count : 0;
        //    }
        //}
        public bool IsRecommended // => Rating >= 8;   simplified version since only one operation
        {
            get //readonly
            {
                return Rating >= 8;
                //return (Rating >= 3.5) ? true : false;   Could also be if/else statement
            }
        }

        // All of the associated Rating objects from the database based on Foreign Key relationship
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();

    }
}
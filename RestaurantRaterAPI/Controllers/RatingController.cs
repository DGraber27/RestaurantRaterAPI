using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RatingController : ApiController
    {
        //Create new ratings
        private readonly RestaurantDbContext _context = new RestaurantDbContext();
        [HttpPost]
        public async Task<IHttpActionResult> CreateRating(Rating model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restaurant = await _context.Restaurants.FindAsync(model.RestaurantId);
            if (restaurant == null)
            {
                return BadRequest($"The target restaurant with the ID of {model.RestaurantId} does not exist.");
            }
            _context.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
                return Ok($"You rated { restaurant.Name} successfully!");
            return InternalServerError();
        }
        //get rating by Id?

        //get all ratings ?

        // Get All Ratings for a specific Restaurant
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                return Ok(restaurant.Ratings);
            }
            return NotFound();
        }
        //public async Task<IHttpActionResult> GetSpecificRestaurantRating(int id)
        //{
        //    Rating rating = await _context.Ratings.FindAsync(id);
        //    if (rating != null)
        //    {

        //        return Ok(rating.Restaurant);
        //    }

        //    return NotFound();
        //}


        //Update
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurantRating([FromUri] int id, [FromBody] Rating updatedRating)
        {
            if (ModelState.IsValid)
            {
                Rating rating = await _context.Ratings.FindAsync(id);
                if (rating != null)
                {
                    rating.FoodScore = updatedRating.FoodScore;
                    rating.EnvironmentScore = updatedRating.EnvironmentScore;
                    rating.CleanlinessScore = updatedRating.CleanlinessScore;
                    await _context.SaveChangesAsync();
                    return Ok("Your rating has been updated!");
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
        //Delete
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRatingByID(int id)
        {
            Rating rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
                return NotFound();

            _context.Ratings.Remove(rating);

            if (await _context.SaveChangesAsync() == 1)
                return Ok("The rating was deleted.");
            return InternalServerError();
        }
    }
}

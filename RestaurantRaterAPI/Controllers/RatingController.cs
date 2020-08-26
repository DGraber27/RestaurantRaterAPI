using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
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
            if(await _context.SaveChangesAsync()==1)
                return Ok($"You rated { restaurant.Name} successfully!");
            return InternalServerError();
        }
        //get rating by Id?

        //get all ratings ?

        // Get All Ratings for a specific Restaurant
    }
}

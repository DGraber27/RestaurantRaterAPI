﻿using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();
        //--Create (POST)
        public async Task<IHttpActionResult> PostRestaurant(Restaurant model)
        {
            if (model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }
            if (ModelState.IsValid)
            {
                _context.Restaurants.Add(model);
                await _context.SaveChangesAsync();

                return Ok("You created a restaurant and it was saved!");
            }

            return BadRequest(ModelState);
        }

        //--Read (Get)
        // Get by ID
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                return Ok(restaurant);
            }
            return NotFound();
        }
        // Get All
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
        }

        //--Update (PUT)
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri] int id, [FromBody] Restaurant updatedRestaurant)
        {
            //check if ou updated restaurant is valid
            if (ModelState.IsValid)
            {
                //  Find and updated the restaurant
                Restaurant restaurant = await _context.Restaurants.FindAsync(id);
                if (restaurant != null)
                {
                    // update the restaurant now that we found it
                    restaurant.Name = updatedRestaurant.Name;
                   

                    await _context.SaveChangesAsync();

                    return Ok();
                }
                //didn't find the restaurant
                return NotFound();
            }
            return BadRequest(ModelState);

            //Return a bad request
        }

        //--Delete (DELETE)
        [HttpDelete]

        public async Task<IHttpActionResult> DeleteRestaurantByID(int id)
        {
            Restaurant entity = await _context.Restaurants.FindAsync(id);

            if(entity ==null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(entity);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("The restaurant was deleted.");
            }

            return InternalServerError();
        }
    }
}

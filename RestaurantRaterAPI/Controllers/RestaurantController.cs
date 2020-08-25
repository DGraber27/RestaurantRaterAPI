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

        }
        // Get All
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
        }

        //--Update (PUT)

        //--Delete (DELETE)
    }
}

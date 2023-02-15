﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDBackend.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _userContext;
        public UserController(UserContext userContext)
        {
            _userContext = userContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetEmployees()
        { 
            if (_userContext.Users == null)
            {
                return NotFound();
            }
            return await _userContext.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_userContext.Users == null)
            {
                return NotFound();
            }
            var user = await _userContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _userContext.Users.Add(user);
            await _userContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.ID }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(int id, User user)
        {
            if (id != user.ID)
            {
                return BadRequest();
            }
            _userContext.Entry(user).State = EntityState.Modified;
            try
            {
                await _userContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (_userContext.Users == null)
            {
                return NotFound();
            }
            var user = await _userContext.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            _userContext.Users.Remove(user);
            await _userContext.SaveChangesAsync();

            return Ok();
        }



    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafeHaven.MobileAppService.Data;
using SafeHaven.MobileAppService.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SafeHaven.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private SafeHavenContext _context;
        public UserController(SafeHavenContext ctx)
        {
            _context = ctx;
        }

        // GET ALL
        [HttpGet]
        public IActionResult Get()
        {
            var users = _context.User.ToList();  
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);  
        }

        // GET SINGLE
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            // If you request anything other than an Id you will get a return of BadRequest. 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = _context.User.SingleOrDefault(x => x.UserID == id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            //if the try statement fails for some reason, will return error of what happened. 
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        // POST
        [HttpPost]
         public IActionResult Post([FromBody] User newUser)
         {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
            if (UserExists(newUser.Email))
            { 
                return BadRequest(ModelState);
            }

			_context.User.Add(newUser);
			try
			{
				_context.SaveChanges();
			}
			catch (DbUpdateException)
			{
				return StatusCode(500);
			}
			return Ok();
         }

		// UPDATE
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] User user)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != user.UserID)
			{
				return BadRequest();
			}

			_context.User.Update(user);

			try
			{
				_context.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return StatusCode(500);
			}

			return Ok();
		}

		private bool UserExists(string email)
		{
			return _context.User.Count(e => e.Email == email) > 0;
		}


	}
}
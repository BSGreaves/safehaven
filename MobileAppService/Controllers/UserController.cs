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
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private SafeHavenContext _context;
        public UserController(SafeHavenContext ctx)
        {
            _context = ctx;
        }

        // GET ALL
        [HttpGet]
        public async Task<UserResponse> Get()
        {
			UserResponse response = new UserResponse();
			var users = await _context.User.ToListAsync();
			if (users == null)
			{
				response.Success = false;
				response.Message = "No Users found.";
				return response;
			}
            response.Success = true;
            response.Message = "Users found.";
            response.Users = users;
            return response;
        }

		[HttpPost]
		public async Task<SingleUserResponse> Login([FromBody] User user)
		{
			SingleUserResponse response = new SingleUserResponse();
			User checkuser = await _context.User.SingleOrDefaultAsync(x => x.Email == user.Email);
			if (checkuser == null)
			{
				response.Success = false;
				response.Message = "This email has not been registered. Please sign up!";
				return response;
			}
            if (checkuser.Password != user.Password)
            {
                response.Success = false;
                response.Message = "Incorrect password. Please try again.";
            }
            response.Success = true;
            response.Message = "Login successful.";
            checkuser.Password = null;
            response.User = checkuser;
			return response;
		}

		// POST
		[HttpPost]
		public async Task<SingleUserResponse> Register([FromBody] User newUser)
		{
			SingleUserResponse response = new SingleUserResponse();
			response.Success = false;
			if (!ModelState.IsValid)
			{
				response.Message = "Invalid credentials. Please enter a valid email and password.";
				return response;
			}
			if (UserExists(newUser.Email))
			{
				response.Message = "This email address has already been registered. Please log in with this email or use a new email address.";
				return response;
			}
			_context.User.Add(newUser);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				response.Message = "We encountered a database error. Please try again later or contact our team for assistance.";
				return response;
			}
			response.Success = true;
			response.Message = "Registration successful!";
            response.User = newUser;
			return response;
		}

        // GET SINGLE
        [HttpGet("{id}")]
        public async Task<SingleUserResponse> Get([FromRoute] int id)
        {
            SingleUserResponse response = new SingleUserResponse();
            if (!ModelState.IsValid)
            {
                response.Success = false;
                response.Message = "Bad Request.";
                return response;
            }
            var user = await _context.User.SingleOrDefaultAsync(x => x.UserID == id);
            if (user == null)
            {
				response.Success = false;
				response.Message = "User not found.";
				return response;
            }
			response.Success = true;
			response.Message = "User found.";
            response.User = user;
			return response;
        }

		// UPDATE
		[HttpPut("{id}")]
		public async Task<JsonResponse> Put(int id, [FromBody] User user)
		{
			JsonResponse response = new JsonResponse();
			response.Success = false;
			if (!ModelState.IsValid)
			{
				response.Message = "Invalid data. Please try again.";
				return response;
			}
			if (id != user.UserID)
			{
				response.Message = "Invalid update. Please try again.";
				return response;
			}
			_context.Update(user);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch
			{
				response.Message = "We encountered a database error. Please try again later or contact our team for assistance.";
				return response;
			}
			response.Success = true;
			response.Message = "Update successful";
			return response;
		}

		private bool UserExists(string email)
		{
			return _context.User.Count(e => e.Email == email) > 0;
		}
	}
}
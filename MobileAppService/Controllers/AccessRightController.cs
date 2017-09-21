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
	public class AccessRightController : Controller
	{
		private SafeHavenContext _context;

		public AccessRightController(SafeHavenContext ctx)
		{
			_context = ctx;
		}

        // GET all where user is grantor
        [HttpGet("{userid}")]
		public async Task<AccessRightResponse> GetWhereGrantor([FromRoute] int userid)
		{
			AccessRightResponse response = new AccessRightResponse();
			if (!ModelState.IsValid)
			{
				response.Success = false;
				response.Message = "Bad Request.";
				return response;
			}
            List<AccessRight> accessrights = await _context.AccessRight.Where(x => x.GrantorID == userid).Include("Accessor").ToListAsync();
			if (accessrights == null)
			{
                response.Success = false;
				response.Message = "No items found.";
				return response;
			}
            foreach (AccessRight ar in accessrights)
            {
                ar.Accessor.Password = null;
            }
            response.AccessRights = accessrights;
            response.Success = true;
            response.Message = "Items found.";
            return response;
		}

		// GET all where user is grantor
		[HttpGet("{userid}")]
		public async Task<AccessRightResponse> GetWhereAccessor([FromRoute] int userid)
		{
			AccessRightResponse response = new AccessRightResponse();
			if (!ModelState.IsValid)
			{
				response.Success = false;
				response.Message = "Bad Request.";
				return response;
			}
			List<AccessRight> accessrights = await _context.AccessRight.Where(x => x.AccessorID == userid).Include("Grantor").ToListAsync();
			if (accessrights == null)
			{
				response.Success = false;
				response.Message = "No items found.";
				return response;
			}
			response.AccessRights = accessrights;
			response.Success = true;
			response.Message = "Items found.";
			return response;
		}

        // POST
        [HttpPost]
        public async Task<JsonResponse> Post([FromBody] NewAccessRight checkaccessright)
        {
            JsonResponse response = new JsonResponse();
			if (!ModelState.IsValid)
			{
				response.Success = false;
				response.Message = "Bad Request.";
				return response;
			}
            var checkuser = await _context.User.SingleOrDefaultAsync(x => x.Email == checkaccessright.AccessorEmail);
            if (checkuser == null)
            {
				response.Success = false;
				response.Message = "That email is not registered. Ask them to sign up and try again.";
				return response;
            }
            AccessRight accessright = new AccessRight
            {
                GrantorID = checkaccessright.GrantorUserID,
                AccessorID = checkuser.UserID
            };
            _context.AccessRight.Add(accessright);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                response.Success = false;
                response.Message = "There was a a database error. Please try again.";
                return response;
            }
            response.Success = true;
            response.Message = "Post successful.";
            return response;
        }

        // DELETE
		[HttpDelete("{accessrightid}")]
		public async Task<JsonResponse> Delete(int accessrightid)
		{
            JsonResponse response = new JsonResponse();
			if (!ModelState.IsValid)
			{
				response.Success = false;
				response.Message = "Bad Request.";
				return response;
			}
			AccessRight accessright = await _context.AccessRight.SingleOrDefaultAsync(x => x.AccessRightID == accessrightid);
			if (accessright == null)
			{
                response.Success = false;
                response.Message = "There was an error deleting this record. Please try again.";
                return response;
			}
			_context.AccessRight.Remove(accessright);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
				response.Success = false;
				response.Message = "There was an error deleting this record. Please try again.";
				return response;
            }
			response.Success = true;
			response.Message = "Delete successful.";
            return response;
		}
	}
}
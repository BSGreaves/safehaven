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

        [HttpGet("{userid}")]
		public IActionResult GetWhereGranter([FromRoute] int userid)
		{
            IQueryable<AccessRight> accessrights = _context.AccessRight.Where(x => x.GrantorID == userid).Include("Accessor");

			if (accessrights == null)
			{
				return NotFound();
			}

            return Ok(accessrights);
		}

        [HttpGet("{userid}")]
        public IActionResult GetWhereAccessor([FromRoute] int userid)
        {
            IQueryable<AccessRight> accessrights = _context.AccessRight.Where(x => x.GrantorID == userid).Include("Grantor");

            if (accessrights == null)
            {
                return NotFound();
            }
            return Ok(accessrights);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AccessRight accessright)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.AccessRight.Add(accessright);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return Ok();
        }

		[HttpDelete("{accessrightid}")]
		public IActionResult Delete(int accessrightid)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			AccessRight accessright = _context.AccessRight.SingleOrDefault(x => x.AccessRightID == accessrightid);
			if (accessright == null)
			{
				return NotFound();
			}

			_context.AccessRight.Remove(accessright);
            _context.SaveChanges();
			return Ok();
		}
	}
}
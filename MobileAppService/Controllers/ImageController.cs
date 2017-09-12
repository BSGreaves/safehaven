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

	public class ImageController : Controller
	{
		private SafeHavenContext _context;

		public ImageController(SafeHavenContext ctx)
		{
			_context = ctx;
		}

		// GET ALL
		[HttpGet("{docid}")]
		public IActionResult GetAll(int docid)
		{
			IQueryable<Image> images = _context.Image.Where(x => x.DocumentID == docid);

			if (images == null)
			{
				return NotFound();
			}
			return Ok(images);
		}

		// GET SINGLE
		[HttpGet("{imgid}")]
		public IActionResult GetSingle([FromRoute] int imgid)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			Image image = _context.Image.SingleOrDefault(m => m.ImageID == imgid);
			if (image == null)
			{
				return NotFound();
			}
			return Ok(image);
		}

		// POST url/Order
		[HttpPost]
		public IActionResult Post([FromBody] Image image)
		{
            // NB! NEEDS LOGIC
            // Needs to save the image to a local filepath, then store the path in the img obj
            // THEN save img to DB
            // Check Bangazon repo for how I did this earlier
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			_context.Image.Add(image);
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
		[HttpPut("{imgid}")]
		public IActionResult Put(int imgid, [FromBody] Image image)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (imgid != image.ImageID)
			{
				return BadRequest();
			}

			_context.Image.Update(image);

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

		// DELETE
		[HttpDelete("{imgid}")]
		public IActionResult Delete(int imgid)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Image image = _context.Image.SingleOrDefault(x => x.ImageID == imgid);
			if (image == null)
			{
				return NotFound();
			}

			_context.Image.Remove(image);
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

	}
}
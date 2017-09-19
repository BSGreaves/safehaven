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
using System.IO;

namespace SafeHaven.Controllers
{
	[Route("[controller]/[action]")]

	public class DocumentImageController : Controller
	{
		private SafeHavenContext _context;

		public DocumentImageController(SafeHavenContext ctx)
		{
			_context = ctx;
		}

		// GET ALL
		[HttpGet("{docid}")]
		public IActionResult GetAll(int docid)
		{
			IQueryable<DocumentImage> images = _context.DocumentImage.Where(x => x.DocumentID == docid);

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
			DocumentImage image = _context.DocumentImage.SingleOrDefault(m => m.DocumentImageID == imgid);
			if (image == null)
			{
				return NotFound();
			}
			return Ok(image);
		}

		[HttpGet("{filepath}")]
        public IActionResult Get([FromRoute] string filepath)
		{
            string directory = Directory.GetCurrentDirectory();
            string fullpath = directory + "/Data/Images/" + filepath;
			var image =  System.IO.File.OpenRead(fullpath);
			return File(image, "image/jpeg");
		}

		// POST url/Order
		[HttpPost]
		public IActionResult Post([FromBody] DocumentImage image)
		{
            // NB! NEEDS LOGIC
            // Needs to save the image to a local filepath, then store the path in the img obj
            // THEN save img to DB
            // Check Bangazon repo for how I did this earlier
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			_context.DocumentImage.Add(image);
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
		public IActionResult Put(int imgid, [FromBody] DocumentImage image)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (imgid != image.DocumentImageID)
			{
				return BadRequest();
			}

			_context.DocumentImage.Update(image);

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

			DocumentImage image = _context.DocumentImage.SingleOrDefault(x => x.DocumentImageID == imgid);
			if (image == null)
			{
				return NotFound();
			}

			_context.DocumentImage.Remove(image);
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
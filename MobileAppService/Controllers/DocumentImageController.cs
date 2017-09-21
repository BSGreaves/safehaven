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
using System.Net.Http;

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

		// GET the actual image file
		[HttpGet("{filename}")]
        public IActionResult Get([FromRoute] string filename)
		{
            string directory = Directory.GetCurrentDirectory();
            string fullpath = directory + "/Data/Images/" + filename;
			var image =  System.IO.File.OpenRead(fullpath);
			return File(image, "image/jpeg");
		}

		//// POST url/Order
		//[HttpPost]
		//public async Task<JsonResponse> Post(ByteFile file)
		//{
		//	JsonResponse response = new JsonResponse { Success = false, Message = "Default" };
		//	if (file != null)
		//	{
		//		Random random = new Random();
		//		string filename = random.Next(2, 999999).ToString() + ".jpg"; 
		//		string directory = Directory.GetCurrentDirectory();
		//		string localSavePath = directory + @"\Data\Images\" + filename;
		//		using (var stream = new FileStream(localSavePath, FileMode.Create))
		//		{
		//			await newimage.CopyToAsync(stream);
		//		}
		//		DocumentImage docimage = new DocumentImage
		//		{
		//			DocumentID = docid,
		//			FilePath = filename,
		//			DateCreated = DateTime.Today,
		//			PageNumber = 1
		//		};
		//		_context.DocumentImage.Add(docimage);
		//		try
		//		{
		//			await _context.SaveChangesAsync();
		//		}
		//		catch
		//		{
		//			response.Success = false;
		//			response.Message = "There was a a database error. Please try again.";
		//			return response;
		//		}
		//		response.Success = true;
		//		response.Message = "Added successfully";
		//		return response;
		//	}
		//	return response;
		//}

		//// POST url/Order
  //      [HttpPost("{docid}")]
		//public async Task<JsonResponse> Post(int docid, StreamContent newimage)
		//{
  //          JsonResponse response = new JsonResponse { Success = false, Message = "Default" };
		//	if (newimage != null)
		//	{
  //              Random random = new Random();
  //              string filename = random.Next(2, 999999).ToString() + ".jpg";
		//	    string directory = Directory.GetCurrentDirectory();
  //              string localSavePath = directory + @"\Data\Images\" + filename;
		//		using (var stream = new FileStream(localSavePath, FileMode.Create))
		//		{
		//			await newimage.CopyToAsync(stream);
		//		}
  //              DocumentImage docimage = new DocumentImage
  //              {
  //                  DocumentID = docid,
  //                  FilePath = filename,
		//			DateCreated = DateTime.Today,
		//			PageNumber = 1
  //              };
		//		_context.DocumentImage.Add(docimage);
		//		try
		//		{
		//			await _context.SaveChangesAsync();
		//		}
		//		catch
		//		{
		//			response.Success = false;
		//			response.Message = "There was a a database error. Please try again.";
		//			return response;
		//		}
  //              response.Success = true;
		//		response.Message = "Added successfully";
		//		return response;
		//	}
  //          return response;
		//}

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
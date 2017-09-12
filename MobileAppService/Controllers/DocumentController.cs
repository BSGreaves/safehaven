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

	public class DocumentController : Controller
    {
        private SafeHavenContext _context;

        public DocumentController(SafeHavenContext ctx)
        {
            _context = ctx;
        }

        // GET ALL
        [HttpGet("{userid}")]
        public IActionResult GetAll(int userid)
        {
            IQueryable<Document> documents = _context.Document.Where(x => x.UserID == userid);

            if (documents == null)
            {
                return NotFound();
            }
            return Ok(documents);
        }

        // GET SINGLE
        [HttpGet("{docid}")]
        public IActionResult GetSingle([FromRoute] int docid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			Document document = _context.Document.Include("DocumentImages").SingleOrDefault(m => m.DocumentID == docid);
            if (document == null)
            {
                return NotFound();
            }
            return Ok(document);
		}

        // POST url/Order
        [HttpPost]
        public IActionResult Post([FromBody] Document document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Document.Add(document); 
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
        public IActionResult Put (int id, [FromBody] Document document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != document.DocumentID)
            {
                return BadRequest();
            }

            _context.Document.Update(document);

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
        [HttpDelete("{docid}")]
        public IActionResult Delete(int docid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Document document = _context.Document.SingleOrDefault(x => x.DocumentID == docid);
            if (document == null)
            {
                return NotFound();
            }

            _context.Document.Remove(document);
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
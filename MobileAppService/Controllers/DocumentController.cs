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

        // GET ALL based on a userID
		[HttpGet("{userid}")]
		public async Task<DocumentResponse> GetAll([FromRoute] int userid)
		{
			DocumentResponse response = new DocumentResponse();
			if (!ModelState.IsValid)
			{
				response.Success = false;
				response.Message = "Bad Request.";
				return response;
			}
            List<Document> documents = await _context.Document.Where(x => x.UserID == userid).ToListAsync();
			if (documents == null)
			{
				response.Success = false;
				response.Message = "No items found.";
				return response;
			}
			response.Documents = documents;
			response.Success = true;
			response.Message = "Items found.";
			return response;
		}

        // GET Single based on docid and include images
        [HttpGet("{docid}")]
        public async Task<SingleDocumentResponse> GetSingle([FromRoute] int docid)
        {
			SingleDocumentResponse response = new SingleDocumentResponse();
			if (!ModelState.IsValid)
			{
				response.Success = false;
				response.Message = "Bad Request.";
				return response;
			}
            var document = await _context.Document.Include("DocumentImages").SingleOrDefaultAsync(m => m.DocumentID == docid);
            document.DocumentType = await _context.DocumentType.SingleOrDefaultAsync(x => x.DocumentTypeID == document.DocumentTypeID);
			if (document == null)
			{
				response.Success = false;
				response.Message = "Item not found.";
				return response;
			}
            response.Document = document;
			response.Success = true;
			response.Message = "Item found.";
			return response;
		}

        // POST
		[HttpPost]
		public async Task<JsonResponse> Post([FromBody] Document document)
		{
			JsonResponse response = new JsonResponse();
			if (!ModelState.IsValid)
			{
				response.Success = false;
				response.Message = "Bad Request.";
				return response;
			}
			_context.Document.Add(document);
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

		// UPDATE
		[HttpPut("{docid}")]
		public async Task<JsonResponse> Put(int docid, [FromBody] Document document)
		{
			JsonResponse response = new JsonResponse();
			response.Success = false;
			if (!ModelState.IsValid)
			{
				response.Message = "Bad Request.";
				return response;
			}
			if (docid != document.UserID)
			{
				response.Message = "Invalid update. Please try again.";
				return response;
			}
			_context.Update(document);
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
			response.Message = "Update successful";
			return response;
		}

		// DELETE
		[HttpDelete("{docid}")]
		public async Task<JsonResponse> Delete(int docid)
		{
			JsonResponse response = new JsonResponse();
			if (!ModelState.IsValid)
			{
				response.Success = false;
				response.Message = "Bad Request.";
				return response;
			}
			var document = _context.Document.SingleOrDefault(x => x.DocumentID == docid);
			if (document == null)
			{
				response.Success = false;
				response.Message = "There was an error deleting this document. Please try again.";
				return response;
			}
			_context.Document.Remove(document);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch
			{
				response.Success = false;
				response.Message = "There was an error deleting this document. Please try again.";
				return response;
			}
			response.Success = true;
			response.Message = "Delete successful.";
			return response;
		}
    }
}
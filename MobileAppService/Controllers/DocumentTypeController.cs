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
    public class DocumentTypeController : Controller
    {
        private SafeHavenContext _context;

        public DocumentTypeController(SafeHavenContext ctx)
        {
            _context = ctx;
        }
        // GET ALL
        [HttpGet]
        public async Task<DocumentTypeResponse> Get()
        {
            DocumentTypeResponse response = new DocumentTypeResponse();
            List<DocumentType> documenttypes = await _context.DocumentType.ToListAsync();
            if (documenttypes == null)
            {
                response.Success = false;
                response.Message = "No items found.";
                return response;
            }
            response.Success = true;
			response.Message = "Items found.";
            response.DocumentTypes = documenttypes;
			return response;
        }
    }
}
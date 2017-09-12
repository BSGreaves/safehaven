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
        public IActionResult Get()
        {
            IQueryable<DocumentType> DocumentTypes = from DocumentType in _context.DocumentType select DocumentType;
            if (DocumentTypes == null)
            {
                return NotFound();
            }
            return Ok(DocumentTypes);
        }

        // GET Single 
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			DocumentType documentType = _context.DocumentType.SingleOrDefault(m => m.DocumentTypeID == id);
            if (documentType == null)
            {
                return NotFound();
            }
			return Ok(documentType);
		}
    }
}
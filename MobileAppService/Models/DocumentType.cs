using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeHaven.MobileAppService.Models
{
    public class DocumentType
    {
        [Key]
        public int DocumentTypeID { get; set; }
        [Required]
        public string Title { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeHaven.MobileAppService.Models
{
    public class Document
    {
        [Key]
        public int DocumentID { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int UserID { get; set; }
        public virtual User User { get; set; }
        [Required]
        public int DocumentTypeID { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public string PhysicalLocation { get; set; }
        public string Notes { get; set; }
        public virtual ICollection<DocumentImage> DocumentImages { get; set; }
    }
}
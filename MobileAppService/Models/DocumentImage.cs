using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeHaven.MobileAppService.Models
{
    public class DocumentImage
    {
        [Key]
        public int DocumentImageID { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public int DocumentID { get; set; }
        public virtual Document Document { get; set; }
        [Required]
        public int PageNumber { get; set; }
        [Required]
        public string FilePath { get; set; }
		[Required]
		public string UrlPath { get; set; }
    }
}
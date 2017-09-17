using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeHaven.MobileAppService.Models
{
  public class Image
  {
    [Key]
    public int ImageID { get; set; }
    [Required]
    public DateTime DateCreated { get; set; }
    [Required]
    public int DocumentID {get; set; }
    public virtual Document Document { get; set; }
    [Required]
    public int PageNumber { get; set; }
    [Required]
    public string FilePath {get; set; }
  }
}
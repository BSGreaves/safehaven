using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeHaven.MobileAppService.Models
{
  public class User
  {
    [Key]
    public int UserID { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }
    public string Street { get; set; }

    [Range(0, 99999)]
    public int ZIP { get; set; }
    public string State {get; set; }
    public ICollection<Document> Documents { get; set;}
    public ICollection<AccessRight> AccessGranted { get; set;}
    public ICollection<AccessRight> AccessAllowed { get; set;}
  }
}
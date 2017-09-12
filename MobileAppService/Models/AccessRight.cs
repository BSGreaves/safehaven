using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeHaven.MobileAppService.Models
{
  public class AccessRight
  {
    [Key]
    public int AccessRightID { get; set; }
    [Required]
    public int GrantorID { get; set; }
    [Required]
    public int AccessorID { get; set; }
    public virtual User Grantor { get; set; }
    public virtual User Accessor { get; set; }
  }
}
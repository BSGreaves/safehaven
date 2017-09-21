using System;
namespace SafeHaven.MobileAppService.Models
{
	public class NewAccessRight
	{
		public int GrantorUserID { get; set; }
		public string AccessorEmail { get; set; }
	}
}


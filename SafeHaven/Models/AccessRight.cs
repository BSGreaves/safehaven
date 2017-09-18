using System;
namespace SafeHaven.Models
{
	public class AccessRight
	{
		public int AccessRightID { get; set; }
		public int GrantorID { get; set; }
		public int AccessorID { get; set; }
		public virtual User Grantor { get; set; }
		public virtual User Accessor { get; set; }
	}
}

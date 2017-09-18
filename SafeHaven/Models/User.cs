using System;
using System.Collections.Generic;

namespace SafeHaven.Models
{
	public class User
	{
		public int UserID { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Street { get; set; }
		public int ZIP { get; set; }
		public string State { get; set; }
		public ICollection<Document> Documents { get; set; }
		public ICollection<AccessRight> AccessGranted { get; set; }
		public ICollection<AccessRight> AccessAllowed { get; set; }
	}
}

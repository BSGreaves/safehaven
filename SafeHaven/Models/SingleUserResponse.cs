using System;
namespace SafeHaven.Models
{
	public class SingleUserResponse : JsonResponse
	{
		public User User { get; set; }
	}
}

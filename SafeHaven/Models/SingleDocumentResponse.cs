using System;
namespace SafeHaven.Models
{
	public class SingleDocumentResponse : JsonResponse
	{
		public Document Document { get; set; }
	}
}

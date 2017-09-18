using System;
using System.Collections.Generic;

namespace SafeHaven.Models
{
	public class DocumentTypeResponse : JsonResponse
	{
		public List<DocumentType> DocumentTypes { get; set; }
	}
}

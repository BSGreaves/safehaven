using System;
using System.Collections.Generic;

namespace SafeHaven.Models
{
	public class DocumentType
	{
		public int DocumentTypeID { get; set; }
		public string Title { get; set; }
		public virtual ICollection<Document> Documents { get; set; }
	}
}

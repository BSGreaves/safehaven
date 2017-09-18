using System;
using System.Collections.Generic;

namespace SafeHaven.Models
{
	public class Document
	{
		public int DocumentID { get; set; }
		public DateTime DateCreated { get; set; }
		public string Title { get; set; }
		public int UserID { get; set; }
		public virtual User User { get; set; }
		public int DocumentTypeID { get; set; }
		public virtual DocumentType DocumentType { get; set; }
		public string PhysicalLocation { get; set; }
		public string Notes { get; set; }
		public virtual ICollection<Image> DocumentImages { get; set; }
	}
}

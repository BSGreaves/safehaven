using System;
namespace SafeHaven.Models
{
	public class DocumentImage
	{
		public int DocumentImageID { get; set; }
		public DateTime DateCreated { get; set; }
		public int DocumentID { get; set; }
		public virtual Document Document { get; set; }
		public int PageNumber { get; set; }
		public string FilePath { get; set; }
	}
}

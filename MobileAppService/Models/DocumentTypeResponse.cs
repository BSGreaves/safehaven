using System;
using System.Collections.Generic;

namespace SafeHaven.MobileAppService.Models
{
    public class DocumentTypeResponse : JsonResponse
    {
        public List<DocumentType> DocumentTypes { get; set; }
    }
}

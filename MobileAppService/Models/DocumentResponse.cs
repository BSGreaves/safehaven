using System;
using System.Collections.Generic;

namespace SafeHaven.MobileAppService.Models
{
    public class DocumentResponse : JsonResponse
    {
        public List<Document> Documents { get; set; }
    }
}

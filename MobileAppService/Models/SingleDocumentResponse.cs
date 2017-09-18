using System;
namespace SafeHaven.MobileAppService.Models
{
    public class SingleDocumentResponse : JsonResponse
    {
        public Document Document { get; set; }
    }
}

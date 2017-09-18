using System;
namespace SafeHaven.MobileAppService.Models
{
    public class SingleUserResponse :JsonResponse
    {
        public User User { get; set; }
    }
}

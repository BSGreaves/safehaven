using System;
using System.Collections.Generic;

namespace SafeHaven.MobileAppService.Models
{
    public class UserResponse : JsonResponse
    {
        public List<User> Users { get; set; }
    }
}

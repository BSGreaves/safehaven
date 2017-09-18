using System;
using System.Collections.Generic;

namespace SafeHaven.Models
{
	public class AccessRightResponse : JsonResponse
	{
		public List<AccessRight> AccessRights { get; set; }
	}
}

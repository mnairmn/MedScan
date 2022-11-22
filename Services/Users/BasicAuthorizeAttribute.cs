using System;
using Microsoft.AspNetCore.Authorization;

namespace MedScan
{
	public class BasicAuthorizeAttribute : AuthorizeAttribute
	{
		public BasicAuthorizeAttribute()
		{
			Policy = "BasicAuthentication";
			//type of authentication
		}
	}
}


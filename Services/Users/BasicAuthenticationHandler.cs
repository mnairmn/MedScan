using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using MedScan.Models;
using System.Security.Claims;

namespace MedScan.Services
{
	public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions> //inherits from AuthenticationHandler
	{
		public BasicAuthenticationHandler(
			IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder,
			ISystemClock clock
			) : base(options, logger, encoder, clock){}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync() //example of polymorphism
		{
			Response.Headers.Add("WWW-Authenticate", "Basic"); //

			if (!Request.Headers.ContainsKey("Authorization"))
			{
				return Task.FromResult(AuthenticateResult.Fail("Authentication Header Missing"));
			}

			var authorizationHeader = Request.Headers["Authorization"].ToString();
			var authHeaderRegex = new Regex("Basic (.*)");

			//sets up authorisation header and asks for an input

			if (!authHeaderRegex.IsMatch(authorizationHeader))
			{
                return Task.FromResult(AuthenticateResult.Fail("Authentication code not formated properly"));
            }

			var authBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderRegex.Replace(authorizationHeader, "$1")));
			//encodes header content into UTF8 and stores this

			var authSplit = authBase64.Split(Convert.ToChar(":"), 2);
			//username and password are split by a ':' so by calling split with this delimiter, we can get separate the two fields
			var authUsername = authSplit[0];
			//assigning username
			var authPassword = authSplit.Length > 1 ? authSplit[1] : throw new Exception("Unable to get password");
			//? is lamda operator, works like an if statement - if authSplit (the array with username and password in two different elements is
			//greater than one, both fields must be there, so assign password (first thing after lamda operator before colon). If not do second thing (after colon)...throw ex

			SecurityService security = new SecurityService();
			User user = new User();
			//instantiates new SecurityService and User objects

			user.Username = authUsername;
			user.Password = authPassword;
			//assigns object properties to retrieved values

			if (!security.ValidUser(user))
			{
				//if user is not valid do not allow access
				return Task.FromResult(AuthenticateResult.Fail("ACCESS DENIED - One or more of the details entered are incorrect."));
			}

			var authenticatedUser = new AuthenticatedUser("BasicAuthentication", true, user.Username);
			var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(authenticatedUser));
			//new objects instantiated - claimsPrincipal is the basis of the authentication

			return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
		}
	}
}


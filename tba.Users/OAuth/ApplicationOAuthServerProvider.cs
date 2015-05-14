using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using tba.Users.Services;

namespace tba.Users.OAuth
{
    public class ApplicationOAuthServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUsersService _usersService;

        public ApplicationOAuthServerProvider(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // not using client authentication, so just validate
            // todo tba(14/5/15)  add multi-tenancy
            await Task.FromResult(context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = await _usersService.GetByEmailAsync(context.UserName);
            if (user == null || !_usersService.PasswordIsValid(user, context.Password))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                context.Rejected();
                return;
            }

            // Add claims associated with this user to the ClaimsIdentity object:
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            foreach (var userClaim in user.Claims)
            {
                identity.AddClaim(new Claim(userClaim.ClaimType, userClaim.ClaimValue));
            }

            context.Validated(identity);
        }
    }
}
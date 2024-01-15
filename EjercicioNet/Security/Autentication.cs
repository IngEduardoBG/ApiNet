using EjercicioNet.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace EjercicioNet.Security
{
    public class Autentication: AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUser _userServices;
        public Autentication(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUser userServices
            ):base(options,logger,encoder,clock)
        {
            _userServices = userServices;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Credenciales no validas");
            bool result = false;
            try
            {
                var autenHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credential = Convert.FromBase64String(autenHeader.Parameter);
                var passAut = Encoding.UTF8.GetString(credential).Split(new[] {':'},2);
                var email = passAut[0];
                var password = passAut[1];
                result = _userServices.IsUser(email, password);
            }
            catch 
            {

                return AuthenticateResult.Fail("Credenciales no validas");

 
            }
            if (!result) return AuthenticateResult.Fail("Credencial invalida");
            var Claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,"id"),
                new Claim(ClaimTypes.Name,"user")
            };
            var identity = new ClaimsIdentity(Claims, Scheme.Name);
            var principal=new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}

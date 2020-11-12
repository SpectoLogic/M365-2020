using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpectoLogic.Blazor.MSTeams.Auth;
using System;
using System.Threading.Tasks;

namespace BlazorTeamApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly ITokenProvider tokenProvider;

        public AuthController(ILogger<AuthController> logger, ITokenProvider tokenProvider)
        {
            this.logger = logger;
            this.tokenProvider = tokenProvider;
        }

        [HttpPost]
        [ActionName("token")]
        public async Task<IActionResult> Token([FromBody] TokenRequest tokenrequest)
        {
            string clientId = "e2088d72-79ce-4543-ab3c-0988dfecf2f1";
            string clientSecret = "_2xb_7h-P1voxOddv4jzaE8I_l-Tx71BlH";
            try
            {
                TokenResponse tokenResponse = await tokenProvider.GetToken(
                tokenrequest.Tid,
                tokenrequest.Token,
                clientId,
                clientSecret,
                tokenrequest.Scopes);
                return new JsonResult(tokenResponse.AccessToken);
            }
            catch (TokenErrorException tokenErrorEx)
            {
                return new JsonResult(tokenErrorEx.TokenError);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

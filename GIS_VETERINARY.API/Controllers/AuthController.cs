using GIS_VETERINARY.Abstractions.IApplication;
using GIS_VETERINARY.DTOs.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GIS_VETERINARY.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserApplication _userAppication;

        public AuthController(IUserApplication userAppication)
        {
            _userAppication = userAppication;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginRequestDto request)
        {
            try
            {
                var res = await _userAppication.Login(request);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}

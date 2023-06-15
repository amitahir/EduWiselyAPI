using EduWisely.DTO.RequestDTO;
using EduWisely.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EduWiselyAPI.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("auth/login")]
        public async Task<object> Login(LoginRequest request)
        {
           var result = await _authService.Login(request.Email, request.Password);

           return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPost]
        [Route("auth/register")]
        public async Task<object> Register(RegisterRequest request)
        {
            var result = await _authService.Register(request);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

       
    }
}

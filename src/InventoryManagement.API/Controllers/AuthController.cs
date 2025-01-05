using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthController(IUserService userService,IJwtTokenGenerator jwtTokenGenerator)
        {
            _userService = userService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            // Authenticate uses from DB
            var user = await _userService.Authenticate(loginRequestDto.UserName, loginRequestDto.Password);
            if (user == null) { return Unauthorized(); }

            //Gererate Jwt Token
            var (token, expires) = await _jwtTokenGenerator.GererateToken(user.Username,user.Role);
            var response = new LoginResponseDto
            {
                Token= token,
                Role = user.Role,
                ExpiresIn = (int)(expires -DateTime.Now).TotalSeconds
            };

            return Ok(response);
        }  
    }
}

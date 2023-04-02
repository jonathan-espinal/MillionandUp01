using Microsoft.AspNetCore.Mvc;
using MillionandUpBackend01.Dtos.Auth;
using MillionandUpBackend01.Dtos.User;
using MillionandUpBackend01.Lib;
using MillionandUpBackend01.Services;

namespace MillionandUpBackend01.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase {

        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService) {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<TokenDto>> Login(LoginDto login) {
            UserDto user;
            try {
                user = await _userService.GetUserAsync(login.Username);
            } catch (Exception) {
                UserCreateDto userCreateDto = new UserCreateDto() {
                    Username = login.Username
                };
                user = await _userService.CreateUserAsync(userCreateDto);
            }
            string jwtToken = CryptoJwt.CreateToken(user.Id, user.Username!, _configuration.GetSection("Crypto:JwtKey").Value!);
            return new TokenDto() { Token = jwtToken };
        }
    }
}

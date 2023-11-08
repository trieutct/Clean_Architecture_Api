using Clean_Architecture.Service.AccountClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Clean_Architecture.Api.Controllers.LogginController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAccountClientService _accountClientService;
        private readonly IConfiguration _configuration;
        public LoginController(IAccountClientService accountClientService, IConfiguration configuration)
        {
            _accountClientService = accountClientService;
            _configuration = configuration; 
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginClient loginClient)
        {

            var accountClient = _accountClientService.GetAll().FirstOrDefault(X=>X.Username.Equals(loginClient.Username) && X.Password.Equals(loginClient.Password));
            if(accountClient!=null)
            {
                //lấy khóa bí mật trong file appsetting.json
                //mã hóa khóa bí mật
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
                //ký vào khóa bí mật đã mã hóa
                var signingCredential = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                //tạo ra claims để chứ thông tin bổ sung
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role,"Admin"),
                    new Claim(ClaimTypes.Name,accountClient.Username),
                    //new Claim(ClaimTypes.Email,accountClient.Fullname)
                };
                //tạo token vs các thông số khớp với cấu hình trong file programs để validate
                var token = new JwtSecurityToken
                (
                      issuer: _configuration["Jwt:Issuer"],
                      audience: _configuration["Jwt:Audience"],
                      expires: DateTime.Now.AddMinutes(5),
                      signingCredentials: signingCredential,
                      claims: claims
                );
                // sinh ra chuỗi token
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return Unauthorized();
        }
    }
}

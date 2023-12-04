using Clean_Architecture.Model.Dto.AccountClient;
using Clean_Architecture.Service.AccountClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using Clean_Architecture.Model.Entities;
using AutoMapper;
using System.Security.Cryptography;
using System;
using Clean_Architecture.Model.Dto.Category;

namespace Clean_Architecture.Api.Controllers.AccountController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountClientService _accountClientService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AccountController(IAccountClientService accountClientService, IConfiguration configuration,IMapper mapper)
        {
            _accountClientService = accountClientService;
            _configuration = configuration;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public IActionResult GetAccount(int id)
        {
            var ac = _accountClientService.GetById(id);
            if (ac == null)
            {
                return NotFound();
            }
            return Ok(ac);
        }
        [HttpPut("{id}")]
        public IActionResult PutAccount(DoiProfile profile)
        {
            var find = _accountClientService.GetById(profile.Id);
            if (find == null)
            {
                return BadRequest();
            }    
            find.Fullname = profile.Fullname;
            find.Address = profile.Address;
            find.Age= profile.Age;
            find.Phone = profile.Phone;
            if (_accountClientService.Update(find))
            {
                return Ok("Cập nhật profile thành công");
            }
            return BadRequest();
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginClient loginClient)
        {
            var accountClient = _accountClientService.GetAll().FirstOrDefault(X => X.Username.Equals(loginClient.Username) && X.Password.Equals(maHoaMatKhau(loginClient.Password)));
            if (accountClient != null)
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
                      expires: DateTime.Now.AddMinutes(10),
                      signingCredentials: signingCredential,
                      claims: claims
                );
                // sinh ra chuỗi token
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    UserId= accountClient.Id
                });
            }
            return BadRequest("Sai tài khoản mật khẩu");
        }
        [HttpPost("singup")]
        public IActionResult SingUp([FromForm] Singup singup)
        {
            var acoount = _accountClientService.GetAll().Where(x => x.Username.Equals(singup.Username)).FirstOrDefault();
            if (acoount != null)
            {
                return Conflict(new { message = "Tài khoản đã tồn tại." });
            }
            var accountClient = new AccountClientDto()
            {
                Username = singup.Username,
                Password = maHoaMatKhau(singup.Password),
                Address = "",
                Age = "",
                Fullname = "",
                Phone = ""
            };
            if (_accountClientService.Add(accountClient))
            {
                return Ok("Bạn đã đăng ký thành công");
            }
            return Conflict();
        }
        [HttpPost("QuenPassword")]
        public IActionResult QuenPassword(string username)
        {
            var accountClient = _accountClientService.GetAll().Where(x=>x.Username.Equals(username)).FirstOrDefault();
            if(accountClient==null)
            {
                return BadRequest("tài khoản ko có");
            }    
            try
            {
                var code = new Random().Next(100000, 999999).ToString();
                MailMessage message = new MailMessage();
                message.From = new MailAddress("trinhcongtrieu2972002@gmail.com");
                message.To.Add(username);
                message.Subject = "Quên mật khẩu";
                message.Body = "Mã xác minh là: " + code;
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("trinhcongtrieu2972002@gmail.com", "thzfyhcvpsgnapvl");
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);
                var codePass = new RouteValueDictionary();
                codePass.Add("code", code);
                codePass.Add("email", username);
                return Ok(new
                {
                    Code = code,
                    Username=username
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("DoiMatKhau2")]
        public IActionResult DoiMatKhau2(int UserId,string PassCu,string PassNew)
        {
            var find = _accountClientService.GetById(UserId);
            if(find==null)
            {
                return BadRequest();
            }    
            if(!find.Password.Equals(maHoaMatKhau(PassCu)))
            {
                return BadRequest("Mật khẩu cũ không đúng");
            }
            find.Password = maHoaMatKhau(PassNew);
            if(_accountClientService.Update(find))
            {
                return Ok("Đổi mật khẩu thành công");
            }
            return BadRequest();
        }
        [HttpPost("DoiMatKhau")]  
        public IActionResult DoiMatKhau(DoiMatKhau model)
        {
            var acoount = _accountClientService.GetAll().Where(x => x.Username.Equals(model.Username)).FirstOrDefault();
            if (acoount == null)
            {
                return BadRequest("tài khoản ko có");
            }
            acoount.Password = maHoaMatKhau(model.NewPass);
            if (_accountClientService.Update(acoount))
            {
                return Ok("Thành công. Vui lòng bạn đăng nhập");
            }
            else
            {
                return BadRequest();
            }
        }
        private readonly string hash = @"foxle@rn";
        private string maHoaMatKhau(string text)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results);
                }
            }
        }
    }
}

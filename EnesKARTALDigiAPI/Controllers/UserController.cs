using System;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using EnesKARTALDigiAPI.Data.Repositories.Infra;
using EnesKARTALDigiAPI.Helpers;
using EnesKARTALDigiAPI.Models;

namespace EnesKARTALDigiAPI.Controllers
{
    [Route("api/User")]
    public class UserController : BaseController
    {
        readonly IUserRepository userRepository;
        ConfigHelper Config;
        public UserController(ICacheManager cacheManager,
           IUserRepository userRepository,
           ConfigHelper config) : base(cacheManager)
        {
            this.userRepository = userRepository;
            this.Config = config;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public ActionResult Authenticate(AuthenticateModel request)
        {
            try
            {
                var user = userRepository.GetUserByName(request.Username);
                if (user == null)
                    throw new Exception("Kullanıcı bulunamadı");

                if (!AccountHelper.VerifyHashedPassword(request.Password, user.Password, Config.SecretKey))
                    throw new Exception("Girdiğiniz e-posta adresi veya parola hatalı");

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Config.SecretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                    }),
                    Expires = DateTime.UtcNow.AddDays(90),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                userRepository.Update(user);

                var result = new
                {
                    Id = user.Id,
                    Name = user.Email,
                    Token = user.Token
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

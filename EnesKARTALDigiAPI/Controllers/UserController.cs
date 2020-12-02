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
using EnesKARTALDigiAPI.Data.Models;
using System.Linq;

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

        // GET api/user
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(userRepository.GetAllUserWithoutPass());
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var user = userRepository.GetUserById(id.Value);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // GET api/user/filter?query=mytitle
        [HttpGet("Filter")]
        public IActionResult Filter(string query)
        {
            var data = userRepository.GetAll();

            if (!string.IsNullOrEmpty(query))
                data = data.Where(x => x.Email.ToLower().Contains(query));

            return Ok(data);
        }

        // POST api/user
        [HttpPost]
        public IActionResult Post(User user)
        {
            if (!ModelState.IsValid || user == null)
                return BadRequest();

            user.Password = AccountHelper.HashPassword(user.Password, Config.SecretKey);
            if (userRepository.Add(user))
            {
                user.Password = ""; //Güvenlik nedeniyle
                return Ok(user);
            }

            return BadRequest();
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public IActionResult Put(int? id, User user)
        {
            if (!id.HasValue)
                return BadRequest();

            if (!ModelState.IsValid || user == null)
                return BadRequest();

            var oldUser = userRepository.GetUserById(id.Value);

            if (oldUser == null)
                return NotFound();

            user.Id = oldUser.Id;
            user.Password = AccountHelper.HashPassword(user.Password, Config.SecretKey);

            if (userRepository.Update(user))
            {
                user.Password = ""; //Güvenlik nedeniyle
                return Ok(user);
            }

            return BadRequest();
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var user = userRepository.GetUserById(id.Value);

            if (user == null)
                return NotFound();

            if (userRepository.Delete(user))
            {
                user.Password = "";//Güvenlik nedeniyle
                return Ok(user);
            }

            return BadRequest();
        }
    }
}

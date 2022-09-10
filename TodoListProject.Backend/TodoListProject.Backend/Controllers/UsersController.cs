using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoListProject.Backend.Models.DataContext;
using TodoListProject.Backend.Models.Entities;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cors;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;

namespace TodoListProject.Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        DataContext db = new DataContext();

       

        [HttpPost("login")]
        public IActionResult Login([FromBody] Users users)

        {
            string password = users.Password;
            string sequrityPassword = "";
            if (password != null)
            {
                List<char> character = password.ToCharArray().ToList();


                for (int i = 0; i < character.Count(); i++)
                {
                    sequrityPassword = sequrityPassword + (char)(character[i] + character.Count());
                }

            }
            byte[] data = UTF8Encoding.UTF8.GetBytes(sequrityPassword);
            SHA256Managed sha = new SHA256Managed();
            byte[] result = sha.ComputeHash(data);
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < result.Length; i++)
            {
                output.Append(result[i].ToString("X2"));
            }
            string resultString = output.ToString();
            users.Password = resultString;
            var user = db.Users.FirstOrDefault(m => m.UserName == users.UserName && m.Password == users.Password);
            if (user != null)
            {
                var claimsForToken = new List<Claim>();
                claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
                claimsForToken.Add(new Claim("userName", user.Name.ToString()));
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:14062",
                    audience: "https://localhost:4200",
                    claimsForToken,
                    expires: DateTime.Now.AddMinutes(1000),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new AuthenticatedResponse { Token = tokenString });

            }
            else
            {
                return NotFound();
            }
                   
        }

   
        [HttpPost("register")]
        public bool Register(Users users)
        {
            string password = users.Password;
            string securityPassword = "";
            if (password != null)
            {
                List<char> character = password.ToCharArray().ToList();


                for (int i = 0; i < character.Count(); i++)
                {
                    securityPassword = securityPassword + (char)(character[i] + character.Count());
                }

            }
            byte[] data = UTF8Encoding.UTF8.GetBytes(securityPassword);
            SHA256Managed sha = new SHA256Managed();
            byte[] result = sha.ComputeHash(data);
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < result.Length; i++)
            {
                output.Append(result[i].ToString("X2"));
            }
            string resultString = output.ToString();
            users.Password = resultString;

            if (users != null)
            {
               var userEmail = db.Users.FirstOrDefault(m => m.Email == users.Email);
                if (userEmail != null)
                {
                    return false;
                }
                else
                {
                    db.Add(users);
                    db.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }


    }
}

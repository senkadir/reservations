using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Reservations.Services.Identity.Commands;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Reservations.Services.Identity.Controllers
{
    [Route("api/identity/v1/identity")]
    public class IdentityController : ControllerBase
    {
        /// <summary>
        /// Get security token
        /// </summary>
        /// <param name="command"></param>
        /// <returns>1 hour jwt token with fake claims</returns>
        [HttpPost, AllowAnonymous]
        public IActionResult Login([FromBody] LoginCommand command)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, command.Email),
                new Claim("location", command.Location)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("3eb23bff-add0-ae82-04ae-22dedf80d53f"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddHours(1);

            var token = new JwtSecurityToken(
             "reservations.com",
             "reservations.com",
             claims,
             expires: expires,
             signingCredentials: creds
             );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}

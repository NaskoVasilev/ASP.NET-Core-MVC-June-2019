using MessagesWebApi.Configuration;
using MessagesWebApi.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MessagesWebApi.Extensions
{
	public static class UserManagerExtensions
	{
		public static async Task<string> Authenticate(this UserManager<ApplicationUser> userManager, string username, string password, JwtSettings settings)
		{
			ApplicationUser user = await userManager.FindByNameAsync(username);
			if (user == null)
			{
				return null;
			}

			bool isPasswordValid = await userManager.CheckPasswordAsync(user, password);
			if (!isPasswordValid)
			{
				return null;
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(settings.Secret);
			var tokenDiscriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.UserName),
					new Claim(ClaimTypes.NameIdentifier, user.Id)
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDiscriptor);
			string securityToken = tokenHandler.WriteToken(token);
			return securityToken;
		}
	}
}

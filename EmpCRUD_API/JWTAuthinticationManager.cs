using AdminEntity.Service;
using EmpCRUD_API.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmpCRUD_API
{
    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {
		Dictionary<string, string> UsersRecords = new Dictionary<string, string>
	{
		{ "user1","password1"},
		{ "user2","password2"},
		{ "user3","password3"},
	};
		IConfiguration _config;
		//private readonly IConfiguration iconfiguration;
		string iconfiguration;
		public JWTAuthenticationManager(string iconfiguration, IConfiguration config)
		{
			this.iconfiguration = iconfiguration;
			_config = config;
		}
		public Tokens Authenticate(Users users)
		{
			var conn = _config.GetValue<string>(
				"ConnectionStrings:Default");
			var user = USERS_SERVICE.CheckUser(users.UserName, users.Password, conn);
			if (user==null)
			{
				return null;
			}
		
			// Else we generate JSON Web Token
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(iconfiguration);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
			  {
			 new Claim(ClaimTypes.Name, users.UserName)
			  }),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return new Tokens { Token = tokenHandler.WriteToken(token) };

		}
	}
}

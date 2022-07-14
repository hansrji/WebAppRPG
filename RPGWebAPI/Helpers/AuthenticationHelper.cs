using Microsoft.IdentityModel.Tokens;
using RPGWebAPI.Models.Identity;
using RPGWebAPI.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RPGWebAPI.Helpers
{
	public static class AuthenticationHelper
	{
		public const string SectionKey = "jwt";

		public static TokenResult<string> GenerateJwtToken(ApplicationUser user, IConfiguration configuration)
		{
			var jwtSettings = configuration.GetSection(SectionKey).Get<JwtSettings>();
			var issuer = jwtSettings.Issuer;
			var key = jwtSettings.KeyAsBytes;
			if (issuer == null)
			{
				return new TokenResult<string>
				{
					Success = false,
					Value = "Invalid Issuer"
				};
			}
			if (key.Length == 0)
			{
				return new TokenResult<string>
				{
					Success = false,
					Value = "Key is invalid."
				};
			}
			return GenerateJwtToken(user, key, jwtSettings.ExpireDays, issuer);
		}

		public static TokenResult<string> GenerateJwtToken(ApplicationUser user, byte[] key, int expireDays, string issuer)
		{
			var claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
			};

			var securityKey = new SymmetricSecurityKey(key);
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.Now.AddDays(expireDays);

			var token = new JwtSecurityToken(
				issuer: issuer,
				audience: issuer,
				claims: claims,
				expires: expires,
				signingCredentials: credentials);

			return new TokenResult<string>
			{
				Success = true,
				Value = new JwtSecurityTokenHandler().WriteToken(token)
			};
		}
	}
}

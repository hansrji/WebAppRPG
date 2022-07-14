using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RPGWebAPI.Helpers;
using RPGWebAPI.Models.DataTransfer;
using RPGWebAPI.Models.Identity;
using System.Diagnostics;
using System.Net;

namespace RPGWebAPI.Controllers
{
	[ApiController]
	[Authorize]
	[Route("accounts")]
	public class AccountController : ControllerBase
	{
		protected SignInManager<ApplicationUser> SignInManager { get; set; }
		protected UserManager<ApplicationUser> UserManager { get; set; }
		protected IConfiguration Configuration { get; set; }

		public AccountController(
			SignInManager<ApplicationUser> signInManager,
			UserManager<ApplicationUser> userManager,
			IConfiguration configuration)
		{
			SignInManager = signInManager;
			UserManager = userManager;
			Configuration = configuration;
		}

		// GET /accounts/userdata
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet("userdata")]
		public async Task<ActionResult> UserData()
		{
			var user = await UserManager.GetUserAsync(User);
			var userData = new UserDataResponse
			{
				Name = user.UserName,
				LastName = user.LastName,
				City = user.City,
				Email = user.Email
			};
			return Ok(userData);
		}

		// POST /accounts/register
		[HttpPost("register")]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] RegisterEntity entity)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					Name = entity.Name,
					LastName = entity.LastName ?? "Invalid last name",
					City = entity.City ?? "No city",
					UserName = entity.Email,
					Email = entity.Email
				};
				var result = await UserManager.CreateAsync(user, entity.Password);
				if (result.Succeeded)
				{
					await SignInManager.SignInAsync(user, false);
					var token = AuthenticationHelper.GenerateJwtToken(user, Configuration);

					var rootData = new SignUpResponse
					{
						Token = token, 
						UserName = user.UserName,
						Email = user.Email
					};
					return Created($"/v1/accounts/{nameof(Register)}", rootData);
				}
				return Ok(string.Join(",", result.Errors?.Select(error => error.Description) ?? Array.Empty<string>()));
			}
			string errorMessage = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
			return BadRequest(errorMessage ?? "Bad Request");
		}

		// POST /accounts
		[HttpPost]
		[AllowAnonymous]
		public async Task<ActionResult> Login([FromBody] LoginEntity entity)
		{
			if (ModelState.IsValid)
			{
				var result = await SignInManager.PasswordSignInAsync(entity.Email, entity.Password, false, false);
				if (result.Succeeded)
				{
					var appUser = UserManager.Users.SingleOrDefault(r => r.Email == entity.Email);
					if (appUser == null)
					{
						return NotFound("User not found.");
					}
					var token = AuthenticationHelper.GenerateJwtToken(appUser, Configuration);

					var rootData = new LoginResponse
					{
						Token = token,
						UserName = appUser.UserName,
						Email = appUser.Email
					};
					return Ok(rootData);
				}
				return Unauthorized("Bad credientials");
			}
			string errorMessage = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
			return BadRequest(errorMessage ?? "Bad Request");
		}
	}

	public static class UserRoles
	{
		public const string RoleAdmin = "Admin";
		public const string RoleCustomer = "Customer";
		public const string RoleGuest = "Guest";
	}
}

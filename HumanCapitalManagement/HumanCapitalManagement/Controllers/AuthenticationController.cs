namespace HumanCapitalManagement.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
	private readonly IAuthService authService;
	private readonly IJwtProvider jwtProvider;

	public AuthenticationController(IAuthService authService, IJwtProvider jwtProvider)
	{
		this.authService = authService;
		this.jwtProvider = jwtProvider;
	}

	[HttpPost]
	[AllowAnonymous]
	[ActionName(nameof(Login))]
	public async Task<IActionResult> Login(LoginInputData loginData)
	{
		var getUser = await this.authService.LoginUserAsync(loginData);
		var token = this.jwtProvider.GenerateToken(getUser);
		return Ok(token);
	}

	[HttpPost]
	//[Authorize(Roles = "Admin")]
	[ActionName(nameof(Register))]
	public async Task<IActionResult> Register(RegisterInputData registerData)
	{
		await this.authService.RegisterUserAsync(registerData);
		return Ok();
	}
}

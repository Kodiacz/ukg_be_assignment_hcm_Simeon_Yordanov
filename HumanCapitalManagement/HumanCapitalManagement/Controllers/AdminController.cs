namespace HumanCapitalManagement.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
	private readonly IAdminService adminService;

	public AdminController(IAdminService adminService)
	{
		this.adminService = adminService;
	}

	[HttpGet]
	[ActionName(nameof(GetAllUsers))]
	public async Task<IActionResult> GetAllUsers([FromQuery] UserInputDto inputDto)
	{
		var users = await this.adminService.GetAllUsers(inputDto);
		return Ok(users);
	}

	[HttpPatch]

	public async Task<IActionResult> UpdateUserRole(UserRoleUpdateInputDto input)
	{
		await adminService.UpdateUserRoleAsync(input);
		return Ok();
	}

}

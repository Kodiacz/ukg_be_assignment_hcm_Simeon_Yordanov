namespace HumanCapitalManagement.Application.Services;

public interface IJwtProvider
{
	public JwtToken GenerateToken(UserInfoDto user);
}

namespace HumanCapitalManagement.Infrastructure.Services;

public class JwtProvider : IJwtProvider
{
	private readonly JwtOptions jwtOptions;

	public JwtProvider(IOptions<JwtOptions> jwtOptions)
	{
		this.jwtOptions = jwtOptions.Value;
	}

	public JwtToken GenerateToken(UserInfoDto user)
	{
		JwtToken jwt = new();

		List<Claim> claims = new()
		{
			new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)!),
			new Claim(ClaimTypes.Email, user.Email!),
			new Claim(ClaimTypes.GivenName, user.FirstName!),
			new Claim(ClaimTypes.Surname, user.LastName!),
		};

		foreach (var role in user.Roles)
		{
			claims.Add(new Claim(ClaimTypes.Role, role.Name));
		}

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));

		var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

		var token = new JwtSecurityToken(
			claims: claims,
			expires: DateTime.UtcNow.AddDays(1),
			signingCredentials: cred);

		jwt.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
		jwt.User = user;

		return jwt;
	}
}

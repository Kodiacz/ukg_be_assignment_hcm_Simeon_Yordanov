using HumanCapitalManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HumanCapitalManagement.Infrastructure.EntityConfiguration;

public class UserRolesEntotyConfiguration : IEntityTypeConfiguration<UserRoles>
{
	public void Configure(EntityTypeBuilder<UserRoles> builder)
	{
		builder
			.HasKey(x => new { x.RoleId, x.UserId });

		builder
			.HasOne(x => x.User)
			.WithMany(x => x.UserRoles)
			.HasForeignKey(x => x.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		builder
			.HasOne(x => x.Role)
			.WithMany(x => x.UserRoles)
			.HasForeignKey(x => x.RoleId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}

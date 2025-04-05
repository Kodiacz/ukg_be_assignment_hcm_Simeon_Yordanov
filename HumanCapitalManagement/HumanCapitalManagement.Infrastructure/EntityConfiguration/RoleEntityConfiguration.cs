using HumanCapitalManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HumanCapitalManagement.Infrastructure.EntityConfiguration;

public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
	const int NameMaxLength = 50;

	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder
			.HasKey(x => x.Id);

		builder
			.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(NameMaxLength);

		builder
			.HasMany(x => x.UserRoles)
			.WithOne(x => x.Role)
			.HasForeignKey(x => x.RoleId);

	}
}

using HumanCapitalManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HumanCapitalManagement.Infrastructure.EntityConfiguration;

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{
		builder
			.HasKey(e => e.Id);

		builder
			.Property(e => e.PersonId)
			.IsRequired();

		builder
			.Property(e => e.PasswardHash)
			.IsRequired();

		builder
		.Property(e => e.PasswardSalt)
		.IsRequired();

		builder
			.HasMany(x => x.UserRoles)
			.WithOne(x => x.User)
			.HasForeignKey(x => x.UserId);

		builder
			.HasOne(x => x.Person)
			.WithOne()
			.HasForeignKey<ApplicationUser>(x => x.PersonId)
			.IsRequired();
	}
}

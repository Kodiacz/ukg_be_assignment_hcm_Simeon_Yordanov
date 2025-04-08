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
			.Property(e => e.PasswordHash)
			.IsRequired();

		builder
		.Property(e => e.PasswordSalt)
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

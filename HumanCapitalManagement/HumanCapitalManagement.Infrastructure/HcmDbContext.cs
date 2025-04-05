using HumanCapitalManagement.Domain.Entities;
using HumanCapitalManagement.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Infrastructure
{
	public class HcmDbContext : DbContext
	{
		public HcmDbContext(DbContextOptions<HcmDbContext> options)
			: base(options) { }

		public DbSet<Person> People { get; set; }

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }

		public DbSet<Role> Roles { get; set; }

		public DbSet<UserRoles> UserRoles { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());
			modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
			modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
			modelBuilder.ApplyConfiguration(new UserRolesEntotyConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}

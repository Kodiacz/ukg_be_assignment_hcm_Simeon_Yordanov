using HumanCapitalManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HumanCapitalManagement.Infrastructure.EntityConfiguration;

public class PersonEntityConfiguration : IEntityTypeConfiguration<Person>
{
	const int NameMaxLength = 50;
	const int EmaileMaxLength = 256;
	const int JobAndDepartmentMaxLength = 50;

	public void Configure(EntityTypeBuilder<Person> builder)
	{
		builder
			.HasKey(x => x.Id);

		builder
			.Property(x => x.FirstName)
			.IsRequired()
			.HasMaxLength(NameMaxLength);

		builder
			.Property(x => x.LastName)
			.IsRequired()
			.HasMaxLength(NameMaxLength);

		builder
			.Property(x => x.Email)
			.IsRequired()
			.HasMaxLength(EmaileMaxLength);

		builder
			.Property(x => x.JobTitle)
			.IsRequired()
			.HasMaxLength(JobAndDepartmentMaxLength);

		builder
			.Property(x => x.Department)
			.IsRequired()
			.HasMaxLength(JobAndDepartmentMaxLength);

		builder
			.Property(x => x.Salary)
			.IsRequired()
			.HasPrecision(18, 2);


	}
}

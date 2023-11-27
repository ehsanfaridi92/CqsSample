using Infrastructure.Query.Ef.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Query.Ef;

public class PersonMapping : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("People").HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();
    }
}
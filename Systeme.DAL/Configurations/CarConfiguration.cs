using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Systeme.Domain.Entityes;

namespace Systeme.DAL.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Model).HasMaxLength(100).IsRequired();
        }

    }
}

using Microsoft.EntityFrameworkCore;
using dewi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace dewi.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Image).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.HasOne(x=>x.Position).WithMany(x => x.Employees).HasForeignKey(x => x.PositionId).HasPrincipalKey(x=>x.Id).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

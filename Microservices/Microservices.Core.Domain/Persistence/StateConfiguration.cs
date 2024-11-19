using Microservices.Core.Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservices.Core.Domain.Persistence
{
    internal class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("States");
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Code).IsRequired()
                .HasMaxLength(3);
            builder.HasIndex(x=>x.Code);
            
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

            builder.HasOne(x=>x.Country).WithMany(x=>x.States).HasForeignKey("Country");
        }
    }
}

using Microservices.Core.Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservices.Core.Domain.Persistence
{
    internal class TaxConfiguration : IEntityTypeConfiguration<Tax>
    {
        public void Configure(EntityTypeBuilder<Tax> builder)
        {
            builder.ToTable("Taxes");
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Code).IsRequired()
                .HasMaxLength(5);
            builder.HasIndex(x=>x.Code);
            
            builder.Property(x => x.Name).HasMaxLength(150).IsRequired();

            builder.HasOne(x=>x.State).WithMany(x=>x.Taxes).HasForeignKey("State");
        }
    }
}

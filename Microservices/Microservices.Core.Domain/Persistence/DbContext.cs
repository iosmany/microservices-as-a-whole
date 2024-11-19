using Microservices.Core.Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Core.Domain.Persistence
{
    internal class CoreDbContext : Microservices.Persistence.DatabaseContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<StateTax> StateTaxes { get; set; }

    }
}

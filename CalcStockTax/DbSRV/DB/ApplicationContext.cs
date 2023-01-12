using DbSRV.Models;
using Microsoft.EntityFrameworkCore;

namespace DbSRV.DB
{
    public class ApplicationContext : DbContext
    {            
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Investment> Investments => Set<Investment>();
        public DbSet<Tariff> Tariffs => Set<Tariff>();
        public DbSet<Tax> Taxs => Set<Tax>();
        public DbSet<TaxAmount> TaxAmounts => Set<TaxAmount>();
    }
}

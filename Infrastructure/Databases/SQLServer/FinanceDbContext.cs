using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases.SQLServer
{
    public class FinanceDbContext : DbContext
    {
        public DbSet<UserAccount> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinanceDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

using System.Data.Entity;

namespace InterestRateCalculation.Models
{
    public class InterestRateContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Agreement> Agreements { get; set; }

        public DbSet<BaseRate> BaseRates { get; set; }
    }
}
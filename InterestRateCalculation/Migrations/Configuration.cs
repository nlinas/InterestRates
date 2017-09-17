namespace InterestRateCalculation.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<InterestRateCalculation.Models.InterestRateContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(InterestRateCalculation.Models.InterestRateContext context)
        {
            context.BaseRates.AddOrUpdate(
                    p => p.BaseRateCode,
                    new Models.BaseRate { BaseRateCode = "VILIBOR1m" },
                    new Models.BaseRate { BaseRateCode = "VILIBOR3m" },
                    new Models.BaseRate { BaseRateCode = "VILIBOR6m" },
                    new Models.BaseRate { BaseRateCode = "VILIBOR1y" }
                );

            context.Customers.AddOrUpdate(
                p => p.PersonalId,
                new Models.Customer { PersonalId = 67812203006, Name = "Goras Trusevièius"},
                new Models.Customer { PersonalId = 78706151287, Name = "Dange Kulkavièiûtë" }
                );

            var customer1 = context.Customers.SingleOrDefault(m => m.PersonalId == 67812203006);
            var customer2 = context.Customers.SingleOrDefault(m => m.PersonalId == 78706151287);
            var baseRate1 = context.BaseRates.SingleOrDefault(m => m.BaseRateCode == "VILIBOR3m");
            var baseRate2 = context.BaseRates.SingleOrDefault(m => m.BaseRateCode == "VILIBOR6m");
            var baseRate3 = context.BaseRates.SingleOrDefault(m => m.BaseRateCode == "VILIBOR1y");

            context.Agreements.AddOrUpdate(
                p => new { p.CustomerId, p.BaseRateId },
                new Models.Agreement { Amount = 12000, BaseRate = baseRate1, BaseRateId = baseRate1.Id, Margin = 1.6m, Duration = 60, Customer = customer1, CustomerId = customer1.Id },
                new Models.Agreement { Amount = 8000, BaseRate = baseRate3, BaseRateId = baseRate3.Id, Margin = 2.2m, Duration = 36, Customer = customer2, CustomerId = customer2.Id },
                new Models.Agreement { Amount = 1000, BaseRate = baseRate2, BaseRateId = baseRate2.Id, Margin = 1.85m, Duration = 24, Customer = customer2, CustomerId = customer2.Id }
                );
        }
    }
}

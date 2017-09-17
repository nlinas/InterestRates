using InterestRateCalculation.Models;
using InterestRateCalculation.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace InterestRateCalculation.Controllers
{
    public class InterestRateController : ApiController
    {
        private InterestRateContext db = new InterestRateContext();
        
        public IHttpActionResult Get(
            long CustomerPersonalId, 
            string BaseRateCode, 
            string NewBaseRateCode,
            int Amount,
            decimal Margin,
            int Duration
            )
        {

            var Customer = db.Customers.FirstOrDefault(m => m.PersonalId == CustomerPersonalId);
            if (Customer == null)
            {
                return NotFound();
            }

            var BaseRate = db.BaseRates.FirstOrDefault(m => m.BaseRateCode == BaseRateCode);
            if (BaseRate == null)
            {
                return NotFound();
            }

            var NewBaseRate = db.BaseRates.FirstOrDefault(m => m.BaseRateCode == NewBaseRateCode);
            var InterestRate = InterBankRates.GetInterestRate(BaseRate.BaseRateCode, Margin);
            var NewInterestRate = InterBankRates.GetInterestRate(NewBaseRate.BaseRateCode, Margin);

            AgreementApiViewModel Agreement = new Agreement
            {
                Amount = Amount,
                BaseRate = BaseRate,
                Customer = Customer,
                Duration = Duration,
                InterestRate = InterestRate,
                Margin = Margin,
                NewInterestRate = NewInterestRate,
                InterestRatesDiff = InterestRate - NewInterestRate
            };

            return Ok(Agreement);
        }
    }

    public class AgreementApiViewModel
    {
        public int Amount { get; set; }
        public decimal Margin { get; set; }
        public int Duration { get; set; }
        public string CustomerName { get; set; }
        public long CustomerPersonalId { get; set; }
        public string BaseRateCode { get; set; }
        public decimal InterestRate { get; set; }
        public decimal NewInterestRate { get; set; }
        public decimal InterestRatesDiff { get; set; }

        public static implicit operator AgreementApiViewModel(Agreement agreement)
        {
            return new AgreementApiViewModel
            {
                Amount = agreement.Amount,
                Margin = agreement.Margin,
                Duration = agreement.Duration,
                CustomerName = agreement.Customer.Name,
                CustomerPersonalId = agreement.Customer.PersonalId,
                BaseRateCode = agreement.BaseRate.BaseRateCode,
                InterestRate = agreement.InterestRate,
                NewInterestRate = agreement.NewInterestRate,
                InterestRatesDiff = agreement.InterestRatesDiff,
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterestRateCalculation.Shared
{
    public static class InterBankRates
    {
        public static decimal GetBaseRateCode(string BaseRateCode)
        {
            try
            {
                VilibidViliborService.VilibidViliborSoapClient ws = new VilibidViliborService.VilibidViliborSoapClient();
                return ws.getLatestVilibRate(BaseRateCode);
            }
            catch (Exception ex)
            {
                //TODO: handle exception
                throw;
            }
        }

        public static decimal GetInterestRate (string BaseRateCode, decimal Margin)
        {
            return GetBaseRateCode(BaseRateCode) + Margin;
        }
    }
}
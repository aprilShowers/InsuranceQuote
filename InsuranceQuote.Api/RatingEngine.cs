using InsuranceQuote.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceQuote.Api
{
    public class RatingEngine
    {
        public decimal Rate(InsuranceCustomer newCustomerData)
        {
            decimal revenue = newCustomerData.Revenue;
            string state = newCustomerData.State;
            string business = newCustomerData.Business;
            int hazardFactor = 4;

            var basePremium = CalculateBasePremium(revenue);
            var stateResult = CalculateWithStateFactor(basePremium, state);
            var businessResult = CalculateWithBusinessFactor(stateResult, business);
            var total = CalculateWithHazardFactor(businessResult, hazardFactor);

            return total;
        }

        decimal CalculateBasePremium(decimal revenue)
        {
            var basePremium = revenue / 1000;
            return basePremium;
        }

        decimal CalculateWithStateFactor(decimal basePremium, string state)
        {
            decimal stateFactor = 0;

            if (state == "OH") stateFactor = 1;
            if (state == "FL") stateFactor = 1.2m;
            if (state == "TX") stateFactor = 0.943m;

            var resultWithState = basePremium * stateFactor;
            return resultWithState;
        }

        decimal CalculateWithBusinessFactor(decimal stateResult, string business)
        {
            decimal businessFactor = 0m;

            if (business == "Architect") businessFactor = 1m;
            if (business == "Plumber") businessFactor = 0.5m;
            if (business == "Programmer") businessFactor = 1.25m;

            var resultWithBusiness = stateResult * businessFactor;
            return resultWithBusiness;
        }

        decimal CalculateWithHazardFactor(decimal businessResult, int hazardFactor)
        {
            decimal calculatedPremium = businessResult * hazardFactor;
            return calculatedPremium;
        }
    }
}

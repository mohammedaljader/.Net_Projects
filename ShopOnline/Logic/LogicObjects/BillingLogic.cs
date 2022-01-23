using Data;
using Data.Repositories;
using Interfaces.Contexts;
using Interfaces.Logic;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;

namespace Logic.LogicObjects
{
    public class BillingLogic : IBillingLogic
    {
        private readonly IBillingRepository BillingRepository;

        public BillingLogic(IBillingContext context)
        {
            BillingRepository = new BillingRepository(context);
        }

        public Billing GetBillingById(int id)
        {
            return BillingRepository.GetBillingById(id);
        }

        public void AddBilling(Billing Billing)
        {
            BillingRepository.AddBilling(Billing);
        }
        public void RemoveBilling(Billing Billing)
        {
            BillingRepository.RemoveBilling(Billing);
        }
    }
}
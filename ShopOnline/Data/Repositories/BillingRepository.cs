using Interfaces.Contexts;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        private readonly IBillingContext Context;

        public BillingRepository(IBillingContext context)
        {
            Context = context;
        }

        public Billing GetBillingById(int id)
        {
            return Context.GetBillingById(id);
        }

        public void AddBilling(Billing billing)
        {
            Context.AddBilling(billing);
        }

        public void RemoveBilling(Billing billing)
        {
            Context.RemoveBilling(billing);
        }
    }
}
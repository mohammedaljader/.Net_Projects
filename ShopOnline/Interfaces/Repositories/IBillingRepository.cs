using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Repositories
{
    /// <summary>
    /// Defines functionality for a product repository.
    /// </summary>
    public interface IBillingRepository
    {
        Billing GetBillingById(int id);
        void AddBilling(Billing billing);
        void RemoveBilling(Billing billing);
    }
}
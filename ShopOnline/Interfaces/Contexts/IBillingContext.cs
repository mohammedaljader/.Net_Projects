using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Contexts
{
    /// <summary>
    /// Defines functionality for a billing context.
    /// </summary>
    public interface IBillingContext
    {
        Billing GetBillingById(int id);
        void AddBilling(Billing billing);
        void RemoveBilling(Billing billing);
    }
}
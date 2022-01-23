

using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Logic
{
    /// <summary>
    /// Defines functionality for a billing logic class.
    /// </summary>
    public interface IBillingLogic
    {
        Billing GetBillingById(int id);
        void AddBilling(Billing billing);
        void RemoveBilling(Billing billing);
    }
}
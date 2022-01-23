

using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Logic
{
    /// <summary>
    /// Defines functionality for a order logic class.
    /// </summary>
    public interface IOrderLogic
    {
        List<Order> GetAllOrdersByUser(User user);
        void AddOrder(Order order);
        void DeleteOrder(Order order);
    }
}
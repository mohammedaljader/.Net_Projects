using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Repositories
{
    /// <summary>
    /// Defines functionality for a order repository.
    /// </summary>
    public interface IOrderRepository
    {
        List<Order> GetAllOrdersByUser(User user);
        void AddOrder(Order order);
        void DeleteOrder(Order order);
    }
}
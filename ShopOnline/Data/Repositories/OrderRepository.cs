using Interfaces.Contexts;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderContext Context;

        public OrderRepository(IOrderContext context)
        {
            Context = context;
        }

        public void AddOrder(Order order)
        {
            Context.AddOrder(order);
        }

        public void DeleteOrder(Order order)
        {
            Context.DeleteOrder(order);
        }

        public List<Order>GetAllOrdersByUser(User user)
        {
            return Context.GetAllOrdersByUser(user);
        }
    }
}
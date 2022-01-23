using Data;
using Data.Repositories;
using Interfaces.Contexts;
using Interfaces.Logic;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;

namespace Logic.LogicObjects
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderRepository OrderRepository;

        public OrderLogic(IOrderContext context)
        {
            OrderRepository = new OrderRepository(context);
        }
        public List<Order> GetAllOrdersByUser(User user)
        {
            return OrderRepository.GetAllOrdersByUser(user);
        }
        public void AddOrder(Order Order)
        {
            OrderRepository.AddOrder(Order);
        }
        public void DeleteOrder(Order Order)
        {
            OrderRepository.DeleteOrder(Order);
        }
    }
}
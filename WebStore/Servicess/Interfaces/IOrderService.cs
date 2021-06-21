using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.Entitys.Orders;
using WebStore.ViewModels;

namespace WebStore.Servicess.Interfaces
{
    public class IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrder(string UserName);

        Task<Order> GetOrderById(int id);

        Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel);
    }
}

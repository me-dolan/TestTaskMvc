using TestTaskMvc.Models;

namespace TestTaskMvc.Data.Interface
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        void Update(Order order);
        Task<bool> Dublicate(Order order);
        Task<IEnumerable<Provider>> GetProviderSelectList();
        Task<IEnumerable<OrderItem>> GetOrderItemsByIdOrder(int? id);
    }
}

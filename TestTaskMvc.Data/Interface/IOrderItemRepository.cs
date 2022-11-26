using TestTaskMvc.Models;

namespace TestTaskMvc.Data.Interface
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
        void Update(OrderItem orderItem);
        Task<bool> Dublicate(OrderItem orderItem);
        Task<IEnumerable<Provider>> GetProviderSelectList();
        Task<IEnumerable<Order>> GetOrdersSelectList();
    }
}

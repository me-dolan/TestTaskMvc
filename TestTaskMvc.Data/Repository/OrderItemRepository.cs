using Microsoft.EntityFrameworkCore;
using TestTaskMvc.Data.Interface;
using TestTaskMvc.Models;

namespace TestTaskMvc.Data.Repository
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> Dublicate(OrderItem orderItem)
        {
            var dublicate = await _context.Order
                .FirstOrDefaultAsync(o => o.Number == orderItem.Name) != null
                ? true : false;

            return dublicate;
        }

        public async Task<IEnumerable<Order>> GetOrdersSelectList()
        {
            var result = await _context.Order.Include(p => p.Provider).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Provider>> GetProviderSelectList()
        {
            var result = await _context.Provider.ToListAsync();
            return result;
        }

        public void Update(OrderItem orderItem)
        {
            _context.OrderItem.Update(orderItem);
        }
    }
}

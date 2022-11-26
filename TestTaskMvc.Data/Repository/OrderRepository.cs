using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskMvc.Data.Interface;
using TestTaskMvc.Models;

namespace TestTaskMvc.Data.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {   
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Order>();
        }

        public void Update(Order order)
        {
            _dbSet.Update(order);
        }

        public async Task<bool> Dublicate(Order order)
        {
            var dublicate = await _dbSet
                .FirstOrDefaultAsync(n => n.Number == order.Number && n.ProviderId == order.ProviderId) != null 
                ? true : false;
            
            return dublicate;
        }

        public async Task<IEnumerable<Provider>> GetProviderSelectList()
        {
            var result = await _context.Provider.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByIdOrder(int? id)
        {
            var result = await _context.OrderItem.Include(o => o.Order)
                .Where(o => o.Order.Id == id).ToListAsync();

            return result;
        }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestTaskMvc.Data;
using TestTaskMvc.Models;
using TestTaskMvc.Models.ViewModel;
using TestTaskMvc.Services.Interface;

namespace TestTaskMvc.Services
{
    public class OrderFilterService : IOrderFilterService
    {
        private readonly ApplicationDbContext _context;
        public OrderFilterService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public FilterSelectListViewModel GetFilterSelectListView()
        {
            var orderItemDistinct = _context.OrderItem.ToList();

            return new FilterSelectListViewModel()
            {
                OrderSelectList = _context.Order.Select(o => new SelectListItem
                {
                    Value = o.Number,
                    Text = o.Number
                }).Distinct(),

                OrderItemNameSelectList = orderItemDistinct.DistinctBy(s => s.Name)
                .Select(s => new SelectListItem
                {
                    Value = s.Name,
                    Text = s.Name
                }),

                OrderItemUnitSelectList = orderItemDistinct.DistinctBy(s => s.Unit)
                .Select(s => new SelectListItem
                {
                    Value = s.Unit,
                    Text = s.Unit
                }),

                ProviderSelectList = _context.Provider.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })
            };
        }

        public HomeViewModel Filter(RequestFilter request)
        {
            var orderContext = _context.Order.Include(p => p.Provider).ToList();
            var orderItemContex = _context.OrderItem.Include(o => o.Order).ToList();

            orderContext = orderContext.Where(o => o.Date >= request.DateFrom && o.Date <= request.DateTo).ToList();

            if (request.OrderNumberFilter != null)
            {
                orderContext = orderContext.Where(o => request.OrderNumberFilter.Contains(o.Number)).ToList();
            }

            if (request.ProviderId != null)
            {
                orderContext = orderContext.Where(o => request.ProviderId.Contains(o.Provider.Id)).ToList();
            }

            if (request.OrderItemNameFilter != null)
            {
                orderItemContex = orderItemContex.Where(o => request.OrderItemNameFilter.Contains(o.Name)).ToList();
            }

            if (request.OrderItemUnitFilter != null)
            {
                orderItemContex = orderItemContex.Where(o => request.OrderItemUnitFilter.Contains(o.Name)).ToList();
            }


            return new HomeViewModel()
            {
                Order = orderContext,
                OrderItem = orderItemContex,
                FilterSelectList = GetFilterSelectListView()
            };
        }
    }
}
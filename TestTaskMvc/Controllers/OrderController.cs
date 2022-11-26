using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using TestTaskMvc.Data;
using TestTaskMvc.Data.Interface;
using TestTaskMvc.Models;
using TestTaskMvc.Models.ViewModel;

namespace TestTaskMvc.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _context;

        public OrderController(IOrderRepository context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var orders = _context.GetAll(includeProperties: "Provider") ;
            return View(orders);
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }

            var order = _context.GetAll(includeProperties: "Provider").FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            OrderViewModel orderViewModel = new OrderViewModel()
            {
                Order = order,
                OrderItems = await _context.GetOrderItemsByIdOrder(id)
            };

            var count = orderViewModel.OrderItems.Count();

            return View(orderViewModel);
        }

        // GET: Order/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ProviderId"] = new SelectList(await _context.GetProviderSelectList(), "Id", "Name");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Date,ProviderId")] Order order)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Dublicate(order))
                {
                    ViewData["ProviderId"] = new SelectList(await _context.GetProviderSelectList(), "Id", "Name", order.ProviderId);
                    ViewData["ErrorCreateMessage"] = "Ошибка. Заказ с таким номером уже существует от этого пользователя";
                    return View(order);
                }
                _context.Add(order);
                await _context.SaveAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["ProviderId"] = new SelectList(await _context.GetProviderSelectList(), "Id", "Name", order.ProviderId);
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }

            var order = _context.GetAll(includeProperties: "Provider").FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ProviderId"] = new SelectList(await _context.GetProviderSelectList(), "Id", "Name", order.ProviderId);
            return View(order);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Date,ProviderId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await _context.Dublicate(order))
                {
                    ViewData["ProviderId"] = new SelectList(await _context.GetProviderSelectList(), "Id", "Name", order.ProviderId);
                    ViewData["ErrorCreateMessage"] = "Ошибка. Заказ с таким номером уже существует от этого пользователя";
                    return View(order);
                }
                _context.Update(order);
                await _context.SaveAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["ProviderId"] = new SelectList(await _context.GetProviderSelectList(), "Id", "Name", order.ProviderId);
            return View(order);
        }


        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }


            var order = _context.GetAll(includeProperties: "Provider").FirstOrDefault(o => o.Id == id);


            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Order'  is null.");
            }
            var order = await _context.FindAsync(id);
            if (order != null)
            {
                _context.Remove(order);
            }
            
            await _context.SaveAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestTaskMvc.Data;
using TestTaskMvc.Data.Interface;
using TestTaskMvc.Models;

namespace TestTaskMvc.Controllers
{
    [Route("Order/[controller]/{action=Index}/{id?}")]
    public class OrderItemController : Controller
    {
        private readonly IOrderItemRepository _context;

        public OrderItemController(IOrderItemRepository context)
        {
            _context = context;
        }


        // GET: OrederItem
        public async Task<IActionResult> Index()
        {
            //var dbCont = _context.OrderItem;
            //var orderDb = await _context.Order.Select(o => new
            //{
            //    Number = o.Number,
            //    Date = o.Date,
            //    Provider = o.ProviderId
            //}).Distinct().ToListAsync();

            var orderItem = _context.GetAll(includeProperties: "Order");
            
            return View(orderItem);
        }

        // GET: OrederItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }

            var orderItem = await _context.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // GET: OrederItem/Create
        public async Task<IActionResult> Create()
        {
            ViewData["OrderId"] = new SelectList(await _context.GetOrdersSelectList(), "Id", "Number");
                
            return View();
        }

        // POST: OrederItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Quantity,Unit,OrderId")] OrderItem item)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Dublicate(item))
                {
                    ViewData["OrderId"] = new SelectList(await _context.GetOrdersSelectList(), "Id", "Number", item.OrderId);
                    ViewData["ErrorCreateMessage"] = "Ошибка. Название товара не должно совпадать с номером заказа";
                    return View(item);
                }
                _context.Add(item);
                await _context.SaveAsync();
                return RedirectToAction("Details", "Order", new { id = item.OrderId});
            }
            ViewData["OrderId"] = new SelectList(await _context.GetOrdersSelectList(), "Id", "Number", item.OrderId);
            return View(item);
        }

        // GET: OrederItem/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (_context == null)
            {
                return NotFound();
            }

            var orderItem = await _context.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(await _context.GetOrdersSelectList(), "Id", "Number", orderItem.OrderId);
            return View(orderItem);
        }

        // POST: OrederItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Quantity,Unit,OrderId")] OrderItem item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await _context.Dublicate(item))
                {
                    ViewData["OrderId"] = new SelectList(await _context.GetOrdersSelectList(), "Id", "Number", item.OrderId);
                    ViewData["ErrorCreateMessage"] = "Ошибка. Название товара не должно совпадать с номером заказа";
                    return View(item);
                }
                _context.Update(item);
                await _context.SaveAsync();
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("Details", "Order", new { id = item.OrderId });
            }
            ViewData["OrderId"] = new SelectList(await _context.GetOrdersSelectList(), "Id", "Number", item.OrderId);
            return View(item);
        }

        // GET: OrederItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }

            var orderItem = await _context.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // POST: OrederItem/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            if (_context == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Order'  is null.");
            }
            var orderItem = await _context.FindAsync(id);
            var orderId = orderItem.OrderId;
            if (orderItem != null)
            {
                _context.Remove(orderItem);
            }

            await _context.SaveAsync();
            return RedirectToAction("Details", "Order", new { id = orderId});
            //return RedirectToAction(nameof(Index));
        }
    }
}

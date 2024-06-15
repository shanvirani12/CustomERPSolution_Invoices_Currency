// Controllers/OrdersController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CustomERPSolution_Invoices_Currency.Data;
using CustomERPSolution_Invoices_Currency.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

public class OrdersController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrdersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Orders
    public async Task<IActionResult> Index()
    {
        var orders = _context.Orders.Include(o => o.Client);
        return View(await orders.ToListAsync());
    }

    // GET: Orders/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _context.Orders
            .Include(o => o.Client)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    // GET: Orders/Create
    public IActionResult Create()
    {
        ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name");
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
        return View();
    }

    // POST: Orders/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Date,ClientId")] Order order, List<OrderItem> orderItems)
    {
        if (ModelState.IsValid)
        {
            order.TotalAmount = orderItems.Sum(oi => oi.TotalPrice);
            _context.Add(order);
            await _context.SaveChangesAsync();

            foreach (var item in orderItems)
            {
                item.OrderId = order.Id;
                _context.Add(item);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", order.ClientId);
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
        return View(order);
    }

    // GET: Orders/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", order.ClientId);
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
        return View(order);
    }

    // POST: Orders/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Date,ClientId")] Order order, List<OrderItem> orderItems)
    {
        if (id != order.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                order.TotalAmount = orderItems.Sum(oi => oi.TotalPrice);
                _context.Update(order);
                await _context.SaveChangesAsync();

                foreach (var item in orderItems)
                {
                    if (item.Id == 0)
                    {
                        item.OrderId = order.Id;
                        _context.Add(item);
                    }
                    else
                    {
                        _context.Update(item);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", order.ClientId);
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
        return View(order);
    }

    // GET: Orders/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _context.Orders
            .Include(o => o.Client)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    // POST: Orders/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool OrderExists(int id)
    {
        return _context.Orders.Any(e => e.Id == id);
    }
}

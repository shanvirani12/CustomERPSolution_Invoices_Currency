// Controllers/QuotationsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CustomERPSolution_Invoices_Currency.Data;
using CustomERPSolution_Invoices_Currency.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

public class QuotationsController : Controller
{
    private readonly ApplicationDbContext _context;

    public QuotationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Quotations
    public async Task<IActionResult> Index()
    {
        var quotations = _context.Quotations.Include(q => q.Client);
        return View(await quotations.ToListAsync());
    }

    // GET: Quotations/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var quotation = await _context.Quotations
            .Include(q => q.Client)
            .Include(q => q.QuotationItems)
            .ThenInclude(qi => qi.Product)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (quotation == null)
        {
            return NotFound();
        }

        return View(quotation);
    }

    // GET: Quotations/Create
    public IActionResult Create()
    {
        ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name");
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
        return View();
    }

    // POST: Quotations/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Date,ClientId")] Quotation quotation, List<QuotationItem> quotationItems)
    {
        if (ModelState.IsValid)
        {
            quotation.TotalAmount = quotationItems.Sum(qi => qi.TotalPrice);
            _context.Add(quotation);
            await _context.SaveChangesAsync();

            foreach (var item in quotationItems)
            {
                item.QuotationId = quotation.Id;
                _context.Add(item);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", quotation.ClientId);
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
        return View(quotation);
    }

    // GET: Quotations/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var quotation = await _context.Quotations.FindAsync(id);
        if (quotation == null)
        {
            return NotFound();
        }
        ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", quotation.ClientId);
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
        return View(quotation);
    }

    // POST: Quotations/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Date,ClientId")] Quotation quotation, List<QuotationItem> quotationItems)
    {
        if (id != quotation.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                quotation.TotalAmount = quotationItems.Sum(qi => qi.TotalPrice);
                _context.Update(quotation);
                await _context.SaveChangesAsync();

                foreach (var item in quotationItems)
                {
                    if (item.Id == 0)
                    {
                        item.QuotationId = quotation.Id;
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
                if (!QuotationExists(quotation.Id))
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
        ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", quotation.ClientId);
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
        return View(quotation);
    }

    // GET: Quotations/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var quotation = await _context.Quotations
            .Include(q => q.Client)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (quotation == null)
        {
            return NotFound();
        }

        return View(quotation);
    }

    // POST: Quotations/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var quotation = await _context.Quotations.FindAsync(id);
        _context.Quotations.Remove(quotation);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool QuotationExists(int id)
    {
        return _context.Quotations.Any(e => e.Id == id);
    }
}

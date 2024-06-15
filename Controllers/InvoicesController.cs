using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomERPSolution_Invoices_Currency.Data;
using CustomERPSolution_Invoices_Currency.Models;

namespace CustomERP.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public IActionResult Index()
        {
            var invoices = _context.Invoices
                .Include(i => i.Client)
                .ToList();
            return View(invoices);
        }

        // GET: Invoices/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = _context.Invoices
                .Include(i => i.Client)
                .Include(i => i.InvoiceItems)
                .ThenInclude(ii => ii.Product)
                .FirstOrDefault(m => m.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewBag.ClientId = new SelectList(_context.Clients, "Id", "Name");
            ViewBag.ProductId = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Invoices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                // Calculate total amount based on invoice items
                invoice.TotalAmount = invoice.InvoiceItems.Sum(i => i.TotalPrice);

                _context.Invoices.Add(invoice);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ClientId = new SelectList(_context.Clients, "Id", "Name", invoice.ClientId);
            ViewBag.ProductId = new SelectList(_context.Products, "Id", "Name");
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = _context.Invoices
                .Include(i => i.Client)
                .Include(i => i.InvoiceItems)
                .ThenInclude(ii => ii.Product)
                .FirstOrDefault(m => m.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            ViewBag.ClientId = new SelectList(_context.Clients, "Id", "Name", invoice.ClientId);
            ViewBag.ProductId = new SelectList(_context.Products, "Id", "Name");

            return View(invoice);
        }

        // POST: Invoices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update total amount based on invoice items
                    invoice.TotalAmount = invoice.InvoiceItems.Sum(i => i.TotalPrice);

                    _context.Update(invoice);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
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
            ViewBag.ClientId = new SelectList(_context.Clients, "Id", "Name", invoice.ClientId);
            ViewBag.ProductId = new SelectList(_context.Products, "Id", "Name");
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = _context.Invoices
                .Include(i => i.Client)
                .FirstOrDefault(m => m.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var invoice = _context.Invoices.Find(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeMoGCS10035.Models;

namespace DeMoGCS10035.Controllers
{
    public class TblBooksController : Controller
    {
        private readonly FpTbook2Context _context;
        
        public TblBooksController(FpTbook2Context context)
        {
            _context = context;
        }

        // GET: TblBooks
        public async Task<IActionResult> Index()
        {
            var fpTbook2Context = _context.TblBooks.Include(t => t.Cat).Include(t => t.Owner).Include(t => t.Publisher);
            return View(await fpTbook2Context.ToListAsync());
        }

        // GET: TblBooks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TblBooks == null)
            {
                return NotFound();
            }

            var tblBook = await _context.TblBooks
                .Include(t => t.Cat)
                .Include(t => t.Owner)
                .Include(t => t.Publisher)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (tblBook == null)
            {
                return NotFound();
            }

            return View(tblBook);
        }

        // GET: TblBooks/Create
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_context.TblCategories, "CatId", "CatId");
            ViewData["OwnerId"] = new SelectList(_context.TblStoreOwners, "OwnerId", "OwnerId");
            ViewData["PublisherId"] = new SelectList(_context.TblPublishers, "PublisherId", "PublisherId");
            return View();
        }

        // POST: TblBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,BookTitle,BookPrice,BookDetailes,CatId,OwnerId,PublisherId")] TblBook tblBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatId"] = new SelectList(_context.TblCategories, "CatId", "CatId", tblBook.CatId);
            ViewData["OwnerId"] = new SelectList(_context.TblStoreOwners, "OwnerId", "OwnerId", tblBook.OwnerId);
            ViewData["PublisherId"] = new SelectList(_context.TblPublishers, "PublisherId", "PublisherId", tblBook.PublisherId);
            return View(tblBook);
        }

        // GET: TblBooks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TblBooks == null)
            {
                return NotFound();
            }

            var tblBook = await _context.TblBooks.FindAsync(id);
            if (tblBook == null)
            {
                return NotFound();
            }
            ViewData["CatId"] = new SelectList(_context.TblCategories, "CatId", "CatId", tblBook.CatId);
            ViewData["OwnerId"] = new SelectList(_context.TblStoreOwners, "OwnerId", "OwnerId", tblBook.OwnerId);
            ViewData["PublisherId"] = new SelectList(_context.TblPublishers, "PublisherId", "PublisherId", tblBook.PublisherId);
            return View(tblBook);
        }

        // POST: TblBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BookId,BookTitle,BookPrice,BookDetailes,CatId,OwnerId,PublisherId")] TblBook tblBook)
        {
            if (id != tblBook.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblBookExists(tblBook.BookId))
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
            ViewData["CatId"] = new SelectList(_context.TblCategories, "CatId", "CatId", tblBook.CatId);
            ViewData["OwnerId"] = new SelectList(_context.TblStoreOwners, "OwnerId", "OwnerId", tblBook.OwnerId);
            ViewData["PublisherId"] = new SelectList(_context.TblPublishers, "PublisherId", "PublisherId", tblBook.PublisherId);
            return View(tblBook);
        }

        // GET: TblBooks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TblBooks == null)
            {
                return NotFound();
            }

            var tblBook = await _context.TblBooks
                .Include(t => t.Cat)
                .Include(t => t.Owner)
                .Include(t => t.Publisher)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (tblBook == null)
            {
                return NotFound();
            }

            return View(tblBook);
        }

        // POST: TblBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TblBooks == null)
            {
                return Problem("Entity set 'FpTbook2Context.TblBooks'  is null.");
            }
            var tblBook = await _context.TblBooks.FindAsync(id);
            if (tblBook != null)
            {
                _context.TblBooks.Remove(tblBook);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblBookExists(string id)
        {
          return (_context.TblBooks?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Medox.Models;

namespace Medox.Controllers
{
    public class KorZstToKorZstSkladsController : Controller
    {
        private readonly MedokForRecruitContext _context;

        public KorZstToKorZstSkladsController(MedokForRecruitContext context)
        {
            _context = context;
        }

        // GET: KorZstToKorZstSklads
        public async Task<IActionResult> Index()
        {
              return _context.KorZstToKorZstSklads != null ? 
                          View(await _context.KorZstToKorZstSklads.ToListAsync()) :
                          Problem("Entity set 'MedokForRecruitContext.KorZstToKorZstSklads'  is null.");
        }

        // GET: KorZstToKorZstSklads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.KorZstToKorZstSklads == null)
            {
                return NotFound();
            }

            var korZstToKorZstSklad = await _context.KorZstToKorZstSklads
                .FirstOrDefaultAsync(m => m.ZstToZstSkladId == id);
            if (korZstToKorZstSklad == null)
            {
                return NotFound();
            }

            return View(korZstToKorZstSklad);
        }

        // GET: KorZstToKorZstSklads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KorZstToKorZstSklads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZstToZstSkladId,ZstToZstSkladData,PerId")] KorZstToKorZstSklad korZstToKorZstSklad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(korZstToKorZstSklad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(korZstToKorZstSklad);
        }

        // GET: KorZstToKorZstSklads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.KorZstToKorZstSklads == null)
            {
                return NotFound();
            }

            var korZstToKorZstSklad = await _context.KorZstToKorZstSklads.FindAsync(id);
            if (korZstToKorZstSklad == null)
            {
                return NotFound();
            }
            return View(korZstToKorZstSklad);
        }

        // POST: KorZstToKorZstSklads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZstToZstSkladId,ZstToZstSkladData,PerId")] KorZstToKorZstSklad korZstToKorZstSklad)
        {
            if (id != korZstToKorZstSklad.ZstToZstSkladId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korZstToKorZstSklad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorZstToKorZstSkladExists(korZstToKorZstSklad.ZstToZstSkladId))
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
            return View(korZstToKorZstSklad);
        }

        // GET: KorZstToKorZstSklads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.KorZstToKorZstSklads == null)
            {
                return NotFound();
            }

            var korZstToKorZstSklad = await _context.KorZstToKorZstSklads
                .FirstOrDefaultAsync(m => m.ZstToZstSkladId == id);
            if (korZstToKorZstSklad == null)
            {
                return NotFound();
            }

            return View(korZstToKorZstSklad);
        }

        // POST: KorZstToKorZstSklads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.KorZstToKorZstSklads == null)
            {
                return Problem("Entity set 'MedokForRecruitContext.KorZstToKorZstSklads'  is null.");
            }
            var korZstToKorZstSklad = await _context.KorZstToKorZstSklads.FindAsync(id);
            if (korZstToKorZstSklad != null)
            {
                _context.KorZstToKorZstSklads.Remove(korZstToKorZstSklad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KorZstToKorZstSkladExists(int id)
        {
          return (_context.KorZstToKorZstSklads?.Any(e => e.ZstToZstSkladId == id)).GetValueOrDefault();
        }
    }
}

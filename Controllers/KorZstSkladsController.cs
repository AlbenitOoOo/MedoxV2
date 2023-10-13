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
    public class KorZstSkladsController : Controller
    {
        private readonly MedokForRecruitContext _context;

        public KorZstSkladsController(MedokForRecruitContext context)
        {
            _context = context;
        }

        // GET: KorZstSklads
        public async Task<IActionResult> Index()
        {
            var medokForRecruitContext = _context.KorZstSklads.Include(k => k.Skd).Include(k => k.ZstToZstSklad);
            return View(await medokForRecruitContext.ToListAsync());
        }

        // GET: KorZstSklads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.KorZstSklads == null)
            {
                return NotFound();
            }

            var korZstSklad = await _context.KorZstSklads
                .Include(k => k.Skd)
                .Include(k => k.ZstToZstSklad)
                .FirstOrDefaultAsync(m => m.ZstSkladId == id);
            if (korZstSklad == null)
            {
                return NotFound();
            }

            return View(korZstSklad);
        }

        // GET: KorZstSklads/Create
        public IActionResult Create()
        {
            ViewData["SkdId"] = new SelectList(_context.KorSkds, "SkdId", "SkdId");
            ViewData["ZstToZstSkladId"] = new SelectList(_context.KorZstToKorZstSklads, "ZstToZstSkladId", "ZstToZstSkladId");
            return View();
        }

        // POST: KorZstSklads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZstSkladId,ZstToZstSkladId,SkdId,ZstSkladIloscSkd,ZstSkladnikUwagi,ZstSkladCzyAktywny,DbDataWpisu,DbPerId")] KorZstSklad korZstSklad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(korZstSklad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SkdId"] = new SelectList(_context.KorSkds, "SkdId", "SkdId", korZstSklad.SkdId);
            ViewData["ZstToZstSkladId"] = new SelectList(_context.KorZstToKorZstSklads, "ZstToZstSkladId", "ZstToZstSkladId", korZstSklad.ZstToZstSkladId);
            return View(korZstSklad);
        }

        // GET: KorZstSklads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.KorZstSklads == null)
            {
                return NotFound();
            }

            var korZstSklad = await _context.KorZstSklads.FindAsync(id);
            if (korZstSklad == null)
            {
                return NotFound();
            }
            ViewData["SkdId"] = new SelectList(_context.KorSkds, "SkdId", "SkdId", korZstSklad.SkdId);
            ViewData["ZstToZstSkladId"] = new SelectList(_context.KorZstToKorZstSklads, "ZstToZstSkladId", "ZstToZstSkladId", korZstSklad.ZstToZstSkladId);
            return View(korZstSklad);
        }

        // POST: KorZstSklads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZstSkladId,ZstToZstSkladId,SkdId,ZstSkladIloscSkd,ZstSkladnikUwagi,ZstSkladCzyAktywny,DbDataWpisu,DbPerId")] KorZstSklad korZstSklad)
        {
            if (id != korZstSklad.ZstSkladId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korZstSklad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorZstSkladExists(korZstSklad.ZstSkladId))
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
            ViewData["SkdId"] = new SelectList(_context.KorSkds, "SkdId", "SkdId", korZstSklad.SkdId);
            ViewData["ZstToZstSkladId"] = new SelectList(_context.KorZstToKorZstSklads, "ZstToZstSkladId", "ZstToZstSkladId", korZstSklad.ZstToZstSkladId);
            return View(korZstSklad);
        }

        // GET: KorZstSklads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.KorZstSklads == null)
            {
                return NotFound();
            }

            var korZstSklad = await _context.KorZstSklads
                .Include(k => k.Skd)
                .Include(k => k.ZstToZstSklad)
                .FirstOrDefaultAsync(m => m.ZstSkladId == id);
            if (korZstSklad == null)
            {
                return NotFound();
            }

            return View(korZstSklad);
        }

        // POST: KorZstSklads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.KorZstSklads == null)
            {
                return Problem("Entity set 'MedokForRecruitContext.KorZstSklads'  is null.");
            }
            var korZstSklad = await _context.KorZstSklads.FindAsync(id);
            if (korZstSklad != null)
            {
                _context.KorZstSklads.Remove(korZstSklad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KorZstSkladExists(int id)
        {
          return (_context.KorZstSklads?.Any(e => e.ZstSkladId == id)).GetValueOrDefault();
        }
    }
}

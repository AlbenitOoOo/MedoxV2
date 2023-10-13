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
    public class KorZstsController : Controller
    {
        private readonly MedokForRecruitContext _context;

        public KorZstsController(MedokForRecruitContext context)
        {
            _context = context;
        }

        // GET: KorZsts
        public async Task<IActionResult> Index()
        {
            var medokForRecruitContext = _context.KorZsts.Include(k => k.ZstMain).Include(k => k.ZstToZstSklad);
            return View(await medokForRecruitContext.ToListAsync());
        }

        // GET: KorZsts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.KorZsts == null)
            {
                return NotFound();
            }

            var korZst = await _context.KorZsts
                .Include(k => k.ZstMain)
                .Include(k => k.ZstToZstSklad)
                .FirstOrDefaultAsync(m => m.ZstId == id);
            if (korZst == null)
            {
                return NotFound();
            }

            return View(korZst);
        }

        // GET: KorZsts/Create
        public IActionResult Create()
        {
            ViewData["ZstMainId"] = new SelectList(_context.KorZstMains, "ZstMainId", "ZstMainId");
            ViewData["ZstToZstSkladId"] = new SelectList(_context.KorZstToKorZstSklads, "ZstToZstSkladId", "ZstToZstSkladId");
            return View();
        }

        // POST: KorZsts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZstId,ZstMainId,ZstToZstSkladId,ZstCzyAktywny,DbDataWpisu,DbPerId")] KorZst korZst)
        {
            if (ModelState.IsValid)
            {
                _context.Add(korZst);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ZstMainId"] = new SelectList(_context.KorZstMains, "ZstMainId", "ZstMainId", korZst.ZstMainId);
            ViewData["ZstToZstSkladId"] = new SelectList(_context.KorZstToKorZstSklads, "ZstToZstSkladId", "ZstToZstSkladId", korZst.ZstToZstSkladId);
            return View(korZst);
        }

        // GET: KorZsts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.KorZsts == null)
            {
                return NotFound();
            }

            var korZst = await _context.KorZsts.FindAsync(id);
            if (korZst == null)
            {
                return NotFound();
            }
            ViewData["ZstMainId"] = new SelectList(_context.KorZstMains, "ZstMainId", "ZstMainId", korZst.ZstMainId);
            ViewData["ZstToZstSkladId"] = new SelectList(_context.KorZstToKorZstSklads, "ZstToZstSkladId", "ZstToZstSkladId", korZst.ZstToZstSkladId);
            return View(korZst);
        }

        // POST: KorZsts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZstId,ZstMainId,ZstToZstSkladId,ZstCzyAktywny,DbDataWpisu,DbPerId")] KorZst korZst)
        {
            if (id != korZst.ZstId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korZst);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorZstExists(korZst.ZstId))
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
            ViewData["ZstMainId"] = new SelectList(_context.KorZstMains, "ZstMainId", "ZstMainId", korZst.ZstMainId);
            ViewData["ZstToZstSkladId"] = new SelectList(_context.KorZstToKorZstSklads, "ZstToZstSkladId", "ZstToZstSkladId", korZst.ZstToZstSkladId);
            return View(korZst);
        }

        // GET: KorZsts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.KorZsts == null)
            {
                return NotFound();
            }

            var korZst = await _context.KorZsts
                .Include(k => k.ZstMain)
                .Include(k => k.ZstToZstSklad)
                .FirstOrDefaultAsync(m => m.ZstId == id);
            if (korZst == null)
            {
                return NotFound();
            }

            return View(korZst);
        }

        // POST: KorZsts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.KorZsts == null)
            {
                return Problem("Entity set 'MedokForRecruitContext.KorZsts'  is null.");
            }
            var korZst = await _context.KorZsts.FindAsync(id);
            if (korZst != null)
            {
                _context.KorZsts.Remove(korZst);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KorZstExists(int id)
        {
          return (_context.KorZsts?.Any(e => e.ZstId == id)).GetValueOrDefault();
        }
    }
}

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
    public class KorZstMainsController : Controller
    {
        private readonly MedokForRecruitContext _context;

        public KorZstMainsController(MedokForRecruitContext context)
        {
            _context = context;
        }

        // GET: KorZstMains
        public async Task<IActionResult> Index()
        {
              return _context.KorZstMains != null ? 
                          View(await _context.KorZstMains.ToListAsync()) :
                          Problem("Entity set 'MedokForRecruitContext.KorZstMains'  is null.");
        }

        // GET: KorZstMains/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.KorZstMains == null)
            {
                return NotFound();
            }

            var korZstMain = await _context.KorZstMains
                .FirstOrDefaultAsync(m => m.ZstMainId == id);
            if (korZstMain == null)
            {
                return NotFound();
            }

            return View(korZstMain);
        }

        // GET: KorZstMains/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KorZstMains/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZstMainId,ZstMainNazwa,ZstMainOpis,ZstMainCzyAktywny,DbDataWpisu,DbPerId")] KorZstMain korZstMain)
        {
            if (ModelState.IsValid)
            {
                _context.Add(korZstMain);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(korZstMain);
        }

        // GET: KorZstMains/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.KorZstMains == null)
            {
                return NotFound();
            }

            var korZstMain = await _context.KorZstMains.FindAsync(id);
            if (korZstMain == null)
            {
                return NotFound();
            }
            return View(korZstMain);
        }

        // POST: KorZstMains/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZstMainId,ZstMainNazwa,ZstMainOpis,ZstMainCzyAktywny,DbDataWpisu,DbPerId")] KorZstMain korZstMain)
        {
            if (id != korZstMain.ZstMainId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korZstMain);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorZstMainExists(korZstMain.ZstMainId))
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
            return View(korZstMain);
        }

        // GET: KorZstMains/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.KorZstMains == null)
            {
                return NotFound();
            }

            var korZstMain = await _context.KorZstMains
                .FirstOrDefaultAsync(m => m.ZstMainId == id);
            if (korZstMain == null)
            {
                return NotFound();
            }

            return View(korZstMain);
        }

        // POST: KorZstMains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.KorZstMains == null)
            {
                return Problem("Entity set 'MedokForRecruitContext.KorZstMains'  is null.");
            }
            var korZstMain = await _context.KorZstMains.FindAsync(id);
            if (korZstMain != null)
            {
                _context.KorZstMains.Remove(korZstMain);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KorZstMainExists(int id)
        {
          return (_context.KorZstMains?.Any(e => e.ZstMainId == id)).GetValueOrDefault();
        }
    }
}

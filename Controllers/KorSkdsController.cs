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
    public class KorSkdsController : Controller
    {
        private readonly MedokForRecruitContext _context;

        public KorSkdsController(MedokForRecruitContext context)
        {
            _context = context;
        }

        // GET: KorSkds
        public async Task<IActionResult> Index()
        {
              return _context.KorSkds != null ? 
                          View(await _context.KorSkds.ToListAsync()) :
                          Problem("Entity set 'MedokForRecruitContext.KorSkds'  is null.");
        }

        // GET: KorSkds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.KorSkds == null)
            {
                return NotFound();
            }

            var korSkd = await _context.KorSkds
                .FirstOrDefaultAsync(m => m.SkdId == id);
            if (korSkd == null)
            {
                return NotFound();
            }

            return View(korSkd);
        }

        // GET: KorSkds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KorSkds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SkdId,SkdNazwa,SkdOpis,SkdCzyAktywny,DbDataWpisu,DbPerId")] KorSkd korSkd)
        {
            if (ModelState.IsValid)
            {
                _context.Add(korSkd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(korSkd);
        }

        // GET: KorSkds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.KorSkds == null)
            {
                return NotFound();
            }

            var korSkd = await _context.KorSkds.FindAsync(id);
            if (korSkd == null)
            {
                return NotFound();
            }
            return View(korSkd);
        }

        // POST: KorSkds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SkdId,SkdNazwa,SkdOpis,SkdCzyAktywny,DbDataWpisu,DbPerId")] KorSkd korSkd)
        {
            if (id != korSkd.SkdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korSkd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorSkdExists(korSkd.SkdId))
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
            return View(korSkd);
        }

        // GET: KorSkds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.KorSkds == null)
            {
                return NotFound();
            }

            var korSkd = await _context.KorSkds
                .FirstOrDefaultAsync(m => m.SkdId == id);
            if (korSkd == null)
            {
                return NotFound();
            }

            return View(korSkd);
        }

        // POST: KorSkds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.KorSkds == null)
            {
                return Problem("Entity set 'MedokForRecruitContext.KorSkds'  is null.");
            }
            var korSkd = await _context.KorSkds.FindAsync(id);
            if (korSkd != null)
            {
                _context.KorSkds.Remove(korSkd);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KorSkdExists(int id)
        {
          return (_context.KorSkds?.Any(e => e.SkdId == id)).GetValueOrDefault();
        }
    }
}

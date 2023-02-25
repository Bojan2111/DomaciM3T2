using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomaciM3T2.Models;
using System.Net.Sockets;

namespace DomaciM3T2.Controllers
{
    public class TipKartesController : Controller
    {
        private readonly AppDbContext _context;

        public TipKartesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TipKartes
        public IActionResult Index(int stranica = 1)
        {
            int stranicaIdx = stranica - 1;
            int velicinaStranice = 2;
            var sviTipovi = _context.TipKartes;
            var ticketTypes = Paginacija<TipKarte>.Create(sviTipovi, stranicaIdx, velicinaStranice);
            return View(ticketTypes);
            //return View(await _context.TipKartes.ToListAsync());
        }

        // GET: TipKartes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipKarte = await _context.TipKartes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipKarte == null)
            {
                return NotFound();
            }

            return View(tipKarte);
        }

        // GET: TipKartes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipKartes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv")] TipKarte tipKarte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipKarte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipKarte);
        }

        // GET: TipKartes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipKarte = await _context.TipKartes.FindAsync(id);
            if (tipKarte == null)
            {
                return NotFound();
            }
            return View(tipKarte);
        }

        // POST: TipKartes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv")] TipKarte tipKarte)
        {
            if (id != tipKarte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipKarte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipKarteExists(tipKarte.Id))
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
            return View(tipKarte);
        }

        // GET: TipKartes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipKarte = await _context.TipKartes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipKarte == null)
            {
                return NotFound();
            }

            return View(tipKarte);
        }

        // POST: TipKartes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipKarte = await _context.TipKartes.FindAsync(id);
            _context.TipKartes.Remove(tipKarte);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipKarteExists(int id)
        {
            return _context.TipKartes.Any(e => e.Id == id);
        }
    }
}

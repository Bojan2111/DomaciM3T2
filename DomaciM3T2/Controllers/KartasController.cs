using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomaciM3T2.Models;
using DomaciM3T2.Models.Filteri;
using System.Net.Sockets;

namespace DomaciM3T2.Controllers
{
    public class KartasController : Controller
    {
        private readonly AppDbContext _context;

        public KartasController(AppDbContext context)
        {
            _context = context;
        }

        public enum NaciniSortiranja
        {
            Datum,
            DatumObrnuto,
            Cena,
            CenaObrnuto
        };

        private static Dictionary<string, NaciniSortiranja> NaciniSortiranjaDict = new Dictionary<string, NaciniSortiranja>()
        {
            { "Datum", NaciniSortiranja.Datum },
            { "DatumObrnuto", NaciniSortiranja.DatumObrnuto },
            { "Cena", NaciniSortiranja.Cena },
            { "CenaObrnuto", NaciniSortiranja.CenaObrnuto }
        };

        SelectList listaOpcija = new SelectList(NaciniSortiranjaDict, "Key", "Key", "Datum");

        // GET: Kartas
        public ActionResult Index(KartaFilter filter, string sortiranje = "Datum")
        {
            IQueryable<Karta> kartas = _context.Kartas.Include(k => k.Festival).Include(k => k.TipKarte);
            //IQueryable<Karta> kartas = _context.Kartas.AsQueryable();
            if (sortiranje == "" || sortiranje == null)
            {
                sortiranje = "Datum";
            }
            if (filter.DatumOd != null)
            {
                kartas = kartas.Where(p => p.DatumKupovine >= filter.DatumOd);
            }
            if (filter.DatumDo != null)
            {
                kartas = kartas.Where(p => p.DatumKupovine <= filter.DatumDo);
            }
            if (filter.CenaOd != null)
            {
                kartas = kartas.Where(p => p.Cena >= filter.CenaOd);
            }
            if (filter.CenaDo != null)
            {
                kartas = kartas.Where(p => p.Cena <= filter.CenaDo);
            }

            NaciniSortiranja sortirajPo = NaciniSortiranjaDict[sortiranje];

            switch (sortirajPo)
            {
                case NaciniSortiranja.Cena:
                    kartas = kartas.OrderBy(c => c.Cena);
                    break;
                case NaciniSortiranja.CenaObrnuto:
                    kartas = kartas.OrderByDescending(c => c.Cena);
                    break;
                case NaciniSortiranja.Datum:
                    kartas = kartas.OrderBy(c => c.DatumKupovine);
                    break;
                case NaciniSortiranja.DatumObrnuto:
                    kartas = kartas.OrderByDescending(c => c.DatumKupovine);
                    break;
            }

            ViewBag.AktuelniFilter = filter;
            ViewBag.AktuelniNacinSortiranja = sortiranje;
            ViewBag.NaciniSortiranja = listaOpcija;
            return View(kartas);
            //var appDbContext = _context.Kartas.Include(k => k.Festival).Include(k => k.TipKarte);
            //return View(await appDbContext.ToListAsync());
        }

        // GET: Kartas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karta = await _context.Kartas
                .Include(k => k.Festival)
                .Include(k => k.TipKarte)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (karta == null)
            {
                return NotFound();
            }

            return View(karta);
        }

        // GET: Kartas/Create
        public IActionResult Create()
        {
            ViewData["FestivalId"] = new SelectList(_context.Festivals, "Id", "Naziv");
            ViewData["TipKarteId"] = new SelectList(_context.TipKartes, "Id", "Naziv");
            return View();
        }

        // POST: Kartas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cena,DatumKupovine,Kupac,Preuzeta,TipKarteId,FestivalId")] Karta karta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(karta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FestivalId"] = new SelectList(_context.Festivals, "Id", "Naziv", karta.FestivalId);
            ViewData["TipKarteId"] = new SelectList(_context.TipKartes, "Id", "Naziv", karta.TipKarteId);
            return View(karta);
        }

        // GET: Kartas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karta = await _context.Kartas.FindAsync(id);
            if (karta == null)
            {
                return NotFound();
            }
            ViewData["FestivalId"] = new SelectList(_context.Festivals, "Id", "Naziv", karta.FestivalId);
            ViewData["TipKarteId"] = new SelectList(_context.TipKartes, "Id", "Naziv", karta.TipKarteId);
            return View(karta);
        }

        // POST: Kartas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cena,DatumKupovine,Kupac,Preuzeta,TipKarteId,FestivalId")] Karta karta)
        {
            if (id != karta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(karta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KartaExists(karta.Id))
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
            ViewData["FestivalId"] = new SelectList(_context.Festivals, "Id", "Naziv", karta.FestivalId);
            ViewData["TipKarteId"] = new SelectList(_context.TipKartes, "Id", "Naziv", karta.TipKarteId);
            return View(karta);
        }

        // GET: Kartas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karta = await _context.Kartas
                .Include(k => k.Festival)
                .Include(k => k.TipKarte)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (karta == null)
            {
                return NotFound();
            }

            return View(karta);
        }

        // POST: Kartas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var karta = await _context.Kartas.FindAsync(id);
            _context.Kartas.Remove(karta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KartaExists(int id)
        {
            return _context.Kartas.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomaciM3T2.Models;
using DomaciM3T2.Models.Filteri;
using System.Globalization;

namespace DomaciM3T2.Controllers
{
    public class FestivalsController : Controller
    {
        private readonly AppDbContext _context;

        public FestivalsController(AppDbContext context)
        {
            _context = context;
        }

        public enum NaciniSortiranja
        {
            Naziv,
            NazivObrnuto,
            MaksimumPosetilaca,
            MaksimumPosetilacaObrnuto,
            Ocena,
            OcenaObrnuto,
            Datum,
            DatumObrnuto
        };

        private static Dictionary<string, NaciniSortiranja> NaciniSortiranjaDict = new Dictionary<string, NaciniSortiranja>()
        {
            { "Naziv", NaciniSortiranja.Naziv },
            { "NazivObrnuto", NaciniSortiranja.NazivObrnuto },
            { "MaksimumPosetilaca", NaciniSortiranja.MaksimumPosetilaca },
            { "MaksimumPosetilacaObrnuto", NaciniSortiranja.MaksimumPosetilacaObrnuto },
            { "Ocena", NaciniSortiranja.Ocena },
            { "OcenaObrnuto", NaciniSortiranja.OcenaObrnuto },
            { "Datum", NaciniSortiranja.Datum },
            { "DatumObrnuto", NaciniSortiranja.DatumObrnuto }
        };

        SelectList listaOpcija = new SelectList(NaciniSortiranjaDict, "Key", "Key", "Naziv");

        // GET: Festivals
        public ActionResult Index(FestivalFilter filter, string sortiranje = "Naziv")
        {
            IQueryable<Festival> festivals = _context.Festivals.AsQueryable();
            if (sortiranje == "")
            {
                sortiranje = "Naziv";
            }
            if (!String.IsNullOrWhiteSpace(filter.Naziv))
            {
                festivals = festivals.Where(p => p.Naziv.Contains(filter.Naziv));
            }
            if (!String.IsNullOrWhiteSpace(filter.Mesto))
            {
                festivals = festivals.Where(p => p.Mesto.StartsWith(filter.Mesto));
            }
            if (filter.DatumOd != null)
            {
                festivals = festivals.Where(p => p.DatumOdrzavanja >= filter.DatumOd);
            }
            if (filter.DatumDo != null)
            {
                festivals = festivals.Where(p => p.DatumOdrzavanja <= filter.DatumDo);
            }
            if (filter.Ocena != null)
            {
                festivals = festivals.Where(p => p.Ocena >= filter.Ocena);
            }

            NaciniSortiranja sortirajPo = NaciniSortiranjaDict[sortiranje];

            switch (sortirajPo)
            {
                case NaciniSortiranja.Naziv:
                    festivals = festivals.OrderBy(c => c.Naziv);
                    break;
                case NaciniSortiranja.NazivObrnuto:
                    festivals = festivals.OrderByDescending(c => c.Naziv);
                    break;
                case NaciniSortiranja.MaksimumPosetilaca:
                    festivals = festivals.OrderBy(c => c.MaksimumPosetilaca);
                    break;
                case NaciniSortiranja.MaksimumPosetilacaObrnuto:
                    festivals = festivals.OrderByDescending(c => c.MaksimumPosetilaca);
                    break;
                case NaciniSortiranja.Ocena:
                    festivals = festivals.OrderBy(c => c.Ocena);
                    break;
                case NaciniSortiranja.OcenaObrnuto:
                    festivals = festivals.OrderByDescending(c => c.Ocena);
                    break;
                case NaciniSortiranja.Datum:
                    festivals = festivals.OrderBy(c => c.DatumOdrzavanja);
                    break;
                case NaciniSortiranja.DatumObrnuto:
                    festivals = festivals.OrderByDescending(c => c.DatumOdrzavanja);
                    break;
            }

            ViewBag.AktuelniFilter = filter;
            ViewBag.AktuelniNacinSortiranja = sortiranje;
            ViewBag.NaciniSortiranja = listaOpcija;
            return View(festivals);
            //return View(await _context.Festivals.ToListAsync());
        }

        // GET: Festivals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var festival = await _context.Festivals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (festival == null)
            {
                return NotFound();
            }

            return View(festival);
        }

        // GET: Festivals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Festivals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Mesto,DatumOdrzavanja,Ocena,MaksimumPosetilaca")] Festival festival)
        {
            if (ModelState.IsValid)
            {
                _context.Add(festival);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(festival);
        }

        // GET: Festivals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var festival = await _context.Festivals.FindAsync(id);
            if (festival == null)
            {
                return NotFound();
            }
            return View(festival);
        }

        // POST: Festivals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Mesto,DatumOdrzavanja,Ocena,MaksimumPosetilaca")] Festival festival)
        {
            if (id != festival.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(festival);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FestivalExists(festival.Id))
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
            return View(festival);
        }

        // GET: Festivals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var festival = await _context.Festivals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (festival == null)
            {
                return NotFound();
            }

            return View(festival);
        }

        // POST: Festivals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var festival = await _context.Festivals.FindAsync(id);
            _context.Festivals.Remove(festival);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FestivalExists(int id)
        {
            return _context.Festivals.Any(e => e.Id == id);
        }
    }
}

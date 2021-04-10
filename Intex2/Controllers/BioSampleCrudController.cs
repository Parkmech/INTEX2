using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Intex2.Models;

namespace Intex2.Controllers
{
    public class BioSampleCrudController : Controller
    {
        private readonly FagElGamousContext _context;

        public BioSampleCrudController(FagElGamousContext context)
        {
            _context = context;
        }

        // GET: BioSampleCrud
        public IActionResult RecordSpecificIndex(string id)
        {
            string newid = id.Replace("%2F", "/");
            if (newid == null)
            {
                return NotFound();
            }
            var samples = _context.BiologicalSamples.Where(x => x.BurialId == newid);

            return View(samples);
        }

            public async Task<IActionResult> Index()
        {
            var fagElGamousContext = _context.BiologicalSamples.Include(b => b.Burial);
            return View(await fagElGamousContext.ToListAsync());
        }

        // GET: BioSampleCrud/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biologicalSample = await _context.BiologicalSamples
                .Include(b => b.Burial)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (biologicalSample == null)
            {
                return NotFound();
            }

            return View(biologicalSample);
        }

        // GET: BioSampleCrud/Create
        public IActionResult Create()
        {
            ViewData["BurialId"] = new SelectList(_context.Burials, "BurialId", "BurialId");
            return View();
        }

        // POST: BioSampleCrud/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BurialId,Rack,F3,Bag,LowNs,HighNs,NorthOrSouth,LowEw,HighEw,EastOrWest,Area,BurialNumber,ClusterNumber,Date,PreviouslySampled,Notes,Initials,Id,SsmaTimeStamp")] BiologicalSample biologicalSample)
        {
            if (ModelState.IsValid)
            {
                _context.Add(biologicalSample);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BurialId"] = new SelectList(_context.Burials, "BurialId", "BurialId", biologicalSample.BurialId);
            return View(biologicalSample);
        }

        // GET: BioSampleCrud/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biologicalSample = await _context.BiologicalSamples.FindAsync(id);
            if (biologicalSample == null)
            {
                return NotFound();
            }
            ViewData["BurialId"] = new SelectList(_context.Burials, "BurialId", "BurialId", biologicalSample.BurialId);
            return View(biologicalSample);
        }

        // POST: BioSampleCrud/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BurialId,Rack,F3,Bag,LowNs,HighNs,NorthOrSouth,LowEw,HighEw,EastOrWest,Area,BurialNumber,ClusterNumber,Date,PreviouslySampled,Notes,Initials,Id,SsmaTimeStamp")] BiologicalSample biologicalSample)
        {
            if (id != biologicalSample.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(biologicalSample);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BiologicalSampleExists(biologicalSample.Id))
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
            ViewData["BurialId"] = new SelectList(_context.Burials, "BurialId", "BurialId", biologicalSample.BurialId);
            return View(biologicalSample);
        }

        // GET: BioSampleCrud/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biologicalSample = await _context.BiologicalSamples
                .Include(b => b.Burial)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (biologicalSample == null)
            {
                return NotFound();
            }

            return View(biologicalSample);
        }

        // POST: BioSampleCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var biologicalSample = await _context.BiologicalSamples.FindAsync(id);
            _context.BiologicalSamples.Remove(biologicalSample);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BiologicalSampleExists(int id)
        {
            return _context.BiologicalSamples.Any(e => e.Id == id);
        }
    }
}

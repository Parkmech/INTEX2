using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Intex2.Models;
using Intex2.Models.ViewModels;

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
            if(newid == null)
            {
                return NotFound();
            }
            Burial burial = _context.Burials.Where(x => x.BurialId == newid).FirstOrDefault();

            var bioSamples = _context.BiologicalSamples.Where(x => x.BurialId == burial.BurialId);


            return View(new BioSampleViewModel()
            {
                biologicalSamples = bioSamples,
                burial = burial
            });   
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
        public IActionResult Create(string id)
        {

            string newid = id.Replace("%2F", "/");

            //Burial burial = _context.Burials.Where(x => x.BurialId == newid).FirstOrDefault();

            if (newid == null)
            {
                return NotFound();
            }

            ViewBag.id = newid;

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
                return View("RecordSpecificIndex", _context.Burials.Where(x => x.BurialId == biologicalSample.BurialId).FirstOrDefaultAsync());
            }
            ViewData["BurialId"] = new SelectList(_context.Burials, "BurialId", "BurialId", biologicalSample.BurialId);
            return View("Index");
        }

        [HttpPost]
        public IActionResult CustomCreate(BiologicalSample bio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bio);
                _context.SaveChanges();
                return View("RecordSpecificIndex", new BioSampleViewModel
                {
                    biologicalSamples = _context.BiologicalSamples.Where(x => x.BurialId == bio.BurialId),

                    burial = _context.Burials.Where(x => x.BurialId == bio.BurialId).FirstOrDefault(),

                    bioSample = bio
                });
            }

            ViewData["BurialId"] = new SelectList(_context.Burials, "BurialId", "BurialId", bio.BurialId);
            return View(bio);

        }

        // GET: BioSampleCrud/Edit/5
        public async Task<IActionResult> Edit(string id, string initials, string notes)
        {
            string newid = id.Replace("%2F", "/");
            string newNotes = notes.Replace("%2F", " ");
            if (newid == null)
            {
                return NotFound();
            }

            var biologicalSample = _context.BiologicalSamples
                .Where(x => x.BurialId == newid)
                .Where(n => n.Notes == notes)
                .Where(i => i.Initials == initials);

            if (biologicalSample == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> Delete(string id, string initials, string notes)
        {
            string newid = id.Replace("%2F", "/");
            if (newid == null)
            {
                return NotFound();
            }

            var biologicalSample = await _context.BiologicalSamples
                .Include(b => b.Burial)
                .Where(n => n.Notes == notes)
                .Where(i => i.Initials == initials)
                .FirstOrDefaultAsync(m => m.BurialId == newid);
                
            if (biologicalSample == null)
            {
                return NotFound();
            }

            return View(biologicalSample);
        }

        // POST: BioSampleCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string burialId, string Notes, string Initials )
        {
            var biologicalSample = await _context.BiologicalSamples
                .Where(b => b.BurialId == burialId)
                .Where(n => n.Notes == Notes)
                .Where(i => i.Initials == Initials)
                .FirstOrDefaultAsync();

            _context.BiologicalSamples.Remove(biologicalSample);
            await _context.SaveChangesAsync();
            return View("RecordSpecificIndex", new BioSampleViewModel()
            {
                biologicalSamples = _context.BiologicalSamples
                .Where(x => x.BurialId == burialId),

                burial = _context.Burials
                .Where(x => x.BurialId == burialId).FirstOrDefault()
            });
        }

        private bool BiologicalSampleExists(int id)
        {
            return _context.BiologicalSamples.Any(e => e.Id == id);
        }
    }
}

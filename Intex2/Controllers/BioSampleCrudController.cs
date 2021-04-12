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
        public IActionResult CustomCreate([Bind("BurialId,Rack,F3,Bag,LowNs,HighNs,NorthOrSouth,LowEw,HighEw,EastOrWest,Area,BurialNumber,ClusterNumber,Date,PreviouslySampled,Notes,Initials,Id")] BiologicalSample bio)
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
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            BiologicalSample biologicalSample = _context.BiologicalSamples
                .Where(x => x.Id == id).FirstOrDefault();

            if (biologicalSample == null)
            {
                return NotFound();
            }

            return View(biologicalSample);
        }
        //POST
        [HttpPost]
        public IActionResult CustomEdit(BiologicalSample bio)
        {
            if (ModelState.IsValid)
            {
                _context.Update(bio);
                _context.SaveChanges();

                return View("RecordSpecificIndex", new BioSampleViewModel
                {
                    biologicalSamples = _context.BiologicalSamples.Where(x => x.BurialId == bio.BurialId),

                    burial = _context.Burials.Where(x => x.BurialId == bio.BurialId).FirstOrDefault(),

                    bioSample = bio
                });
            }
            return View(bio);
        }

        // GET: BioSampleCrud/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var biologicalSample = _context.BiologicalSamples
                .Include(b => b.Burial)
                .Where(x => x.Id == id).FirstOrDefault();
                
            if (biologicalSample == null)
            {
                return NotFound();
            }
            ViewBag.id = id;

            return View(biologicalSample);
        }

        // POST: BioSampleCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var biologicalSample = _context.BiologicalSamples
                .Where(x => x.Id == id).FirstOrDefault();

            _context.BiologicalSamples.Remove(biologicalSample);
            await _context.SaveChangesAsync();
            return View("RecordSpecificIndex", new BioSampleViewModel()
            {
                biologicalSamples = _context.BiologicalSamples
                .Where(x => x.BurialId == biologicalSample.BurialId),

                burial = _context.Burials
                .Where(x => x.BurialId == biologicalSample.BurialId).FirstOrDefault()
            });
        }

        private bool BiologicalSampleExists(int id)
        {
            return _context.BiologicalSamples.Any(e => e.Id == id);
        }
    }
}

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
        // Returns all items for specific burial Id
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

        // Scaffold index, unused
        public async Task<IActionResult> Index()
        {
            var fagElGamousContext = _context.BiologicalSamples.Include(b => b.Burial);
            return View(await fagElGamousContext.ToListAsync());
        }


        // GET: BioSampleCrud/Create
        // Returns a create form for Bio Sample
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        // Create a new BioSample for a specific Burial Id
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
        // Return a form to edit Bio sample
        public IActionResult Edit(int id)
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
        // Submit form to update Bio sample (edit is a keywork in ASP.net, functionality gets weirdddd)
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
        // Return a delete view for a bio sample related to a specific ID
        public IActionResult Delete(int id)
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

        // Delete a specific bio sample related to a burial id
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Intex2.Models;
using Intex2.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Intex2.Controllers
{
    [Authorize]
    public class BioSampleCrudController : Controller
    {
        private readonly FagElGamousContext _context;
        public int pageNum { get; set; } = 1;

        public BioSampleCrudController(FagElGamousContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult FullTableDisplay(int pageNum = 1)
        {
            int pageSize = 20;
            var fagElGamousContext  = _context.BiologicalSamples;



            //var y = (IEnumerable<string>)ctx.Burials.BurialId.ToList();

            return View(new BioSampleViewModel
            {
                biologicalSamples = _context.BiologicalSamples.OrderBy(b => b.BurialId)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    //FOR THE PRESENTATION TO PRESENT CLEAN DATA .Where(x => x.BurialSouthToFeet != null)
                    .ToList(),

                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    TotalNumItems = _context.BiologicalSamples
                    //FOR THE PRESENTATION TO PRESENT CLEAN DATA .Where(x=> x.BurialSouthToFeet != null)
                    .Count()
                },
            });
        }

        // GET: BioSampleCrud
        [AllowAnonymous]
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
        // Returns a create form for Bio Sample
        [Authorize(Roles = "Admins,Researcher")]
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
        [Authorize(Roles = "Admins,Researcher")]
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
        [Authorize(Roles = "Admins,Researcher")]
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
        [Authorize(Roles = "Admins,Researcher")]
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
        [Authorize(Roles = "Admins")]
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
        [Authorize(Roles = "Admins")]
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

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
    public class CarbonDatingCrudController : Controller
    {
        private readonly FagElGamousContext _context;

        public CarbonDatingCrudController(FagElGamousContext context)
        {
            _context = context;
        }

        // GET: BioSampleCrud
        public IActionResult RecordDetails(string id)
        {

            string newid = id.Replace("%2F", "/");
            if(newid == null)
            {
                return NotFound();
            }
            Burial burial = _context.Burials.Where(x => x.BurialId == newid).FirstOrDefault();

            var carbDateSamples = _context.C14s.Where(x => x.BurialId == burial.BurialId);


            return View(new CarbonDatingViewModel()
            {
                carbDateSamples = carbDateSamples,
                burial = burial
            });   
        }

        // GET: BioSampleCrud/Create
        public IActionResult Create(string id)
        {

            string newid = id.Replace("%2F", "/");


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
        public IActionResult Create( C14 carbDateSample)
        {
            if (ModelState.IsValid)
            {
              
                _context.Add(carbDateSample);
                _context.SaveChanges();

                return View("RecordDetails", new CarbonDatingViewModel
                {
                    carbDateSamples = _context.C14s.Where(x => x.BurialId == carbDateSample.BurialId),

                    burial = _context.Burials.Where(x => x.BurialId == carbDateSample.BurialId).FirstOrDefault(),

                    carbDateSample = carbDateSample
                });
            }

            return View(carbDateSample);

        }

        // GET: BioSampleCrud/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {

            C14 carbDateSample = _context.C14s
                .Where(x => x.Id == id).FirstOrDefault();

            if (carbDateSample == null)
            {
                return NotFound();
            }

            return View(carbDateSample);
        }
        //POST
        [HttpPost]
        public IActionResult Edit(C14 carbDateSample)
        {
            if (ModelState.IsValid)
            {
                _context.Update(carbDateSample);
                _context.SaveChanges();

                return View("RecordSpecificIndex", new CarbonDatingViewModel
                {
                    carbDateSamples = _context.C14s.Where(x => x.BurialId == carbDateSample.BurialId),

                    burial = _context.Burials.Where(x => x.BurialId == carbDateSample.BurialId).FirstOrDefault(),

                    carbDateSample = carbDateSample
                });
            }
            return View(carbDateSample);
        }

        // GET: BioSampleCrud/Delete/5
        public IActionResult Delete(int id)
        {
            var carbDateSample = _context.C14s
                .Include(b => b.Burial)
                .Where(x => x.Id == id).FirstOrDefault();
                
            if (carbDateSample == null)
            {
                return NotFound();
            }
            ViewBag.id = id;

            return View(carbDateSample);
        }

        // POST: BioSampleCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carbDateSample = _context.C14s
                .Where(x => x.Id == id).FirstOrDefault();

            _context.C14s.Remove(carbDateSample);
            await _context.SaveChangesAsync();
            return View("RecordDetails", new CarbonDatingViewModel()
            {
                carbDateSamples = _context.C14s
                .Where(x => x.BurialId == carbDateSample.BurialId),

                burial = _context.Burials
                .Where(x => x.BurialId == carbDateSample.BurialId).FirstOrDefault()
            });
        }

        private bool CarbDateSampleExists(int id)
        {
            return _context.C14s.Any(e => e.Id == id);
        }
    }
}

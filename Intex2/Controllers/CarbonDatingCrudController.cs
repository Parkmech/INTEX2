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
    public class CarbonDatingCrudController : Controller
    {
        private readonly FagElGamousContext _context;

        public CarbonDatingCrudController(FagElGamousContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult FullTableDisplay()
        {
            var fagElGamousContext = _context.C14s.Skip(4);

            return View(fagElGamousContext.ToList());
        }

        // GET: CarbonDatingCrud
        // Returns all carbon records associated with a specific burial id
        [AllowAnonymous]
        public IActionResult RecordDetails(string id)
        {

            string newid = id.Replace("%2F", "/");
            if (newid == null)
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

        // GET: CarbonDatingCrud/Create
        // Return a view to create a new carbon dating
        [Authorize(Roles = "Admins,Researcher")]
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
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins,Researcher")]
        // Create new carbon dating -- create is keyword with strange functions
        public IActionResult CustomCreate(C14 carbDateSample)
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

        // GET: CarbonDatingCrud/Edit/5
        [HttpGet]
        [Authorize(Roles = "Admins,Researcher")]
        // Return a view to edit a carbon dating sample
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
        [Authorize(Roles = "Admins")]
        // Edit value of carbon dating sample in database
        public IActionResult CustomEdit(C14 carbDateSample)
        {
            if (ModelState.IsValid)
            {
                _context.Update(carbDateSample);
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

        // GET: CarbonDatingCrud/Delete/5
        // return view to delete a specific carbon sample
        [Authorize(Roles = "Admins")]
        public IActionResult Delete(int id)
        {
            var carbDateSample = _context.C14s
                .Where(x => x.Id == id).FirstOrDefault();
                
            if (carbDateSample == null)
            {
                return NotFound();
            }
            ViewBag.id = id;

            return View(carbDateSample);
        }

        // POST: CarbonDatingCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        // Delete a specific carbon sample record
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

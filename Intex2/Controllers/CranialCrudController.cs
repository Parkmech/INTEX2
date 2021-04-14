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
    public class CranialCrudController : Controller
    {
        private readonly FagElGamousContext _context;

        public CranialCrudController(FagElGamousContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult FullTableDisplay()
        {
            var fagElGamousContext = _context.Cranials;

            return View(fagElGamousContext.ToList());
        }


        // GET: FieldNotesCrud/RecordDetails
        // Return details for a cranial records
        [AllowAnonymous]
        public IActionResult RecordDetails(string id)
        {
            string newid = id.Replace("%2F", "/");
            if(newid == null)
            {
                return NotFound();
            }

            Burial burial = _context.Burials.Where(x => x.BurialId == newid).FirstOrDefault();

            Cranial cranialSample = _context.Cranials.Where(x => x.BurialId == burial.BurialId).FirstOrDefault();

            IEnumerable<Cranial> cranials = _context.Cranials.Where(x => x.BurialId == burial.BurialId);


            return View(new CranialViewModel()
            {
                cranialSamples = cranials,
                cranialSample = cranialSample,
                burial = burial
            }) ;   
        }

        // GET: FieldNotesCrud/Create
        // Return a view that can create data
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
        // Create a new cranial record
        [Authorize(Roles = "Admins,Researcher")]
        public IActionResult CustomCreate(Cranial cranialSample)
        {
            if (_context.Cranials.Where(x => x.BurialId == cranialSample.BurialId).FirstOrDefault() != null)
            {
                return View("No");
            }

            if (ModelState.IsValid)
            {
              
                _context.Add(cranialSample);
                _context.SaveChanges();

                return View("RecordDetails", new CranialViewModel
                {
                    cranialSamples = _context.Cranials.Where(x => x.BurialId == cranialSample.BurialId),

                    burial = _context.Burials.Where(x => x.BurialId == cranialSample.BurialId).FirstOrDefault(),

                    cranialSample = cranialSample
                });
            }

            return View(cranialSample);

        }

        // GET: FieldNotesCrud/Edit/5
        [HttpGet]
        [Authorize(Roles = "Admins,Researcher")]
        //return view to edit cranial data
        public IActionResult Edit(int id)
        {

            Cranial cranialSample = _context.Cranials
                .Where(x => x.Id == id).FirstOrDefault();

            if (cranialSample == null)
            {
                return NotFound();
            }

            return View(cranialSample);
        }
        //POST
        [HttpPost]
        [Authorize(Roles = "Admins,Researcher")]
        // Edit cranial data in database
        public IActionResult CustomEdit(Cranial cranialSample)
        {
            if (ModelState.IsValid)
            {
                _context.Update(cranialSample);
                _context.SaveChanges();

                return View("RecordDetails", new CranialViewModel
                {
                    cranialSamples = _context.Cranials.Where(x => x.BurialId == cranialSample.BurialId),

                    burial = _context.Burials.Where(x => x.BurialId == cranialSample.BurialId).FirstOrDefault(),

                    cranialSample = cranialSample
                });
            }
            return View(cranialSample);
        }

        // GET: FieldNotesCrud/Delete/5
        // Return a view to confirm delete of cranial data
        [Authorize(Roles = "Admins")]
        public IActionResult Delete(int id)
        {
            var cranialSample = _context.Cranials
                .Where(x => x.Id == id).FirstOrDefault();
                
            if (cranialSample == null)
            {
                return NotFound();
            }
            ViewBag.id = id;

            return View(cranialSample);
        }

        // POST: FieldNotesCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        // Delete speific cranial data
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            string newid = id.Replace("%2F", "/");

            if (newid == null)
            {
                return NotFound();
            }

            var cranialSample = _context.Cranials
                .Where(x => x.BurialId == newid).FirstOrDefault();

            _context.Cranials.Remove(cranialSample);
            await _context.SaveChangesAsync();
            return View("RecordDetails", new CranialViewModel()
            {
                cranialSamples = _context.Cranials
                .Where(x => x.BurialId == cranialSample.BurialId),

                burial = _context.Burials
                .Where(x => x.BurialId == cranialSample.BurialId).FirstOrDefault()
            });
        }

        // Delete a cranial record
        [Authorize(Roles = "Admins")]
        public IActionResult CustomDelete(int id)
        {

            Cranial cranial = _context.Cranials.Where(x => x.Id == id).FirstOrDefault();

            _context.Cranials.Remove(cranial);
            _context.SaveChanges();

            return View("RecordDetails", new CranialViewModel()
            {
                cranialSamples = _context.Cranials
                .Where(x => x.BurialId == cranial.BurialId),

                burial = _context.Burials
                .Where(x => x.BurialId == cranial.BurialId).FirstOrDefault()
            });
        }

        private bool CranialExists(int id)
        {
            return _context.Cranials.Any(e => e.Id == id);
        }
    }
}

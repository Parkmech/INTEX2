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
    public class PhotosCrudController : Controller
    {
        private readonly FagElGamousContext _context;

        public PhotosCrudController(FagElGamousContext context)
        {
            _context = context;
        }

        // GET: PhotosCrud
        //public async Task<IActionResult> Index()
        //{
        //    var fagElGamousContext = _context.Photos.Include(p => p.Burial);
        //    return View(await fagElGamousContext.ToListAsync());
        //}

        // GET: PhotosCrud/Details/5
        public async Task<IActionResult> Details(string id)
        {
            string newid = id.Replace("%2F", "/");
            if (newid == null)
            {
                return NotFound();
            }

            Burial newBurial = _context.Burials.Where(x => x.BurialId == newid).FirstOrDefault();

            var photos = _context.Photos.Where(x => x.BurialId == newBurial.BurialId);
            if (photos == null)
            {
                return NotFound();
            }

            return View(new PhotosViewModel()
            {
                Photos = photos,
                Burial = newBurial
            });
        }

        ////[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admins")]
        //// GET: PhotosCrud/Create
        //public IActionResult Create()
        //{
        //    ViewData["BurialId"] = new SelectList(_context.Burials, "BurialId", "BurialId");
        //    return View();
        //}

        //// POST: PhotosCrud/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("BurialId,PhotoId,Id")] Photo photo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(photo);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["BurialId"] = new SelectList(_context.Burials, "BurialId", "BurialId", photo.BurialId);
        //    return View(photo);
        //}

        // GET: PhotosCrud/Delete/5
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photos
                .Include(p => p.Burial)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // POST: PhotosCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //NEED TO FIX THE DELETE CONFIRMED BUTTON DOWN BELOW, SO IT TAKES YOU BACK TO DETAILS APPROPRIATELY
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photo = await _context.Photos.FindAsync(id);

            string newid = photo.BurialId;

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();


            return View("Details", new PhotosViewModel()
            {
                Photos = _context.Photos
                .Where(x => x.BurialId == photo.BurialId),

                Burial = _context.Burials
                .Where(x => x.BurialId == photo.BurialId).FirstOrDefault()
            });
        }

        private bool PhotoExists(int id)
        {
            return _context.Photos.Any(e => e.Id == id);
        }
    }
}

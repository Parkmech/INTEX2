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


        // GET: PhotosCrud/Details/5
        // Returns all photos for a specific burial id
        public IActionResult Details(string id)
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

        // GET: PhotosCrud/Delete/5
        //[ValidateAntiForgeryToken]
        // Delete a photo
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

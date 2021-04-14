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
    public class FieldNotesCrudController : Controller
    {
        private readonly FagElGamousContext _context;

        public FieldNotesCrudController(FagElGamousContext context)
        {
            _context = context;
        }

        public IActionResult FullTableDisplay(int pageNum = 1)
        {
            int pageSize = 20;

            return View(new FieldNotesViewModel
            {
                fieldNotes = (_context.FieldBook
                    .OrderBy(b => b.BurialId)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    //FOR THE PRESENTATION TO PRESENT CLEAN DATA .Where(x => x.BurialSouthToFeet != null)
                    .ToList()
                    ),

                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    TotalNumItems = _context.FieldBook
                    //FOR THE PRESENTATION TO PRESENT CLEAN DATA .Where(x=> x.BurialSouthToFeet != null)
                    .Count()
                },

            });
        }


        public IActionResult SingleRecord()
        {
            FieldBook Fieldnote = _context.FieldBook.FirstOrDefault();

            return View(Fieldnote);
        }

        // GET: FieldNotesCrud/RecordDetails
        public IActionResult RecordDetails(string id)
        {
            string newid = id.Replace("%2F", "/");
            if (newid == null)
            {
                return NotFound();
            }
            Burial burial = _context.Burials.Where(x => x.BurialId == newid).FirstOrDefault();

            var fieldNotes = _context.FieldBook.Where(x => x.BurialId == burial.BurialId);


            return View(new FieldNotesViewModel()
            {
                fieldNotes = fieldNotes,
                burial = burial
            });
        }

        // GET: FieldNotesCrud/Create
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

        // POST: FieldNotesCrud/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CustomCreate(FieldBook fieldNote)
        {
            if (ModelState.IsValid)
            {

                _context.Add(fieldNote);
                _context.SaveChanges();

                return View("RecordDetails", new FieldNotesViewModel
                {
                    fieldNotes = _context.FieldBook.Where(x => x.BurialId == fieldNote.BurialId),

                    burial = _context.Burials.Where(x => x.BurialId == fieldNote.BurialId).FirstOrDefault(),

                    fieldNote = fieldNote
                });
            }

            return View(fieldNote);

        }

        // GET: FieldNotesCrud/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {

            FieldBook fieldNote = _context.FieldBook
                .Where(x => x.Id == id).FirstOrDefault();

            if (fieldNote == null)
            {
                return NotFound();
            }

            return View(fieldNote);
        }
        //POST
        [HttpPost]
        public IActionResult CustomEdit(FieldBook fieldNote)
        {
            if (ModelState.IsValid)
            {
                _context.Update(fieldNote);
                _context.SaveChanges();

                return View("RecordDetails", new FieldNotesViewModel
                {
                    fieldNotes = _context.FieldBook.Where(x => x.BurialId == fieldNote.BurialId),

                    burial = _context.Burials.Where(x => x.BurialId == fieldNote.BurialId).FirstOrDefault(),

                    fieldNote = fieldNote
                });
            }
            return View(fieldNote);
        }

        // GET: FieldNotesCrud/Delete/5
        public IActionResult Delete(int id)
        {
            var fieldNote = _context.FieldBook
                .Include(b => b.Burial)
                .Where(x => x.Id == id).FirstOrDefault();

            if (fieldNote == null)
            {
                return NotFound();
            }
            ViewBag.id = id;

            return View(fieldNote);
        }

        // POST: FieldNotesCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fieldNote = _context.FieldBook
                .Where(x => x.Id == id).FirstOrDefault();

            _context.FieldBook.Remove(fieldNote);
            await _context.SaveChangesAsync();
            return View("RecordDetails", new FieldNotesViewModel()
            {
                fieldNotes = _context.FieldBook
                .Where(x => x.BurialId == fieldNote.BurialId),

                burial = _context.Burials
                .Where(x => x.BurialId == fieldNote.BurialId).FirstOrDefault()
            });
        }

        private bool FieldNoteExists(int id)
        {
            return _context.FieldBook.Any(e => e.Id == id);
        }
    }
}

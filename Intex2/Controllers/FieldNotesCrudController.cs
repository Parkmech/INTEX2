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

        // Return a single record from the field notes
        public IActionResult SingleRecord()
        {
            FieldBook Fieldnote = _context.FieldBook.FirstOrDefault();

            return View(Fieldnote);
        }

        // GET: FieldNotesCrud/RecordDetails
        // Return all fieldnotes for a specific burial id
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
        // Return a view to create new fieldnote refeerence
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
        // Create a new field notes record
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
        // Return a view to edit a fieldnote entry
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
        // Edit a field note
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
        // Return a view to delete fieldnote reference
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
        // Delete a record of fieldnotes
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

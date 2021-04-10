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
    public class BurialCrudController : Controller
    {
        private readonly FagElGamousContext _context;
        public int pageNum { get; set; } = 1;

        public BurialCrudController(FagElGamousContext context)
        {
            _context = context;
        }

        // GET: BurialCrud
        public IActionResult Index(int pageNum = 2)
        {
            int pageSize = 20;

            return View(new BurialListViewModel
            {
                Burials = (_context.Burials
                    .OrderBy(b => b.BurialId)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    //FOR THE PRESENTATION TO PRESENT CLEAN DATA .Where(x => x.BurialSouthToFeet != null)
                    .ToList()),

                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    TotalNumItems = _context.Burials
                    //FOR THE PRESENTATION TO PRESENT CLEAN DATA .Where(x=> x.BurialSouthToFeet != null)
                    .Count()
                },

            });
        }

        // GET: BurialCrud/Details/5
        public async Task<IActionResult> Details(string id)
        {
            string newid = id.Replace("%2F", "/");
            if (newid == null)
            {
                return NotFound();
            }

            var burials = await _context.Burials
                .Include(b => b.AgeCodeSingleNavigation)
                .Include(b => b.BurialAdultChildNavigation)
                .Include(b => b.BurialWrappingNavigation)
                .FirstOrDefaultAsync(m => m.BurialId == newid);
            if (burials == null)
            {
                return NotFound();
            }

            return View(burials);
        }

        [Authorize(Roles = "Admins")]
        // GET: BurialCrud/Create
        public IActionResult Create()
        {
            ViewData["AgeCodeSingle"] = new SelectList(_context.AgeCodes, "AgeCode1", "AgeCode1");
            ViewData["BurialAdultChild"] = new SelectList(_context.BurialAdultChildren, "BurialAdultChild1", "BurialAdultChild1");
            ViewData["BurialWrapping"] = new SelectList(_context.BurialWrappings, "BurialWrapping1", "BurialWrapping1");
            return View();
        }

        // POST: BurialCrud/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Create([Bind("BurialId,BurialId2018,YearOnSkull,MonthOnSkull,DateOnSkull,InitialsOfDataEntryExpert,InitialsOfDataEntryChecker,ByuSample,BodyAnalysis,SkullAtMagazine,PostcraniaAtMagazine,AgeSkull2018,RackAndShelf,ToBeConfirmed,SkullTrauma,PostcraniaTrauma,CribraOrbitala,PoroticHyperostosis,PoroticHyperostosisLocations,MetopicSuture,ButtonOsteoma,PostcraniaTrauma1,OsteologyUnknownComment,TemporalMandibularJointOsteoarthritisTmjOa,LinearHypoplasiaEnamel,AreaHillBurials,Tomb,NsLowPosition,NsHighPosition,NorthOrSouth,EwLowPosition,EwHighPosition,EastOrWest,Square,BurialNumber,BurialWestToHead,BurialWestToFeet,BurialSouthToHead,BurialSouthToFeet,BurialDepth,YearExcav,MonthExcavated,DateExcavated,BurialDirection,BurialPreservation,BurialWrapping,BurialAdultChild,Sex,GenderCode,BurialGenderMethod,AgeCodeSingle,BurialDirection1,NumericMinAge,NumericMaxAge,BurialAgeMethod,HairColorCode,BurialSampleTaken,LengthM,LengthCm,Goods,Cluster,FaceBundle,OsteologyNotes,OtherNotes,SampleNumber,GenderGe,GeFunctionTotal,GenderBodyCol,BasilarSuture,VentralArc,SubpubicAngle,SciaticNotch,PubicBone,PreaurSulcus,MedialIpRamus,DorsalPitting,ForemanMagnum,FemurHead,HumerusHead,Osteophytosis,PubicSymphysis,BoneLength,MedialClavicle,IliacCrest,FemurDiameter,Humerus,FemurLength,HumerusLength,TibiaLength,Robust,SupraorbitalRidges,OrbitEdge,ParietalBossing,Gonian,NuchalCrest,ZygomaticCrest,CranialSuture,MaximumCranialLength,MaximumCranialBreadth,BasionBregmaHeight,BasionNasion,BasionProsthionLength,BizygomaticDiameter,NasionProsthion,MaximumNasalBreadth,InterorbitalBreadth,ArtifactsDescription,PreservationIndex,HairTaken,SoftTissueTaken,BoneTaken,ToothTaken,TextileTaken,DescriptionOfTaken,ArtifactFound,EstimateLivingStature,ToothAttrition,ToothEruption,PathologyAnomalies,EpiphysealUnion,SsmaTimeStamp")] Burial burials)
        {
            if (ModelState.IsValid)
            {
                _context.Add(burials);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgeCodeSingle"] = new SelectList(_context.AgeCodes, "AgeCode1", "AgeCode1", burials.AgeCodeSingle);
            ViewData["BurialAdultChild"] = new SelectList(_context.BurialAdultChildren, "BurialAdultChild1", "BurialAdultChild1", burials.BurialAdultChild);
            ViewData["BurialWrapping"] = new SelectList(_context.BurialWrappings, "BurialWrapping1", "BurialWrapping1", burials.BurialWrapping);
            return View(burials);
        }

        // GET: BurialCrud/Edit/5
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Edit(string id)
        {
            string newid = id.Replace("%2F", "/");

            if (newid == null)
            {
                return NotFound();
            }

            var burials = await _context.Burials.FindAsync(newid);
            if (burials == null)
            {
                return NotFound();
            }
            ViewData["AgeCodeSingle"] = new SelectList(_context.AgeCodes, "AgeCode1", "AgeCode1", burials.AgeCodeSingle);
            ViewData["BurialAdultChild"] = new SelectList(_context.BurialAdultChildren, "BurialAdultChild1", "BurialAdultChild1", burials.BurialAdultChild);
            ViewData["BurialWrapping"] = new SelectList(_context.BurialWrappings, "BurialWrapping1", "BurialWrapping1", burials.BurialWrapping);
            return View(burials);
        }

        // POST: BurialCrud/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Edit(string id, [Bind("BurialId,BurialId2018,YearOnSkull,MonthOnSkull,DateOnSkull,InitialsOfDataEntryExpert,InitialsOfDataEntryChecker,ByuSample,BodyAnalysis,SkullAtMagazine,PostcraniaAtMagazine,AgeSkull2018,RackAndShelf,ToBeConfirmed,SkullTrauma,PostcraniaTrauma,CribraOrbitala,PoroticHyperostosis,PoroticHyperostosisLocations,MetopicSuture,ButtonOsteoma,PostcraniaTrauma1,OsteologyUnknownComment,TemporalMandibularJointOsteoarthritisTmjOa,LinearHypoplasiaEnamel,AreaHillBurials,Tomb,NsLowPosition,NsHighPosition,NorthOrSouth,EwLowPosition,EwHighPosition,EastOrWest,Square,BurialNumber,BurialWestToHead,BurialWestToFeet,BurialSouthToHead,BurialSouthToFeet,BurialDepth,YearExcav,MonthExcavated,DateExcavated,BurialDirection,BurialPreservation,BurialWrapping,BurialAdultChild,Sex,GenderCode,BurialGenderMethod,AgeCodeSingle,BurialDirection1,NumericMinAge,NumericMaxAge,BurialAgeMethod,HairColorCode,BurialSampleTaken,LengthM,LengthCm,Goods,Cluster,FaceBundle,OsteologyNotes,OtherNotes,SampleNumber,GenderGe,GeFunctionTotal,GenderBodyCol,BasilarSuture,VentralArc,SubpubicAngle,SciaticNotch,PubicBone,PreaurSulcus,MedialIpRamus,DorsalPitting,ForemanMagnum,FemurHead,HumerusHead,Osteophytosis,PubicSymphysis,BoneLength,MedialClavicle,IliacCrest,FemurDiameter,Humerus,FemurLength,HumerusLength,TibiaLength,Robust,SupraorbitalRidges,OrbitEdge,ParietalBossing,Gonian,NuchalCrest,ZygomaticCrest,CranialSuture,MaximumCranialLength,MaximumCranialBreadth,BasionBregmaHeight,BasionNasion,BasionProsthionLength,BizygomaticDiameter,NasionProsthion,MaximumNasalBreadth,InterorbitalBreadth,ArtifactsDescription,PreservationIndex,HairTaken,SoftTissueTaken,BoneTaken,ToothTaken,TextileTaken,DescriptionOfTaken,ArtifactFound,EstimateLivingStature,ToothAttrition,ToothEruption,PathologyAnomalies,EpiphysealUnion,SsmaTimeStamp,PhotoTaken")] Burial burials)
        {
            if (id != burials.BurialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(burials);
                    await _context.SaveChangesAsync();
                }
                    catch (DbUpdateConcurrencyException)
                {
                    if (!BurialsExists(burials.BurialId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgeCodeSingle"] = new SelectList(_context.AgeCodes, "AgeCode1", "AgeCode1", burials.AgeCodeSingle);
            ViewData["BurialAdultChild"] = new SelectList(_context.BurialAdultChildren, "BurialAdultChild1", "BurialAdultChild1", burials.BurialAdultChild);
            ViewData["BurialWrapping"] = new SelectList(_context.BurialWrappings, "BurialWrapping1", "BurialWrapping1", burials.BurialWrapping);
            return View(burials);
        }

        // GET: BurialCrud/Delete/5
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burials = await _context.Burials
                .Include(b => b.AgeCodeSingleNavigation)
                .Include(b => b.BurialAdultChildNavigation)
                .Include(b => b.BurialWrappingNavigation)
                .FirstOrDefaultAsync(m => m.BurialId == id);
            if (burials == null)
            {
                return NotFound();
            }

            return View(burials);
        }

        // POST: BurialCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admins")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var burials = await _context.Burials.FindAsync(id);
            _context.Burials.Remove(burials);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BurialsExists(string id)
        {
            return _context.Burials.Any(e => e.BurialId == id);
        }
    }
}

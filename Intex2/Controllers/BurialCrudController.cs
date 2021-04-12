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
using Microsoft.AspNetCore.Mvc.Filters;

namespace Intex2.Controllers
{
    public class BurialCrudController : Controller
    {
        private readonly FagElGamousContext _context;
        private readonly FagElGamousContext _contextFiltered;
        public int pageNum { get; set; } = 1;

        public string sex { get; set; }

        public BurialCrudController(FagElGamousContext context)
        {
            _context = context;
            _contextFiltered = _context;
        }

        [HttpGet]
        // GET: BurialCrud
        public IActionResult Index(int pageNum = 6)
        {
            int pageSize = 20;

            return View(new BurialListViewModel
            {
                Burials = (_context.Burials
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
                    TotalNumItems = _context.Burials
                    //FOR THE PRESENTATION TO PRESENT CLEAN DATA .Where(x=> x.BurialSouthToFeet != null)
                    .Count()
                },
                Photos = _context.Photos

            }) ;
        }


        [HttpPost]
        public IActionResult Index(BurialListViewModel filterAtr)
        {
            string sex = filterAtr.FilterItems.Sex;
            string area = filterAtr.FilterItems.Area;
            double length = filterAtr.FilterItems.Length;
            double depth = filterAtr.FilterItems.Depth;
            string bdirection = filterAtr.FilterItems.BDirection;
            string nors = filterAtr.FilterItems.NorS;
            string eorw = filterAtr.FilterItems.EorW;
            


            FilterItems filtered = new FilterItems
            {
                Sex = sex,
                Area = area,
                Length = length,
                Depth = depth,
                BDirection = bdirection,
                NorS = nors,
                EorW = eorw
            };

            return RedirectToAction("Filtered", filtered);
        }

        [HttpGet]
        public IActionResult PagedIndex(BurialListViewModel blvm, int pagenum = 1)
        {
            return View("Index", blvm);
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
            burials.BurialId = burials.NorthOrSouth + " " + burials.NsLowPosition + "/" +
                         burials.NsHighPosition + " " + burials.EastOrWest + " " +
                         burials.EwLowPosition + "/" + burials.EwHighPosition + " " +
                         burials.Square + " #" + burials.BurialNumber;


            if (ModelState.IsValid)
            {
                _context.Add(burials);
                await _context.SaveChangesAsync();
                return View("Details", burials);
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

        // POST: BurialCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admins")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            string newid = id.Replace("%2F", "/");

            if (newid == null)
            {
                return NotFound();
            }

            var burials = await _context.Burials.FindAsync(newid);
            _context.Burials.Remove(burials);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Display(string sortOrder, string searchString)
        {
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "BurialId_Desc" : "";
            ViewData["DateSortParm"] = sortOrder == "date_desc" ? "Date" : "date_desc";
            ViewData["CurrentFilter"] = searchString;
            var mummies = from s in _context.Burials select s;
            //var mummies = from s in _context.Burials.IsRequired(false) select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                mummies = mummies.Where(s => s.BurialId.Contains(searchString)
                                       || s.Sex.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "BurialId_Desc":
                    mummies = mummies.OrderByDescending(s => s.BurialId);
                    break;
                case "Date":
                    mummies = mummies.OrderBy(s => s.DateExcavated);
                    break;
                case "date_desc":
                    mummies = mummies.OrderByDescending(s => s.DateExcavated);
                    break;
                default:
                    mummies = mummies.OrderBy(s => s.BurialId);
                    break;
            }
            return View(await mummies.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Search(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var mummies = from s in _context.Burials select s;
  

            if (!String.IsNullOrEmpty(searchString))
            {
                mummies = mummies.Where(s => s.BurialId.Contains(searchString)
                                       || s.Sex.Contains(searchString));
            }

            
            return View("Index", await mummies.AsNoTracking().ToListAsync());
        }

        [HttpPost]
        public IActionResult Filtered(BurialListViewModel filterAtr)
        {
            string sex = filterAtr.FilterItems.Sex;
            string area = filterAtr.FilterItems.Area;
            double length = filterAtr.FilterItems.Length;
            double depth = filterAtr.FilterItems.Depth;
            string bdirection = filterAtr.FilterItems.BDirection;
            string nors = filterAtr.FilterItems.NorS;
            string eorw = filterAtr.FilterItems.EorW;
            string burialid = filterAtr.FilterItems.BurialId;


            if (sex == "ALL")
            {
                sex = "%";
            }
            if (area == "ALL")
            {
                area = "%";
            }
            if (bdirection == "ALL")
            {
                bdirection = "%";
            }
            if (nors == "ALL")
            {
                nors = "%";
            }
            if (eorw == "ALL")
            {
                eorw = "%";
            }

            burialid = "%" + burialid + "%";

            return View(new BurialListViewModel
            {
                Burials = _context.Burials
                    .FromSqlInterpolated($"SELECT * FROM Burials WHERE Gender_Code LIKE {sex} AND Square LIKE {area} AND Burial_Direction LIKE {bdirection} AND North_or_South LIKE {nors} AND East_or_West LIKE {eorw} AND BurialID LIKE {burialid}")
                    .Where(b => b.LengthM >= length)
                    .Where(b => b.BurialDepth >= depth)
                    .ToList()
            });
        }

        private bool BurialsExists(string id)
        {
            return _context.Burials.Any(e => e.BurialId == id);
        }
    }
}

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
using Intex2.Services;

namespace Intex2.Controllers
{
    public class BurialCrudController : Controller
    {
        private readonly FagElGamousContext _context;
        private readonly FagElGamousContext _contextFiltered;
        private readonly IS3Service _s3Storage;

        public int pageNum { get; set; } = 1;

        public string sex { get; set; }

        public BurialCrudController(FagElGamousContext context, IS3Service s3)
        {
            _context = context;
            _contextFiltered = _context;
            _s3Storage = s3;
        }

        [HttpGet]
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
                Photos = _context.Photos,

                FieldBooks = _context.FieldBook

            });
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
        [HttpGet]
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
        [HttpPost, ActionName("DeleteConf")]
        [Authorize(Roles = "Admins")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConf(string id)
        {
            string newid = id.Replace("%2F", "/");

            if (newid == null)
            {
                return NotFound();
            }

            List<Photo> photos = new List<Photo>();
            List<BiologicalSample> bios = new List<BiologicalSample>();
            List<FieldBook> fbook = new List<FieldBook>();


            photos.AddRange(_context.Photos.Where(p => p.BurialId == id).ToList());

            for (int i = 0; i < photos.Count(); i++)
            {
                _context.Photos.Remove(photos.FirstOrDefault(p => p.Id == photos[i].Id));
                await _context.SaveChangesAsync();
            }

            bios.AddRange(_context.BiologicalSamples.Where(p => p.BurialId == id).ToList());

            for (int i = 0; i < bios.Count(); i++)
            {
                _context.BiologicalSamples.Remove(bios.FirstOrDefault(p => p.Id == bios[i].Id));
                await _context.SaveChangesAsync();
            }

            fbook.AddRange(_context.FieldBook.Where(p => p.BurialId == id).ToList());

            for (int i = 0; i < photos.Count(); i++)
            {
                _context.FieldBook.Remove(fbook.FirstOrDefault(p => p.Id == fbook[i].Id));
                await _context.SaveChangesAsync();
            }


            var cranial = await _context.Cranials.FindAsync(newid);
            _context.Cranials.Remove(cranial);
            await _context.SaveChangesAsync();

            var burials = await _context.Burials.FindAsync(newid);
            _context.Burials.Remove(burials);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Display(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "BurialId_Desc" : "";
            ViewData["SexSortParm"] = sortOrder == "sex_desc" ? "Sex" : "sex_desc";
            ViewData["LengthSortParm"] = sortOrder == "length_desc" ? "Length" : "length_desc";
            ViewData["DepthSortParm"] = sortOrder == "depth_desc" ? "Depth" : "depth_desc";
            ViewData["SquareSortParm"] = sortOrder == "square_desc" ? "Square" : "square_desc";
            ViewData["DirectionSortParm"] = sortOrder == "dir_desc" ? "Dir" : "dir_desc";
            ViewData["EorWSortParm"] = sortOrder == "EorW_desc" ? "EorW" : "EorW_desc";
            ViewData["NorSSortParm"] = sortOrder == "NorS_desc" ? "NorS" : "NorS_desc";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var mummies = from s in _context.Burials select s;
            //var mummies = from s in _context.Burials.IsRequired(false) select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                mummies = mummies.Where(s => s.BurialId.Contains(searchString) || s.DescriptionOfTaken.Contains(searchString)
                                       || s.GenderCode.Contains(searchString) || s.EastOrWest.Contains(searchString)
                                       || s.NorthOrSouth.Contains(searchString) || s.BurialDirection.Contains(searchString)
                                       || s.OsteologyNotes.Contains(searchString) || s.OtherNotes.Contains(searchString)
                                       || s.RackAndShelf.Contains(searchString) || s.BurialPreservation.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "BurialId_Desc":
                    mummies = mummies.OrderByDescending(s => s.BurialId);
                    break;
                case "Sex":
                    mummies = mummies.OrderBy(s => s.GenderCode);
                    break;
                case "sex_desc":
                    mummies = mummies.OrderByDescending(s => s.GenderCode);
                    break;
                case "Length":
                    mummies = mummies.OrderBy(s => s.LengthM);
                    break;
                case "length_desc":
                    mummies = mummies.OrderByDescending(s => s.LengthM);
                    break;
                case "Depth":
                    mummies = mummies.OrderBy(s => s.BurialDepth);
                    break;
                case "depth_desc":
                    mummies = mummies.OrderByDescending(s => s.BurialDepth);
                    break;
                case "Dir":
                    mummies = mummies.OrderBy(s => s.BurialDirection);
                    break;
                case "dir_desc":
                    mummies = mummies.OrderByDescending(s => s.BurialDirection);
                    break;
                case "Square":
                    mummies = mummies.OrderBy(s => s.Square);
                    break;
                case "square_desc":
                    mummies = mummies.OrderByDescending(s => s.Square);
                    break;
                case "EorW":
                    mummies = mummies.OrderBy(s => s.EastOrWest);
                    break;
                case "EorW_desc":
                    mummies = mummies.OrderByDescending(s => s.EastOrWest);
                    break;
                case "NorS":
                    mummies = mummies.OrderBy(s => s.NorthOrSouth);
                    break;
                case "NorS_desc":
                    mummies = mummies.OrderByDescending(s => s.NorthOrSouth);
                    break;
                default:
                    mummies = mummies.OrderBy(s => s.BurialId);
                    break;
            }

            int pageSize = 20;
            return View(await PaginatedList<Burial>.CreateAsync(mummies.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await mummies.AsNoTracking().ToListAsync());
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

        
        public IActionResult Filtered(BurialListViewModel filterAtr, string searchString)
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



            ViewData["CurrentFilter"] = searchString;
            var mummies = from s in _context.Burials select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                mummies = mummies.Where(s => s.BurialId.Contains(searchString)
                                       || s.Sex.Contains(searchString));
            }

            return View(new BurialListViewModel
            {
                Burials = _context.Burials
                    .FromSqlInterpolated($"SELECT * FROM Burials WHERE Gender_Code LIKE {sex} AND Square LIKE {area} AND Burial_Direction LIKE {bdirection} AND North_or_South LIKE {nors} AND East_or_West LIKE {eorw} AND BurialID LIKE {burialid}")
                    .Where(b => b.LengthM >= length)
                    .Where(b => b.BurialDepth >= depth)
                    .OrderBy(b => b.BurialId)
                    .ToList()
            });
        }

        //public IActionResult Filtered(BurialListViewModel filterAtr, string searchString)
        //{
        //    string sex = filterAtr.FilterItems.Sex;
        //    string area = filterAtr.FilterItems.Area;
        //    double length = filterAtr.FilterItems.Length;
        //    double depth = filterAtr.FilterItems.Depth;
        //    string bdirection = filterAtr.FilterItems.BDirection;
        //    string nors = filterAtr.FilterItems.NorS;
        //    string eorw = filterAtr.FilterItems.EorW;
        //    string burialid = filterAtr.FilterItems.BurialId;


        //    if (sex == "ALL")
        //    {
        //        sex = "%";
        //    }
        //    if (area == "ALL")
        //    {
        //        area = "%";
        //    }
        //    if (bdirection == "ALL")
        //    {
        //        bdirection = "%";
        //    }
        //    if (nors == "ALL")
        //    {
        //        nors = "%";
        //    }
        //    if (eorw == "ALL")
        //    {
        //        eorw = "%";
        //    }


        //    burialid = "%" + burialid + "%";

        //    ViewData["CurrentFilter"] = searchString;
        //    var mummies = from s in _context.Burials select s;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        BurialListViewModel blViewModel = (new BurialListViewModel
        //        {
        //            Burials = mummies.Where(s => s.BurialId.Contains(searchString)
        //                               || s.Sex.Contains(searchString))
        //                    .OrderBy(b => b.BurialId)
        //                    .ToList()
        //        });
        //    }
        //    else
        //    {
        //        BurialListViewModel blViewModel = (new BurialListViewModel
        //        {
        //            Burials = _context.Burials
        //            .FromSqlInterpolated($"SELECT * FROM Burials WHERE Gender_Code LIKE {sex} AND Square LIKE {area} AND Burial_Direction LIKE {bdirection} AND North_or_South LIKE {nors} AND East_or_West LIKE {eorw} AND BurialID LIKE {burialid}")
        //            .Where(b => b.LengthM >= length)
        //            .Where(b => b.BurialDepth >= depth)
        //            .OrderBy(b => b.BurialId)
        //            .ToList()
        //        });
        //    }



        //    return View(blViewModel);
        //}

        public IActionResult AdvancedFiltering(BurialListViewModel filterAtr)
        {
            string sex = "%";
            string area = "%";
            double length = 0.00;
            double depth = 0.00;
            string bdirection = "%";
            string nors = "%";
            string eorw = "%";
            string burialid = "%";

            if (filterAtr.FilterItems != null)
            {
                sex = filterAtr.FilterItems.Sex;
                area = filterAtr.FilterItems.Area;
                length = filterAtr.FilterItems.Length;
                depth = filterAtr.FilterItems.Depth;
                bdirection = filterAtr.FilterItems.BDirection;
                nors = filterAtr.FilterItems.NorS;
                eorw = filterAtr.FilterItems.EorW;
                burialid = filterAtr.FilterItems.BurialId;
            }


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
                    .OrderBy(b => b.BurialId)
                    //.Skip((pageNum - 1) * pageSize)
                    //.Take(pageSize)
                    .ToList(),
                //PagingInfo = new PagingInfo
                //{
                //    ItemsPerPage = pageSize,
                //    CurrentPage = pageNum,
                //    TotalNumItems = _context.Burials
                //    .FromSqlInterpolated($"SELECT * FROM Burials WHERE Gender_Code LIKE {sex} AND Square LIKE {area} AND Burial_Direction LIKE {bdirection} AND North_or_South LIKE {nors} AND East_or_West LIKE {eorw} AND BurialID LIKE {burialid}")
                //    .Where(b => b.LengthM >= length)
                //    .Where(b => b.BurialDepth >= depth)
                //    .Count()
                //},
            });
        }

        private bool BurialsExists(string id)
        {
            return _context.Burials.Any(e => e.BurialId == id);
        }

        [Authorize(Roles = "Researcher")]
        public IActionResult UploadPhoto(string id)
        {
            string newid = id.Replace("%2F", "/");

            if (newid == null)
            {
                return NotFound();
            }

            BurialListViewModel blvm = new BurialListViewModel{
                Burials = _context.Burials.Where(x => x.BurialId == newid)
                             
        };

            //string newid = id.Replace("%2F", "/");

            //if (newid == null)
            //{
            //    return NotFound();
            //}

            //var burials = _context.Photos.FirstOrDefaultAsync(x => x.BurialId == newid);
            return View(blvm);
        }


        public async Task<IActionResult> SavePhoto(BurialListViewModel photo)
        {
            // magic happens here
            // check if model is not empty
            //Photo uploadPhoto = (Photo)photo.ImageUpload;

            var x = photo.ImageUpload;

            string id = x.BurialId;

            if (ModelState.IsValid)
            {
                // create new entity
                await _s3Storage.AddItem(photo.ImageUpload.file, "ForFun");


                Photo PhotoTable = new Photo
                {
                    BurialId = x.BurialId,
                    PhotoId = x.PhotoName,
                    Burial = _context.Burials.Where(x => x.BurialId == x.BurialId).FirstOrDefault()
                };

                Burial bur = _context.Burials.Where(x => x.BurialId == x.BurialId).FirstOrDefault();

                _context.Photos.Add(PhotoTable);
                await _context.SaveChangesAsync();

                return View("Details", bur);
            }
            else
            {
                return View("Home");
            }

        }
    }
}

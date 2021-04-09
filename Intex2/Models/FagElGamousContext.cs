using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Intex2.Models
{
    public partial class FagElGamousContext : DbContext
    {
        public FagElGamousContext()
        {
        }

        public FagElGamousContext(DbContextOptions<FagElGamousContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AgeCode> AgeCode { get; set; }
        public virtual DbSet<BiologicalSample> BiologicalSample { get; set; }
        public virtual DbSet<BurialAdultChild> BurialAdultChild { get; set; }
        public virtual DbSet<BurialWrapping> BurialWrapping { get; set; }
        public virtual DbSet<Burials> Burials { get; set; }
        public virtual DbSet<C14> C14 { get; set; }
        public virtual DbSet<Cluster> Cluster { get; set; }
        public virtual DbSet<Cranial> Cranial { get; set; }
        public virtual DbSet<FieldBook> FieldBook { get; set; }
        public virtual DbSet<GenderCode> GenderCode { get; set; }
        public virtual DbSet<HairCode> HairCode { get; set; }
        public virtual DbSet<OracleGis> OracleGis { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=aa16xgpayfb2xja.c1okjvee6ouq.us-east-1.rds.amazonaws.com,1433;Database=FagElGamous;User=admin;Password=adminadmin; MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgeCode>(entity =>
            {
                entity.HasKey(e => e.AgeCode1)
                    .HasName("AgeCode$PrimaryKey");

                entity.Property(e => e.AgeCode1)
                    .HasColumnName("Age Code")
                    .HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<BiologicalSample>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bag)
                    .HasColumnName("Bag #")
                    .HasMaxLength(255);

                entity.Property(e => e.BurialId)
                    .HasColumnName("BurialID")
                    .HasMaxLength(255);

                entity.Property(e => e.BurialNumber).HasColumnName("Burial_Number");

                entity.Property(e => e.ClusterNumber).HasColumnName("Cluster_Number");

                entity.Property(e => e.EastOrWest)
                    .HasColumnName("East_Or_West")
                    .HasMaxLength(255);

                entity.Property(e => e.F3).HasMaxLength(255);

                entity.Property(e => e.HighEw).HasColumnName("HighEW");

                entity.Property(e => e.HighNs).HasColumnName("HighNS");

                entity.Property(e => e.Initials).HasMaxLength(255);

                entity.Property(e => e.LowEw).HasColumnName("LowEW");

                entity.Property(e => e.LowNs).HasColumnName("LowNS");

                entity.Property(e => e.NorthOrSouth)
                    .HasColumnName("North_Or_South")
                    .HasMaxLength(255);

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.PreviouslySampled)
                    .HasColumnName("Previously_Sampled")
                    .HasMaxLength(255);

                entity.Property(e => e.Rack).HasColumnName("Rack #");

                entity.Property(e => e.SsmaTimeStamp)
                    .IsRequired()
                    .HasColumnName("SSMA_TimeStamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Burial)
                    .WithMany(p => p.BiologicalSample)
                    .HasForeignKey(d => d.BurialId)
                    .HasConstraintName("BiologicalSample$BurialsBiologicalSample");
            });

            modelBuilder.Entity<BurialAdultChild>(entity =>
            {
                entity.HasKey(e => e.BurialAdultChild1)
                    .HasName("BurialAdultChild$PrimaryKey");

                entity.Property(e => e.BurialAdultChild1)
                    .HasColumnName("Burial Adult Child")
                    .HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<BurialWrapping>(entity =>
            {
                entity.HasKey(e => e.BurialWrapping1)
                    .HasName("BurialWrapping$PrimaryKey");

                entity.Property(e => e.BurialWrapping1)
                    .HasColumnName("Burial Wrapping")
                    .HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Burials>(entity =>
            {
                entity.HasKey(e => e.BurialId)
                    .HasName("Burials$PrimaryKey");

                entity.Property(e => e.BurialId)
                    .HasColumnName("BurialID")
                    .HasMaxLength(255);

                entity.Property(e => e.AgeCodeSingle)
                    .HasColumnName("Age_Code_SINGLE")
                    .HasMaxLength(255);

                entity.Property(e => e.AgeSkull2018)
                    .HasColumnName("Age _ (Skull; _ 2018)")
                    .HasMaxLength(255);

                entity.Property(e => e.AreaHillBurials).HasColumnName("Area_Hill_Burials");

                entity.Property(e => e.ArtifactFound)
                    .HasColumnName("Artifact_Found")
                    .HasMaxLength(255);

                entity.Property(e => e.ArtifactsDescription)
                    .HasColumnName("Artifacts_Description")
                    .HasMaxLength(255);

                entity.Property(e => e.BasilarSuture)
                    .HasColumnName("Basilar_Suture")
                    .HasMaxLength(255);

                entity.Property(e => e.BasionBregmaHeight)
                    .HasColumnName("Basion_Bregma_Height")
                    .HasMaxLength(255);

                entity.Property(e => e.BasionNasion)
                    .HasColumnName("Basion_Nasion")
                    .HasMaxLength(255);

                entity.Property(e => e.BasionProsthionLength)
                    .HasColumnName("Basion_Prosthion_Length")
                    .HasMaxLength(255);

                entity.Property(e => e.BizygomaticDiameter)
                    .HasColumnName("Bizygomatic_Diameter")
                    .HasMaxLength(255);

                entity.Property(e => e.BodyAnalysis)
                    .HasColumnName("Body_Analysis")
                    .HasMaxLength(255);

                entity.Property(e => e.BoneLength)
                    .HasColumnName("Bone_Length")
                    .HasMaxLength(255);

                entity.Property(e => e.BoneTaken)
                    .HasColumnName("Bone_Taken")
                    .HasMaxLength(255);

                entity.Property(e => e.BurialAdultChild)
                    .HasColumnName("Burial_Adult_Child")
                    .HasMaxLength(255);

                entity.Property(e => e.BurialAgeMethod)
                    .HasColumnName("Burial_Age_Method")
                    .HasMaxLength(255);

                entity.Property(e => e.BurialDepth).HasColumnName("Burial_Depth");

                entity.Property(e => e.BurialDirection)
                    .HasColumnName("Burial_Direction")
                    .HasMaxLength(255);

                entity.Property(e => e.BurialDirection1)
                    .HasColumnName("Burial_Direction1")
                    .HasMaxLength(255);

                entity.Property(e => e.BurialGenderMethod)
                    .HasColumnName("Burial_Gender_Method")
                    .HasMaxLength(255);

                entity.Property(e => e.BurialId2018).HasColumnName("BurialID_2018");

                entity.Property(e => e.BurialNumber).HasColumnName("Burial_Number");

                entity.Property(e => e.BurialPreservation)
                    .HasColumnName("Burial_Preservation")
                    .HasMaxLength(255);

                entity.Property(e => e.BurialSampleTaken)
                    .HasColumnName("Burial_Sample_Taken")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BurialSouthToFeet).HasColumnName("Burial_South_To_Feet");

                entity.Property(e => e.BurialSouthToHead).HasColumnName("Burial_South_To_Head");

                entity.Property(e => e.BurialWestToFeet).HasColumnName("Burial_West_To_Feet");

                entity.Property(e => e.BurialWestToHead).HasColumnName("Burial_West_To_Head");

                entity.Property(e => e.BurialWrapping)
                    .HasColumnName("Burial_Wrapping")
                    .HasMaxLength(255);

                entity.Property(e => e.ButtonOsteoma)
                    .HasColumnName("Button_Osteoma")
                    .HasMaxLength(255);

                entity.Property(e => e.ByuSample)
                    .HasColumnName("BYU_Sample")
                    .HasMaxLength(255);

                entity.Property(e => e.Cluster).HasMaxLength(255);

                entity.Property(e => e.CranialSuture)
                    .HasColumnName("Cranial_Suture")
                    .HasMaxLength(255);

                entity.Property(e => e.CribraOrbitala)
                    .HasColumnName("Cribra_Orbitala")
                    .HasMaxLength(255);

                entity.Property(e => e.DateExcavated)
                    .HasColumnName("Date_Excavated")
                    .HasMaxLength(255);

                entity.Property(e => e.DateOnSkull)
                    .HasColumnName("Date_On_Skull")
                    .HasMaxLength(255);

                entity.Property(e => e.DescriptionOfTaken)
                    .HasColumnName("Description_Of_Taken")
                    .HasMaxLength(255);

                entity.Property(e => e.DorsalPitting)
                    .HasColumnName("Dorsal_Pitting")
                    .HasMaxLength(255);

                entity.Property(e => e.EastOrWest)
                    .HasColumnName("East_or_West")
                    .HasMaxLength(255);

                entity.Property(e => e.EpiphysealUnion)
                    .HasColumnName("Epiphyseal_Union")
                    .HasMaxLength(255);

                entity.Property(e => e.EstimateLivingStature)
                    .HasColumnName("Estimate_Living_Stature")
                    .HasMaxLength(255);

                entity.Property(e => e.EwHighPosition).HasColumnName("EW_High_Position");

                entity.Property(e => e.EwLowPosition).HasColumnName("EW_Low_Position");

                entity.Property(e => e.FaceBundle)
                    .HasColumnName("Face Bundle")
                    .HasMaxLength(255);

                entity.Property(e => e.FemurDiameter)
                    .HasColumnName("Femur_Diameter")
                    .HasMaxLength(255);

                entity.Property(e => e.FemurHead)
                    .HasColumnName("Femur_Head")
                    .HasMaxLength(255);

                entity.Property(e => e.FemurLength)
                    .HasColumnName("Femur_Length")
                    .HasMaxLength(255);

                entity.Property(e => e.ForemanMagnum)
                    .HasColumnName("Foreman_Magnum")
                    .HasMaxLength(255);

                entity.Property(e => e.GeFunctionTotal)
                    .HasColumnName("GE_Function_Total")
                    .HasMaxLength(255);

                entity.Property(e => e.GenderBodyCol)
                    .HasColumnName("Gender_Body_Col")
                    .HasMaxLength(255);

                entity.Property(e => e.GenderCode)
                    .HasColumnName("Gender_Code")
                    .HasMaxLength(255);

                entity.Property(e => e.GenderGe)
                    .HasColumnName("Gender_GE")
                    .HasMaxLength(255);

                entity.Property(e => e.Gonian).HasMaxLength(255);

                entity.Property(e => e.Goods).HasMaxLength(255);

                entity.Property(e => e.HairColorCode)
                    .HasColumnName("Hair_Color_Code")
                    .HasMaxLength(255);

                entity.Property(e => e.HairTaken)
                    .HasColumnName("Hair_Taken")
                    .HasMaxLength(255);

                entity.Property(e => e.Humerus).HasMaxLength(255);

                entity.Property(e => e.HumerusHead)
                    .HasColumnName("Humerus_Head")
                    .HasMaxLength(255);

                entity.Property(e => e.HumerusLength)
                    .HasColumnName("Humerus_Length")
                    .HasMaxLength(255);

                entity.Property(e => e.IliacCrest)
                    .HasColumnName("Iliac_Crest")
                    .HasMaxLength(255);

                entity.Property(e => e.InitialsOfDataEntryChecker)
                    .HasColumnName("Initials_Of_Data_Entry_Checker")
                    .HasMaxLength(255);

                entity.Property(e => e.InitialsOfDataEntryExpert)
                    .HasColumnName("Initials_of_Data_Entry_Expert")
                    .HasMaxLength(255);

                entity.Property(e => e.InterorbitalBreadth)
                    .HasColumnName("Interorbital_Breadth")
                    .HasMaxLength(255);

                entity.Property(e => e.LengthCm).HasColumnName("Length_CM");

                entity.Property(e => e.LengthM).HasColumnName("Length_M");

                entity.Property(e => e.LinearHypoplasiaEnamel)
                    .HasColumnName("Linear_Hypoplasia_Enamel")
                    .HasMaxLength(255);

                entity.Property(e => e.MaximumCranialBreadth)
                    .HasColumnName("Maximum_Cranial_Breadth")
                    .HasMaxLength(255);

                entity.Property(e => e.MaximumCranialLength)
                    .HasColumnName("Maximum_Cranial_Length")
                    .HasMaxLength(255);

                entity.Property(e => e.MaximumNasalBreadth)
                    .HasColumnName("Maximum_Nasal_Breadth")
                    .HasMaxLength(255);

                entity.Property(e => e.MedialClavicle)
                    .HasColumnName("Medial_Clavicle")
                    .HasMaxLength(255);

                entity.Property(e => e.MedialIpRamus)
                    .HasColumnName("Medial_IP_Ramus")
                    .HasMaxLength(255);

                entity.Property(e => e.MetopicSuture)
                    .HasColumnName("Metopic_Suture")
                    .HasMaxLength(255);

                entity.Property(e => e.MonthExcavated)
                    .HasColumnName("Month_Excavated")
                    .HasMaxLength(255);

                entity.Property(e => e.MonthOnSkull)
                    .HasColumnName("Month _On_Skull")
                    .HasMaxLength(255);

                entity.Property(e => e.NasionProsthion)
                    .HasColumnName("Nasion_Prosthion")
                    .HasMaxLength(255);

                entity.Property(e => e.NorthOrSouth)
                    .HasColumnName("North_or_South")
                    .HasMaxLength(255);

                entity.Property(e => e.NsHighPosition).HasColumnName("NS_High_Position");

                entity.Property(e => e.NsLowPosition).HasColumnName("NS_Low_Position");

                entity.Property(e => e.NuchalCrest)
                    .HasColumnName("Nuchal_Crest")
                    .HasMaxLength(255);

                entity.Property(e => e.NumericMaxAge).HasColumnName("Numeric_Max_Age");

                entity.Property(e => e.NumericMinAge).HasColumnName("Numeric_Min_Age");

                entity.Property(e => e.OrbitEdge)
                    .HasColumnName("Orbit_Edge")
                    .HasMaxLength(255);

                entity.Property(e => e.OsteologyNotes)
                    .HasColumnName("Osteology_Notes")
                    .HasMaxLength(255);

                entity.Property(e => e.OsteologyUnknownComment)
                    .HasColumnName("Osteology_Unknown_Comment")
                    .HasMaxLength(255);

                entity.Property(e => e.Osteophytosis).HasMaxLength(255);

                entity.Property(e => e.OtherNotes).HasColumnName("Other_Notes");

                entity.Property(e => e.ParietalBossing)
                    .HasColumnName("Parietal_Bossing")
                    .HasMaxLength(255);

                entity.Property(e => e.PathologyAnomalies)
                    .HasColumnName("Pathology_Anomalies")
                    .HasMaxLength(255);

                entity.Property(e => e.PoroticHyperostosis)
                    .HasColumnName("Porotic_Hyperostosis")
                    .HasMaxLength(255);

                entity.Property(e => e.PoroticHyperostosisLocations)
                    .HasColumnName("Porotic Hyperostosis_LOCATIONS")
                    .HasMaxLength(255);

                entity.Property(e => e.PostcraniaAtMagazine)
                    .HasColumnName("Postcrania_At_Magazine")
                    .HasMaxLength(255);

                entity.Property(e => e.PostcraniaTrauma)
                    .HasColumnName("Postcrania_Trauma")
                    .HasMaxLength(255);

                entity.Property(e => e.PostcraniaTrauma1)
                    .HasColumnName("Postcrania_Trauma1")
                    .HasMaxLength(255);

                entity.Property(e => e.PreaurSulcus)
                    .HasColumnName("Preaur_Sulcus")
                    .HasMaxLength(255);

                entity.Property(e => e.PreservationIndex)
                    .HasColumnName("Preservation_Index")
                    .HasMaxLength(255);

                entity.Property(e => e.PubicBone)
                    .HasColumnName("Pubic_Bone")
                    .HasMaxLength(255);

                entity.Property(e => e.PubicSymphysis)
                    .HasColumnName("Pubic_Symphysis")
                    .HasMaxLength(255);

                entity.Property(e => e.RackAndShelf)
                    .HasColumnName("Rack and Shelf")
                    .HasMaxLength(255);

                entity.Property(e => e.Robust).HasMaxLength(255);

                entity.Property(e => e.SampleNumber)
                    .HasColumnName("Sample_Number")
                    .HasMaxLength(255);

                entity.Property(e => e.SciaticNotch)
                    .HasColumnName("Sciatic_Notch")
                    .HasMaxLength(255);

                entity.Property(e => e.Sex).HasMaxLength(255);

                entity.Property(e => e.SkullAtMagazine)
                    .HasColumnName("Skull_At_Magazine")
                    .HasMaxLength(255);

                entity.Property(e => e.SkullTrauma)
                    .HasColumnName("Skull_Trauma")
                    .HasMaxLength(255);

                entity.Property(e => e.SoftTissueTaken)
                    .HasColumnName("Soft_Tissue_Taken")
                    .HasMaxLength(255);

                entity.Property(e => e.Square).HasMaxLength(255);

                entity.Property(e => e.SsmaTimeStamp)
                    .IsRequired()
                    .HasColumnName("SSMA_TimeStamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.SubpubicAngle)
                    .HasColumnName("Subpubic_Angle")
                    .HasMaxLength(255);

                entity.Property(e => e.SupraorbitalRidges)
                    .HasColumnName("Supraorbital_Ridges")
                    .HasMaxLength(255);

                entity.Property(e => e.TemporalMandibularJointOsteoarthritisTmjOa)
                    .HasColumnName("Temporal_Mandibular_Joint_Osteoarthritis_TMJ_OA")
                    .HasMaxLength(255);

                entity.Property(e => e.TextileTaken)
                    .HasColumnName("Textile_Taken")
                    .HasMaxLength(255);

                entity.Property(e => e.TibiaLength)
                    .HasColumnName("Tibia_Length")
                    .HasMaxLength(255);

                entity.Property(e => e.ToBeConfirmed)
                    .HasColumnName("TO_BE_CONFIRMED")
                    .HasMaxLength(255);

                entity.Property(e => e.Tomb).HasMaxLength(255);

                entity.Property(e => e.ToothAttrition)
                    .HasColumnName("Tooth_Attrition")
                    .HasMaxLength(255);

                entity.Property(e => e.ToothEruption)
                    .HasColumnName("Tooth_Eruption")
                    .HasMaxLength(255);

                entity.Property(e => e.ToothTaken)
                    .HasColumnName("Tooth_Taken")
                    .HasMaxLength(255);

                entity.Property(e => e.VentralArc)
                    .HasColumnName("Ventral_Arc")
                    .HasMaxLength(255);

                entity.Property(e => e.YearExcav).HasColumnName("Year_Excav");

                entity.Property(e => e.YearOnSkull).HasColumnName("Year_On_Skull");

                entity.Property(e => e.ZygomaticCrest)
                    .HasColumnName("Zygomatic_Crest")
                    .HasMaxLength(255);

                entity.HasOne(d => d.AgeCodeSingleNavigation)
                    .WithMany(p => p.Burials)
                    .HasForeignKey(d => d.AgeCodeSingle)
                    .HasConstraintName("Burials$AgeCodeBurials");

                entity.HasOne(d => d.BurialAdultChildNavigation)
                    .WithMany(p => p.Burials)
                    .HasForeignKey(d => d.BurialAdultChild)
                    .HasConstraintName("Burials$BurialAdultChildBurials");

                entity.HasOne(d => d.BurialWrappingNavigation)
                    .WithMany(p => p.Burials)
                    .HasForeignKey(d => d.BurialWrapping)
                    .HasConstraintName("Burials$BurialWrappingBurials");
            });

            modelBuilder.Entity<C14>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Area).HasColumnName("AREA");

                entity.Property(e => e.Burial).HasColumnName("Burial#");

                entity.Property(e => e.BurialId)
                    .HasColumnName("BurialID")
                    .HasMaxLength(255);

                entity.Property(e => e.C14Sample2017).HasColumnName("C14 Sample 2017");

                entity.Property(e => e.Calibrated95CalendarDateAvg)
                    .HasColumnName("Calibrated 95% Calendar Date AVG")
                    .HasMaxLength(255);

                entity.Property(e => e.Calibrated95CalendarDateMax).HasColumnName("Calibrated 95% Calendar Date MAX");

                entity.Property(e => e.Calibrated95CalendarDateMin).HasColumnName("Calibrated 95% Calendar Date MIN");

                entity.Property(e => e.Calibrated95CalendarDateSpan).HasColumnName("Calibrated 95% Calendar Date SPAN");

                entity.Property(e => e.Category).HasMaxLength(255);

                entity.Property(e => e.Conventional14cAgeBp).HasColumnName("Conventional 14C age BP");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.EW).HasColumnName("E/W");

                entity.Property(e => e.F27).HasMaxLength(255);

                entity.Property(e => e.F4).HasMaxLength(255);

                entity.Property(e => e.F6).HasMaxLength(255);

                entity.Property(e => e.Location).HasMaxLength(255);

                entity.Property(e => e.NS).HasColumnName("N/S");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.QuestionS)
                    .HasColumnName("Question(s)")
                    .HasMaxLength(255);

                entity.Property(e => e.SizeMl).HasColumnName("Size (ml)");

                entity.Property(e => e.Square).HasMaxLength(255);

                entity.Property(e => e.SsmaTimeStamp)
                    .IsRequired()
                    .HasColumnName("SSMA_TimeStamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Tube).HasColumnName("TUBE#");

                entity.Property(e => e._14cCalendarDate).HasColumnName("14C Calendar Date");

                entity.HasOne(d => d.BurialNavigation)
                    .WithMany(p => p.C14)
                    .HasForeignKey(d => d.BurialId)
                    .HasConstraintName("C14$BurialsC14");
            });

            modelBuilder.Entity<Cluster>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AgeCodeSingle)
                    .HasColumnName("Age_Code_SINGLE")
                    .HasMaxLength(255);

                entity.Property(e => e.BurialDirection1)
                    .HasColumnName("Burial Direction")
                    .HasMaxLength(255);

                entity.Property(e => e.BurialId2018).HasColumnName("BurialID_2018");

                entity.Property(e => e.Burialadultchild)
                    .HasColumnName("burialadultchild")
                    .HasMaxLength(255);

                entity.Property(e => e.Burialageatdeath)
                    .HasColumnName("burialageatdeath")
                    .HasMaxLength(255);

                entity.Property(e => e.Burialagemethod)
                    .HasColumnName("burialagemethod")
                    .HasMaxLength(255);

                entity.Property(e => e.Burialdirection)
                    .HasColumnName("burialdirection")
                    .HasMaxLength(255);

                entity.Property(e => e.Burialextractorder).HasColumnName("burialextractorder");

                entity.Property(e => e.Burialgendermethod)
                    .HasColumnName("burialgendermethod")
                    .HasMaxLength(255);

                entity.Property(e => e.Burialhaircolor)
                    .HasColumnName("burialhaircolor")
                    .HasMaxLength(255);

                entity.Property(e => e.Burialnors)
                    .HasColumnName("burialnors")
                    .HasMaxLength(255);

                entity.Property(e => e.Burialplotquad)
                    .HasColumnName("burialplotquad")
                    .HasMaxLength(255);

                entity.Property(e => e.Burialpreservation).HasColumnName("burialpreservation");

                entity.Property(e => e.Burialsampletaken)
                    .HasColumnName("burialsampletaken")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Burialwrapping)
                    .HasColumnName("burialwrapping")
                    .HasMaxLength(255);

                entity.Property(e => e.Burialx).HasColumnName("burialx");

                entity.Property(e => e.Burialxeorw)
                    .HasColumnName("burialxeorw")
                    .HasMaxLength(255);

                entity.Property(e => e.Burialxtofeet).HasColumnName("burialxtofeet");

                entity.Property(e => e.Burialxtohead).HasColumnName("burialxtohead");

                entity.Property(e => e.Burialy).HasColumnName("burialy");

                entity.Property(e => e.Burialytofeet).HasColumnName("burialytofeet");

                entity.Property(e => e.Burialytohead).HasColumnName("burialytohead");

                entity.Property(e => e.Burialz).HasColumnName("burialz");

                entity.Property(e => e.Cluster1).HasColumnName("Cluster");

                entity.Property(e => e.F32).HasMaxLength(255);

                entity.Property(e => e.F33).HasMaxLength(255);

                entity.Property(e => e.F34).HasMaxLength(255);

                entity.Property(e => e.F35).HasMaxLength(255);

                entity.Property(e => e.GenderCode)
                    .HasColumnName("Gender_Code")
                    .HasMaxLength(255);

                entity.Property(e => e.HairColorCode)
                    .HasColumnName("Hair_Color_Code")
                    .HasMaxLength(255);

                entity.Property(e => e.Length).HasColumnName("length");

                entity.Property(e => e.LengthCm).HasColumnName("length(CM)");

                entity.Property(e => e.LengthM).HasColumnName("length(M)");

                entity.Property(e => e.SsmaTimeStamp)
                    .IsRequired()
                    .HasColumnName("SSMA_TimeStamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Yearexcav).HasColumnName("yearexcav");
            });

            modelBuilder.Entity<Cranial>(entity =>
            {
                entity.HasKey(e => e.BurialId)
                    .HasName("Cranial$PrimaryKey");

                entity.Property(e => e.BurialId)
                    .HasColumnName("BurialID")
                    .HasMaxLength(255);

                entity.Property(e => e.BasionBregmaHeight).HasColumnName("Basion-Bregma Height");

                entity.Property(e => e.BasionNasion).HasColumnName("Basion-Nasion");

                entity.Property(e => e.BasionProsthionLength).HasColumnName("Basion-Prosthion Length");

                entity.Property(e => e.BizygomaticDiameter).HasColumnName("Bizygomatic Diameter");

                entity.Property(e => e.BodyGender).HasMaxLength(255);

                entity.Property(e => e.BurialArtifactDescription)
                    .HasColumnName("Burial/ Artifact Description")
                    .HasMaxLength(255);

                entity.Property(e => e.BurialDepth).HasColumnName("Burial Depth");

                entity.Property(e => e.BurialNumber).HasColumnName("Burial Number");

                entity.Property(e => e.BurialPositioningEastWestDirection)
                    .HasColumnName("Burial Positioning (East/West) Direction")
                    .HasMaxLength(255);

                entity.Property(e => e.BurialPositioningEastWestNumber)
                    .HasColumnName("Burial Positioning (East/West) Number")
                    .HasMaxLength(255);

                entity.Property(e => e.BurialPositioningNorthSouthDirection)
                    .HasColumnName("Burial Positioning (North/South) Direction")
                    .HasMaxLength(255);

                entity.Property(e => e.BurialPositioningNorthSouthNumber)
                    .HasColumnName("Burial Positioning (North/South) Number")
                    .HasMaxLength(255);

                entity.Property(e => e.BurialSubPlotDirection)
                    .HasColumnName("Burial sub-plot direction")
                    .HasMaxLength(255);

                entity.Property(e => e.BuriedWithArtifacts)
                    .HasColumnName("Buried with Artifacts")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.GilesGender).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.InterorbitalBreadth).HasColumnName("Interorbital Breadth");

                entity.Property(e => e.MaximumCranialBreadth).HasColumnName("Maximum Cranial Breadth");

                entity.Property(e => e.MaximumCranialLength).HasColumnName("Maximum Cranial Length");

                entity.Property(e => e.MaximumNasalBreadth).HasColumnName("Maximum Nasal Breadth");

                entity.Property(e => e.NasionProsthion).HasColumnName("Nasion-Prosthion");

                entity.Property(e => e.SampleNumber).HasColumnName("Sample Number");

                entity.Property(e => e.SsmaTimeStamp)
                    .IsRequired()
                    .HasColumnName("SSMA_TimeStamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<FieldBook>(entity =>
            {
                entity.HasIndex(e => e.BurialId)
                    .HasName("FieldBook$FieldBookBurialID");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BurialId)
                    .HasColumnName("BurialID")
                    .HasMaxLength(255);

                entity.Property(e => e.FieldBook1)
                    .HasColumnName("Field_Book")
                    .HasMaxLength(255);

                entity.Property(e => e.FieldBookPageNumber).HasColumnName("Field_Book_Page_Number");

                entity.Property(e => e.SsmaTimeStamp)
                    .IsRequired()
                    .HasColumnName("SSMA_TimeStamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Burial)
                    .WithMany(p => p.FieldBook)
                    .HasForeignKey(d => d.BurialId)
                    .HasConstraintName("FieldBook$BurialsFieldBook");
            });

            modelBuilder.Entity<GenderCode>(entity =>
            {
                entity.HasKey(e => e.GenderCodeSingle)
                    .HasName("GenderCode$PrimaryKey");

                entity.Property(e => e.GenderCodeSingle)
                    .HasColumnName("Gender Code single")
                    .HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<HairCode>(entity =>
            {
                entity.HasKey(e => e.HairColumn)
                    .HasName("HairCode$PrimaryKey");

                entity.Property(e => e.HairColumn)
                    .HasColumnName("Hair_Column")
                    .HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OracleGis>(entity =>
            {
                entity.HasKey(e => e.BurialId)
                    .HasName("OracleGIS$PrimaryKey");

                entity.ToTable("OracleGIS");

                entity.Property(e => e.BurialId)
                    .HasColumnName("BurialID")
                    .HasMaxLength(255);

                entity.Property(e => e.Ageatdeath)
                    .HasColumnName("AGEATDEATH")
                    .HasMaxLength(255);

                entity.Property(e => e.Agemethod)
                    .HasColumnName("AGEMETHOD")
                    .HasMaxLength(255);

                entity.Property(e => e.Area)
                    .HasColumnName("AREA")
                    .HasMaxLength(255);

                entity.Property(e => e.Burialicon)
                    .HasColumnName("BURIALICON")
                    .HasMaxLength(255);

                entity.Property(e => e.Burialicon2)
                    .HasColumnName("BURIALICON2")
                    .HasMaxLength(255);

                entity.Property(e => e.Burialnum).HasColumnName("BURIALNUM");

                entity.Property(e => e.Burialsquare).HasColumnName("BURIALSQUARE");

                entity.Property(e => e.Depth).HasColumnName("DEPTH");

                entity.Property(e => e.Eorw)
                    .HasColumnName("EORW")
                    .HasMaxLength(255);

                entity.Property(e => e.Gamous).HasColumnName("GAMOUS");

                entity.Property(e => e.Haircolor)
                    .HasColumnName("HAIRCOLOR")
                    .HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Nors)
                    .HasColumnName("NORS")
                    .HasMaxLength(255);

                entity.Property(e => e.Preservation)
                    .HasColumnName("PRESERVATION")
                    .HasMaxLength(255);

                entity.Property(e => e.Sample)
                    .HasColumnName("SAMPLE")
                    .HasMaxLength(255);

                entity.Property(e => e.Sex)
                    .HasColumnName("SEX")
                    .HasMaxLength(255);

                entity.Property(e => e.Sexmethod)
                    .HasColumnName("SEXMETHOD")
                    .HasMaxLength(255);

                entity.Property(e => e.Southtofeet).HasColumnName("SOUTHTOFEET");

                entity.Property(e => e.Southtohead).HasColumnName("SOUTHTOHEAD");

                entity.Property(e => e.Sq2).HasColumnName("SQ2");

                entity.Property(e => e.SsmaTimeStamp)
                    .IsRequired()
                    .HasColumnName("SSMA_TimeStamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Westtofeet).HasColumnName("WESTTOFEET");

                entity.Property(e => e.Westtohead).HasColumnName("WESTTOHEAD");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BurialId)
                    .HasColumnName("BurialID")
                    .HasMaxLength(255);

                entity.Property(e => e.PhotoId)
                    .HasColumnName("PhotoID")
                    .HasMaxLength(255);

                entity.HasOne(d => d.Burial)
                    .WithMany(p => p.Photo)
                    .HasForeignKey(d => d.BurialId)
                    .HasConstraintName("Photo$BurialsPhoto");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

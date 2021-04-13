using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

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

        public virtual DbSet<AgeCode> AgeCodes { get; set; }
        public virtual DbSet<BiologicalSample> BiologicalSamples { get; set; }
        public virtual DbSet<Burial> Burials { get; set; }
        public virtual DbSet<BurialAdultChild> BurialAdultChildren { get; set; }
        public virtual DbSet<BurialWrapping> BurialWrappings { get; set; }
        public virtual DbSet<C14> C14s { get; set; }
        public virtual DbSet<Cluster> Clusters { get; set; }
        public virtual DbSet<Cranial> Cranials { get; set; }
        public virtual DbSet<FieldBook> FieldBook { get; set; }
        public virtual DbSet<GenderCode> GenderCodes { get; set; }
        public virtual DbSet<HairCode> HairCodes { get; set; }
        public virtual DbSet<OracleGi> OracleGis { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=aa16xgpayfb2xja.c1okjvee6ouq.us-east-1.rds.amazonaws.com,1433;Database=FagElGamous;User=admin;Password=adminadmin;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgeCode>(entity =>
            {
                entity.HasKey(e => e.AgeCode1)
                    .HasName("AgeCode$PrimaryKey");

                entity.ToTable("AgeCode");

                entity.Property(e => e.AgeCode1)
                    .HasMaxLength(255)
                    .HasColumnName("Age Code");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<BiologicalSample>(entity =>
            {
                entity.ToTable("BiologicalSample");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bag)
                    .HasMaxLength(255)
                    .HasColumnName("Bag #");

                entity.Property(e => e.BurialId)
                    .HasMaxLength(255)
                    .HasColumnName("BurialID");

                entity.Property(e => e.BurialNumber).HasColumnName("Burial_Number");

                entity.Property(e => e.ClusterNumber).HasColumnName("Cluster_Number");

                entity.Property(e => e.EastOrWest)
                    .HasMaxLength(255)
                    .HasColumnName("East_Or_West");

                entity.Property(e => e.F3).HasMaxLength(255);

                entity.Property(e => e.HighEw).HasColumnName("HighEW");

                entity.Property(e => e.HighNs).HasColumnName("HighNS");

                entity.Property(e => e.Initials).HasMaxLength(255);

                entity.Property(e => e.LowEw).HasColumnName("LowEW");

                entity.Property(e => e.LowNs).HasColumnName("LowNS");

                entity.Property(e => e.NorthOrSouth)
                    .HasMaxLength(255)
                    .HasColumnName("North_Or_South");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.PreviouslySampled)
                    .HasMaxLength(255)
                    .HasColumnName("Previously_Sampled");

                entity.Property(e => e.Rack).HasColumnName("Rack #");

                //entity.Property(e => e.SsmaTimeStamp)
                //    .IsRequired()
                //    .IsRowVersion()
                //    .IsConcurrencyToken()
                //    .HasColumnName("SSMA_TimeStamp");

                entity.HasOne(d => d.Burial)
                    .WithMany(p => p.BiologicalSamples)
                    .HasForeignKey(d => d.BurialId)
                    .HasConstraintName("BiologicalSample$BurialsBiologicalSample");
            });

            modelBuilder.Entity<Burial>(entity =>
            {
                entity.Property(e => e.BurialId)
                    .HasMaxLength(255)
                    .HasColumnName("BurialID");

                entity.Property(e => e.AgeCodeSingle)
                    .HasMaxLength(255)
                    .HasColumnName("Age_Code_SINGLE");

                entity.Property(e => e.AgeSkull2018)
                    .HasMaxLength(255)
                    .HasColumnName("Age _ (Skull; _ 2018)");

                entity.Property(e => e.AreaHillBurials).HasColumnName("Area_Hill_Burials");

                entity.Property(e => e.ArtifactFound)
                    .HasMaxLength(255)
                    .HasColumnName("Artifact_Found");

                entity.Property(e => e.ArtifactsDescription)
                    .HasMaxLength(255)
                    .HasColumnName("Artifacts_Description");

                entity.Property(e => e.BasilarSuture)
                    .HasMaxLength(255)
                    .HasColumnName("Basilar_Suture");

                entity.Property(e => e.BasionBregmaHeight)
                    .HasMaxLength(255)
                    .HasColumnName("Basion_Bregma_Height");

                entity.Property(e => e.BasionNasion)
                    .HasMaxLength(255)
                    .HasColumnName("Basion_Nasion");

                entity.Property(e => e.BasionProsthionLength)
                    .HasMaxLength(255)
                    .HasColumnName("Basion_Prosthion_Length");

                entity.Property(e => e.BizygomaticDiameter)
                    .HasMaxLength(255)
                    .HasColumnName("Bizygomatic_Diameter");

                entity.Property(e => e.BodyAnalysis)
                    .HasMaxLength(255)
                    .HasColumnName("Body_Analysis");

                entity.Property(e => e.BoneLength)
                    .HasMaxLength(255)
                    .HasColumnName("Bone_Length");

                entity.Property(e => e.BoneTaken)
                    .HasMaxLength(255)
                    .HasColumnName("Bone_Taken");

                entity.Property(e => e.BurialAdultChild)
                    .HasMaxLength(255)
                    .HasColumnName("Burial_Adult_Child");

                entity.Property(e => e.BurialAgeMethod)
                    .HasMaxLength(255)
                    .HasColumnName("Burial_Age_Method");

                entity.Property(e => e.BurialDepth).HasColumnName("Burial_Depth");

                entity.Property(e => e.BurialDirection)
                    .HasMaxLength(255)
                    .HasColumnName("Burial_Direction");

                entity.Property(e => e.BurialDirection1)
                    .HasMaxLength(255)
                    .HasColumnName("Burial_Direction1");

                entity.Property(e => e.BurialGenderMethod)
                    .HasMaxLength(255)
                    .HasColumnName("Burial_Gender_Method");

                entity.Property(e => e.BurialId2018).HasColumnName("BurialID_2018");

                entity.Property(e => e.BurialNumber).HasColumnName("Burial_Number");

                entity.Property(e => e.BurialPreservation)
                    .HasMaxLength(255)
                    .HasColumnName("Burial_Preservation");

                entity.Property(e => e.BurialSampleTaken)
                    .HasColumnName("Burial_Sample_Taken")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BurialSouthToFeet).HasColumnName("Burial_South_To_Feet");

                entity.Property(e => e.BurialSouthToHead).HasColumnName("Burial_South_To_Head");

                entity.Property(e => e.BurialWestToFeet).HasColumnName("Burial_West_To_Feet");

                entity.Property(e => e.BurialWestToHead).HasColumnName("Burial_West_To_Head");

                entity.Property(e => e.BurialWrapping)
                    .HasMaxLength(255)
                    .HasColumnName("Burial_Wrapping");

                entity.Property(e => e.ButtonOsteoma)
                    .HasMaxLength(255)
                    .HasColumnName("Button_Osteoma");

                entity.Property(e => e.ByuSample)
                    .HasMaxLength(255)
                    .HasColumnName("BYU_Sample");

                entity.Property(e => e.Cluster).HasMaxLength(255);

                entity.Property(e => e.CranialSuture)
                    .HasMaxLength(255)
                    .HasColumnName("Cranial_Suture");

                entity.Property(e => e.CribraOrbitala)
                    .HasMaxLength(255)
                    .HasColumnName("Cribra_Orbitala");

                entity.Property(e => e.DateExcavated)
                    .HasMaxLength(255)
                    .HasColumnName("Date_Excavated");

                entity.Property(e => e.DateOnSkull)
                    .HasMaxLength(255)
                    .HasColumnName("Date_On_Skull");

                entity.Property(e => e.DescriptionOfTaken)
                    .HasMaxLength(255)
                    .HasColumnName("Description_Of_Taken");

                entity.Property(e => e.DorsalPitting)
                    .HasMaxLength(255)
                    .HasColumnName("Dorsal_Pitting");

                entity.Property(e => e.EastOrWest)
                    .HasMaxLength(255)
                    .HasColumnName("East_or_West");

                entity.Property(e => e.EpiphysealUnion)
                    .HasMaxLength(255)
                    .HasColumnName("Epiphyseal_Union");

                entity.Property(e => e.EstimateLivingStature)
                    .HasMaxLength(255)
                    .HasColumnName("Estimate_Living_Stature");

                entity.Property(e => e.EwHighPosition).HasColumnName("EW_High_Position");

                entity.Property(e => e.EwLowPosition).HasColumnName("EW_Low_Position");

                entity.Property(e => e.FaceBundle)
                    .HasMaxLength(255)
                    .HasColumnName("Face Bundle");

                entity.Property(e => e.FemurDiameter)
                    .HasMaxLength(255)
                    .HasColumnName("Femur_Diameter");

                entity.Property(e => e.FemurHead)
                    .HasMaxLength(255)
                    .HasColumnName("Femur_Head");

                entity.Property(e => e.FemurLength)
                    .HasMaxLength(255)
                    .HasColumnName("Femur_Length");

                entity.Property(e => e.ForemanMagnum)
                    .HasMaxLength(255)
                    .HasColumnName("Foreman_Magnum");

                entity.Property(e => e.GeFunctionTotal)
                    .HasMaxLength(255)
                    .HasColumnName("GE_Function_Total");

                entity.Property(e => e.GenderBodyCol)
                    .HasMaxLength(255)
                    .HasColumnName("Gender_Body_Col");

                entity.Property(e => e.GenderCode)
                    .HasMaxLength(255)
                    .HasColumnName("Gender_Code");

                entity.Property(e => e.GenderGe)
                    .HasMaxLength(255)
                    .HasColumnName("Gender_GE");

                entity.Property(e => e.Gonian).HasMaxLength(255);

                entity.Property(e => e.Goods).HasMaxLength(255);

                entity.Property(e => e.HairColorCode)
                    .HasMaxLength(255)
                    .HasColumnName("Hair_Color_Code");

                entity.Property(e => e.HairTaken)
                    .HasMaxLength(255)
                    .HasColumnName("Hair_Taken");

                entity.Property(e => e.Humerus).HasMaxLength(255);

                entity.Property(e => e.HumerusHead)
                    .HasMaxLength(255)
                    .HasColumnName("Humerus_Head");

                entity.Property(e => e.HumerusLength)
                    .HasMaxLength(255)
                    .HasColumnName("Humerus_Length");

                entity.Property(e => e.IliacCrest)
                    .HasMaxLength(255)
                    .HasColumnName("Iliac_Crest");

                entity.Property(e => e.InitialsOfDataEntryChecker)
                    .HasMaxLength(255)
                    .HasColumnName("Initials_Of_Data_Entry_Checker");

                entity.Property(e => e.InitialsOfDataEntryExpert)
                    .HasMaxLength(255)
                    .HasColumnName("Initials_of_Data_Entry_Expert");

                entity.Property(e => e.InterorbitalBreadth)
                    .HasMaxLength(255)
                    .HasColumnName("Interorbital_Breadth");

                entity.Property(e => e.LengthCm).HasColumnName("Length_CM");

                entity.Property(e => e.LengthM).HasColumnName("Length_M");

                entity.Property(e => e.LinearHypoplasiaEnamel)
                    .HasMaxLength(255)
                    .HasColumnName("Linear_Hypoplasia_Enamel");

                entity.Property(e => e.MaximumCranialBreadth)
                    .HasMaxLength(255)
                    .HasColumnName("Maximum_Cranial_Breadth");

                entity.Property(e => e.MaximumCranialLength)
                    .HasMaxLength(255)
                    .HasColumnName("Maximum_Cranial_Length");

                entity.Property(e => e.MaximumNasalBreadth)
                    .HasMaxLength(255)
                    .HasColumnName("Maximum_Nasal_Breadth");

                entity.Property(e => e.MedialClavicle)
                    .HasMaxLength(255)
                    .HasColumnName("Medial_Clavicle");

                entity.Property(e => e.MedialIpRamus)
                    .HasMaxLength(255)
                    .HasColumnName("Medial_IP_Ramus");

                entity.Property(e => e.MetopicSuture)
                    .HasMaxLength(255)
                    .HasColumnName("Metopic_Suture");

                entity.Property(e => e.MonthExcavated)
                    .HasMaxLength(255)
                    .HasColumnName("Month_Excavated");

                entity.Property(e => e.MonthOnSkull)
                    .HasMaxLength(255)
                    .HasColumnName("Month _On_Skull");

                entity.Property(e => e.NasionProsthion)
                    .HasMaxLength(255)
                    .HasColumnName("Nasion_Prosthion");

                entity.Property(e => e.NorthOrSouth)
                    .HasMaxLength(255)
                    .HasColumnName("North_or_South");

                entity.Property(e => e.NsHighPosition).HasColumnName("NS_High_Position");

                entity.Property(e => e.NsLowPosition).HasColumnName("NS_Low_Position");

                entity.Property(e => e.NuchalCrest)
                    .HasMaxLength(255)
                    .HasColumnName("Nuchal_Crest");

                entity.Property(e => e.NumericMaxAge).HasColumnName("Numeric_Max_Age");

                entity.Property(e => e.NumericMinAge).HasColumnName("Numeric_Min_Age");

                entity.Property(e => e.OrbitEdge)
                    .HasMaxLength(255)
                    .HasColumnName("Orbit_Edge");

                entity.Property(e => e.OsteologyNotes)
                    .HasMaxLength(255)
                    .HasColumnName("Osteology_Notes");

                entity.Property(e => e.OsteologyUnknownComment)
                    .HasMaxLength(255)
                    .HasColumnName("Osteology_Unknown_Comment");

                entity.Property(e => e.Osteophytosis).HasMaxLength(255);

                entity.Property(e => e.OtherNotes).HasColumnName("Other_Notes");

                entity.Property(e => e.ParietalBossing)
                    .HasMaxLength(255)
                    .HasColumnName("Parietal_Bossing");

                entity.Property(e => e.PathologyAnomalies)
                    .HasMaxLength(255)
                    .HasColumnName("Pathology_Anomalies");

                entity.Property(e => e.PhotoTaken).HasColumnName("Photo_Taken");

                entity.Property(e => e.PoroticHyperostosis)
                    .HasMaxLength(255)
                    .HasColumnName("Porotic_Hyperostosis");

                entity.Property(e => e.PoroticHyperostosisLocations)
                    .HasMaxLength(255)
                    .HasColumnName("Porotic Hyperostosis_LOCATIONS");

                entity.Property(e => e.PostcraniaAtMagazine)
                    .HasMaxLength(255)
                    .HasColumnName("Postcrania_At_Magazine");

                entity.Property(e => e.PostcraniaTrauma)
                    .HasMaxLength(255)
                    .HasColumnName("Postcrania_Trauma");

                entity.Property(e => e.PostcraniaTrauma1)
                    .HasMaxLength(255)
                    .HasColumnName("Postcrania_Trauma1");

                entity.Property(e => e.PreaurSulcus)
                    .HasMaxLength(255)
                    .HasColumnName("Preaur_Sulcus");

                entity.Property(e => e.PreservationIndex)
                    .HasMaxLength(255)
                    .HasColumnName("Preservation_Index");

                entity.Property(e => e.PubicBone)
                    .HasMaxLength(255)
                    .HasColumnName("Pubic_Bone");

                entity.Property(e => e.PubicSymphysis)
                    .HasMaxLength(255)
                    .HasColumnName("Pubic_Symphysis");

                entity.Property(e => e.RackAndShelf)
                    .HasMaxLength(255)
                    .HasColumnName("Rack and Shelf");

                entity.Property(e => e.Robust).HasMaxLength(255);

                entity.Property(e => e.SampleNumber)
                    .HasMaxLength(255)
                    .HasColumnName("Sample_Number");

                entity.Property(e => e.SciaticNotch)
                    .HasMaxLength(255)
                    .HasColumnName("Sciatic_Notch");

                entity.Property(e => e.Sex).HasMaxLength(255);

                entity.Property(e => e.SkullAtMagazine)
                    .HasMaxLength(255)
                    .HasColumnName("Skull_At_Magazine");

                entity.Property(e => e.SkullTrauma)
                    .HasMaxLength(255)
                    .HasColumnName("Skull_Trauma");

                entity.Property(e => e.SoftTissueTaken)
                    .HasMaxLength(255)
                    .HasColumnName("Soft_Tissue_Taken");

                entity.Property(e => e.Square).HasMaxLength(255);

                //entity.Property(e => e.SsmaTimeStamp)
                //    .IsRequired()
                //    .IsRowVersion()
                //    .IsConcurrencyToken()
                //    .HasColumnName("SSMA_TimeStamp");

                entity.Property(e => e.SubpubicAngle)
                    .HasMaxLength(255)
                    .HasColumnName("Subpubic_Angle");

                entity.Property(e => e.SupraorbitalRidges)
                    .HasMaxLength(255)
                    .HasColumnName("Supraorbital_Ridges");

                entity.Property(e => e.TemporalMandibularJointOsteoarthritisTmjOa)
                    .HasMaxLength(255)
                    .HasColumnName("Temporal_Mandibular_Joint_Osteoarthritis_TMJ_OA");

                entity.Property(e => e.TextileTaken)
                    .HasMaxLength(255)
                    .HasColumnName("Textile_Taken");

                entity.Property(e => e.TibiaLength)
                    .HasMaxLength(255)
                    .HasColumnName("Tibia_Length");

                entity.Property(e => e.ToBeConfirmed)
                    .HasMaxLength(255)
                    .HasColumnName("TO_BE_CONFIRMED");

                entity.Property(e => e.Tomb).HasMaxLength(255);

                entity.Property(e => e.ToothAttrition)
                    .HasMaxLength(255)
                    .HasColumnName("Tooth_Attrition");

                entity.Property(e => e.ToothEruption)
                    .HasMaxLength(255)
                    .HasColumnName("Tooth_Eruption");

                entity.Property(e => e.ToothTaken)
                    .HasMaxLength(255)
                    .HasColumnName("Tooth_Taken");

                entity.Property(e => e.VentralArc)
                    .HasMaxLength(255)
                    .HasColumnName("Ventral_Arc");

                entity.Property(e => e.YearExcav).HasColumnName("Year_Excav");

                entity.Property(e => e.YearOnSkull).HasColumnName("Year_On_Skull");

                entity.Property(e => e.ZygomaticCrest)
                    .HasMaxLength(255)
                    .HasColumnName("Zygomatic_Crest");

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

            modelBuilder.Entity<BurialAdultChild>(entity =>
            {
                entity.HasKey(e => e.BurialAdultChild1)
                    .HasName("BurialAdultChild$PrimaryKey");

                entity.ToTable("BurialAdultChild");

                entity.Property(e => e.BurialAdultChild1)
                    .HasMaxLength(255)
                    .HasColumnName("Burial Adult Child");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<BurialWrapping>(entity =>
            {
                entity.HasKey(e => e.BurialWrapping1)
                    .HasName("BurialWrapping$PrimaryKey");

                entity.ToTable("BurialWrapping");

                entity.Property(e => e.BurialWrapping1)
                    .HasMaxLength(255)
                    .HasColumnName("Burial Wrapping");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<C14>(entity =>
            {
                entity.ToTable("C14");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Area).HasColumnName("AREA");

                entity.Property(e => e.Burial).HasColumnName("Burial#");

                entity.Property(e => e.BurialId)
                    .HasMaxLength(255)
                    .HasColumnName("BurialID");

                entity.Property(e => e.C14Sample2017).HasColumnName("C14 Sample 2017");

                entity.Property(e => e.Calibrated95CalendarDateAvg)
                    .HasMaxLength(255)
                    .HasColumnName("Calibrated 95% Calendar Date AVG");

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
                    .HasMaxLength(255)
                    .HasColumnName("Question(s)");

                entity.Property(e => e.SizeMl).HasColumnName("Size (ml)");

                entity.Property(e => e.Square).HasMaxLength(255);

                //entity.Property(e => e.SsmaTimeStamp)
                //    .IsRequired()
                //    .IsRowVersion()
                //    .IsConcurrencyToken()
                //    .HasColumnName("SSMA_TimeStamp");

                entity.Property(e => e.Tube).HasColumnName("TUBE#");

                entity.Property(e => e._14cCalendarDate).HasColumnName("14C Calendar Date");

                entity.HasOne(d => d.BurialNavigation)
                    .WithMany(p => p.C14s)
                    .HasForeignKey(d => d.BurialId)
                    .HasConstraintName("C14$BurialsC14");
            });

            modelBuilder.Entity<Cluster>(entity =>
            {
                entity.ToTable("Cluster");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AgeCodeSingle)
                    .HasMaxLength(255)
                    .HasColumnName("Age_Code_SINGLE");

                entity.Property(e => e.BurialDirection1)
                    .HasMaxLength(255)
                    .HasColumnName("Burial Direction");

                entity.Property(e => e.BurialId2018).HasColumnName("BurialID_2018");

                entity.Property(e => e.Burialadultchild)
                    .HasMaxLength(255)
                    .HasColumnName("burialadultchild");

                entity.Property(e => e.Burialageatdeath)
                    .HasMaxLength(255)
                    .HasColumnName("burialageatdeath");

                entity.Property(e => e.Burialagemethod)
                    .HasMaxLength(255)
                    .HasColumnName("burialagemethod");

                entity.Property(e => e.Burialdirection)
                    .HasMaxLength(255)
                    .HasColumnName("burialdirection");

                entity.Property(e => e.Burialextractorder).HasColumnName("burialextractorder");

                entity.Property(e => e.Burialgendermethod)
                    .HasMaxLength(255)
                    .HasColumnName("burialgendermethod");

                entity.Property(e => e.Burialhaircolor)
                    .HasMaxLength(255)
                    .HasColumnName("burialhaircolor");

                entity.Property(e => e.Burialnors)
                    .HasMaxLength(255)
                    .HasColumnName("burialnors");

                entity.Property(e => e.Burialplotquad)
                    .HasMaxLength(255)
                    .HasColumnName("burialplotquad");

                entity.Property(e => e.Burialpreservation).HasColumnName("burialpreservation");

                entity.Property(e => e.Burialsampletaken)
                    .HasColumnName("burialsampletaken")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Burialwrapping)
                    .HasMaxLength(255)
                    .HasColumnName("burialwrapping");

                entity.Property(e => e.Burialx).HasColumnName("burialx");

                entity.Property(e => e.Burialxeorw)
                    .HasMaxLength(255)
                    .HasColumnName("burialxeorw");

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
                    .HasMaxLength(255)
                    .HasColumnName("Gender_Code");

                entity.Property(e => e.HairColorCode)
                    .HasMaxLength(255)
                    .HasColumnName("Hair_Color_Code");

                entity.Property(e => e.Length).HasColumnName("length");

                entity.Property(e => e.LengthCm).HasColumnName("length(CM)");

                entity.Property(e => e.LengthM).HasColumnName("length(M)");

                //entity.Property(e => e.SsmaTimeStamp)
                //    .IsRequired()
                //    .IsRowVersion()
                //    .IsConcurrencyToken()
                //    .HasColumnName("SSMA_TimeStamp");

                entity.Property(e => e.Yearexcav).HasColumnName("yearexcav");
            });

            modelBuilder.Entity<Cranial>(entity =>
            {
                entity.HasKey(e => e.BurialId)
                    .HasName("Cranial$PrimaryKey");

                entity.ToTable("Cranial");

                entity.Property(e => e.BurialId)
                    .HasMaxLength(255)
                    .HasColumnName("BurialID");

                entity.Property(e => e.BasionBregmaHeight).HasColumnName("Basion-Bregma Height");

                entity.Property(e => e.BasionNasion).HasColumnName("Basion-Nasion");

                entity.Property(e => e.BasionProsthionLength).HasColumnName("Basion-Prosthion Length");

                entity.Property(e => e.BizygomaticDiameter).HasColumnName("Bizygomatic Diameter");

                entity.Property(e => e.BodyGender).HasMaxLength(255);

                entity.Property(e => e.BurialArtifactDescription)
                    .HasMaxLength(255)
                    .HasColumnName("Burial/ Artifact Description");

                entity.Property(e => e.BurialDepth).HasColumnName("Burial Depth");

                entity.Property(e => e.BurialNumber).HasColumnName("Burial Number");

                entity.Property(e => e.BurialPositioningEastWestDirection)
                    .HasMaxLength(255)
                    .HasColumnName("Burial Positioning (East/West) Direction");

                entity.Property(e => e.BurialPositioningEastWestNumber)
                    .HasMaxLength(255)
                    .HasColumnName("Burial Positioning (East/West) Number");

                entity.Property(e => e.BurialPositioningNorthSouthDirection)
                    .HasMaxLength(255)
                    .HasColumnName("Burial Positioning (North/South) Direction");

                entity.Property(e => e.BurialPositioningNorthSouthNumber)
                    .HasMaxLength(255)
                    .HasColumnName("Burial Positioning (North/South) Number");

                entity.Property(e => e.BurialSubPlotDirection)
                    .HasMaxLength(255)
                    .HasColumnName("Burial sub-plot direction");

                entity.Property(e => e.BuriedWithArtifacts)
                    .HasColumnName("Buried with Artifacts")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.GilesGender).HasMaxLength(255);

                entity.Property(e => e.Id).ValueGeneratedOnAdd()
                    .HasColumnName("id").Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                //entity.Property(e => e.Id)
                //    .ValueGeneratedOnAdd()
                //    .HasColumnName("id")
                //    .ValueGeneratedOnAddOrUpdate()
                //    .Metadata
                //    .SetAfterSaveBehavior(PropertySaveBehavior.Save);

                //modelBuilder.Entity<Type>().Property(u => u.Property).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


                entity.Property(e => e.InterorbitalBreadth).HasColumnName("Interorbital Breadth");

                entity.Property(e => e.MaximumCranialBreadth).HasColumnName("Maximum Cranial Breadth");

                entity.Property(e => e.MaximumCranialLength).HasColumnName("Maximum Cranial Length");

                entity.Property(e => e.MaximumNasalBreadth).HasColumnName("Maximum Nasal Breadth");

                entity.Property(e => e.NasionProsthion).HasColumnName("Nasion-Prosthion");

                entity.Property(e => e.SampleNumber).HasColumnName("Sample Number");

                //entity.Property(e => e.SsmaTimeStamp)
                //    .IsRequired()
                //    .IsRowVersion()
                //    .IsConcurrencyToken()
                //    .HasColumnName("SSMA_TimeStamp");
            });
            //FieldBooks FieldBook

            modelBuilder.Entity<FieldBook>(entity =>
            {
                entity.HasIndex(e => e.BurialId).HasName("BurialID");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BurialId)
                    .HasMaxLength(255)
                    .HasColumnName("BurialID");

                entity.Property(e => e.FieldBook1)
                    .HasMaxLength(255)
                    .HasColumnName("Field_Book");

                entity.Property(e => e.FieldBookPageNumber).HasColumnName("Field_Book_Page_Number");

                //entity.Property(e => e.SsmaTimeStamp)
                //    .IsRequired()
                //    .IsRowVersion()
                //    .IsConcurrencyToken()
                //    .HasColumnName("SSMA_TimeStamp");

                entity.HasOne(d => d.Burial)
                    .WithMany(p => p.FieldBooks)
                    .HasForeignKey(d => d.BurialId)
                    .HasConstraintName("FieldBook$BurialsFieldBook");
            });

            modelBuilder.Entity<GenderCode>(entity =>
            {
                entity.HasKey(e => e.GenderCodeSingle)
                    .HasName("GenderCode$PrimaryKey");

                entity.ToTable("GenderCode");

                entity.Property(e => e.GenderCodeSingle)
                    .HasMaxLength(255)
                    .HasColumnName("Gender Code single");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<HairCode>(entity =>
            {
                entity.HasKey(e => e.HairColumn)
                    .HasName("HairCode$PrimaryKey");

                entity.ToTable("HairCode");

                entity.Property(e => e.HairColumn)
                    .HasMaxLength(255)
                    .HasColumnName("Hair_Column");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<OracleGi>(entity =>
            {
                entity.HasKey(e => e.BurialId)
                    .HasName("OracleGIS$PrimaryKey");

                entity.ToTable("OracleGIS");

                entity.Property(e => e.BurialId)
                    .HasMaxLength(255)
                    .HasColumnName("BurialID");

                entity.Property(e => e.Ageatdeath)
                    .HasMaxLength(255)
                    .HasColumnName("AGEATDEATH");

                entity.Property(e => e.Agemethod)
                    .HasMaxLength(255)
                    .HasColumnName("AGEMETHOD");

                entity.Property(e => e.Area)
                    .HasMaxLength(255)
                    .HasColumnName("AREA");

                entity.Property(e => e.Burialicon)
                    .HasMaxLength(255)
                    .HasColumnName("BURIALICON");

                entity.Property(e => e.Burialicon2)
                    .HasMaxLength(255)
                    .HasColumnName("BURIALICON2");

                entity.Property(e => e.Burialnum).HasColumnName("BURIALNUM");

                entity.Property(e => e.Burialsquare).HasColumnName("BURIALSQUARE");

                entity.Property(e => e.Depth).HasColumnName("DEPTH");

                entity.Property(e => e.Eorw)
                    .HasMaxLength(255)
                    .HasColumnName("EORW");

                entity.Property(e => e.Gamous).HasColumnName("GAMOUS");

                entity.Property(e => e.Haircolor)
                    .HasMaxLength(255)
                    .HasColumnName("HAIRCOLOR");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Nors)
                    .HasMaxLength(255)
                    .HasColumnName("NORS");

                entity.Property(e => e.Preservation)
                    .HasMaxLength(255)
                    .HasColumnName("PRESERVATION");

                entity.Property(e => e.Sample)
                    .HasMaxLength(255)
                    .HasColumnName("SAMPLE");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .HasColumnName("SEX");

                entity.Property(e => e.Sexmethod)
                    .HasMaxLength(255)
                    .HasColumnName("SEXMETHOD");

                entity.Property(e => e.Southtofeet).HasColumnName("SOUTHTOFEET");

                entity.Property(e => e.Southtohead).HasColumnName("SOUTHTOHEAD");

                entity.Property(e => e.Sq2).HasColumnName("SQ2");

                //entity.Property(e => e.SsmaTimeStamp)
                //    .IsRequired()
                //    .IsRowVersion()
                //    .IsConcurrencyToken()
                //    .HasColumnName("SSMA_TimeStamp");

                entity.Property(e => e.Westtofeet).HasColumnName("WESTTOFEET");

                entity.Property(e => e.Westtohead).HasColumnName("WESTTOHEAD");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("Photo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BurialId)
                    .HasMaxLength(255)
                    .HasColumnName("BurialID");

                entity.Property(e => e.PhotoId)
                    .HasMaxLength(255)
                    .HasColumnName("PhotoID");

                entity.HasOne(d => d.Burial)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.BurialId)
                    .HasConstraintName("Photo$BurialsPhoto");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

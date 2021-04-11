using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Intex2.Models
{
    public partial class Burial
    {
        public Burial()
        {
            BiologicalSamples = new HashSet<BiologicalSample>();
            C14s = new HashSet<C14>();
            FieldBooks = new HashSet<FieldBook>();
            Photos = new HashSet<Photo>();
        }

        [Required]
        public string BurialId { get; set; }

        public double? BurialId2018 { get; set; }

        public double? YearOnSkull { get; set; }

        public string MonthOnSkull { get; set; }

        public string DateOnSkull { get; set; }

        public string InitialsOfDataEntryExpert { get; set; }

        public string InitialsOfDataEntryChecker { get; set; }

        public string ByuSample { get; set; }

        public string BodyAnalysis { get; set; }

        public string SkullAtMagazine { get; set; }

        public string PostcraniaAtMagazine { get; set; }

        public string AgeSkull2018 { get; set; }

        public string RackAndShelf { get; set; }

        public string ToBeConfirmed { get; set; }

        public string SkullTrauma { get; set; }

        public string PostcraniaTrauma { get; set; }

        public string CribraOrbitala { get; set; }

        public string PoroticHyperostosis { get; set; }

        public string PoroticHyperostosisLocations { get; set; }

        public string MetopicSuture { get; set; }

        public string ButtonOsteoma { get; set; }

        public string PostcraniaTrauma1 { get; set; }

        public string OsteologyUnknownComment { get; set; }

        public string TemporalMandibularJointOsteoarthritisTmjOa { get; set; }

        public string LinearHypoplasiaEnamel { get; set; }

        public double? AreaHillBurials { get; set; }

        public string Tomb { get; set; }

        [Required]
        public double? NsLowPosition { get; set; }

        [Required]
        public double? NsHighPosition { get; set; }

        [Required]
        public string NorthOrSouth { get; set; }

        [Required]
        public double? EwLowPosition { get; set; }

        [Required]
        public double? EwHighPosition { get; set; }

        [Required]
        public string EastOrWest { get; set; }

        [Required]
        public string Square { get; set; }

        [Required]
        public double? BurialNumber { get; set; }

        public double? BurialWestToHead { get; set; }

        public double? BurialWestToFeet { get; set; }

        public double? BurialSouthToHead { get; set; }

        public double? BurialSouthToFeet { get; set; }

        public double? BurialDepth { get; set; }

        public double? YearExcav { get; set; }

        public string MonthExcavated { get; set; }

        public string DateExcavated { get; set; }

        public string BurialDirection { get; set; }

        public string BurialPreservation { get; set; }

        public string BurialWrapping { get; set; }

        public string BurialAdultChild { get; set; }

        public string Sex { get; set; }

        public string GenderCode { get; set; }

        public string BurialGenderMethod { get; set; }

        public string AgeCodeSingle { get; set; }

        public string BurialDirection1 { get; set; }

        public double? NumericMinAge { get; set; }

        public double? NumericMaxAge { get; set; }

        public string BurialAgeMethod { get; set; }

        public string HairColorCode { get; set; }

        public bool? BurialSampleTaken { get; set; }

        public double? LengthM { get; set; }

        public double? LengthCm { get; set; }

        public string Goods { get; set; }

        public string Cluster { get; set; }

        public string FaceBundle { get; set; }

        public string OsteologyNotes { get; set; }

        public string OtherNotes { get; set; }

        public string SampleNumber { get; set; }

        public string GenderGe { get; set; }

        public string GeFunctionTotal { get; set; }

        public string GenderBodyCol { get; set; }

        public string BasilarSuture { get; set; }

        public string VentralArc { get; set; }

        public string SubpubicAngle { get; set; }

        public string SciaticNotch { get; set; }

        public string PubicBone { get; set; }

        public string PreaurSulcus { get; set; }

        public string MedialIpRamus { get; set; }

        public string DorsalPitting { get; set; }

        public string ForemanMagnum { get; set; }

        public string FemurHead { get; set; }

        public string HumerusHead { get; set; }

        public string Osteophytosis { get; set; }

        public string PubicSymphysis { get; set; }

        public string BoneLength { get; set; }

        public string MedialClavicle { get; set; }

        public string IliacCrest { get; set; }

        public string FemurDiameter { get; set; }

        public string Humerus { get; set; }

        public string FemurLength { get; set; }

        public string HumerusLength { get; set; }

        public string TibiaLength { get; set; }

        public string Robust { get; set; }

        public string SupraorbitalRidges { get; set; }

        public string OrbitEdge { get; set; }

        public string ParietalBossing { get; set; }

        public string Gonian { get; set; }

        public string NuchalCrest { get; set; }

        public string ZygomaticCrest { get; set; }

        public string CranialSuture { get; set; }

        public string MaximumCranialLength { get; set; }

        public string MaximumCranialBreadth { get; set; }

        public string BasionBregmaHeight { get; set; }

        public string BasionNasion { get; set; }

        public string BasionProsthionLength { get; set; }

        public string BizygomaticDiameter { get; set; }

        public string NasionProsthion { get; set; }

        public string MaximumNasalBreadth { get; set; }

        public string InterorbitalBreadth { get; set; }

        public string ArtifactsDescription { get; set; }

        public string PreservationIndex { get; set; }

        public string HairTaken { get; set; }

        public string SoftTissueTaken { get; set; }

        public string BoneTaken { get; set; }

        public string ToothTaken { get; set; }

        public string TextileTaken { get; set; }

        public string DescriptionOfTaken { get; set; }

        public string ArtifactFound { get; set; }

        public string EstimateLivingStature { get; set; }

        public string ToothAttrition { get; set; }

        public string ToothEruption { get; set; }

        public string PathologyAnomalies { get; set; }

        public string EpiphysealUnion { get; set; }

        //public byte[] SsmaTimeStamp { get; set; }

        public bool? PhotoTaken { get; set; }

        public virtual AgeCode AgeCodeSingleNavigation { get; set; }

        public virtual BurialAdultChild BurialAdultChildNavigation { get; set; }

        public virtual BurialWrapping BurialWrappingNavigation { get; set; }

        public virtual ICollection<BiologicalSample> BiologicalSamples { get; set; }

        public virtual ICollection<C14> C14s { get; set; }

        public virtual ICollection<FieldBook> FieldBooks { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }

}


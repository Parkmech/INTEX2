using System;
using System.Collections.Generic;

#nullable disable

namespace Intex2.Models
{
    public partial class Cranial
    {
        public string BurialId { get; set; }
        public double? SampleNumber { get; set; }
        public double? MaximumCranialLength { get; set; }
        public double? MaximumCranialBreadth { get; set; }
        public double? BasionBregmaHeight { get; set; }
        public double? BasionNasion { get; set; }
        public double? BasionProsthionLength { get; set; }
        public double? BizygomaticDiameter { get; set; }
        public double? NasionProsthion { get; set; }
        public double? MaximumNasalBreadth { get; set; }
        public double? InterorbitalBreadth { get; set; }
        public string BurialPositioningNorthSouthNumber { get; set; }
        public string BurialPositioningNorthSouthDirection { get; set; }
        public string BurialPositioningEastWestNumber { get; set; }
        public string BurialPositioningEastWestDirection { get; set; }
        public double? BurialNumber { get; set; }
        public double? BurialDepth { get; set; }
        public string BurialSubPlotDirection { get; set; }
        public string BurialArtifactDescription { get; set; }
        public bool? BuriedWithArtifacts { get; set; }
        public string GilesGender { get; set; }
        public string BodyGender { get; set; }
        public int Id { get; set; }
        public byte[] SsmaTimeStamp { get; set; }
    }
}

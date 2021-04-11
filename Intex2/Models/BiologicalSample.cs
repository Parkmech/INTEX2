using System;
using System.Collections.Generic;

#nullable disable

namespace Intex2.Models
{
    public partial class BiologicalSample
    {
        public string BurialId { get; set; }
        public double? Rack { get; set; }
        public string F3 { get; set; }
        public string Bag { get; set; }
        public double? LowNs { get; set; }
        public double? HighNs { get; set; }
        public string NorthOrSouth { get; set; }
        public double? LowEw { get; set; }
        public double? HighEw { get; set; }
        public string EastOrWest { get; set; }
        public double? Area { get; set; }
        public double? BurialNumber { get; set; }
        public double? ClusterNumber { get; set; }
        public double? Date { get; set; }
        public string PreviouslySampled { get; set; }
        public string Notes { get; set; }
        public string Initials { get; set; }
        public int Id { get; set; }
        //public byte[] SsmaTimeStamp { get; set; }

        public virtual Burial Burial { get; set; }
    }
}

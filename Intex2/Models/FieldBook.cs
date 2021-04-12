using System;
using System.Collections.Generic;

#nullable disable

namespace Intex2.Models
{
    public partial class FieldBook
    {
        public string BurialId { get; set; }
        public string FieldBook1 { get; set; }
        public double? FieldBookPageNumber { get; set; }
        public int Id { get; set; }
        //public byte[] SsmaTimeStamp { get; set; }

        public virtual Burial Burial { get; set; }
    }
}

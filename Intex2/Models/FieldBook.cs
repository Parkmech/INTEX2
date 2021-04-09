using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Intex2.Models
{
    public partial class FieldBook
    {
        public string BurialId { get; set; }
        public string FieldBook1 { get; set; }
        public double? FieldBookPageNumber { get; set; }
        public int Id { get; set; }
        public byte[] SsmaTimeStamp { get; set; }

        public virtual Burials Burial { get; set; }
    }
}

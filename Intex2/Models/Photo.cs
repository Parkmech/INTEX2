using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Intex2.Models
{
    public partial class Photo
    {
        public string BurialId { get; set; }
        public string PhotoId { get; set; }
        public int Id { get; set; }

        public virtual Burials Burial { get; set; }
    }
}

using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Intex2.Models
{
    public partial class BurialAdultChild
    {
        public BurialAdultChild()
        {
            Burials = new HashSet<Burials>();
        }

        public string BurialAdultChild1 { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Burials> Burials { get; set; }
    }
}

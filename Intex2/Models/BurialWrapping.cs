using System;
using System.Collections.Generic;

#nullable disable

namespace Intex2.Models
{
    public partial class BurialWrapping
    {
        public BurialWrapping()
        {
            Burials = new HashSet<Burial>();
        }

        public string BurialWrapping1 { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Burial> Burials { get; set; }
    }
}

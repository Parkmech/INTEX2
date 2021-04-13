using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

#nullable disable

namespace Intex2.Models
{
    public partial class Photo
    {
        public string BurialId { get; set; }
        public string PhotoId { get; set; }
        public int Id { get; set; }

        public virtual Burial Burial { get; set; }
    }
}

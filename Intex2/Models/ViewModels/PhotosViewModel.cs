using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models.ViewModels
{
    public class PhotosViewModel
    {
        public IEnumerable<Photo> Photos { get; set; }
        public Burial Burial { get; set; }
        public Photo Photo { get; set; }
    }
}

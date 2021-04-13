using Intex2.Models.ViewModels;
using Intex2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models
{
    public class BurialListViewModel
    {
        public IEnumerable<Burial> Burials { get; set; }
        public Burial burial { get; set; }
        public FilterItems FilterItems { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<Photo> Photos{ get; set; }
        public IEnumerable<FieldBook> FieldBooks { get; set; }
        public ImageUpload ImageUpload { get; set; }
    }
}

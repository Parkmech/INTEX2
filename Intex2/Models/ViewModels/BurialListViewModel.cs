using Intex2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models
{
    public class BurialListViewModel
    {
        public IEnumerable<Burial> Burials { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}

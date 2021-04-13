using System;
using System.Collections.Generic;

namespace Intex2.Models.ViewModels
{
    public class CarbonDatingViewModel
    {
        public IEnumerable<C14> carbDateSamples { get; set; }
        public Burial burial { get; set; }
        public C14 carbDateSample { get; set; }
    }
}

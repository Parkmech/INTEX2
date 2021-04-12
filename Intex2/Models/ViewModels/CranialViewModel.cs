using System;
using System.Collections.Generic;

namespace Intex2.Models.ViewModels
{
    public class CranialViewModel
    {
        public IEnumerable<Cranial> cranialSamples { get; set; }
        public Burial burial { get; set; }
        public Cranial cranialSample { get; set; }
    }
}

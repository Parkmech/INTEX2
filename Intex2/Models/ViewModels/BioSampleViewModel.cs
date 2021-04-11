using System;
using System.Collections.Generic;

namespace Intex2.Models.ViewModels
{
    public class BioSampleViewModel
    {
        public IEnumerable<BiologicalSample> biologicalSamples { get; set; }
        public Burial burial { get; set; }
        public BiologicalSample bioSample { get; set; }
    }
}

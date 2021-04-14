using System;
using System.Collections.Generic;

namespace Intex2.Models.ViewModels
{
    public class FieldNotesViewModel
    {
        public IEnumerable<FieldBook> fieldNotes { get; set; }
        public Burial burial { get; set; }
        public FieldBook fieldNote { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}

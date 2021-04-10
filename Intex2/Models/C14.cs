using System;
using System.Collections.Generic;

#nullable disable

namespace Intex2.Models
{
    public partial class C14
    {
        public string BurialId { get; set; }
        public double? Rack { get; set; }
        public double? NS { get; set; }
        public string F4 { get; set; }
        public double? EW { get; set; }
        public string F6 { get; set; }
        public string Square { get; set; }
        public double? Area { get; set; }
        public double? Burial { get; set; }
        public double? Rack1 { get; set; }
        public double? Tube { get; set; }
        public string Description { get; set; }
        public double? SizeMl { get; set; }
        public double? Foci { get; set; }
        public double? C14Sample2017 { get; set; }
        public string Location { get; set; }
        public string QuestionS { get; set; }
        public double? F18 { get; set; }
        public double? Conventional14cAgeBp { get; set; }
        public double? _14cCalendarDate { get; set; }
        public double? Calibrated95CalendarDateMax { get; set; }
        public double? Calibrated95CalendarDateMin { get; set; }
        public double? Calibrated95CalendarDateSpan { get; set; }
        public string Calibrated95CalendarDateAvg { get; set; }
        public string Category { get; set; }
        public string Notes { get; set; }
        public string F27 { get; set; }
        public int Id { get; set; }
        public byte[] SsmaTimeStamp { get; set; }

        public virtual Burial BurialNavigation { get; set; }
    }
}

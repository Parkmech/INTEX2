using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models
{
    public class Filters
    {
        public Filters (string filterstring)
        {
            FilterString = filterstring ?? "all-all-all";
            string[] filters = FilterString.Split('-');
            Sex = filters[0];
            Area = filters[1];
            //Length = filters[2];
            //Depth = filters[3];

        }

        public string FilterString { get; }
        public string Sex { get; }
        public string Area { get; }
        public double? Length { get; }
        public double? Depth { get; }

        public bool HasSex => Sex.ToLower() != "all";
        public bool HasArea => Area.ToLower() != "all";
        //public bool HasLength => Length.ToLower() != "all";
        //public bool HasDepth => Depth.ToLower() != "all";

    }
}

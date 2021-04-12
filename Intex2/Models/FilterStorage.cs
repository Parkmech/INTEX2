using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models
{
    public static class FilterStorage
    {
        private static List<Burial> burials = new List<Burial>();
        public static IEnumerable<Burial> Burials => burials;

        

    }
}

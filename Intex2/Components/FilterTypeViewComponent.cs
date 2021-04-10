using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intex2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Intex2.Components
{
    public class FilterTypeViewComponent : ViewComponent
    {
        private FagElGamousContext _context;

        private FilterItems _filtered;

        public FilterTypeViewComponent(FagElGamousContext context, FilterItems ft)
        {
            _context = context;
            _filtered = ft;
        }

        public IViewComponentResult Invoke()
        {
            return View(_filtered);
        }
    }
}

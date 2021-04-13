using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models
{
    public class ImageUpload
    {
        public IFormFile file { get; set; }
        public string PhotoName { get; set; }
        public string BurialId { get; set; }

    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Services
{
    public interface IS3Service
    {
        Task<string> AddItem(IFormFile file, string ReaderName);
    }
}

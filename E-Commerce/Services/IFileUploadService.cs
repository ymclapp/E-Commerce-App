using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public interface IFileUploadService
    {
        Task<string> Upload ( IFormFile file );

    }
}

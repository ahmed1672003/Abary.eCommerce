using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Presentation.Features.Photo.Upload;

internal class UploadPhotoRequest
{
    public IFormFile Photo { get; set; }
}

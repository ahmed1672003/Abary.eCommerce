using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Presentation.Features.Photo.Upload;

internal class UploadPhotoEndpoint : Endpoint<UploadPhotoRequest, Response>
{
    public override void Configure()
    {
        Post($"{nameof(ModuleName)}/{nameof(FeatureName.Photo)}/{nameof(Upload)}");
        AllowFileUploads();
        Permissions();
    }

    public override async Task HandleAsync(UploadPhotoRequest req, CancellationToken ct)
    {
        Response = new Response { IsSuccess = true, Message = "operation done successfully", };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Presentation.Features.Files.Upload;

internal class UploadFileEndpoint : Endpoint<UploadFileRequest, Response>
{
    public override void Configure()
    {
        Post($"{nameof(ModuleName)}/{nameof(FeatureName.File)}/{nameof(Upload)}");
        AllowFileUploads();
        Permissions();
    }

    public override async Task HandleAsync(UploadFileRequest req, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}

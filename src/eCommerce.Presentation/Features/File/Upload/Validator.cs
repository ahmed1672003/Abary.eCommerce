using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Presentation.Features.Files.Upload;

internal class UploadFileValidator : Validator<UploadFileRequest>
{
    public UploadFileValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .Must(req =>
            {
                return req.File.Length >= (1024 * 5);
            })
            .WithMessage("max size 5mb");

        RuleFor(x => x)
            .Must(req =>
            {
                var fileExtension = Enum.GetNames(typeof(FileExtensions)).ToList();
                // var photoExtension = Enum.GetNames(typeof(PhotoExtensions)).ToList();

                var extension = Path.GetExtension(req.File.FileName.ToLower());
                if (string.IsNullOrEmpty(extension))
                    return false;

                extension = extension.Remove(extension.LastIndexOf('.'), 1);

                if (fileExtension.Contains(extension))
                    return true;

                return false;
            })
            .WithMessage("invalid file extension");
    }
}

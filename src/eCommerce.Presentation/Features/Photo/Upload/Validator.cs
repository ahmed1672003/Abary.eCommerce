using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Presentation.Features.Photo.Upload;

internal class UploadPhotoValidator : Validator<UploadPhotoRequest>
{
    public UploadPhotoValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .Must(req =>
            {
                return req.Photo.Length >= (1024 * 2);
            })
            .WithMessage("max size 2mb");

        RuleFor(x => x)
            .Must(req =>
            {
                var photoExtension = Enum.GetNames(typeof(PhotoExtensions)).ToList();

                var extension = Path.GetExtension(req.Photo.FileName.ToLower());
                if (string.IsNullOrEmpty(extension))
                    return false;

                extension = extension.Remove(extension.LastIndexOf('.'), 1);

                if (photoExtension.Contains(extension))
                    return true;

                return false;
            })
            .WithMessage("invalid photo extension");
    }
}

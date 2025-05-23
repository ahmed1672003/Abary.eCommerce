﻿namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.ChangePassword;

public class ChangePasswordValidator : Validator<ChangePasswordRequest>
{
    public ChangePasswordValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.OldPassword)
            .NotNull()
            .WithMessage("كلمة المرور القديمة مطلوب")
            .NotEmpty()
            .WithMessage("كلمة المرور القديمة مطلوب");

        RuleFor(x => x.NewPassword)
            .NotNull()
            .WithMessage("كلمة المرور الجديدة مطلوب")
            .NotEmpty()
            .WithMessage("كلمة المرور الجديدة مطلوب");

        RuleFor(x => x.ConfirmNewPassword)
            .NotNull()
            .WithMessage("تأكيد كلمة المرور الجديدة مطلوب")
            .NotEmpty()
            .WithMessage("تأكيد كلمة المرور الجديدة مطلوب");

        RuleFor(x => x.OldPassword)
            .NotEqual(x => x.NewPassword)
            .WithMessage("لايمكن التغيير بكلمة المرور القديمة");

        RuleFor(x => x).Must(IsPasswordValid).WithMessage("كلمة المرور الجديدة غير صالحة");

        RuleFor(x => x.NewPassword)
            .Matches(x => x.ConfirmNewPassword)
            .WithMessage("كلمتا المرور غير متطابقتان");

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    var userId = Resolve<IHttpContextAccessor>()
                        .HttpContext.User.FindFirstValue(nameof(CustomeClaimTypes.UserId));

                    var userManager = Resolve<UserManager<User>>();

                    var user = await userManager
                        .Users.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == Guid.Parse(userId));

                    return !await userManager.CheckPasswordAsync(user, req.NewPassword)
                        && await userManager.CheckPasswordAsync(user, req.OldPassword);
                }
            )
            .WithMessage("كلمة المرور خاطئة");

        bool IsPasswordValid(ChangePasswordRequest request)
        {
            return request.NewPassword.Count(x => char.IsNumber(x)) >= 1
                && request.NewPassword.Count(x => char.IsUpper(x)) >= 1
                //&& request.Password.Count(x => char.IsSymbol(x)) >= 1
                && request.NewPassword.Count(x => char.IsLower(x)) >= 1;
        }
    }
}

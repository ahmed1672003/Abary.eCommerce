namespace eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.GetAll;

internal sealed class GetAllCategoriesValidator : Validator<GetAllCategoriesRequest>
{
    public GetAllCategoriesValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;
    }
}

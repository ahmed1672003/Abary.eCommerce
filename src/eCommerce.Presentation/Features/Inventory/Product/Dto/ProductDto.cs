using eCommerce.Domain.Bases.Dto;

namespace eCommerce.Presentation.Features.Inventory.Products.Dto;

public record ProductDto : BaseDto<Guid>
{
    public string Name { get; set; }
    public double PurchasePrice { get; set; }
    public double SellingPrice { get; set; }
    public double Discount { get; set; } = 0;
    public double Tax { get; set; } = 0;
    public bool IsTaxPercentage { get; set; }
    public bool IsDiscountPercentage { get; set; }
    public bool AllowDiscount { get; set; }
    public bool AllowTax { get; set; }
    public UnitDto Unit { get; set; }
    public List<string> PhotoUrls { get; set; }
    public List<CategoryDto> Categories { get; set; }
    public List<StockDto> Stocks { get; set; }

    public record UnitDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public record StockDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public string Address { get; set; }
    }

    public record CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

public record CreateProductRequest
{
    public string Name { get; set; }
    public double SellingPrice { get; set; }
    public double PurchasePrice { get; set; }
    public double Discount { get; set; } = 0;
    public double Tax { get; set; } = 0;
    public bool IsTaxPercentage { get; set; }
    public bool IsDiscountPercentage { get; set; }
    public bool AllowDiscount { get; set; }
    public bool AllowTax { get; set; }
    public List<Guid> CategoryIds { get; set; }
    public List<StockDto> Stock { get; set; }

    public record StockDto
    {
        public Guid StockId { get; set; }
        public int Qty { get; set; }
    }
}

public record UpdateProductRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Guid> Categories { get; set; }
    public double SellingPrice { get; set; }
    public double PurchasePrice { get; set; }
    public double Discount { get; set; } = 0;
    public double Tax { get; set; } = 0;
    public bool IsTaxPercentage { get; set; }
    public bool IsDiscountPercentage { get; set; }
    public bool AllowDiscount { get; set; }
    public bool AllowTax { get; set; }

    public record StockDto
    {
        public Guid StockId { get; set; }
        public int Qty { get; set; }
    }
}

public record DeleteProductRequest
{
    [FromHeader]
    public Guid Id { get; set; }
}

public record GetProductRequest
{
    public Guid Id { get; set; }
}

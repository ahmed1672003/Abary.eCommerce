using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Entities.Inventory;

public sealed class Vendor
    : BaseEntity<Guid>,
        ITrackableCreate<Guid>,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>,
        ISoftDeleteable
{
    public string Name { get; set; }
    public string Photo { get; set; }
    public string Comment { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public string Address { get; set; }

    public DateTime CreatedOn { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public Guid DeletedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public Guid UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }

    public List<Product> Products { get; set; }
}

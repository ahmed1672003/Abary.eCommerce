using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Entities.Inventory;

public class Deal
    : BaseEntity<Guid>,
        ITrackableCreate<Guid>,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>,
        ISoftDeleteable
{
    public string ContactName { get; set; }
    public string Name { get; set; }

    public decimal Value { get; set; }
    public string Currency { get; set; }
    public string Description { get; set; }
    public string Comment { get; set; }
    public string Product { get; set; }

    public DateTime CreatedOn { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public Guid DeletedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public Guid UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}

﻿namespace eCommerce.Persistence.Configs.Identity;

internal sealed class RoleClaimConfig : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable(EntityName.RoleClaim.ToString(), ModuleName.Identity.ToString());
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}

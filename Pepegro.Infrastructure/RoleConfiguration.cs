namespace Infrastructure;

using Domain.Entities.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
            new Role
            {
                Id = 2,
                Name = "User",
                NormalizedName = "USER"
            },
            new Role
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new Role
            {
                Id = 3,
                Name = "Seller",
                NormalizedName = "SELLER"
            }
        );
    }
}
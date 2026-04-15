using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MultimodalShippingSystem.Data;

public class ShippingDbContextFactory : IDesignTimeDbContextFactory<ShippingDbContext>
{
    public ShippingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ShippingDbContext>();
        optionsBuilder.UseSqlServer(ShippingDbContext.ResolveDesignTimeConnectionString());

        return new ShippingDbContext(optionsBuilder.Options);
    }
}

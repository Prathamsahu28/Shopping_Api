using Zero.AspNetCoreServiceProjectExample.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zero.AspNetCoreServiceProjectExample.Domain.OrderAggregate;

namespace Zero.AspNetCoreServiceProjectExample.Data.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.ItemId);

            builder.Property(x => x.ItemName);
                
            builder.Property(m => m.Quantity);

            builder.Property(m => m.Price);




        }
    }
}

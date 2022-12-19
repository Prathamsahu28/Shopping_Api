using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Api.Domain;
using Shopping_Api.Domain.OrderAggregate;
using Zero.Shopping_Api.Domain.ProductAggregate;

namespace Shopping_Api.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            //throw new NotImplementedException();

            builder.HasKey(x => x.OrderItemId);

            //builder.HasMany<OrderItem>(g => g.ProductId);



            builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(m => m.ProductId);

            
            builder.Property(m => m.Quantity)
                .HasConversion(x => x.Value, x => Quantity.Create(x).Value); ;

            builder.Property(m => m.Price);

            //builder.HasOne<OrderItem>(m => m.Quantity)
            //.WithMany();
            //.HasForeignKey(m => m.ProductId);
            //builder.Property();



        }

    }
}

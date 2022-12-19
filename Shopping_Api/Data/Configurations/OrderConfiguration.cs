using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Api.Domain;
using Shopping_Api.Domain.OrderAggregate;
using Zero.Shopping_Api.Domain;
using Zero.Shopping_Api.Domain.ProductAggregate;

namespace Shopping_Api.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //throw new NotImplementedException();

            builder.HasKey(x => x.OrderId);
            builder.Property(x => x.OrderDate);

            builder.HasOne<Customer>()
             .WithMany()
             .HasForeignKey(m => m.CustomerId);

            builder.Property(t => t.NumberOfItems);
            builder.Property(x => x.TotalPrice);

            //builder.HasMany(x => x.OrderProducts)
            //WithOne();

            //;
            //builder.Property(m => m.OrderProducts);


            // builder.HasOne<Order>()
            //.HasOne(p => p.OrderProducts)
            //.HasMany(p=>p.Order);


            //builder.HasOne<Order>()
            //.Property(g => g.ProductId);

            //builder.Entity<OrderItem>()
            //.HasOne(pt => pt.OrderProducts)
            // .WithMany(p => p.LineItems)
            // .HasForeignKey(pt => pt.ProductId);

            //builder.HasOne<Order>()
            //.HasMany<OrderProducts>(g => g.Students)
            //.WithOne(s => s.OrderProducts)

            //.HasMany<OrderProducts>(g => g.Students)
            //.HasForeignKey(s => s.CurrentGradeId);


            // builder.Entity<Order>()
            //.Property(g => g.OrderProducts).IsRequired();
            //.HasColumnType("decimal(18,2)");
            //builder.Property(m => m.OrderProducts);


            //builder.Property(m => m._items);


            // builder.Property(m => m.Quantity);

            //builder.HasOne<Order>()

            //WithMany()
            //.HasForeignKey(m => m.ProductId);


            //builder.HasOne<Order>()
            //.WithMany();
            // builder.Property(m => m.Quantity)

            //builder.Property(m => m.Quantity)
            //.HasConversion(x => x.Value, x => Quantity.Create(x).Value);





        }
    }
}

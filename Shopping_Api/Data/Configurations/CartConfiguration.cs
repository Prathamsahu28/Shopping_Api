using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Api.Domain;
using Shopping_Api.Domain.ProductAggregate;
using Shopping_Api.Domain.ShoppingCartAggregate;
using Zero.Shopping_Api.Domain;
using Zero.Shopping_Api.Domain.ProductAggregate;

namespace Shopping_Api.Data.Configurations
{

    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            //builder.Entity<Customer>()
            //.HasMany<ShoppingCart>(g => g.CustomerId)
            //.WithOne(s => s.Grade)
            // .HasForeignKey(s => s.CurrentGradeId);

           // builder.HasKey(x => x.CartId);


            builder.HasKey(x => new { x.CustomerId, x.ProductId });

            builder.HasOne<Customer>()
              .WithMany()
              .HasForeignKey(m => m.CustomerId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Product>()
               .WithMany()
               .HasForeignKey(m => m.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.ProductName)
              .HasMaxLength(100)
              .IsUnicode(true)
              .IsRequired();

            builder.Property(x => x.Quantity)
                 .HasConversion(x => x.Value, x => Quantity.Create(x).Value);

            builder.Property(m => m.Price);
                



            //builder.HasKey(x => x.CustomerId);

            //builder.HasOne<Product>()
            //   .WithMany()
            //   .HasForeignKey(m => m.ProductId)

            //   .OnDelete(DeleteBehavior.Restrict);


            ////builder.Property(x => x.ProductName)
            //    .HasConversion(x => x.Value, x => ProductName.Create(x).Value);

            //builder.Property(x => x.Quantity)
            //    .HasConversion(x => x.Value, x => Quantity.Create(x).Value);

            //builder.Property(x=>x.Price) ;


        }
    }
}

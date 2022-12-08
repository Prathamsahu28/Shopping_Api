using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Api.Domain.ShoppingCartAggregate;
using Zero.Shopping_Api.Domain;
using Zero.Shopping_Api.Domain.ItemAggregate;

namespace Shopping_Api.Data.Configurations
{

    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            //builder.Entity<Customer>()
            //.HasMany<ShoppingCart>(g => g.CustomerId)
            //.WithOne(s => s.Grade)
            // .HasForeignKey(s => s.CurrentGradeId);
            builder.HasKey(x => x.CartId);

            builder.HasOne<Customer>()
               .WithMany()
               .HasForeignKey(m => m.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);


            //builder.HasKey(x => x.CustomerId);

            builder.HasOne<Item>()
               .WithMany()
               .HasForeignKey(m => m.ItemId)
               .OnDelete(DeleteBehavior.Restrict);


            builder.Property(x => x.ItemName);
            builder.Property(x => x.Quantity);

            builder.Property(x=>x.Price) ;


        }
    }
}

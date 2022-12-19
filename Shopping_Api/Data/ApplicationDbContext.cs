using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping_Api.Domain.OrderAggregate;
using System.Reflection;
using Zero.EFCoreSpecification;

namespace Zero.Shopping_Api.Data
{
    public class ApplicationDbContext : DbContextBase<ApplicationDbContext>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options, mediator) { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
      // public DbSet<Order> order { get; set; }
    }
}

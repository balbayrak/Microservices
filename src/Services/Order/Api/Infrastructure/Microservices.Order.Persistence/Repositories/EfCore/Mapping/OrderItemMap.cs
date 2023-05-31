using Microservices.Order.Domain.Models;
using Microservices.Persistence.Repository.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservices.Order.Persistence.Repositories.EfCore.Mapping
{
    public class OrderItemMap : BaseEntityMap<OrderItem>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);

            builder.ToTable("OrderItems");

            builder.Property(p => p.OrderId).IsRequired();

            builder.Property(p => p.ProductId).IsRequired();

            builder.Property(p => p.Price).HasPrecision(18, 2);

            builder.HasOne(p => p.Order)
                .WithMany(p => p.Items)
                .HasForeignKey(p => p.OrderId);

        }
    }
}

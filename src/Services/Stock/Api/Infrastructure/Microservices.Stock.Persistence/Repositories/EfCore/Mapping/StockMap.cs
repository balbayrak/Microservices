using Microservices.Persistence.Repository.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Stock.Persistence.Repositories.EfCore.Mapping
{
    public class StockMap : BaseEntityMap<Domain.Models.Stock>
    {
        public override void Configure(EntityTypeBuilder<Domain.Models.Stock> builder)
        {
            base.Configure(builder);

            builder.ToTable("Stocks");

            builder.Property(e => e.AvailableStock)
                .IsRequired()
                .HasColumnName("AvailableStock")
                .HasDefaultValue(0);

            builder.Property(e => e.ProductId)
                .IsRequired()
                .HasColumnName("ProductId");

            builder.Property(e => e.StockMotionId)
                .IsRequired()
                .HasColumnName("StockMotionId");
        }
    }
}

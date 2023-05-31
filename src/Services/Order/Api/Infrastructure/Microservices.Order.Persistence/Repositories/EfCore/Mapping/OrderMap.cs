using Microservices.Persistence.Repository.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Order.Persistence.Repositories.EfCore.Mapping
{
    public class OrderMap : BaseEntityMap<Domain.Models.Order>
    {
        public override void Configure(EntityTypeBuilder<Domain.Models.Order> builder)
        {
            base.Configure(builder);

            builder.ToTable("Orders");

            builder.OwnsOne(e => e.Address);

        }
    }
}

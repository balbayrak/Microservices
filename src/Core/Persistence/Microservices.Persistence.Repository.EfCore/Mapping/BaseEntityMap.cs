using Microservices.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Persistence.Repository.EfCore.Mapping
{
    public abstract class BaseEntityMap<T> : IEntityTypeConfiguration<T>
        where T : class, IEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasColumnName("Id").ValueGeneratedOnAdd();

            builder.Property(t => t.LastModificationDate).HasColumnName("LastModificationDate").IsRequired().ValueGeneratedOnUpdate();

            builder.Property(t => t.CreationDate).HasColumnName("CreationDate").IsRequired().ValueGeneratedOnAdd();

            builder.Property(t => t.IsDeleted).HasColumnName("IsDeleted").IsRequired().HasDefaultValue(false);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Domain
{
    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModificationDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}

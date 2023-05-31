using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Domain
{
    public interface IEntity
    {
        Guid Id { get; set; }

        DateTime CreationDate { get; set; }

        DateTime LastModificationDate { get; set; }

        bool IsDeleted { get; set; }
    }
}

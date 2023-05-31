using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Product.Application.Exceptions.Category
{
    public class CategoryNameEmptyException : ValidationException
    {
        public CategoryNameEmptyException() : base(ExceptionMessages.CATEGORY_NAME_EMPTY)
        {
        }
    }
}

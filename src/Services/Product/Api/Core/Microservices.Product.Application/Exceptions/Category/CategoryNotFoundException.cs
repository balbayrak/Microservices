using Microservices.Application.Exceptions;

namespace Microservices.Product.Application.Exceptions.Category
{
    public class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException() : base(ExceptionMessages.CATEGORY_NOTFOUND)
        {
        }
    }
}

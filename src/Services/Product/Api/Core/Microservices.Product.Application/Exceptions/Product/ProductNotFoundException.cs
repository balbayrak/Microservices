using Microservices.Application.Exceptions;

namespace Microservices.Product.Application.Exceptions.Product
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException() : base(ExceptionMessages.PRODUCT_NOTFOUND)
        {
        }
    }
}

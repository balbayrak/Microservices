using Microservices.Application.Exceptions;

namespace Microservices.Product.Application.Exceptions.Product
{
    public class ProductCodeEmptyException : ValidationException
    {
        public ProductCodeEmptyException() : base(ExceptionMessages.PRODUCT_CODE_EMPTY)
        {
        }
    }
}

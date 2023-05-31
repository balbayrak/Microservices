using Microservices.Application.Exceptions;

namespace Microservices.Product.Application.Exceptions.Product
{
    public class ProductCodeAlreadyExistException : ConflictException
    {
        public ProductCodeAlreadyExistException(string productCode) : base(string.Format(ExceptionMessages.PRODUCT_CODE_ALREADY_EXIST, productCode))
        {
        }
    }
}

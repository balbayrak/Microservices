using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Product.Application.Exceptions
{
    public class ExceptionMessages
    {
        public const string PRODUCT_CREATEDTO_NOT_EMPTY = "createProductDto shouldn't be empty";
        public const string PRODUCT_UPDATEDTO_NOT_EMPTY = "updateProductDto shouldn't be empty";
        public const string PRODUCT_CODE_EMPTY = "Product code shouldn't be empty";
        public const string PRODUCT_NAME_EMPTY = "Product code shouldn't be empty";
        public const string PRODUCT_NOTFOUND = "Product not found";
        public const string PRODUCT_CODE_ALREADY_EXIST = "{0} productCode already exist";

        public const string CATEGORY_DTO_NOT_EMPTY = "createCategoryDto shouldn't be empty";
        public const string CATEGORY_NAME_EMPTY = "Category code shouldn't be empty";
        public const string CATEGORY_NOTFOUND = "Category not found";
    }
}

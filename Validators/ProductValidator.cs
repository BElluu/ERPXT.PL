using ERPXTpl.Models;
using ERPXTpl.Resources;
using System;

namespace ERPXTpl.Validators
{
    internal static class ProductValidator
    {

        internal static string DeleteAndGetProductValidator(int productId)
        {
            if (productId == 0)
            {
                return ValidatorMessage.PRODUCT_ID_VALIDATE;
            }
            return "";
        }
        internal static string AddProductValidator(Product product)
        {
            if (product == null)
            {
                return ValidatorMessage.PRODUCT_OBJECT_VALIDATE;
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                return ValidatorMessage.PRODUCT_NAME_VALIDATE;
            }

            if (string.IsNullOrEmpty(product.UnitOfMeasurment))
            {
                return ValidatorMessage.PRODUCT_UNIT_VALIDATE;
            }
            return "";
        }

        internal static string ModifyProductValidator(Product product)
        {
            if (product == null)
            {
                return ValidatorMessage.PRODUCT_OBJECT_VALIDATE;
            }

            if (product.Id == 0)
            {
                return ValidatorMessage.PRODUCT_ID_VALIDATE;
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                return ValidatorMessage.PRODUCT_NAME_VALIDATE;
            }

            if (string.IsNullOrEmpty(product.UnitOfMeasurment))
            {
                return ValidatorMessage.PRODUCT_UNIT_VALIDATE;
            }
            return "";
        }
    }
}

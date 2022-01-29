using ERPXTpl.Models;

namespace ERPXTpl.Validators
{
    internal static class ProductValidator
    {

        internal static string DeleteAndGetProductValidator(int productId)
        {
            if (productId == 0)
            {
                return "Product Id cannot be zero";
            }
            return "";
        }
        internal static string AddProductValidator(Product product)
        {
            if (product == null)
            {
                return "Product object cannot be null";
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                return "Product Name cannot be null or empty";
            }

            if (string.IsNullOrEmpty(product.UnitOfMeasurment))
            {
                return "Product UnitOfMeasurment cannot be null or empty";
            }
            return "";
        }

        internal static string ModifyProductValidator(Product product)
        {
            if (product == null)
            {
                return "Product object cannot be null";
            }

            if (product.Id == 0)
            {
                return "Product Id cannot be zero";
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                return "Product Name cannot be null or empty";
            }

            if (string.IsNullOrEmpty(product.UnitOfMeasurment))
            {
                return "Product UnitOfMeasurment cannot be null or empty";
            }
            return "";
        }
    }
}

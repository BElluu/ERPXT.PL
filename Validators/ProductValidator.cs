using ERPXTpl.Models;
using System;

namespace ERPXTpl.Validators
{
    internal static class ProductValidator
    {

        internal static void DeleteAndGetProductValidator(int productId)
        {
            if (productId == 0)
            {
                throw new ArgumentException("Id cannot be zero");
            }
        }
        internal static void AddProductValidator(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Product cannot be null");
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                throw new ArgumentException("Name cannot be null or empty");
            }

            if (string.IsNullOrEmpty(product.UnitOfMeasurment))
            {
                throw new ArgumentException("UnitOfMeasurment cannot be null or empty");
            }
        }

        internal static void ModifyProductValidator(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Product cannot be null");
            }

            if (product.Id == 0)
            {
                throw new ArgumentException("Id cannot be zero");
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                throw new ArgumentException("Name cannot be null or empty");
            }

            if (string.IsNullOrEmpty(product.UnitOfMeasurment))
            {
                throw new ArgumentException("UnitOfMeasurment cannot be null or empty");
            }
        }
    }
}

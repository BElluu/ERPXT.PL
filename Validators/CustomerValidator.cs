using ERPXTpl.Models;
using System;

namespace ERPXTpl.Validators
{
    internal static class CustomerValidator
    {
        internal static void DeleteAndGetCustomerByIdValidator(long value)
        {
            if (value == 0)
            {
                throw new ArgumentException("Id cannot be zero");
            }
        }

        internal static void GetCustomerByTINValidator(long value)
        {
            if (value == 0)
            {
                throw new ArgumentException("TIN or PESEL cannot be zero");
            }
        }

        internal static void PostCustomerValidator(Customer customer)
        {
            if(customer == null)
            {
                throw new ArgumentNullException("Customer cannot be null");
            }

            if (string.IsNullOrEmpty(customer.Name))
            {
                throw new ArgumentException("Name cannot be null or empty");
            }

            if (customer.CustomerStatus == Enums.CustomerStatusEnum.INTRA_EU && 
                customer.CustomerStatus == Enums.CustomerStatusEnum.TRILATERAL_INTRA_EU &&
                customer.CustomerStatus == Enums.CustomerStatusEnum.OSS_PROCEDURE)
            {
                if (string.IsNullOrEmpty(customer.CountryCode))
                {
                    throw new ArgumentException("The country code is mandatory for intra-EU and the OSS procedure customers");
                }
            }
        }

        internal static void ModifyCustomerValidator(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("Customer cannot be null");
            }

            if (customer.Id == 0)
            {
                throw new ArgumentException("Id cannot be zero");
            }

            if (string.IsNullOrEmpty(customer.Name))
            {
                throw new ArgumentException("Name cannot be null or empty");
            }

            if (customer.CustomerStatus == Enums.CustomerStatusEnum.INTRA_EU &&
                customer.CustomerStatus == Enums.CustomerStatusEnum.TRILATERAL_INTRA_EU &&
                customer.CustomerStatus == Enums.CustomerStatusEnum.OSS_PROCEDURE)
            {
                if (string.IsNullOrEmpty(customer.CountryCode))
                {
                    throw new ArgumentException("The country code is mandatory for intra-EU and the OSS procedure customers");
                }
            }
        }
    }
}

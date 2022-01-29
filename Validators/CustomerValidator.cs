using ERPXTpl.Models;

namespace ERPXTpl.Validators
{
    internal static class CustomerValidator
    {
        internal static string DeleteAndGetCustomerByIdValidator(long value)
        {
            if (value == 0)
            {
                return "Customer Id cannot be zero or empty";
            }
            return "";
        }

        internal static string GetCustomerByTINValidator(long value)
        {
            if (value == 0)
            {
                return "Customer TIN cannot be zero or empty";
            }
            return "";
        }

        internal static string PostCustomerValidator(Customer customer)
        {
            if(customer == null)
            {
                return "Customer object cannot be null";
            }

            if (string.IsNullOrEmpty(customer.Name))
            {
                return "Customer Name cannot be null or empty";
            }

            if (customer.CustomerStatus == Enums.CustomerStatusEnum.INTRA_EU || 
                customer.CustomerStatus == Enums.CustomerStatusEnum.TRILATERAL_INTRA_EU ||
                customer.CustomerStatus == Enums.CustomerStatusEnum.OSS_PROCEDURE)
            {
                if (string.IsNullOrEmpty(customer.CountryCode))
                {
                    return "The country code is mandatory for intra-EU and the OSS procedure customers";
                }
            }
            return "";
        }

        internal static string ModifyCustomerValidator(Customer customer)
        {
            if (customer == null)
            {
                return "Customer object cannot be null";
            }

            if (customer.Id == 0)
            {
                return "Customer Id cannot be zero or empty";
            }

            if (string.IsNullOrEmpty(customer.Name))
            {
                return "Customer Name cannot be null or empty";
            }

            if ((customer.CustomerStatus == Enums.CustomerStatusEnum.INTRA_EU ||
                customer.CustomerStatus == Enums.CustomerStatusEnum.TRILATERAL_INTRA_EU ||
                customer.CustomerStatus == Enums.CustomerStatusEnum.OSS_PROCEDURE) &&
                string.IsNullOrEmpty(customer.CountryCode))
            {
                    return "The country code is mandatory for intra-EU and the OSS procedure customers";
            }
            return "";
        }
    }
}

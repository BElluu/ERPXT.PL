using ERPXTpl.Model;
using ERPXTpl.Resources;

namespace ERPXTpl.Validator
{
    internal static class CustomerValidator
    {
        internal static string DeleteAndGetCustomerByIdValidator(long value)
        {
            if (value == 0)
            {
                return ValidatorMessage.CUSTOMER_ID_VALIDATE;
            }
            return string.Empty;
        }

        internal static string GetCustomerByTINValidator(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return ValidatorMessage.CUSTOMER_TIN_VALIDATE;
            }
            return string.Empty;
        }

        internal static string PostCustomerValidator(Customer customer)
        {
            if (customer == null)
            {
                return ValidatorMessage.CUSTOMER_OBJECT_VALIDATE;
            }

            if (string.IsNullOrEmpty(customer.Name))
            {
                return ValidatorMessage.CUSTOMER_NAME_VALIDATE;
            }

            if (customer.CustomerStatus == Enum.CustomerStatus.INTRA_EU ||
                customer.CustomerStatus == Enum.CustomerStatus.TRILATERAL_INTRA_EU ||
                customer.CustomerStatus == Enum.CustomerStatus.OSS_PROCEDURE)
            {
                if (string.IsNullOrEmpty(customer.CountryCode))
                {
                    return ValidatorMessage.CUSTOMER_COUNTRY_CODE_VALIDATE;
                }
            }
            return string.Empty;
        }

        internal static string ModifyCustomerValidator(Customer customer)
        {
            if (customer == null)
            {
                return ValidatorMessage.CUSTOMER_OBJECT_VALIDATE;
            }

            if (customer.Id == 0)
            {
                return ValidatorMessage.CUSTOMER_ID_VALIDATE;
            }

            if (string.IsNullOrEmpty(customer.Name))
            {
                return ValidatorMessage.CUSTOMER_NAME_VALIDATE;
            }

            if ((customer.CustomerStatus == Enum.CustomerStatus.INTRA_EU ||
                customer.CustomerStatus == Enum.CustomerStatus.TRILATERAL_INTRA_EU ||
                customer.CustomerStatus == Enum.CustomerStatus.OSS_PROCEDURE) &&
                string.IsNullOrEmpty(customer.CountryCode))
            {
                return ValidatorMessage.CUSTOMER_COUNTRY_CODE_VALIDATE;
            }
            return string.Empty;
        }
    }
}

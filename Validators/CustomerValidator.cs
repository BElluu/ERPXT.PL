using ERPXTpl.Models;
using ERPXTpl.Resources;

namespace ERPXTpl.Validators
{
    internal static class CustomerValidator
    {
        internal static string DeleteAndGetCustomerByIdValidator(long value)
        {
            if (value == 0)
            {
                return ValidatorMessage.CUSTOMER_ID_VALIDATE;
            }
            return "";
        }

        internal static string GetCustomerByTINValidator(long value)
        {
            if (value == 0)
            {
                return ValidatorMessage.CUSTOMER_TIN_VALIDATE;
            }
            return "";
        }

        internal static string PostCustomerValidator(Customer customer)
        {
            if(customer == null)
            {
                return ValidatorMessage.CUSTOMER_OBJECT_VALIDATE;
            }

            if (string.IsNullOrEmpty(customer.Name))
            {
                return ValidatorMessage.CUSTOMER_NAME_VALIDATE;
            }

            if (customer.CustomerStatus == Enums.CustomerStatusEnum.INTRA_EU || 
                customer.CustomerStatus == Enums.CustomerStatusEnum.TRILATERAL_INTRA_EU ||
                customer.CustomerStatus == Enums.CustomerStatusEnum.OSS_PROCEDURE)
            {
                if (string.IsNullOrEmpty(customer.CountryCode))
                {
                    return ValidatorMessage.CUSTOMER_COUNTRY_CODE_VALIDATE;
                }
            }
            return "";
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

            if ((customer.CustomerStatus == Enums.CustomerStatusEnum.INTRA_EU ||
                customer.CustomerStatus == Enums.CustomerStatusEnum.TRILATERAL_INTRA_EU ||
                customer.CustomerStatus == Enums.CustomerStatusEnum.OSS_PROCEDURE) &&
                string.IsNullOrEmpty(customer.CountryCode))
            {
                    return ValidatorMessage.CUSTOMER_COUNTRY_CODE_VALIDATE;
            }
            return "";
        }
    }
}

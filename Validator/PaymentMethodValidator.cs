using ERPXTpl.Model;
using ERPXTpl.Resources;

namespace ERPXTpl.Validator
{
    internal class PaymentMethodValidator
    {
        internal static string GetPaymentMethodById(long paymentMethodId)
        {
            if (paymentMethodId == 0)
            {
                return ValidatorMessage.PAYMENT_METHOD_ID_VALIDATE;
            }
            return string.Empty;
        }

        internal static string AddPaymentMethodValidator(PaymentMethod paymentMethod)
        {
            if (paymentMethod == null)
            {
                return ValidatorMessage.PAYMENT_METHOD_OBJECT_VALIDATE;
            }

            if (string.IsNullOrEmpty(paymentMethod.Name))
            {
                return ValidatorMessage.PAYMENT_METHOD_NAME_VALIDATE;
            }

            if (paymentMethod.Deadline > 0 && paymentMethod.Deadline <= 366)
            {
                return ValidatorMessage.PAYMENT_METHOD_DEADLINE_VALIDATE;
            }
            if (paymentMethod.Type == null)
            {
                return ValidatorMessage.PAYMENT_METHOD_TYPE_VALIDATE;
            }
            return string.Empty;
        }
    }
}

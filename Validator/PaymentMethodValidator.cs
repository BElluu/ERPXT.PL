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
    }
}

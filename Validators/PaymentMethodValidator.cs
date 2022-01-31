using ERPXTpl.Resources;

namespace ERPXTpl.Validators
{
    internal class PaymentMethodValidator
    {
        internal static string GetPaymentMethodById(long paymentMethodId)
        {
            if (paymentMethodId == 0)
            {
                return ValidatorMessage.PAYMENT_METHOD_ID_VALIDATE;
            }
            return "";
        }
    }
}

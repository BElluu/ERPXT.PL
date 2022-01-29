namespace ERPXTpl.Validators
{
    internal class PaymentMethodValidator
    {
        internal static string GetPaymentMethodById(int paymentMethodId)
        {
            if (paymentMethodId == 0)
            {
                return "Payment Method Id cannot be zero";
            }
            return "";
        }
    }
}

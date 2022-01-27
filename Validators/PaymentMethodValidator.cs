using System;

namespace ERPXTpl.Validators
{
    internal class PaymentMethodValidator
    {
        internal static void GetPaymentMethodById(int paymentMethodId)
        {
            if (paymentMethodId == 0)
            {
                throw new ArgumentException("Id cannot be zero");
            }
        }
    }
}

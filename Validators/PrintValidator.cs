using System;

namespace ERPXTpl.Validators
{
    internal class PrintValidator
    {
        internal static void GetPrintValidator(int invoiceId)
        {
            if (invoiceId == 0)
            {
                throw new ArgumentException("Id cannot be zero");
            }
        }

        internal static void GetCustomPrintValidator(int invoiceId, int printTemplateId)
        {
            if (invoiceId == 0 || printTemplateId == 0)
            {
                throw new ArgumentException("Id cannot be zero");
            }
        }
    }
}

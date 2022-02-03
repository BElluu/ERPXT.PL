using ERPXTpl.Resources;

namespace ERPXTpl.Validator
{
    internal class PrintValidator
    {
        internal static string GetPrintValidator(long invoiceId)
        {
            if (invoiceId == 0)
            {
                return ValidatorMessage.INVOICE_ID_VALIDATE;
            }
            return string.Empty;
        }

        internal static string GetCustomPrintValidator(long invoiceId, long printTemplateId)
        {
            if (invoiceId == 0)
            {
                return ValidatorMessage.INVOICE_ID_VALIDATE;
            }

            if (printTemplateId == 0)
            {
                return ValidatorMessage.PRINT_TEMPLATE_ID_VALIDATE;
            }

            return string.Empty;
        }
    }
}

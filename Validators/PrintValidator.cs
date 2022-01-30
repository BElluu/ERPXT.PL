using ERPXTpl.Resources;

namespace ERPXTpl.Validators
{
    internal class PrintValidator
    {
        internal static string GetPrintValidator(int invoiceId)
        {
            if (invoiceId == 0)
            {
                return ValidatorMessage.INVOICE_ID_VALIDATE;
            }
            return "";
        }

        internal static string GetCustomPrintValidator(int invoiceId, int printTemplateId)
        {
            if (invoiceId == 0)
            {
                return ValidatorMessage.INVOICE_ID_VALIDATE;
            }

            if (printTemplateId == 0)
            {
                return ValidatorMessage.PRINT_TEMPLATE_ID_VALIDATE;
            }

            return "";
        }
    }
}

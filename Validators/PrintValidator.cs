namespace ERPXTpl.Validators
{
    internal class PrintValidator
    {
        internal static string GetPrintValidator(int invoiceId)
        {
            if (invoiceId == 0)
            {
                return "Invoice Id cannot be zero";
            }
            return "";
        }

        internal static string GetCustomPrintValidator(int invoiceId, int printTemplateId)
        {
            if (invoiceId == 0 || printTemplateId == 0)
            {
                return "Invoice Id or Print Template Id cannot be zero";
            }
            return "";
        }
    }
}

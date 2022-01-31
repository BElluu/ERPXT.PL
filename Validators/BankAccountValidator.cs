using ERPXTpl.Resources;

namespace ERPXTpl.Validators
{
    internal class BankAccountValidator
    {
        internal static string GetBankAccountById(long bankAccountId)
        {
            if (bankAccountId == 0)
            {
                return ValidatorMessage.BANK_ACCOUNT_ID_VALIDATE;
            }
            else
            {
                return "";
            }
        }
    }
}

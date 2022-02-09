using ERPXTpl.Model;
using ERPXTpl.Resources;

namespace ERPXTpl.Validator
{
    internal class BankAccountValidator
    {
        internal static string GetBankAccountByIdValidator(long bankAccountId)
        {
            if (bankAccountId == 0)
            {
                return ValidatorMessage.BANK_ACCOUNT_ID_VALIDATE;
            }
            else
            {
                return string.Empty;
            }
        }
        internal static string AddBankAccountValidator(BankAccount bankAccount)
        {
            if (bankAccount == null)
            {
                return ValidatorMessage.BANK_ACCOUNT_OBJECT_VALIDATE;
            }
            if (string.IsNullOrEmpty(bankAccount.AccountNumber))
            {
                return ValidatorMessage.BANK_ACCOUNT_NUMBER_VALIDATE;
            }
            if (string.IsNullOrEmpty(bankAccount.Symbol))
            {
                return ValidatorMessage.BANK_ACCOUNT_SYMBOL_VALIDATE;
            }
            if (bankAccount.ReportingPeriod == null)
            {
                return ValidatorMessage.BANK_ACCOUNT_REPORTING_PERIOD_VALIDATE;
            }
            return string.Empty;
        }
    }
}

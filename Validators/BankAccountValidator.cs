namespace ERPXTpl.Validators
{
    internal class BankAccountValidator
    {
        internal static string GetBankAccountById(int bankAccountId)
        {
            if (bankAccountId == 0)
            {
                return "Bank Account ID cannot be zero";
            }
            else
            {
                return "";
            }
        }
    }
}

using System;

namespace ERPXTpl.Validators
{
    internal class BankAccountValidator
    {
        internal static void GetBankAccountById(int bankAccountId)
        {
            if (bankAccountId == 0)
            {
                throw new ArgumentException("Id cannot be zero");
            }
        }
    }
}

using System;

namespace ERPXTpl.Validators
{
    internal static class AuthorizationValidator
    {
        internal static void GetTokenValidator(string clientId, string secretKey)
        {
            if (string.IsNullOrEmpty(clientId) && string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("ClientId and SecretKey cannot be null or empty");
            }
        }
    }
}

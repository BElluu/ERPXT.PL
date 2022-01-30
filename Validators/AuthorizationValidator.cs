using ERPXTpl.Resources;

namespace ERPXTpl.Validators
{
    internal static class AuthorizationValidator
    {
        internal static string GetTokenValidator(string clientId, string secretKey)
        {
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(secretKey))
            {
                return ValidatorMessage.CLIENTID_SECRETKEY_VALIDATE;
            }
            return "";
        }
    }
}

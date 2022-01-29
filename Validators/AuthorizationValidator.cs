namespace ERPXTpl.Validators
{
    internal static class AuthorizationValidator
    {
        internal static string GetTokenValidator(string clientId, string secretKey)
        {
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(secretKey))
            {
                return "ClientId and SecretKey cannot be null or empty";
            }
            return "";
        }
    }
}

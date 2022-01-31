namespace ERPXTpl
{
    internal class Endpoint
    {
        public static string BANK_ACCOUNTS = "https://app.erpxt.pl/api2/public/bankaccounts/";
        public static string PRODUCTS = "https://app.erpxt.pl/api2/public/products/";
        public static string CUSTOMERS = "https://app.erpxt.pl/api2/public/v1.2/customers/";
        public static string AUTHORIZATION = "https://app.erpxt.pl/api2/public/token";
        public static string PAYMENT_METHODS = "https://app.erpxt.pl/api2/public/paymenttypes/";
        public static string PRINT_TEMPLATES = "https://app.erpxt.pl/api2/public/customprints";
        public static string INVOICE_PRINT_CUSTOMER = "https://app.erpxt.pl/api2/public/v1.1/invoices/print/"; // API 1.1
        public static string PROFORMA_INVOICE_PRINT_CUSTOMER = "https://app.erpxt.pl/api2/public/v1.1/proformas/print/"; // API 1.1
        public static string INVOICE_PRINT_CUSTOM = "https://app.erpxt.pl/api2/public/v1.2/invoices/{0}/print?customPrintId={1}"; // API 1.2
        public static string PROFORMA_PRINT_CUSTOM = "https://app.erpxt.pl/api2/public/v1.2/proformas/{0}/print?customPrintId={1}"; // API 1.2
        public static string SALES_INVOICES = "https://app.erpxt.pl/api2/public/v1.2/invoices";
        public static string VAT_RATES = "https://app.erpxt.pl/api2/public/v1.2/vatrates/";
    }
}

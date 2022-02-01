﻿namespace ERPXTpl.Resources
{
    internal class ValidatorMessage
    {
        public static readonly string CLIENTID_SECRETKEY_VALIDATE = "ClientId and SecretKey cannot be null or empty";
        public static readonly string BANK_ACCOUNT_ID_VALIDATE = "Bank Account Id cannot be zero";
        public static readonly string CUSTOMER_ID_VALIDATE = "Customer Id cannot be zero or empty";
        public static readonly string CUSTOMER_TIN_VALIDATE = "Customer TIN cannot be zero or empty";
        public static readonly string CUSTOMER_OBJECT_VALIDATE = "Customer object cannot be null";
        public static readonly string CUSTOMER_NAME_VALIDATE = "Customer Name cannot be null or empty";
        public static readonly string CUSTOMER_COUNTRY_CODE_VALIDATE = "The country code is mandatory for intra-EU and the OSS procedure customers";
        public static readonly string PAYMENT_METHOD_ID_VALIDATE = "Payment Method Id cannot be zero";
        public static readonly string INVOICE_ID_VALIDATE = "Invoice Id cannot be zero";
        public static readonly string PRINT_TEMPLATE_ID_VALIDATE = "Print Template Id cannot be zero";
        public static readonly string PRODUCT_ID_VALIDATE = "Product Id cannot be zero or null";
        public static readonly string PRODUCT_OBJECT_VALIDATE = "Product object cannot be null";
        public static readonly string PRODUCT_NAME_VALIDATE = "Product Name cannot be null or empty";
        public static readonly string PRODUCT_UNIT_VALIDATE = "Product UnitOfMeasurment cannot be null or empty";
        public static readonly string PRODUCT_RATE_VALIDATE = "Product Rate must be valid and cannot be null or empty";
        public static readonly string INVOICE_NUMBER_VALIDATE = "Invoice Number cannot be null or empty";
        public static readonly string INVOICE_OBJECT_VALIDATE = "Invoice object cannot be empty";
        public static readonly string INVOICE_PURCHASING_PARTY_ID_VALIDATE = "Invoice PurchasingPartyId cannot be null";
        public static readonly string INVOICE_PAYMENT_TYPE_ID_VALIDATE = "Invoice PaymentTypeId cannot be null";
        public static readonly string PRODUCT_QUANTITY_VALIDATE = "Product Quantity cannot be null";
        public static readonly string PRODUCT_CURRENCY_PRICE_VALIDATE = "ProductCurrencyPrice cannot be null";
    }
}

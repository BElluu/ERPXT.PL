using ERPXTpl.Models;
using ERPXTpl.Resources;

namespace ERPXTpl.Validators
{
    internal class SalesInvoiceValidator
    {
        internal static string GetSalesInvoiceValidator(long invoiceId)
        {
            if(invoiceId == 0)
            {
                return ValidatorMessage.INVOICE_ID_VALIDATE;
            }
            return "";
        }

        internal static string GetSalesInvoiceValidator(string invoiceNumber)
        {
            if (string.IsNullOrEmpty(invoiceNumber))
            {
                return ValidatorMessage.INVOICE_NUMBER_VALIDATE;
            }
            return "";
        }

        internal static string PostSalesInvoiceValidator(SalesInvoice salesInvoice)
        {
            if(salesInvoice == null)
            {
                return ValidatorMessage.INVOICE_OBJECT_VALIDATE;
            }
            if(salesInvoice.PurchasingPartyId == 0)
            {
                return ValidatorMessage.INVOICE_PURCHASING_PARTY_ID_VALIDATE;
            }
            if (salesInvoice.PaymentTypeId == 0 || salesInvoice.PaymentTypeId == null)
            {
                return ValidatorMessage.INVOICE_PAYMENT_TYPE_ID_VALIDATE;
            }
            if(salesInvoice.Items.Count > 0)
            {
                foreach(var item in salesInvoice.Items)
                {
                    if(item.ProductId == 0)
                    {
                        return ValidatorMessage.PRODUCT_ID_VALIDATE;
                    }
                    if(item.Quantity == 0)
                    {
                        return ValidatorMessage.PRODUCT_QUANTITY_VALIDATE;
                    }
                    if(item.ProductCurrencyPrice == 0)
                    {
                        return ValidatorMessage.PRODUCT_CURRENCY_PRICE_VALIDATE;
                    }
                }
            }
            return "";
        }

        internal static string PutSalesInvoiceValidator(SalesInvoice salesInvoice)
        {
            if (salesInvoice == null)
            {
                return ValidatorMessage.INVOICE_OBJECT_VALIDATE;
            }

            if (salesInvoice.Id == 0)
            {
                return ValidatorMessage.INVOICE_ID_VALIDATE;
            }
            if (salesInvoice.PurchasingPartyId == 0)
            {
                return ValidatorMessage.INVOICE_PURCHASING_PARTY_ID_VALIDATE;
            }
            if (salesInvoice.PaymentTypeId == 0 || salesInvoice.PaymentTypeId == null)
            {
                return ValidatorMessage.INVOICE_PAYMENT_TYPE_ID_VALIDATE;
            }
            if (salesInvoice.Items.Count > 0)
            {
                foreach (var item in salesInvoice.Items)
                {
                    if (item.ProductId == 0)
                    {
                        return ValidatorMessage.PRODUCT_ID_VALIDATE;
                    }
                    if (item.Quantity == 0)
                    {
                        return ValidatorMessage.PRODUCT_QUANTITY_VALIDATE;
                    }
                    if (item.ProductCurrencyPrice == 0)
                    {
                        return ValidatorMessage.PRODUCT_CURRENCY_PRICE_VALIDATE;
                    }
                }
            }
            return "";
        }
    }
}

using ERPXTpl.Models;
using ERPXTpl.Resources;

namespace ERPXTpl.Validators
{
    internal class InvoiceValidator
    {
        internal static string GetInvoiceValidator(long invoiceId)
        {
            if(invoiceId == 0)
            {
                return ValidatorMessage.INVOICE_ID_VALIDATE;
            }
            return "";
        }

        internal static string GetInvoiceValidator(string invoiceNumber)
        {
            if (string.IsNullOrEmpty(invoiceNumber))
            {
                return ValidatorMessage.INVOICE_NUMBER_VALIDATE;
            }
            return "";
        }

        internal static string PostInvoiceValidator(IInvoice invoice)
        {
            if(invoice == null)
            {
                return ValidatorMessage.INVOICE_OBJECT_VALIDATE;
            }
            if(invoice.PurchasingPartyId == 0)
            {
                return ValidatorMessage.INVOICE_PURCHASING_PARTY_ID_VALIDATE;
            }
            if (invoice.PaymentTypeId == 0 || invoice.PaymentTypeId == null)
            {
                return ValidatorMessage.INVOICE_PAYMENT_TYPE_ID_VALIDATE;
            }
            if(invoice.Items.Count > 0)
            {
                foreach(var item in invoice.Items)
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

        internal static string PutSalesInvoiceValidator(IInvoice invoice)
        {
            if (invoice == null)
            {
                return ValidatorMessage.INVOICE_OBJECT_VALIDATE;
            }

            if (invoice.Id == 0)
            {
                return ValidatorMessage.INVOICE_ID_VALIDATE;
            }
            if (invoice.PurchasingPartyId == 0)
            {
                return ValidatorMessage.INVOICE_PURCHASING_PARTY_ID_VALIDATE;
            }
            if (invoice.PaymentTypeId == 0 || invoice.PaymentTypeId == null)
            {
                return ValidatorMessage.INVOICE_PAYMENT_TYPE_ID_VALIDATE;
            }
            if (invoice.Items.Count > 0)
            {
                foreach (var item in invoice.Items)
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

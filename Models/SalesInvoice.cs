using ERPXTpl.Enums;
using System.Collections.Generic;

namespace ERPXTpl.Models
{
    public class SalesInvoice
    {
        public SalesInvoice()
        {
            Items = new List<Item>();
        }

        public long Id { get; set; }
        public PaymentStatusEnum PaymentStatus { get; set; }
        public string OSSProcedureCountryCode { get; set; }
        public bool IsOSSProcedure { get; set; }
        public long PurchasingPartyId { get; set; }
        public long? ReceivingPartyId { get; set; }
        public long? PaymentTypeId { get; set; }
        public long? BankAccountId { get; set; }
        public string SalesDate { get; set; }
        public InvoiceTypeEnum InvoiceType { get; set; }
        public string Description { get; set; }
        public string IssueDate { get; set; }
        public string Number { get; set; }
        public DocumentStatusEnum Status { get; set; } 
        public List<Item> Items { get; set; }

        internal bool ShouldSerializeId()
        {
            return Id != 0;
        }
    }

    public class Item
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal ProductCurrencyPrice { get; set; }
        public string ProductDescription { get; set; }
        public int VatRateId { get; set; }

        internal bool ShouldSerializeId()
        {
            return Id != 0;
        }
    }
}

using ERPXTpl.Enums;
using System.Collections.Generic;

namespace ERPXTpl.Models
{
    public class SalesInvoice
    {
        public SalesInvoice()
        {
            Item = new List<Item>();
        }

        public PaymentStatusEnum PaymentStatus { get; set; }
        public string OSSProcedureCountryCode { get; set; }
        public bool IsOSSProcedure { get; set; }
        public int PurchasingPartyId { get; set; }
        public int ReceivingPartyId { get; set; }
        public int PaymentTypeId { get; set; }
        public int BankAccountId { get; set; }
        public string SalesDate { get; set; }
        public byte InvoiceType { get; set; }
        public string Description { get; set; }
        public string IssueDate { get; set; }
        public string Number { get; set; }
        public DocumentStatusEnum Status { get; set; } 
        public List<Item> Item { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal ProductCurrencyPrice { get; set; }
        public string ProductDescription { get; set; }
        public int VatRateId { get; set; }
    }
}

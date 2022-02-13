using ERPXTpl.Enum;
using System.Collections.Generic;

namespace ERPXTpl.Model
{
    public class ProformaInvoice : IInvoice
    {

        public ProformaInvoice()
        {
            Items = new List<Item>();
        }

        public long Id { get; set; }
        public string OSSProcedureCountryCode { get; set; }
        public bool IsOSSProcedure { get; set; }
        public long? PurchasingPartyId { get; set; }
        public long? ReceivingPartyId { get; set; }
        public long? PaymentTypeId { get; set; }
        public long? BankAccountId { get; set; }
        public long? InvoiceDocIdNum { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public string Description { get; set; }
        public string IssueDate { get; set; }
        public string Number { get; set; }
        public DocumentStatus Status { get; set; }
        public List<Item> Items { get; set; }

        internal bool ShouldSerializeId()
        {
            return Id != 0;
        }
    }
}


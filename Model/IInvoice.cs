using ERPXTpl.Enum;
using System.Collections.Generic;

namespace ERPXTpl.Model
{
    public interface IInvoice
    {
        long Id { get; set; }
        string OSSProcedureCountryCode { get; set; }
        bool IsOSSProcedure { get; set; }
        long? PurchasingPartyId { get; set; }
        long? ReceivingPartyId { get; set; }
        long? PaymentTypeId { get; set; }
        long? BankAccountId { get; set; }
        InvoiceType InvoiceType { get; set; }
        string Description { get; set; }
        string IssueDate { get; set; }
        string Number { get; set; }
        DocumentStatus Status { get; set; }
        List<Item> Items { get; set; }
    }
}
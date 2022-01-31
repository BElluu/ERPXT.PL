using ERPXTpl.Enums;
using System.Collections.Generic;

namespace ERPXTpl.Models
{
    public interface IInvoice
    {
        long Id { get; set; }
        string OSSProcedureCountryCode { get; set; }
        bool IsOSSProcedure { get; set; }
        long PurchasingPartyId { get; set; }
        long? ReceivingPartyId { get; set; }
        long? PaymentTypeId { get; set; }
        long? BankAccountId { get; set; }
        InvoiceTypeEnum InvoiceType { get; set; }
        string Description { get; set; }
        string IssueDate { get; set; }
        string Number { get; set; }
        DocumentStatusEnum Status { get; set; }
        List<Item> Items { get; set; }
    }
}
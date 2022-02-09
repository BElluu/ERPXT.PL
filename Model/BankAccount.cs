using ERPXTpl.Enum;

namespace ERPXTpl.Model
{
    public class BankAccount
    {
        public long Id { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string Symbol { get; set; }
        public ReportingPeriod? ReportingPeriod { get; set; }
        public bool Primary { get; set;}
    }
}

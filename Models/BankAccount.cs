namespace ERPXTpl.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string Symbol { get; set; }
        public bool Primary { get; set;}
    }
}

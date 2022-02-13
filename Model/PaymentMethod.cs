using ERPXTpl.Enum;

namespace ERPXTpl.Model
{
    public class PaymentMethod
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Primary { get; set; }
        public int Deadline { get; set; }
        public PaymentMethodType? Type { get; set; }
    }
}

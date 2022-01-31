using ERPXTpl.Enums;

namespace ERPXTpl.Models
{
    public class PaymentMethod
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Primary { get; set; }
        public PaymentMethodTypeEnum Type { get; set; }
    }
}

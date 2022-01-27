using ERPXTpl.Enums;

namespace ERPXTpl.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Primary { get; set; }
        public PaymentMethodTypeEnum Type { get; set; }
    }
}

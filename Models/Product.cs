using ERPXTpl.Enums;

namespace ERPXTpl.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string ItemCode { get; set; }

        public string ProductCode { get; set; }

        public string UnitOfMeasurment { get; set; }

        public RateEnum Rate { get; set; }

        public double SaleNetPrice { get; set; }

        public double SaleGrossPrice { get; set; }

        public double Quantity { get; set; }

        public ProductTypeEnum ProductType { get; set; }

        public bool ShouldSerializeId()
        {
            return Id != 0;
        }
    }
}

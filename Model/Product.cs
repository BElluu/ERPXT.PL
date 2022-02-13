using ERPXTpl.Enum;

namespace ERPXTpl.Model
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string ItemCode { get; set; }

        public string ProductCode { get; set; }

        public string UnitOfMeasurment { get; set; }

        public Rate Rate { get; set; }

        public double SaleNetPrice { get; set; }

        public double SaleGrossPrice { get; set; }

        public double Quantity { get; set; }

        public ProductType ProductType { get; set; }

        internal bool ShouldSerializeId()
        {
            return Id != 0;
        }
    }
}

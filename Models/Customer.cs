using ERPXTpl.Enums;

namespace ERPXTpl.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string CustomerTaxNumber { get; set; }
        public string CustomerCode { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
        public CustomerTypeEnum CustomerType { get; set; }
        public CustomerStatusEnum CustomerStatus { get; set; }
        public Address Address { get; set; }

        internal bool ShouldSerializeId()
        {
            return Id != 0;
        }
    }

    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string FlatNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        internal bool ShouldSerializeId()
        {
            return Id != 0;
        }
    }

}

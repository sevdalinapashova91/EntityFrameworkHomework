namespace NorthwindManipulation.Models
{
    public class Customer
    {
        public Customer(string companyName, string contactName, string contactTitle, string address,
            string city, string region, string postCode, string country, string phone, string fax)
        {
            this.CompanyName = companyName;
            this.ContactName = contactName;
            this.ContactTitle = ContactTitle;
            this.Address = address;
            this.City = city;
            this.Region = region;
            this.PostalCode = postCode;
            this.Country = country;
            this.Phone = phone;
            this.Fax = fax;
        }
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}

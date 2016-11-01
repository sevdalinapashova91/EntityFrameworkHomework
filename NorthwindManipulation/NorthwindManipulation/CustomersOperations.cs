namespace NorthwindManipulation
{
    using System.Linq;
   
    using System;
    using System.Data.Entity.Infrastructure;

    public static class CustomersOperations
    {
        public static void CreateCustomer(Models.Customer customer)
        {
            using (NorthwindEntities northwindEntities = new NorthwindEntities())
            {
                Customer newCustomer = new Customer();
                newCustomer.Address = customer.Address;
                newCustomer.City = customer.City;
                newCustomer.CompanyName = customer.CompanyName;
                newCustomer.ContactName = customer.ContactName;
                newCustomer.ContactTitle = customer.ContactTitle;
                newCustomer.Country = customer.Country;
                northwindEntities.Customers.Add(newCustomer);
                northwindEntities.SaveChanges();
            }
        }

        public static void DeleteCustomer(string id)
        {
            using (NorthwindEntities northwindEntities = new NorthwindEntities())
            {
                var customerToDelete = GetCustomerById(id, northwindEntities);
                northwindEntities.Customers.Remove(customerToDelete);
                northwindEntities.SaveChanges();
            }
        }

        public static void ChangeCustomer(string customerId, Models.Customer customer)
        {
            using (NorthwindEntities northwindEntities = new NorthwindEntities())
            {
                var customerToUpdate = GetCustomerById(customerId, northwindEntities);
                if (customerToUpdate != null)
                {
                    customerToUpdate.Address = customer.Address;
                    customerToUpdate.City = customer.City;
                    customerToUpdate.CompanyName = customer.CompanyName;
                    customerToUpdate.ContactName = customer.ContactName;
                    customerToUpdate.ContactTitle = customer.ContactTitle;
                    customerToUpdate.Country = customer.Country;
                    northwindEntities.SaveChanges();
                }
            }
        }

        private static Customer GetCustomerById(string id, NorthwindEntities northwindEntities)
        {
            return northwindEntities.Customers
                    .Select(customer => customer)
                    .Where(customer => customer.CustomerID == id)
                    .FirstOrDefault();
        }
    }
}

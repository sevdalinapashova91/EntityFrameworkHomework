namespace NorthwindManipulation
{
    using System.Linq;

    using System;
    using System.Data.Entity.Infrastructure;
    using System.Collections;
    using System.Collections.Generic;
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

        public static IEnumerable<Customer> GetAllCustomersWithOrdersFrom1997AndShippedToChina()
        {
            IEnumerable<Customer> customers = null;
            using (NorthwindEntities entities = new NorthwindEntities())
            {
                customers = entities.Customers
                    .Select(c => c)
                    .Where(c =>
                        c.Orders
                            .Select(o => o)
                            .Where(o => o.OrderDate != null && o.OrderDate.Value.Year == 1997 && o.ShipCountry == "Canada") != null)
                    .ToList();
            }

            return customers;
        }

        public static IEnumerable<Customer> GetAllCustomersWithOrdersFrom1997AndShippedToChinaThroughDbContext()
        {
            IEnumerable<Customer> result = null;
            using (NorthwindEntities entities = new NorthwindEntities())
            {
                string customersQuery = @"Select * 
                                            From Customers c JOIN
                                                    Orders o ON c.CustomerID = o.CustomerID
                                            Where Year(o.OrderDate) = 1997 AND o.ShipCountry = 'Canada';";
                result = entities.Database.SqlQuery<Customer>(customersQuery);
            }

            return result;
        }

        public static IEnumerable<Order> GetSalesBySpecificRegionAndPeriod(DateTime start, DateTime end, string region)
        {
            IEnumerable<Order> orders = null;
            using (NorthwindEntities entities = new NorthwindEntities())
            {
                orders = entities.Orders.Join(
                     entities.Employees,
                     order => order.EmployeeID,
                     employee => employee.EmployeeID,
                     (order, employee) => new { Order = order, Employee = employee })
                    .Where(x => x.Employee.Region == region)
                    .Select(a => a.Order)
                    .ToList();
            }

            return orders;
        }

        public static void ConcurrenChangesSave()
        {
            NorthwindEntities northwindEntity1 = new NorthwindEntities();
            NorthwindEntities nortwindEntity2 = new NorthwindEntities();
            var customer = northwindEntity1.Customers.FirstOrDefault(x => x.Country == "Canada");
            var customer2 = nortwindEntity2.Customers.FirstOrDefault(x => x.Country == "Canada");
            customer.CompanyName = "Test";
            customer2.CompanyName = "Test2";
            nortwindEntity2.SaveChanges();
            northwindEntity1.SaveChanges();
        }

        public static void NorthwindTwinCreation()
        {// Added a connection string in App.config
            using (NorthwindEntities northwindEntity = new NorthwindEntities())
            {
                northwindEntity.Database.CreateIfNotExists();
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

using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            if (customers is null) throw new ArgumentNullException();

            var result = customers.Where(c => c.Orders.Sum(o => o.Total) > limit);

            return result;
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if (customers is null || suppliers is null) throw new ArgumentNullException();

            var result = customers.Select(c => (
                customer: c,
                suppliers: suppliers.Where(s => s.City == c.City && s.Country == c.Country)
            ));

            return result;
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if (customers is null || suppliers is null) throw new ArgumentNullException();

            var result = customers.GroupJoin(suppliers,
                c => new { c.City, c.Country },
                s => new { s.City, s.Country },
                (c, s) => (Customer: c, Suppliers: s));

            return result;
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            if (customers is null) throw new ArgumentNullException();

            var result = customers.Where(c => c.Orders.Any(o => o.Total > limit));

            return result;
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            if (customers is null) throw new ArgumentNullException();

            var result = customers.Where(c => c.Orders.Any()).Select(c =>
                (customer: c, dateOfEntry: c.Orders.OrderBy(o => o.OrderDate).Select(o => o.OrderDate).First()));

            return result;
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            if (customers is null) throw new ArgumentNullException();

            var result = customers.Where(c => c.Orders.Any())
                .Select(c =>
                    (customer: c, dateOfEntry: c.Orders.OrderBy(o => o.OrderDate).Select(o => o.OrderDate).First()))
                .OrderBy(c => c.dateOfEntry.Year)
                .ThenBy(c => c.dateOfEntry.Month)
                .ThenByDescending(c => c.customer.Orders.Sum(o => o.Total))
                .ThenBy(c => c.customer.CustomerID);

            return result;
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            if (customers is null) throw new ArgumentNullException();

            var result = customers.Where(c => c.PostalCode != null &&
                                              c.PostalCode.Any(symbol => symbol < '0' || symbol > '9') ||
                                              string.IsNullOrEmpty(c.Region) ||
                                              c.Phone.FirstOrDefault() != '(');

            return result;
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */
            if (products is null) throw new ArgumentNullException();

            var result = products.GroupBy(p => p.Category).Select(group => new Linq7CategoryGroup
            {
                Category = group.Key,
                UnitsInStockGroup = group.GroupBy(p => p.UnitsInStock)
                    .Select(p => new Linq7UnitsInStockGroup
                    {
                        UnitsInStock = p.Key,
                        Prices = p.OrderBy(product => product.UnitPrice).Select(p => p.UnitPrice)
                    })
            });

            return result;
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            if (products is null) throw new ArgumentNullException();

            var groupedByPriceProducts =
                products.GroupBy(p => p.UnitPrice <= cheap ? cheap : p.UnitPrice <= middle ? middle : expensive);

            var result = groupedByPriceProducts.Select(p =>
            (
                category: p.Key,
                products: p.Select(prod => prod)
            ));

            return result;
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            if (customers is null) throw new ArgumentNullException();

            var result = customers
                .GroupBy(c => c.City)
                .Select(c => (
                    city: c.Key,
                    averageIncome: Convert.ToInt32(c.Average(p => p.Orders.Sum(o => o.Total))),
                    averageIntensity: Convert.ToInt32(c.Average(p => p.Orders.Length))
                ));

            return result;
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            if (suppliers is null) throw new ArgumentNullException();

            var countries = suppliers.Select(s => s.Country)
                .Distinct()
                .OrderBy(c => c.Length)
                .ThenBy(c => c);

            var result = string.Join("", countries);

            return result;
        }
    }
}
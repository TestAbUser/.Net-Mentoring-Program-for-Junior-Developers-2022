// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Linq;
using SampleSupport;
using Task.Data;
using System.Data;

namespace SampleQueries
{
    [Title("LINQ Module")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {

        private DataSource dataSource = new DataSource();

        [Category("Restriction Operators")]
        [Title("Where - Task 1")]
        [Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
        public void Linq1()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var lowNums =
                from num in numbers
                where num < 5
                select num;

            Console.WriteLine("Numbers < 5:");
            foreach (var x in lowNums)
            {
                Console.WriteLine(x);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 2")]
        [Description("This sample return return all presented in market products")]

        public void Linq2()
        {
            var products =
                from p in dataSource.Products
                where p.UnitsInStock > 0
                select p;

            foreach (var p in products)
            {
                ObjectDumper.Write(p);
            }
        }


        [Category("Restriction Operators")]
        [Title("Task 1")]
        [Description("Retrieve all clients whose total orders cost exceeds a certain amount X")]

        public void Linq3()
        {
            int[] xArray = { 1000, 10300, 30000, 50803 };
            var greaterThanXGroups =
                from x in xArray
                select new
                {
                    x,
                    SumsOfTotalsGroups = from c in dataSource.Customers
                                         let sumOfAllTotals = c.Orders.Select(orders => orders.Total).Sum()
                                         where sumOfAllTotals > x
                                         select new { sumOfAllTotals, c.CustomerID }
                };
            foreach (var group in greaterThanXGroups)
            {
                ObjectDumper.Write($"X={group.x}");
                foreach (var c in group.SumsOfTotalsGroups)
                {
                    ObjectDumper.Write($"\tClient={c.CustomerID}, Sum of all Totals={c.sumOfAllTotals}");
                }
            }
        }

        [Category("Restriction Operators")]
        [Title("Task 2")]
        [Description("For each client find all the suppliers located in the same country and city.")]

        public void Linq4()
        {
            var suppliers =
                from s in dataSource.Suppliers
                join c in dataSource.Customers on new { s.City, s.Country } equals new { c.City, c.Country } into cs
                from c in cs
                select new { c.CustomerID, s.SupplierName, s.Country, s.City };

            foreach (var s in suppliers)
            {
                ObjectDumper.Write(s);
            }
        }

        [Category("Restriction Operators")]
        [Title("Task 2 with Group By")]
        [Description("For each client find all the suppliers located in the same country and city.")]

        public void Linq5()
        {
            var customers =
                from c in dataSource.Customers
                from s in dataSource.Suppliers
                where c.Country == s.Country && c.City == s.City
                group s.SupplierName by c.CustomerID into supplierGroups
                select new { CustomerID = supplierGroups.Key, Suppliers = supplierGroups };

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
                foreach (var s in c.Suppliers)
                {
                    ObjectDumper.Write(s);
                }
            }
        }

        [Category("Restriction Operators")]
        [Title("Task 3")]
        [Description("Find all the clients with orders worth more than a certain amount X.")]

        public void Linq6()
        {
            int x = 5000;
            var customers =
                from c in dataSource.Customers
                where c.Orders.Any(o => o.Total > x)
                select new { c.CustomerID, c.Orders };
            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
                foreach (var o in c.Orders)
                {
                    if (o.Total > x)
                        ObjectDumper.Write(o.Total);
                }
            }
        }

        [Category("Restriction Operators")]
        [Title("Task 4")]
        [Description("Display all the clients with the month and year when they made their first order.")]

        public void Linq7()
        {
            var customers =
                from c in dataSource.Customers
                where c.Orders.Any()
                let minDate = c.Orders.Min(o => o.OrderDate)
                select new { c.CustomerID, minDate.Month, minDate.Year };
            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Restriction Operators")]
        [Title("Task 5")]
        [Description("Display all the clients with the month and year when they made their first order. " +
            "Sort the list by year, month, total orders and client name.")]

        public void Linq8()
        {
            var customers =
                from c in dataSource.Customers
                where c.Orders.Any()
                let minDate = c.Orders.Min(o => o.OrderDate)
                let allOrdersTotal = c.Orders.Sum(o => o.Total)
                orderby minDate.Year, minDate.Month, allOrdersTotal descending, c.CompanyName
                select new { c.CompanyName, minDate.Month, minDate.Year, allOrdersTotal };
            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Restriction Operators")]
        [Title("Task 6")]
        [Description("Find all clients who have letters in their postal code or empty region or " +
            "whose phone number does not have provider code.")]

        public void Linq9()
        {
            var customers =
                from c in dataSource.Customers
                where (c.PostalCode != null && c.PostalCode.Any(ch => char.IsLetter(ch))) ||
                c.Region == null || c.Phone.StartsWith("(") == false
                select new { c.CustomerID, c.PostalCode, c.Region, c.Phone };
            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Restriction Operators")]
        [Title("Task 7")]
        [Description("Group all the products by category, then sort each category by stock and then by " +
            "price.")]

        public void Linq10()
        {
            var products =
                from p in dataSource.Products
                group p by p.Category into categoryGroup
                let inStock = categoryGroup.OrderBy(p => p.UnitPrice).Where(p => p.UnitsInStock > 0)
                let outOfStock = categoryGroup.OrderBy(p => p.UnitPrice).Where(p => p.UnitsInStock == 0)
                select new { categoryGroup.Key, UnitInStock = inStock, UnitOutOfStock = outOfStock };

            foreach (var p in products)
            {
                ObjectDumper.Write(p.Key);
                ObjectDumper.Write($"\tUnits in Stock:");
                foreach (var inStock in p.UnitInStock)
                {
                    ObjectDumper.Write($"\t\t\t{inStock.ProductName}, Price={inStock.UnitPrice}");
                }
                ObjectDumper.Write($"\tUnits out of Stock:");
                foreach (var outOfStock in p.UnitOutOfStock)
                {
                    ObjectDumper.Write($"\t\t\t{outOfStock.ProductName}, Price={outOfStock.UnitPrice}");
                }
            }
        }

        public static string GetPriceRange(decimal price)
        {
            string category = default;
            if (price <= 10)
                category = "1.Cheap";
            else if (price > 10 && price <= 40)
                category = "2.Middle-range";
            else if (price > 40)
                category = "3.Expensive";
            return category;
        }

        [Category("Restriction Operators")]
        [Title("Task 8")]
        [Description("Group products by the following categories: 'cheap', 'mid-range', 'expensive'.")]
        public void Linq11()
        {
            var prices =
                from p in dataSource.Products
                let priceRanges = GetPriceRange(p.UnitPrice)
                group p by priceRanges into priceRangeGroup
                orderby priceRangeGroup.Key ascending
                select priceRangeGroup;

            foreach (var category in prices)
            {
                ObjectDumper.Write(category.Key);
                foreach (var product in category)
                {
                    ObjectDumper.Write($"\t{product.ProductName}= {product.UnitPrice}");
                }
            }
        }

        [Category("Restriction Operators")]
        [Title("Task 9")]
        [Description("Calculate the average total of each city and average number of orders from them.")]
        public void Linq12()
        {
            var cities =
                from c in dataSource.Customers
                group c by c.City into cityGroup
                select new
                {
                    cityGroup.Key,
                    AverageGroups =
                    from c in dataSource.Customers
                    where c.Orders != null && c.Orders.Count() != 0 && c.City == cityGroup.Key
                    let averageTotal = c.Orders.Average(o => o.Total)
                    let orderCount = c.Orders.Count()
                    select new { Customer = c.CustomerID, Orders = c.Orders, averageTotal, orderCount }
                };

            foreach (var city in cities)
            {
                decimal cityAverageTotal = city.AverageGroups.Average(i => i.averageTotal);
                double cityAverageOrderCount = city.AverageGroups.Average(x => x.orderCount);
                ObjectDumper.Write($"{city.Key}: CityAverageTotal= {cityAverageTotal}, CityAverageOrderCount= {cityAverageOrderCount}");

                foreach (var c in city.AverageGroups)
                {
                    ObjectDumper.Write($"\tCustomerID={c.Customer}");

                    foreach (var order in c.Orders)
                    {
                        ObjectDumper.Write($"\t\t\tOrderID={order.OrderID}, Total={order.Total}");
                    }
                }
            }
        }

        [Category("Restriction Operators")]
        [Title("Task 10")]
        [Description("Display the monthly client activity for each year, then yearly " +
            "activity, then monthly activity in all years.")]
        public void Linq13()
        {
            var customers =
              from c in dataSource.Customers
              select new
              {
                  c.CustomerID,
                  MonthGroups = c.Orders.GroupBy(o => o.OrderDate.Month).Select(monthGroup =>
                  new { Month = monthGroup.Key, Orders = monthGroup }),
                  YearGroups = from o in c.Orders
                               group o by o.OrderDate.Year into yearGroup
                               select
                                      new
                                      {
                                          yearGroup.Key,
                                          OrderByYear = yearGroup,
                                          YearAndMonthGroups =
                                      from order in c.Orders
                                      where yearGroup.Key == order.OrderDate.Year
                                      group order by order.OrderDate.Month into yearAndMonthGroup
                                      select new { Month = yearAndMonthGroup.Key, Orders = yearAndMonthGroup }
                                      }
              };

            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer.CustomerID);
                ObjectDumper.Write($"\t\t\t\tBy Months only:");

                foreach (var ord in customer.MonthGroups)
                {
                    ObjectDumper.Write($"\tMonth={ord.Month}:");

                    foreach (var o in ord.Orders)
                    {
                        ObjectDumper.Write($"\t\tOrderID={o.OrderID}, Total={o.Total}");
                    }
                }

                ObjectDumper.Write($"\r\n\t\t\t\tBy Years only:");

                foreach (var ord in customer.YearGroups)
                {
                    ObjectDumper.Write($"\tYear={ord.Key}:");

                    foreach (var o in ord.OrderByYear)
                    {
                        ObjectDumper.Write($"\t\tOrderID={o.OrderID}, Total={o.Total}");
                    }
                }
                ObjectDumper.Write($"\r\n\t\t\t\tBy Years and Months:");

                foreach (var ord in customer.YearGroups)
                {
                    ObjectDumper.Write($"\tYear={ord.Key}:");

                    foreach (var ordElement in ord.YearAndMonthGroups)
                    {
                        ObjectDumper.Write($"\t\tMonth={ordElement.Month}:");

                        foreach (var o in ordElement.Orders)
                        {
                            ObjectDumper.Write($"\t\t\tOrderID={o.OrderID}, Total={o.Total}");
                        }
                    }
                }
            }
        }
    }
}
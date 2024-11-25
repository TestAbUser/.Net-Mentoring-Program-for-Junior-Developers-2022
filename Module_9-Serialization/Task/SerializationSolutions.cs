using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace Task
{
    [TestClass]
    public class SerializationSolutions
    {
        Northwind dbContext;

        [TestInitialize]
        public void Initialize()
        {
            dbContext = new Northwind();
        }

        [TestMethod]
        public void SerializationCallbacks()
        {
            // Disable proxy creation since it causes exception during deserialization.
            dbContext.Configuration.ProxyCreationEnabled = false;

            // Uses NetDataContractSerializer to serialize IEnumerable<Category> type.
            var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(new
                NetDataContractSerializer(), true);
            var categories = dbContext.Categories.ToList();

            // Initialize the entity's property, which is itself an entity.
            foreach (var category in categories)
            {
                var t = (dbContext as IObjectContextAdapter).ObjectContext;
                t.LoadProperty(category, f => f.Products);
            }
            var deserializedCategories = tester.SerializeAndDeserialize(categories);
            foreach (var el in deserializedCategories)
            {
                Console.WriteLine(el.Products.First().ProductName);
            }
        }

        // Test serialization of a type marked with binary serialization attribute - ISerializable.
        [TestMethod]
        public void ISerializable()
        {
            // Disable proxy creation since it causes exception during deserialization.
            dbContext.Configuration.ProxyCreationEnabled = false;

            // Uses NetDataContractSerializer to serialize IEnumerable<Product> type.
            var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(
                new NetDataContractSerializer(), true);
            var products = dbContext.Products.ToList();

            // Initialize the entity's properties, which are themselves entities.
            foreach (var product in products)
            {
                var t = (dbContext as IObjectContextAdapter).ObjectContext;
                t.LoadProperty(product, f => f.Category);
                t.LoadProperty(product, f => f.Order_Details);
                t.LoadProperty(product, f => f.Supplier);
            }

            var deserializedProducts = tester.SerializeAndDeserialize(products);
            foreach (var el in deserializedProducts)
            {
                Console.WriteLine(el.Supplier.HomePage);
            }
        }

        // Test serialization involving ISerializationSurrogate interface.
        [TestMethod]
        public void ISerializationSurrogate()
        {
            // Disable proxy creation since it causes exception during deserialization.
            dbContext.Configuration.ProxyCreationEnabled = false;
            IFormatter formatter = new NetDataContractSerializer();
            var ss = new SurrogateSelector();

            // Register an instance of the surrogate class that will be used instead of Order_Detail type.
            ss.AddSurrogate(typeof(Order_Detail), formatter.Context, new SerializationSurrogate());
            formatter.SurrogateSelector = ss;

            // Use NetDataContractSerializer to serialize IEnumerable<Order_Detail> type.
            var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(
                (NetDataContractSerializer)formatter, true);
            var orderDetails = dbContext.Order_Details.ToList();

            // Initialize Order_Detail entity's properties, which are themselves entities.
            foreach (var orderDetail in orderDetails)
            {
                var t = (dbContext as IObjectContextAdapter).ObjectContext;
                t.LoadProperty(orderDetail, f => f.Order);
                t.LoadProperty(orderDetail, f => f.Product);
            }

            var deserializedOrderDetails = tester.SerializeAndDeserialize(orderDetails);
            foreach (var el in deserializedOrderDetails)
            {
                Console.WriteLine(el.Order.Freight);
            }
        }

        // Test serialization involving IDataContractSurrogate interface.
        [TestMethod]
        public void IDataContractSurrogate()
        {
            dbContext.Configuration.ProxyCreationEnabled = true;
            dbContext.Configuration.LazyLoadingEnabled = true;

            // Create a list of the dependent types that may also need to be serialized.
            var knownTypes = new List<Type>()
            { typeof(List<Order>),
                typeof(Customer),
                typeof(Employee),
                typeof(Order_Detail),
                typeof(Shipper)
            };

            OrderSurrogate surrogate = new OrderSurrogate();

            // Use DataContractSerializer to serialize IEnumerable<Order> type.
            // OrderSurrogate class will be used instead of Order during serialization. 
            var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(new
                DataContractSerializer(typeof(Order), knownTypes, Int16.MaxValue,
                false, true, surrogate), true);
            var orders = dbContext.Orders.ToList();
            var deserializedOrders = tester.SerializeAndDeserialize(orders);
            foreach (var el in deserializedOrders)
            {
                Console.WriteLine(el.Shipper.CompanyName);
            }
        }
    }
}

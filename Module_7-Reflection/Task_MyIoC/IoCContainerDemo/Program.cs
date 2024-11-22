using System;
using MyIoC;
using System.Reflection;

namespace IoCContainerDemo
{
    /// <summary>
    /// Demonstrates the work of MyIoC library.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.AddAssembly(Assembly.GetExecutingAssembly());

            var container2 = new Container();
            container2.AddType(typeof(CustomerBLL2)); // CustomerBLL2 properties are marked with ImportAttribute.
            container2.AddType(typeof(Logger)); //Logger is marked with ExportAttribute.
            
            // CustomerDAL is marked with ExportAttribute and derives from ICustomerDAL.
            container2.AddType(typeof(CustomerDAL), typeof(ICustomerDAL)); 

            // Create instances.
            var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
            var customerBLL2 = container2.CreateInstance<CustomerBLL2>();

            Console.WriteLine(customerBLL.DAL.SaySomething());
            Console.WriteLine(customerBLL.Log.Message);
            Console.WriteLine(customerBLL2.Logger.Message);
            Console.WriteLine(customerBLL2.CustomerDAL.SaySomething());
        }

    }
}

namespace MyIoC
{
    public interface ICustomerDAL
    {
        string SaySomething();
    }

    /// <summary>
    /// Demonstrates the use ExportAttribute with classes.
    /// </summary>
    [Export(typeof(ICustomerDAL))]
    public class CustomerDAL : ICustomerDAL
    {
        public string SaySomething()
        {
            return "Hello world!";
        }
    }
}
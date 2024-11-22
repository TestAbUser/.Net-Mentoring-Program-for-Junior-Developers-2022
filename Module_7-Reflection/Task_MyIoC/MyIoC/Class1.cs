namespace MyIoC
{
    /// <summary>
    /// Demonstrates the use of ImportConstructorAttribute on classes.
    /// </summary>
    [ImportConstructor]
    public class CustomerBLL
    {
        private readonly ICustomerDAL _dal;
        private readonly Logger _logger;
        public CustomerBLL(ICustomerDAL dal, Logger logger)
        {
            _dal = dal;
            _logger = logger;
        }
        public ICustomerDAL DAL => _dal;
        public Logger Log => _logger;
    }


    /// <summary>
    /// Demonstrates use of ImportAttribute on properties.
    /// </summary>
    public class CustomerBLL2
    {
        [Import]
        public ICustomerDAL CustomerDAL { get; set; }
        [Import]
        public Logger Logger { get; set; }
    }
}

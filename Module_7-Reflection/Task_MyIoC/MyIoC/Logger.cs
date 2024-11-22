namespace MyIoC
{
    /// <summary>
    /// Demonstrates the use of ExportAttribute on classes.
    /// </summary>
    [Export]
    public class Logger
    {
        public string Message => "Some message";
    }
}
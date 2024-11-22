using System;

namespace HelloWorldConsole
{
    /// <summary>
    /// The program passes the command-line argument to ClassLibrary's SayHello() method, which reterns
    /// a greeting.
    /// </summary>
   public  class Program
    {
        // Provide name as command-line argument via Properties-> Debug settings of the project.
       public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a name.");
                Environment.Exit(0);
            }
            string greeting = ClassLibrary.HelloClass.SayHello(args[0]);
            Console.WriteLine(greeting);
        }
    }
}

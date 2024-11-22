using System;

namespace ClassLibrary
{
    public class HelloClass
    {
        /// <summary>
        /// Greets the user who enters his name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string SayHello(string name)
        {
            string greeting = $"{DateTime.Now}. Hello, {name}!";
            return greeting;
        }
    }
}

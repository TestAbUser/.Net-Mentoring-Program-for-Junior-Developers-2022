using System;
using NLog;

namespace ExceptionHandlingTask
{
    /// <summary>
    /// Displays the first symbol of every entered line.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            InitializeLogging();
            ConsoleKey key;
            DisplayMenu();
            string firstSymbol = default;
            do
            {
                key = Console.ReadKey(true).Key;
                do
                {
                    CheckForEscape(key);
                    string line = Console.ReadLine();
                    try
                    {
                        firstSymbol += "\n" + line.Substring(0, 1);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Logger log = LogManager.GetCurrentClassLogger();
                        log.Info("ArgumentOutOfRangeException raised due to empty line.");
                    }
                    key = Console.ReadKey(true).Key;
                } while (key != ConsoleKey.D1);

                Console.WriteLine("\nFirst symbols: " + firstSymbol);
                firstSymbol = default;
                DisplayMenu();
            } while (key != ConsoleKey.Escape);
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("\nEnter any number of lines. " +
                              "\nTo display the first symbol of each line press '1' key." +
                              "\nTo exit app press 'Esc' key.");
        }

        public static void CheckForEscape(ConsoleKey key)
        {
            if (key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }

        public static void InitializeLogging()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "file.txt" };

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply config           
            NLog.LogManager.Configuration = config;
        }
    }
}

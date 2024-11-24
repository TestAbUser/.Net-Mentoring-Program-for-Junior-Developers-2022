using System;
using System.IO;
using System.Configuration;

namespace MvcLogParserDemo
{
    // Parsing the logs to report some statistics.
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string logOutputDirectory =config.AppSettings.Settings["path"].Value;
                if (!Directory.Exists(logOutputDirectory))
                {
                    Console.WriteLine("Directory does not exist!");
                }
                else
                {
                    foreach (var file in Directory.GetFiles(logOutputDirectory))
                    {
                        var logQuery = new MSUtil.LogQueryClass();
                        var input = new MSUtil.COMEventLogInputContextClass();
                        var resultDataSet = logQuery.Execute($"SELECT Extract_Token(Text, 2, ' ') as Levels, Count (*) FROM {file} " +
                                                             "Where(Text LIKE '% INFO %' OR Text LIKE '% DEBUG %' OR Text LIKE '% ERROR %') Group By Levels");

                        Console.WriteLine($"File '{Path.GetFileName(file)}' has:");

                        // Displaying the count of INFO, DEBUG and ERROR level messages
                        while (!resultDataSet.atEnd())
                        {
                            var record = resultDataSet.getRecord();
                            Console.WriteLine($"{record.getValue(0)}={record.getValue(1)}");
                            resultDataSet.moveNext();
                        }
                        Console.WriteLine();

                        // Extracting the text of the error level messages 
                        var resultDataSet2 = logQuery.Execute($"SELECT Extract_Token(Text, 0) as Error FROM {file} " +
                                                              "Where(Text NOT LIKE '% INFO %' AND Text NOT LIKE '% DEBUG %')");

                        Console.WriteLine($"File '{Path.GetFileName(file)}' has following errors:");
                        while (!resultDataSet2.atEnd())
                        {
                            var record = resultDataSet2.getRecord();
                            Console.WriteLine($"{record.getValue(0)}");
                            resultDataSet2.moveNext();
                        }
                        Console.WriteLine();
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException exc)
            {
                Console.WriteLine("Unexpected error: " + exc.Message);
            }
        }
    }
}

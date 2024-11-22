using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;
using GlobalizedConsoleApp.Configuration;
using resources = GlobalizedConsoleApp.Resources.Messages;

namespace GlobalizedConsoleApp
{
    public class Program
    {
        private static string newFileName;
        private static CustomConfigurationSection customSection;

        public static void Main()
        {
            List<string> watchedDirectories = new();
            List<FileSystemWatcher> watchers = new();
            try
            {
                customSection = (CustomConfigurationSection)ConfigurationManager.GetSection("customSection");
                if (customSection != null)
                {
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(customSection.Culture);
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(customSection.Culture);

                    foreach (DirectoryElement dir in customSection.Directories)
                    {
                        if (!Directory.Exists(dir.Path))
                        {
                            Directory.CreateDirectory(dir.Path);
                        }

                        VerifyDirectoryExists(dir.Path);
                        watchedDirectories.Add(dir.Path);
                        Console.WriteLine(string.Format(resources.WatchedFolder, dir.Path));
                    }
                }
                else
                {
                    Console.WriteLine(resources.NoCustomSection);
                    Console.WriteLine(resources.ExitAppMessage);
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
            catch (Exception e) when (e is ArgumentNullException || e is NullReferenceException || e is ConfigurationErrorsException)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            for (int i = 0; i < watchedDirectories.Count; i++)
            {
                watchers.Add(new FileSystemWatcher(watchedDirectories[i]));
                watchers[i].NotifyFilter = NotifyFilters.FileName;
                watchers[i].Created += OnCreated;
                watchers[i].Renamed += OnRenamed;
                watchers[i].Filter = "";
                watchers[i].EnableRaisingEvents = true;
            }
            Console.ReadLine();
        }

        private static void OnCreated(object sender, FileSystemEventArgs file)
        {
            if (file.Name != newFileName)
            {
                // display the file name, path and date of creation
                Console.WriteLine(string.Format(resources.Created, file.Name, Path.GetDirectoryName(file.FullPath), File.GetCreationTime(file.FullPath)));
                bool matchFound = false;
                // check the file name against each rule in the config
                foreach (RuleElement rule in customSection.Rules)
                {
                    VerifyDirectoryExists(rule.TargetFolder);
                    bool nameMatchesPattern = Regex.IsMatch(file.Name, rule.Pattern);
                    
                    // if file name matches a rule pattern
                    if (nameMatchesPattern == true)
                    {
                        string targetFolderPath = Path.Combine(rule.TargetFolder, file.Name);
                        
                        // if file is not located in a folder designated for storing files with a particular name pattern 
                        if (file.FullPath != targetFolderPath)
                        {
                            // rename the file
                            newFileName = RenameWithOrderAndDate(rule, file);
                            string newFileFullPath = Path.Combine(Path.GetDirectoryName(file.FullPath), newFileName);
                            string newFileTargetFullPath = Path.Combine(rule.TargetFolder, newFileName);
                            Console.WriteLine(string.Format(resources.MatchesPattern, file.Name, rule.TargetFolder, rule.Pattern));
                            Console.WriteLine(string.Format(resources.MovedTo, file.Name, rule.TargetFolder));
                           
                            // move the file to its designated folder
                            try
                            {
                                File.Move(newFileFullPath, newFileTargetFullPath, true);
                            }
                            catch (Exception e) when (e is UnauthorizedAccessException || e is DirectoryNotFoundException)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine(e.StackTrace);
                            }
                            matchFound = true;
                        }
                        // if the file is located in its designated location just rename it
                        else
                        {
                            RenameWithOrderAndDate(rule, file);
                            Console.WriteLine(string.Format(resources.FileInTargetFolder, file.Name, rule.TargetFolder));
                        }
                        matchFound = true;
                    }
                }
                // if the file name doesn't match any pattern
                if (!matchFound)
                {
                    VerifyDirectoryExists(customSection.OutputDirectory.Path);
                    string defaultFolder = customSection.OutputDirectory.Path;
                    
                    // if the file isn't located at the designated folder
                    if (file.FullPath != Path.Combine(defaultFolder, file.Name))
                    {
                        Console.WriteLine(string.Format(resources.NotMatchPattern, file.Name));
                        
                        // move the file to the designated folder
                        try
                        {
                            File.Move(file.FullPath, Path.Combine(defaultFolder, file.Name), true);
                        }
                        catch (Exception e) when (e is DirectoryNotFoundException || e is ArgumentNullException)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine(e.StackTrace);
                        }
                    }
                    else
                    {
                        Console.WriteLine(string.Format(resources.FileInTargetFolder, file.Name, defaultFolder));
                    }
                }
            }
        }

        private static void VerifyDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine(string.Format(resources.NoDirectoryMessage, path));
                Console.WriteLine(resources.ExitAppMessage);
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        // displays the old name and the new name
        private static void OnRenamed(object sender, RenamedEventArgs file)
        {
            Console.WriteLine(string.Format(resources.Renamed, file.OldName));
            Console.WriteLine(string.Format(resources.NewName, file.Name));
        }

        /// <summary>
        /// Adds ordinal number and the date of creation to the file name
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="file"></param>
        public static string RenameWithOrderAndDate(RuleElement rule, FileSystemEventArgs file)
        {
            string newName = Path.GetFileNameWithoutExtension(file.Name);
            string currentDirectoryName = Path.GetDirectoryName(file.FullPath);
            string newFullPath;
            if (rule.OrdinalNumber)
            {
                int count;
                if (currentDirectoryName == rule.TargetFolder)
                {
                    count = Directory.GetFiles(rule.TargetFolder).Length;
                }
                else
                {
                    count = Directory.GetFiles(rule.TargetFolder).Length + 1;
                }

                newName += "(" + count.ToString() + ")";
            }
            if (rule.RelocationDate)
            {
                newName += "_" + DateTime.Now.ToString(resources.DateFormat);
            }
            newName += Path.GetExtension(file.Name);
            newFullPath = Path.Combine(currentDirectoryName, newName);
            File.Move(file.FullPath, newFullPath, true);
            return newName;
        }
    }
}

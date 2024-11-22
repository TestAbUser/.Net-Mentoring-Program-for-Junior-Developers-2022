using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileSystemVisitorClassLibrary;

namespace FileSystemVisitorUnitTestProject
{
    [TestClass]
    public class FileSystemVisitorUnitTest
    {
        private static readonly string currentDirectoryPath = $"{Environment.CurrentDirectory}";
        private static readonly string testDir = $"{ currentDirectoryPath }\\abcFolder";
        private static readonly string anotherTestDir = $"{ currentDirectoryPath }\\anotherabcFolder";
        private static readonly string invalidTestDirName = $"{ currentDirectoryPath }\\NoSuchDir";
        private static readonly string pattern = "abc";
        private static readonly string patternForConstructor = "ABC";
        private readonly DirectoryInfo di = Directory.CreateDirectory(testDir);
        private readonly DirectoryInfo anotherDi = Directory.CreateDirectory(anotherTestDir);

        [TestMethod]
        public void CancelRequested_ReturnsOnlyFirstEntry()
        {
            int count = default;

            FileSystemVisitorClass fileSystemVisitor = new FileSystemVisitorClass();
            fileSystemVisitor.Args.CancelRequested = true;
            foreach (var file in fileSystemVisitor.SearchSelectedDirectory(currentDirectoryPath, pattern))
            {
                count++;
            }
            // since here CancelRequested is "true" even before anything is found, count stays zero
            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public void RemoveFound_ReturnsOppositePattern()
        {
            int count = default;

            FileSystemVisitorClass fileSystemVisitor = new FileSystemVisitorClass();
            fileSystemVisitor.Args.RemoveFound = true;
            foreach (var file in fileSystemVisitor.SearchSelectedDirectory(currentDirectoryPath, pattern))
            {
                count++;
            }
            Assert.IsTrue(count != 2);
        }

        [TestMethod]
        public void Select_Invalid_Path()
        {
            int count = default;
            FileSystemVisitorClass fileSystemVisitor = new FileSystemVisitorClass();
            foreach (var file in fileSystemVisitor.SearchSelectedDirectory(invalidTestDirName, pattern))
            {
                count++;
            }
            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public void Check_That_Entry_Exists()
        {
            int count = default;
            FileSystemVisitorClass fileSystemVisitor = new FileSystemVisitorClass();
            foreach (var file in fileSystemVisitor.SearchSelectedDirectory(currentDirectoryPath, pattern))
            {
                count++;
            }
            Assert.IsTrue(count == 2);
        }

        [TestMethod]
        public void Check_Overloaded_Constructor()
        {
            int count = default;
            FileSystemVisitorClass fileSystemVisitor = new FileSystemVisitorClass((path) => path.Contains(patternForConstructor.ToLower()));
            foreach (var file in fileSystemVisitor.SearchSelectedDirectory(currentDirectoryPath, patternForConstructor))
            {
                count++;
            }
            Assert.IsTrue(count == 2);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void Use_Invalid_Path()
        {
            string unauthorizedPath = $"C:\\";
            FileSystemVisitorClass fileSystemVisitor = new FileSystemVisitorClass();
            foreach (var file in fileSystemVisitor.SearchSelectedDirectory(unauthorizedPath, pattern))
            {
                throw new NotImplementedException("Test failed!");
            }
        }
    }
}

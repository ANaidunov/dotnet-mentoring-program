using AdvancedCSharp;
using FileSystemVisitorTest.Fakes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemVisitorTest
{
    [TestFixture]
    public class FileSystemVisitorTest
    {
        private FileSystemVisitor visitor;
        private FileSystemMock fileSystem;

        [OneTimeSetUp]
        public void SetupFileSystem()
        {
            fileSystem = new FileSystemMock();
            fileSystem.InitializeMockFileSystem();
        }

        [SetUp]
        public void Setup()
        {
            visitor = new FileSystemVisitor(() => "*.jpg");
            visitor.Start += ConsoleLog;
            visitor.Finish += ConsoleLog;
            visitor.FileFound += ConsoleLog;
            visitor.FilteredFileFound += ConsoleLog;
            visitor.DirectoryFound += ConsoleLog;
            visitor.FilteredDirectoryFound += ConsoleLog;
        }

        [Test]
        public void SearchTest()
        {
            string root = fileSystem.GetRootPath();
            var expectedList = new string[]
            {
                Path.Combine(root, @"dir1"),
                Path.Combine(root, @"dir2"),
                Path.Combine(root, @"dir1\dir1"),
                Path.Combine(root, @"dir1\dir2"),
                Path.Combine(root, @"dir2\dir1"),
                Path.Combine(root, @"file1.txt"),
                Path.Combine(root, @"file2.txt"),
                Path.Combine(root, @"file3.bat"),
                Path.Combine(root, @"dir1\file1.txt"),
                Path.Combine(root, @"dir1\file2.doc"),
                Path.Combine(root, @"dir1\file3.jpg"),
                Path.Combine(root, @"dir1\dir1\file1.txt"),
                Path.Combine(root, @"dir1\dir1\file2.doc"),
                Path.Combine(root, @"dir1\dir1\file2.jpg"),
                Path.Combine(root, @"dir1\dir2\file1.txt"),
                Path.Combine(root, @"dir1\dir2\file2.doc"),
                Path.Combine(root, @"dir1\dir2\file2.jpg"),
                Path.Combine(root, @"dir2\dir1\file1.txt"),
                Path.Combine(root, @"dir2\dir1\file2.doc"),
                Path.Combine(root, @"dir2\dir1\file2.jpg")
            };

            var actualList = visitor.Search(root);

            Assert.That(actualList, Is.EqualTo(expectedList));
        }

        [Test]
        public void DirectoriesOnlySearchTest()
        {
            string root = fileSystem.GetRootPath();
            var expectedList = new string[]
            {
                Path.Combine(root, @"dir1"),
                Path.Combine(root, @"dir2"),
                Path.Combine(root, @"dir1\dir1"),
                Path.Combine(root, @"dir1\dir2"),
                Path.Combine(root, @"dir2\dir1")
            };

            visitor.ExcludeFiles();
            var actualList = visitor.Search(root);

            Assert.That(actualList, Is.EqualTo(expectedList));
        }

        [Test]
        public void FilesOnlySearchTest()
        {
            string root = fileSystem.GetRootPath();
            var expectedList = new string[]
            {
                Path.Combine(root, @"file1.txt"),
                Path.Combine(root, @"file2.txt"),
                Path.Combine(root, @"file3.bat"),
                Path.Combine(root, @"dir1\file1.txt"),
                Path.Combine(root, @"dir1\file2.doc"),
                Path.Combine(root, @"dir1\file3.jpg"),
                Path.Combine(root, @"dir1\dir1\file1.txt"),
                Path.Combine(root, @"dir1\dir1\file2.doc"),
                Path.Combine(root, @"dir1\dir1\file2.jpg"),
                Path.Combine(root, @"dir1\dir2\file1.txt"),
                Path.Combine(root, @"dir1\dir2\file2.doc"),
                Path.Combine(root, @"dir1\dir2\file2.jpg"),
                Path.Combine(root, @"dir2\dir1\file1.txt"),
                Path.Combine(root, @"dir2\dir1\file2.doc"),
                Path.Combine(root, @"dir2\dir1\file2.jpg")
            };

            visitor.ExcludeFolders();
            var actualList = visitor.Search(root);

            Assert.That(actualList, Is.EqualTo(expectedList));
        }

        [Test]
        public void FilesByPatternSearchTest()
        {
            string root = fileSystem.GetRootPath();
            var expectedList = new string[]
            {
                Path.Combine(root, @"dir1\file3.jpg"),
                Path.Combine(root, @"dir1\dir1\file2.jpg"),
                Path.Combine(root, @"dir1\dir2\file2.jpg"),
                Path.Combine(root, @"dir2\dir1\file2.jpg")
            };

            visitor.ExcludeFolders();
            var actualList = visitor.Search(root, true);

            Assert.That(actualList, Is.EqualTo(expectedList));
        }


        [Test]
        public void EnumeratorTest()
        {
            string root = fileSystem.GetRootPath();
            var visitor = new FileSystemVisitor(() => "*", root);

            var expectedList = new string[]
            {
                Path.Combine(root, @"dir1"),
                Path.Combine(root, @"dir2"),
                Path.Combine(root, @"dir1\dir1"),
                Path.Combine(root, @"dir1\dir2"),
                Path.Combine(root, @"dir2\dir1"),
                Path.Combine(root, @"file1.txt"),
                Path.Combine(root, @"file2.txt"),
                Path.Combine(root, @"file3.bat"),
                Path.Combine(root, @"dir1\file1.txt"),
                Path.Combine(root, @"dir1\file2.doc"),
                Path.Combine(root, @"dir1\file3.jpg"),
                Path.Combine(root, @"dir1\dir1\file1.txt"),
                Path.Combine(root, @"dir1\dir1\file2.doc"),
                Path.Combine(root, @"dir1\dir1\file2.jpg"),
                Path.Combine(root, @"dir1\dir2\file1.txt"),
                Path.Combine(root, @"dir1\dir2\file2.doc"),
                Path.Combine(root, @"dir1\dir2\file2.jpg"),
                Path.Combine(root, @"dir2\dir1\file1.txt"),
                Path.Combine(root, @"dir2\dir1\file2.doc"),
                Path.Combine(root, @"dir2\dir1\file2.jpg")
            };

            List<string> actualList = new List<string>();

            foreach(var element in visitor)
            {
                actualList.Add(element);
            }

            Assert.That(actualList, Is.EqualTo(expectedList));
        }
        private void ConsoleLog(object sender, ProgressArgs args)
        {
            Console.WriteLine(args.Message);
        }
    }
}

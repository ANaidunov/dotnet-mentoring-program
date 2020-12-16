using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemVisitorTest.Fakes
{
    class FileSystemMock
    {
        public string CurrentDirectory { get; private set; } = Environment.CurrentDirectory;
        public void InitializeMockFileSystem()
        {
            DeleteMockFileSystem();

            foreach (var dir in GetDirectoryElements())
            {
                string path = Path.Combine(CurrentDirectory, dir);
                Directory.CreateDirectory(path);
            }

            foreach (var file in GetFileElements())
            {
                string path = Path.Combine(CurrentDirectory, file);
                File.Create(path);
            }
        }
        public string GetRootPath()
        {
            return Path.Combine(CurrentDirectory, @"root\");
        }

        private void DeleteMockFileSystem()
        {
            string path = GetRootPath();

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        private string[] GetDirectoryElements()
        {
            string[] dirTree = {
                @"root\",
                @"root\dir1",
                @"root\dir2",
                @"root\dir1\dir1",
                @"root\dir1\dir2",
                @"root\dir2\dir1",
            };

            return dirTree;
        }

        private string[] GetFileElements()
        {
            return new string[]
            {
                @"root\file1.txt",
                @"root\file2.txt",
                @"root\file3.bat",
                @"root\dir1\file1.txt",
                @"root\dir1\file2.doc",
                @"root\dir1\file3.jpg",
                @"root\dir1\dir1\file1.txt",
                @"root\dir1\dir1\file2.doc",
                @"root\dir1\dir1\file2.jpg",
                @"root\dir1\dir2\file1.txt",
                @"root\dir1\dir2\file2.doc",
                @"root\dir1\dir2\file2.jpg",
                @"root\dir2\dir1\file1.txt",
                @"root\dir2\dir1\file2.doc",
                @"root\dir2\dir1\file2.jpg"
            };
        }
    }
}

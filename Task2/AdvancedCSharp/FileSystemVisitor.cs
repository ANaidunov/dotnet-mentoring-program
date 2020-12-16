using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp
{
    public class FileSystemVisitor : IEnumerable<string>
    {
        private readonly Func<string> filterDelegate;
        private readonly string startPointPath;
        public bool IsSearchActive { get; private set; } = true;
        public bool IsFilesIncluded { get; private set; } = true;
        public bool IsDirectoriesIncluded { get; private set; } = true;

        public event EventHandler<ProgressArgs> Start;
        public event EventHandler<ProgressArgs> Finish;
        public event EventHandler<ProgressArgs> FileFound;
        public event EventHandler<ProgressArgs> DirectoryFound;
        public event EventHandler<ProgressArgs> FilteredFileFound;
        public event EventHandler<ProgressArgs> FilteredDirectoryFound;

        public FileSystemVisitor(Func<string> filterDelegate)
        {
            if (filterDelegate is null)
            {
                throw new ArgumentNullException(nameof(filterDelegate));
            }
            this.filterDelegate = filterDelegate;
        }

        public FileSystemVisitor(Func<string> filterDelegate, string startPointPath)
        {
            if (string.IsNullOrEmpty(startPointPath))
            {
                throw new ArgumentException(nameof(startPointPath));
            }
            if (filterDelegate is null)
            {
                throw new ArgumentNullException(nameof(filterDelegate));
            }
            this.filterDelegate = filterDelegate;
            this.startPointPath = startPointPath;
        }

        public void Stop()
        {
            this.IsSearchActive = false;
        }

        public void ExcludeFiles()
        {
            this.IsFilesIncluded = false;
        }

        public void ExcludeFolders()
        {
            this.IsDirectoriesIncluded = false;
        }

        public IEnumerable<string> Search(string path, bool isPatternFilterOn = false)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Root directory is not found. Try to check a start path.");
            }

            OnStart(new ProgressArgs { IsSearchActive = this.IsSearchActive, Message = "Search started" });
            if (IsDirectoriesIncluded)
            {
                foreach (var directory in GetDirectories(path, isPatternFilterOn))
                {
                    yield return directory;
                }
            }

            if (IsFilesIncluded)
            {
                foreach (var file in GetFiles(path, isPatternFilterOn))
                {
                    yield return file;
                }
            }

            OnFinish(new ProgressArgs { IsSearchActive = this.IsSearchActive, Message = "Search finished" });
        }

        private IEnumerable<string> GetDirectories(string path, bool isPatternFilterOn = false)
        {
            string filter = isPatternFilterOn ? filterDelegate.Invoke() : "*";

            string[] directories = { };
            try
            {
                directories = Directory.GetDirectories(path, filter, SearchOption.AllDirectories);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception thrown, exception message: {exception.Message}");
            }

            if (isPatternFilterOn)
            {
                var args = new ProgressArgs
                {
                    IsSearchActive = this.IsSearchActive,
                    Message = "Filtered directories found:"
                };
                OnFilteredDirectoryFound(args);
            }
            else
            {
                var args = new ProgressArgs
                {
                    IsSearchActive = this.IsSearchActive,
                    Message = "Directories found:"
                };
                OnDirectoryFound(args);
            }

            foreach (string directory in directories)
            {
                if (this.IsSearchActive)
                {
                    yield return directory;
                }
                else
                {
                    break;
                }
            }
        }

        private IEnumerable<string> GetFiles(string path, bool isPatternFilterOn = false)
        {
            string filter = isPatternFilterOn ? filterDelegate.Invoke() : "*";

            string[] files = { };
            try
            {
                files = Directory.GetFiles(path, filter, SearchOption.AllDirectories);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception thrown, exception message: {exception.Message}");
            }

            if (isPatternFilterOn)
            {
                var args = new ProgressArgs
                {
                    IsSearchActive = this.IsSearchActive,
                    Message = "Filtered files found:"
                };
                OnFileFound(args);
            }
            else
            {
                var args = new ProgressArgs
                {
                    IsSearchActive = this.IsSearchActive,
                    Message = "Files found:"
                };
                OnFileFound(args);
            }

            foreach (string file in files)
            {
                if (this.IsSearchActive)
                {
                    yield return file;
                }
                else
                {
                    break;
                }
            }
        }

        protected void OnStart(ProgressArgs args)
        {
            if (Start is null || !args.IsSearchActive)
            {
                return;
            }

            Start(this, args);
        }

        protected void OnFinish(ProgressArgs args)
        {
            if (Finish is null || !args.IsSearchActive)
            {
                return;
            }

            Finish(this, args);
        }

        protected void OnFileFound(ProgressArgs args)
        {
            if (FileFound is null || !args.IsSearchActive)
            {
                return;
            }

            FileFound(this, args);
        }

        protected void OnFilteredFileFound(ProgressArgs args)
        {
            if (FilteredFileFound is null || !args.IsSearchActive)
            {
                return;
            }

            FilteredFileFound(this, args);
        }

        protected void OnDirectoryFound(ProgressArgs args)
        {
            if (DirectoryFound is null || !args.IsSearchActive)
            {
                return;
            }

            DirectoryFound(this, args);
        }

        protected void OnFilteredDirectoryFound(ProgressArgs args)
        {
            if (FilteredDirectoryFound is null || !args.IsSearchActive)
            {
                return;
            }

            FilteredDirectoryFound(this, args);
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var element in Search(this.startPointPath, false))
            {
                yield return element;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
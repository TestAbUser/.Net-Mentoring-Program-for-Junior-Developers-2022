using System;
using System.IO;
using System.Collections.Generic;

namespace FileSystemVisitorClassLibrary
{
    public class FileSystemVisitorClass
    {
        private readonly Func<string, bool> _filter;
        private readonly EntryFoundArgs args = new EntryFoundArgs();

        public FileSystemVisitorClass() { }
        public FileSystemVisitorClass(Func<string, bool> filter)
        {
            _filter = filter;
        }

        public event EventHandler<EntryFoundArgs> FileSystemEntriesFound;
        public event EventHandler<EntryFoundArgs> FilteredFileSystemEntriesFound;
        public event EventHandler SearchStarted;
        public event EventHandler SearchFinished;
        public EntryFoundArgs Args
        {
            get { return args; }
        }

        /// <summary>
        /// Searches for directories and files starting in a selected directory.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public IEnumerable<string> SearchSelectedDirectory(string dir, string pattern)
        {
            if (Directory.Exists(dir))
            {
                // inform that the search started
                OnSearchStarted(EventArgs.Empty);

                // start searching files and directories
                foreach (var entry in Directory.GetFileSystemEntries(dir))
                {
                    // count found items
                    args.NumberOfEntries++;
                    if (args.CancelRequested)
                        break;
                    // return nested folders and files
                    foreach (var nestedEntry in SearchSelectedDirectory(entry, pattern))
                    {
                        yield return nestedEntry;
                    }
                    // removes the found entry from the list
                    if (args.RemoveFound)
                    {
                        // if filter is not empty
                        if (_filter != null)
                        {
                            // if no entry matches the filter condition
                            if (!_filter.Invoke(entry))
                            {
                                if (args.CancelRequested)
                                    break;
                                OnFilteredFileSystemEntriesFound(args);
                                yield return entry;
                            }
                        }
                        // if none of the folders or files match the search parameters
                        else if (!entry.Contains(pattern))
                        {
                            if (args.CancelRequested)
                                break;
                            OnFilteredFileSystemEntriesFound(args);
                            yield return entry;
                        }
                    }
                    // if filter is not empty and the item matches the filter condition
                    else if (_filter != null && _filter.Invoke(entry))
                    {
                        if (args.CancelRequested)
                            break;
                        OnFilteredFileSystemEntriesFound(args);
                        yield return entry;
                    }
                    // if the item name matches the search string
                    else if (entry.Contains(pattern))
                    {
                        if (args.CancelRequested)
                            break;
                        OnFilteredFileSystemEntriesFound(args);
                        yield return entry;
                    }
                    args.NumberOfEntries--;
                }
            }
            OnFileSystemEntriesFound(args);
        }

        // raises FileSystemEntriesFound event signalling that unfiltered entries has been found
        protected virtual void OnFileSystemEntriesFound(EntryFoundArgs eventArgs)
        {
            FileSystemEntriesFound?.Invoke(this, eventArgs);
            if (eventArgs.NumberOfEntries == 0)
            {
                OnSearchFinished(EventArgs.Empty);
            }
        }

        // raises FilteredFileSystemEntriesFound event signalling that filtered entries has been found
        protected virtual void OnFilteredFileSystemEntriesFound(EntryFoundArgs eventArgs)
        {
            FilteredFileSystemEntriesFound?.Invoke(this, eventArgs);
            if (eventArgs.CancelRequested)
            {
                OnSearchFinished(EventArgs.Empty);
            }
        }

        // raises SearchStarted event signalling that search has started
        protected virtual void OnSearchStarted(EventArgs e)
        {
            EventHandler handler = SearchStarted;
            handler?.Invoke(this, e);
        }

        // raises SearchFinished event signalling that search has finished
        public virtual void OnSearchFinished(EventArgs e)
        {
            EventHandler handler = SearchFinished;
            handler?.Invoke(this, e);
        }
    }
}

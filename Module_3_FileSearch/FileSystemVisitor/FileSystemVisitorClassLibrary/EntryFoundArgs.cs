using System;

namespace FileSystemVisitorClassLibrary
{
    public class EntryFoundArgs : EventArgs
    {
        public bool CancelRequested { get; set; }
        public bool RemoveFound { get; set; }
        public int NumberOfEntries { get; set; }
    }
}

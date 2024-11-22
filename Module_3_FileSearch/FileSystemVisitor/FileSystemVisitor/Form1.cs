using System;
using System.Windows.Forms;
using FileSystemVisitorClassLibrary;

namespace FileSystemVisitor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    listBox.Items.Clear();
                    pathTextBox.Text = dialog.SelectedPath;
                }
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            FileSystemVisitorClass fileSystemVisitor = new FileSystemVisitorClass((path) => path.Contains(searchTextBox.Text.ToLower()));
            fileSystemVisitor.SearchStarted += SetStartedStatus;
            fileSystemVisitor.SearchFinished += SetFinishedStatus;
            fileSystemVisitor.FileSystemEntriesFound += FileSystemEntriesFound;
            fileSystemVisitor.FilteredFileSystemEntriesFound += FilteredFileSystemEntriesFound;
            listBox.Items.Clear();
            foreach (var file in fileSystemVisitor.SearchSelectedDirectory(pathTextBox.Text, searchTextBox.Text))
            {
                listBox.Items.Add(file);
            }
        }

        // event handler for the unfiltered entries found during the search 
        private void FileSystemEntriesFound(object sender, EntryFoundArgs e)
        {
            e.RemoveFound = false;
            if (excludeItemsCheckBox.Checked)
            {
                e.RemoveFound = true;
            }
        }

        // event handler for the filtered entries found during the search
        private void FilteredFileSystemEntriesFound(object sender, EntryFoundArgs e)
        {
            e.CancelRequested = false;
            if (stopSearchCheckBox.Checked)
            {
                e.CancelRequested = true;
            }
        }

        // event handler that notifies about the start of the search
        private void SetStartedStatus(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "Search started";
            statusStrip.Refresh();
        }

        // event handler that notifies about the finish of the search
        private void SetFinishedStatus(object sender, EventArgs e)
        {
            FilesFoundLabel.Text = "Files and Folders found";
            toolStripStatusLabel.Text = "Search finished";
            statusStrip.Refresh();
        }
    }
}

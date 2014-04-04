using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GSLib.Setup
{
    public class InstallingEventArgs : EventArgs 
    {
        FileData file;
        DirectoryInfo directory;

        public InstallingEventArgs(FileData file) : base()
        {
            this.file = file;
            directory = null;
        }

        public InstallingEventArgs(DirectoryInfo directory) : base()
        {
            this.directory = directory;
            file = null;
        }

        public FileData File
        {
            get { return file; }
        }

        public DirectoryInfo Directory
        {
            get
            {
                return directory;
            }
        }
    }

    public class InstalledEventArgs : InstallingEventArgs
    {
        bool succeeded;
        Exception ex;

        public InstalledEventArgs(FileData file, bool succeeded, Exception ex)
            : base(file)
        {
            this.succeeded = succeeded;
            this.ex = ex;
        }

        public InstalledEventArgs(DirectoryInfo directory, bool succeeded, Exception ex)
            : base(directory)
        {
            this.succeeded = succeeded;
            this.ex = ex;
        }

        public bool Succeeded
        {
            get
            {
                return succeeded;
            }
        }

        public Exception Ex
        {
            get
            {
                return ex;
            }
        }
    }
}

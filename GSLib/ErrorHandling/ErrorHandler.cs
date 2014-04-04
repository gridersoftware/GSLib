using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GSLib.ErrorHandling
{
    public class ErrorHandler
    {
        protected List<Exception> exceptions;
        protected List<string> notes;

        /// <summary>
        /// Generic constructor.
        /// </summary>
        public ErrorHandler()
        {
            exceptions = new List<Exception>();
            notes = new List<string>();
        }

        /// <summary>
        /// Raised when an exception is caught.
        /// </summary>
        public event EventHandler<ErrorHandlerEventArgs> ErrorCaught;

        /// <summary>
        /// Add exception to log and raises the ErrorCaught event.
        /// </summary>
        /// <param name="ex">Exception to log.</param>
        public void CatchError(Exception ex)
        {
            exceptions.Add(ex);
            ErrorCaught(this, new ErrorHandlerEventArgs(ex));
        }

        /// <summary>
        /// Adds a line of text to the log.
        /// </summary>
        /// <param name="line">Line to add.</param>
        public void AddToLog(string line)
        {
            notes.Add(line);
        }

        /// <summary>
        /// Write log to the hard disk.
        /// </summary>
        /// <param name="path">Path to write to.</param>
        public void GenerateLog(string path)
        {
            List<string> lines = new List<string>();

            try
            {
                foreach (Exception ex in exceptions)
                {
                    lines.Add("Message: " + ex.Message);
                    if (ex.InnerException != null) lines.Add("Inner Message: " + ex.InnerException.Message);
                    lines.Add("Call Stack: " + ex.StackTrace);
                    lines.Add("");
                }
                foreach (string line in notes)
                {
                    lines.Add(line);
                }
                File.WriteAllLines(path, lines.ToArray());
            }
            catch
            {
                throw;
            }
        }
    }
}

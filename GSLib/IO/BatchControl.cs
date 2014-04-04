using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace GSLib.Common
{
    /// <summary>
    /// Builds and executes Windows 2000-compatible Command Prompt batch files.
    /// </summary>
    public class BatchControl
    {
        /// <summary>
        /// Builds a Windows 2000-compatible Command Prompt batch file with the lines provided.
        /// </summary>
        /// <param name="path">Location to save the file. Include the filename.</param>
        /// <param name="lines">Code to write to the file.</param>
        /// <remarks>Deprecated. It might just be easier to use System.IO.File.WriteAllLines(). That's what this newer version of this class uses.</remarks>
        [Obsolete("Use System.IO.File.WriteAllLines")]
        public static void BuildBatch(string path, string[] lines)
        {
            File.WriteAllLines(path, lines);
        }

        /// <summary>
        /// Executes a Windows 2000-compatible Command Prompt batch file.
        /// </summary>
        /// <param name="path">Path of the file to execute.</param>
        /// <returns>Returns true on success, false if the file does not exist.</returns>
        public static bool ExecuteBatch(string path)
        {
            if (File.Exists(path))
            {
                string execPath = Environment.GetEnvironmentVariable("ComSpec");
                string args = "/c " + path;
                try
                {
                    Process.Start(path, args);
                }
                catch
                {
                    throw;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

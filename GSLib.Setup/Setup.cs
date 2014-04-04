using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using GSLib.ErrorHandling;
using GSLib.Collections.Trees;

namespace GSLib.Setup
{
    /// <summary>
    /// Represents a setup configuration. 
    /// </summary>
    public class Setup
    {
        TreeNode<DirectoryData> directoryTree;
        TreeNode<Feature> featureTree;
        string applicationName;

        /// <summary>
        /// Gets or sets a string value containing the application's End-User License Agreement.
        /// </summary>
        public string EULA { get; set; }

        /// <summary>
        /// Gets the directory tree containing all of the files and folders in the setup.
        /// </summary>
        public TreeNode<DirectoryData> DirectoryTree
        {
            get
            {
                return directoryTree;
            }
        }

        /// <summary>
        /// Gets the feature tree containing all features.
        /// </summary>
        public TreeNode<Feature> FeatureTree
        {
            get
            {
                return featureTree;
            }
        }

        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        public string Name
        {
            get
            {
                return applicationName;
            }
        }

        //internal static Internal.AutoErrorHandler errorHandler = new Internal.AutoErrorHandler();

        //internal static void CatchError(object sender, AutoErrorHandlerEventArgs e)
        //{
        //    errorHandler.CatchError(e.ex, e.UserData);
        //}

        /// <summary>
        /// Creates a new instance of Setup
        /// </summary>
        /// <param name="applicationName">Name of the application to install</param>
        public Setup(string applicationName)
        {
            //errorHandler.ErrorCaught += new EventHandler<AutoErrorHandlerEventArgs>(CatchError);

            directoryTree = new TreeNode<DirectoryData>(new DirectoryData(""));
            featureTree = new TreeNode<Feature>(new Feature("All", "All", true));  

            this.applicationName = applicationName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Collections.Trees
{
    public interface IGenericTreeNode<TreeType, T> where TreeType : ITreeNode<T>
    {
        /// <summary>
        /// Gets the parent of the node.
        /// </summary>
        TreeType Parent
        {
            get;
        }

        /// <summary>
        /// Gets the siblings of the node.
        /// </summary>
        TreeType[] Siblings
        {
            get;
        }
        /// <summary>
        /// Gets the collection of child nodes.
        /// </summary>
        TreeType[] Children
        { get; }
    }
}

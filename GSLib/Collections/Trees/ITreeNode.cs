using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Collections.Trees
{
    /// <summary>
    /// Represents a tree node.
    /// </summary>
    /// <typeparam name="T">Type of value to represent.</typeparam>
    /// VB: Public Interface ITreeNode(Of T) 
    ///               Inherits IEnumerable(Of ITreeNode(Of T))
    public interface ITreeNode<T> : IEnumerable<ITreeNode<T>>
    {
        /****************************************
         * Properties
         ***************************************/

        /// <summary>
        /// Gets or sets the value of the node.
        /// </summary>
        /// VB: Property Value As T
        T Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the parent of the node.
        /// </summary>
        /// VB: ReadOnly Property Parent As ITreeNode(Of T)
        ITreeNode<T> Parent
        {
            get;
        }

        /// <summary>
        /// Gets the siblings of the node.
        /// </summary>
        /// VB: ReadOnly Property Siblings ITreeNode(Of T)()
        ITreeNode<T>[] Siblings
        {
            get;
        }

        /// <summary>
        /// Gets the depth of the node from the root node.
        /// </summary>
        /// VB: ReadOnly Property Depth As Integer
        int Depth
        { get; }

        /// <summary>
        /// Gets the number of child nodes.
        /// </summary>
        /// VB: ReadOnly Property Count As Integer
        int Count
        { get; }

        /// <summary>
        /// Gets the collection of child nodes.
        /// </summary>
        /// VB: ReadOnly Property Children As ITreeNode(Of T)()
        ITreeNode<T>[] Children
        { get; }

        /// <summary>
        /// Determines if the node allows duplicate children.
        /// </summary>
        /// Property AllowsDuplicates As Boolean
        bool AllowsDuplicates
        {
            get;
            set;
        }

        /*************************************
         * Methods
         ************************************/
        /// <summary>
        /// Adds a child to the node with the given value.
        /// </summary>
        /// <param name="value">Value of the child node.</param>
        /// <exception cref="ArgumentException">Thrown if a duplicate value is added, and AllowsDuplicates is false.</exception>
        /// VB: Sub Add(ByVal value As T)
        void Add(T value);

        /// <summary>
        /// Removes the first child with the given value from the node.
        /// </summary>
        /// <param name="value">Value to remove.</param>
        /// VB: Sub Remove(ByVal value As T)
        void Remove(T value);

        /// <summary>
        /// Determines if the tree contains the given value.
        /// </summary>
        /// <param name="value">Value to look for.</param>
        /// <returns>Returns true if the value exists. Otherwise, returns false.</returns>
        /// VB: Function Contains(ByVal value As T) As Boolean
        bool Contains(T value);
    }
}

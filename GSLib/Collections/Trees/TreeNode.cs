using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Collections.Trees
{
    /// <summary>
    /// Represents a tree node. Can be used to build tree structures.
    /// </summary>
    /// <typeparam name="T">Type of value to contain.</typeparam>
    /// VB: Public Class TreeNode(Of T)
    ///          Inherits ITreeNode(Of T)
    public class TreeNode<T> : ITreeNode<T>
    {
        /***********************************************************************
         * Fields
         **********************************************************************/
        #region Fields
        /// <summary>
        /// Contains the value of the node.
        /// </summary>
        /// <remarks>This field can only be accessed directly by inheriting TreeNode.</remarks>
        /// VB: Protected value As T
        protected T value;    // value of the node

        /// <summary>
        /// Contains the parent of the node.
        /// </summary>
        /// <remarks>This field can only be accessed directly by inheriting TreeNode.</remarks>
        /// VB: Protected parent As TreeNode(Of T)
        protected TreeNode<T> parent;     // the node's parent

        /// <summary>
        /// Contains a list of children.
        /// </summary>
        /// <remarks>This field can only be accessed directly by inheriting TreeNode. 
        /// <warning>When inheriting TreeNode, you must initialize the children field in the 
        /// constructor before it can be used. Failing to do so may cause an NullReferenceException.
        /// </warning></remarks>
        /// VB: Protected children As List(Of TreeNode(Of T))
        protected List<TreeNode<T>> children;     // initialize in constructors

        /// <summary>
        /// Determines whether duplicate children are allowed.
        /// </summary>
        /// <remarks>This field can only be accessed directly by inheriting TreeNode.</remarks>
        /// VB: Protected allowDuplicates As Boolean
        protected bool allowDuplicates;   // determines if duplicate children can be added

        #endregion

        /***********************************************************************
         * Constructors
         **********************************************************************/
        #region Constructors

        /// <summary>
        /// Default constructor. Sets the value to the default of T.
        /// </summary>
        /// VB: Public Sub New()
        public TreeNode()
        {
            children = new List<TreeNode<T>>();
            value = default(T);
            allowDuplicates = true;
            parent = null;
        }

        /// <summary>
        /// Constructor. Sets the value to the given value.
        /// </summary>
        /// <param name="value">Value to set to the TreeNode.</param>
        /// VB: Public Sub New(ByVal value As T)
        public TreeNode(T value)
        {
            this.value = value;
            children = new List<TreeNode<T>>();
            parent = null;
            allowDuplicates = true;
        }

        #endregion 

        /***********************************************************************
         * Properties
         **********************************************************************/
        #region Properties

        /// <summary>
        /// Gets or sets the value of the TreeNode.
        /// </summary>
        /// VB: Public Property Value As T
        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value; 
            }
        }

        /// <summary>
        /// Gets the parent of the TreeNode.
        /// </summary>
        /// VB: Public ReadOnly Property Parent As TreeNode(Of T)
        public virtual TreeNode<T> Parent
        {
            get
            {
                return parent;
            }
        }

        /// <summary>
        /// Gets the parent of the TreeNode.
        /// </summary>
        ITreeNode<T> ITreeNode<T>.Parent
        {
            get
            {
                return parent;
            }
        }

        ITreeNode<T>[] ITreeNode<T>.Siblings
        {
            get
            {
                return Siblings;
            }
        }

        ITreeNode<T>[] ITreeNode<T>.Children
        {
            get 
            {
                return Children;
            }
        }

        public TreeNode<T>[] Siblings
        {
            get
            {
                List<TreeNode<T>> sib = new List<TreeNode<T>>(Parent.children);
                sib.Remove(this);
                return sib.ToArray();
            }
        }

        public TreeNode<T>[] Children
        {
            get
            {
                return children.ToArray();
            }
        }

        /// <summary>
        /// Gets the depth of the node from the root node.
        /// </summary>
        /// VB: Public ReadOnly Property Depth As Integer
        public int Depth
        {
            get
            {
                int depth = 0;
                TreeNode<T> current = this;
                while (current.Parent != null)
                {
                    depth++;
                    current = current.Parent;
                }
                return depth;
            }
        }

        /// <summary>
        /// Gets the number of items in the TreeNode.
        /// </summary>
        /// VB: Public ReadOnly Property Count As Integer
        public int Count
        {
            get
            {
                return children.Count;
            }
        }

        

        /// <summary>
        /// Gets or sets the setting determining if the TreeNode allows duplicate children to be added.
        /// </summary>
        /// VB: Public Property AllowsDuplicates As Boolean
        public bool AllowsDuplicates
        {
            get
            {
                return allowDuplicates;
            }
            set
            {
                allowDuplicates = value;
            }
        }

        /// <summary>
        /// Gets or sets the child node at the given index.
        /// </summary>
        /// <param name="index">Index of the child node.</param>
        /// <returns>Returns the child node.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is less than zero, or greater than the last child index.</exception>
        /// VB: Public Property Item(ByVal index As Integer) As TreeNode(Of T)
        public TreeNode<T> this[int index]
        {
            get
            {
                if (index > children.Count - 1)
                {
                    throw new IndexOutOfRangeException();
                }

                return children[index];
            }
            set
            {
                if (index > children.Count - 1)
                {
                    throw new IndexOutOfRangeException();
                }

                children[index] = value;
            }
        }

        #endregion 

        /***********************************************************************
         * Methods
         **********************************************************************/
        #region Methods

        /// <summary>
        /// Adds a new leaf with the given value.
        /// </summary>
        /// <param name="value">Value to add.</param>
        /// <exception cref="NotSupportedException">Thrown when AllowDuplicates is false, and a duplicate child is added.</exception>
        /// VB: Public Sub Add(ByVal value As T)
        public virtual void Add(T value)
        {
            if (!allowDuplicates && Contains(value)) throw new NotSupportedException("Cannot add duplicate children.");
            TreeNode<T> item = new TreeNode<T>(value);
            item.parent = this;
            children.Add(item);
        }

        /// <summary>
        /// Adds the given leaf.
        /// </summary>
        /// <param name="value">Node to add.</param>
        /// <exception cref="NotSupportedException">Thrown when AllowDuplicates is false, and a duplicate child is added.</exception>
        public void Add(TreeNode<T> value)
        {
            if (!allowDuplicates && Contains(value.Value)) throw new NotSupportedException("Cannot add duplicate children.");
            value.parent = this;
            children.Add(value);
        }

        /// <summary>
        /// Clears all children.
        /// </summary>
        /// VB: Public Sub Clear()
        public void Clear()
        {
            children.Clear();
        }

        /// <summary>
        /// Removes the child with the given value, if it exists.
        /// </summary>
        /// <param name="value">Value to remove</param>
        /// VB: Public Sub Remove(ByVal value As T)
        public void Remove(T value)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].value.Equals(value))
                {
                    children.RemoveAt(i);
                    return;
                }
            }
        }

        /// <summary>
        /// Copies the children of the TreeNode to a new array.
        /// </summary>
        /// <returns>An array containing the children of the TreeNode.</returns>
        /// VB: Public Function ToArray() As TreeNode(Of T)()
        public TreeNode<T>[] ToArray()
        {
            return children.ToArray();
        }

        /// <summary>
        /// Gets the IEnumerator for the children of this TreeNode.
        /// </summary>
        /// <returns>Returns the IEnumerator for the children.</returns>
        /// VB: Public Function GetEnumerator() As IEnumerator(Of ITreeNode(Of T))
        public IEnumerator<ITreeNode<T>> GetEnumerator()
        {
            return children.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Determines if the TreeNode children contains the current value.
        /// </summary>
        /// <param name="value">Value to look for.</param>
        /// <returns>Returns true if one of the children has the value. Otherwise, returns false.</returns>
        /// VB: Public Function Contains(ByVal value As T) As Boolean
        public bool Contains(T value)
        {
            foreach (TreeNode<T> item in children)
            {
                if (item.Value.Equals(value)) return true;
            }
            return false;
        }

        /// <summary>
        /// If a child with the given value exists, finds the first instance of that value and returns the containing TreeNode.
        /// </summary>
        /// <param name="value">Value to look for.</param>
        /// <returns>Returns the TreeNode containing the value. If the value is not found, returns null.</returns>
        public virtual TreeNode<T> FindChild(T value)
        {
            foreach (TreeNode<T> item in children)
            {
                if (item.Value.Equals(value)) return item;
            }
            return null;
        }

        /// <summary>
        /// If a child with the given value exists, finds the first instance of that value and returns the containing TreeNode.
        /// </summary>
        /// <param name="value">Value to look for.</param>
        /// <param name="comparer">Comparer to use.</param>
        /// <returns>Returns the TreeNode containing the value. If the value is not found, returns null.</returns>
        public TreeNode<T> FindChild(T value, IEqualityComparer<T> comparer)
        {
            foreach (TreeNode<T> item in children)
            {
                if (comparer.Equals(value, item.Value)) return item;
            }
            return null;
        }

        public TreeNode<T>[] GetChildren()
        {
            return children.ToArray();
        }

        /// <summary>
        /// Gets the path of all of the elements from the root of the element to 
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            return GetPath('\\');
        }

        public string GetPath(Func<T, string> nameGetter)
        {
            return GetPath('\\', nameGetter);
        }

        public string GetPath(INameGetter nameGetter)
        {
            return GetPath('\\', nameGetter);
        }

        public string GetPath(char seperator, Func<T, string> nameGetter)
        {
            return GetPath(new string('\\', 1), nameGetter);
        }

        public string GetPath(char seperator, INameGetter nameGetter)
        {
            return GetPath(new string('\\', 1), nameGetter);
        }

        public string GetPath(char seperator)
        {
            return GetPath(new string(seperator, 1));
        }

        public string GetPath(string seperator)
        {
            return GetPath(seperator, new DefaultNameGetter());
        }

        public string GetPath(string seperator, Func<T, string> nameGetter)
        {
            StringBuilder path = new StringBuilder();

            Stack<string> elements = new Stack<string>();
            TreeNode<T> node = this;
            while (node.Parent != null)
            {
                elements.Push(nameGetter.Invoke(node.Value));
                node = node.Parent;
            }

            while (elements.Count > 0)
            {
                path.Append(elements.Pop());
                if (elements.Count > 0) path.Append(seperator);
            }

            return path.ToString();
        }

        public string GetPath(string seperator, INameGetter nameGetter)
        {
            return GetPath(seperator, (T obj) => { return nameGetter.GetName(obj); });
        }
        #endregion







        
    }
}

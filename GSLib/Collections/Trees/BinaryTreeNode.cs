using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Collections.Trees
{
    /// <summary>
    /// Represents a binary tree node. Used to build binary trees.
    /// </summary>
    /// <typeparam name="T">Type for the value of each node.</typeparam>
    public class BinaryTreeNode<T> : ITreeNode<T>
    {
        /***********************************************************************
         * Private Fields
         **********************************************************************/ 
        T value;    // value of node
        BinaryTreeNode<T> left;
        BinaryTreeNode<T> right;
        BinaryTreeNode<T> parent;
        Sides side;
        bool allowDuplicates;

        /***********************************************************************
         * Enumerators
         **********************************************************************/
        /// <summary>
        /// Represents the side a node lies on in relation to its sibling.
        /// </summary>
        /// VB: Public Enum Sides
        public enum Sides
        {
            /// <summary>
            /// Neither side
            /// </summary>
            None,   // Neither side.
            /// <summary>
            /// Left side
            /// </summary>
            Left,   // The left side.
            /// <summary>
            /// Right side
            /// </summary>
            Right   // The right side.
        }

        /***********************************************************************
         * Properties
         **********************************************************************/ 
        /// <summary>
        /// Gets or sets the value of the node.
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
        /// Gets or sets the left child of the node.
        /// </summary>
        /// VB: Public Property Left As BinaryTreeNode(Of T)
        public BinaryTreeNode<T> Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
                left.parent = this;
                left.side = Sides.Left;
            }
        }

        /// <summary>
        /// Gets or sets the right child of the node.
        /// </summary>
        /// VB: Public Property Right As BinaryTreeNode(Of T)
        public BinaryTreeNode<T> Right
        {
            get
            {
                return right;
            }
            set
            {
                right = value;
                right.parent = this;
                right.side = Sides.Right;
            }
        }

        /// <summary>
        /// Gets the parent of the node.
        /// </summary>
        /// VB: Public Property Parent As ITreeNode(Of T)
        ITreeNode<T> ITreeNode<T>.Parent
        {
            get
            {
                return Parent;
            }
        }

        /// <summary>
        /// Gets the parent of the node.
        /// </summary>
        public BinaryTreeNode<T> Parent
        {
            get
            {
                return parent;
            }
        }

        /// <summary>
        /// Gets the side of the node
        /// </summary>
        /// VB: Public Property Side As Sides
        public Sides Side
        {
            get
            {
                return side;
            }
        }

        /// <summary>
        /// Gets or sets the sibling of this node.
        /// </summary>
        /// VB: Public Property Sibling As BinaryTreeNode(Of T)
        public BinaryTreeNode<T> Sibling
        {
            get
            {
                return parent[OppositeSide(side)];
            }
            set
            {
                parent[OppositeSide(side)] = value;
            }
        }

        /// <summary>
        /// Depth of the node from the root node.
        /// </summary>
        /// VB: Public Property Depth As Integer
        public int Depth
        {
            get
            {
                int i = 0;
                BinaryTreeNode<T> n = this;
                while (n.Parent != null)
                {
                    n = (BinaryTreeNode<T>)n.Parent;
                    i++;
                }
                return i;
            }
        }

        /// <summary>
        /// Gets the number of child nodes.
        /// </summary>
        /// VB: Public Property Count As Integer
        public int Count
        {
            get
            {
                int i = 0;
                if (left != null) i++;
                if (right != null) i++;
                return i;
            }
        }

        /// <summary>
        /// Gets or sets the indicated child.
        /// </summary>
        /// <param name="side">Side to get or set. Must be Sides.Left or Sides.Right.</param>
        /// <returns>Returns the indicated side.</returns>
        /// <exception cref="ArgumentException">Thrown if the side indicated is Sides.None.</exception>
        /// VB: Public Property This(ByVal side As Sides)
        public BinaryTreeNode<T> this[Sides side]
        {
            get
            {
                if (side == Sides.Left)
                {
                    return left;
                }
                else if (side == Sides.Right)
                {
                    return right;
                }
                else
                {
                    throw new ArgumentException("Given Sides cannot be Sides.None.");
                }
            }
            set
            {
                if (side == Sides.Left)
                {
                    Left = value;
                }
                else if (side == Sides.Right)
                {
                    Right = value;
                }
                else
                {
                    throw new ArgumentException("Given Sides cannot be Sides.None.");
                }
            }
        }

        /// <summary>
        /// Gets the siblings.
        /// </summary>
        /// <remarks>While this property is not read-only in ITreeNode, it is treated as read-only here.</remarks>
        /// <exception cref="NotSupportedException">Thrown if it is attempted to set the property.</exception>
        /// VB: Public Property Siblings As ITreeNode(Of T)()
        public ITreeNode<T>[] Siblings
        {
            get
            {
                return new ITreeNode<T>[] { Sibling };
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets or sets a value determining if duplicate values can be added.
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
        /// Gets or sets the children of the node.
        /// </summary>
        /// <remarks>
        /// <warning>When setting this property, you must use a BinaryTreeNode&lt;T&gt; array that contains exactly two elements. Any other number of elements will cause an exception.</warning>
        /// <warning>Be sure that the values of the BinaryTreeNode items you use are not equal if the AllowDuplicates property is false. Otherwise, an exception will be thrown.</warning>
        /// </remarks>
        /// <exception cref="ArgumentException">Thrown during set if the number of elements is not two.</exception>
        /// <exception cref="Exception">Thrown during set if AllowsDuplicates is false and the two elements have equal values.</exception>
        /// VB: Public Property Children As ITreeNode(Of T)()
        public ITreeNode<T>[] Children
        {
            get
            {
                return new BinaryTreeNode<T>[] { left, right };
            }
            set
            {
                BinaryTreeNode<T>[] val = new BinaryTreeNode<T>[2];
                value.CopyTo(val, 0);
                try
                {
                    if (value.Length == 2)
                    {
                        if ((val[0].Value.Equals(val[1].Value)) && (!allowDuplicates)) throw new Exception("Duplicates values are not allowed.");
                        Left = (BinaryTreeNode<T>)val[0];
                        Right = (BinaryTreeNode<T>)val[1];
                    }
                    else
                    {
                        throw new ArgumentException("Collection must contain two elements.");
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        /***********************************************************************
         * Constructors
         **********************************************************************/
        /// <summary>
        /// Default constructor. Sets both children to null, and value to default.
        /// </summary>
        /// VB: Public Sub New()
        public BinaryTreeNode()
        {
            left = null;
            right = null;
        }

        /// <summary>
        /// Constructor. Sets the value to the given value.
        /// </summary>
        /// <param name="value">Value to assign to the node.</param>
        /// VB: Public Sub New(ByVal value As T)
        public BinaryTreeNode(T value)
        {
            this.value = value;
            left = null;
            right = null;
        }

        /***********************************************************************
         * Methods
         **********************************************************************/ 
        /// <summary>
        /// Returns the side opposite the given side.
        /// </summary>
        /// <param name="side">Side to get the opposite of.</param>
        /// <returns>If side is Sides.Left, returns Sides.Right. If side is Sides.Right, returns Sides.Left. If side is Sides.None, returns Sides.None.</returns>
        /// VB: Public Shared Function OppositeSide(ByVal side As Sides) As Sides
        public static Sides OppositeSide(Sides side)
        {
            if (side == Sides.Left) return Sides.Right;
            else if (side == Sides.Right) return Sides.Left;
            else return Sides.None;
        }

        /// <summary>
        /// Inserts the given value as a new node on the given side. If side is Sides.None, an exception is thrown.
        /// </summary>
        /// <param name="value">Value to set the side to.</param>
        /// <param name="side">Side to insert the node.</param>
        /// <remarks>If a node already exists, that node becomes a child of the new node, on the indicated side.</remarks>
        /// <exception cref="ArgumentException">Thrown when the side indicated is Sides.None.</exception>
        /// VB: Public Sub Insert(ByVal value As T, ByVal side As Sides)
        public void Insert(T value, Sides side)
        {
            if (side == Sides.Left)
            {
                if (left == null)
                {
                    if (right.Equals(value) && allowDuplicates) throw new Exception("Cannot add duplicate value.");
                    Left = new BinaryTreeNode<T>(value);
                }
                else
                {
                    BinaryTreeNode<T> temp = new BinaryTreeNode<T>(value);
                    temp.Left = left;
                    Left = temp;
                }
            }
            else if (side == Sides.Right)
            {
                if (right == null)
                {
                    if (left.Equals(value) && allowDuplicates) throw new Exception("Cannot add duplicate value.");
                    Right = new BinaryTreeNode<T>(value);
                }
                else
                {
                    BinaryTreeNode<T> temp = new BinaryTreeNode<T>(value);
                    temp.Right = right;
                    Right = temp;
                }
            }
            else
            {
                throw new ArgumentException("Given Sides cannot be Sides.None.");
            }
        }

        /// <summary>
        /// Removes the node containing the given value, if it exists.
        /// </summary>
        /// <param name="value">Value to remove.</param>
        /// <remarks>This will remove the entire node, including its children.</remarks>
        /// VB: Public Sub Remove(ByVal value As T)
        public void Remove(T value)
        {
            if (left.Value.Equals(value))
            {
                Delete(Sides.Left);
            }
            else if (right.Value.Equals(value))
            {
                Delete(Sides.Right);
            }
        }

        /// <summary>
        /// Deletes the node on the indicated side, and replaces the side with the node's child, if any.
        /// </summary>
        /// <param name="side">Side to delete.</param>
        /// <remarks>Deletes the node on the given side. If the node has a child, the child is assigned to the indicated side. 
        /// If the node has two children, deletion cannot continue because there would be loss of data.</remarks>
        /// <returns>Returns true of deletion was successful. Otherwise returns false.</returns>
        /// VB: Public Function Delete(ByVal side As Sides) As Boolean
        public bool Delete(Sides side)
        {
            if (side == Sides.Left) // starting on the left
            {
                if ((left != null) && (left.Count < 2)) // if left is not null, and the left node has one or no children
                {
                    if (left.Count == 0)    // if the left has no children
                    {
                        left = null;        // set left to null
                    }
                    else if (left.left != null) // if the child is on the left
                    {   
                        left = left.left;   // set this.left to the child's left.
                    }
                    else if (left.right != null)    // if the child is on the right
                    {
                        left = left.right;  // set this.left to the child's right
                    }
                    return true;        // return true
                }
                else
                {
                    return false;       // if the child has two children, return false.
                }
            }
            else if (side == Sides.Right)
            {
                if ((right != null) && (right.Count < 2))
                {
                    if (right.Count == 0)
                    {
                        right = null;
                    }
                    else if (right.left != null)
                    {
                        right = right.left;
                    }
                    else if (right.right != null)
                    {
                        right = right.right;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Performs a pre-order search for the given value.
        /// </summary>
        /// <param name="value">Value to look for.</param>
        /// <returns>Returns the first node containing the value.</returns>
        /// VB: Public Function PreOrderSearch(ByVal value As T) As BinaryTreeNode(Of T)
        public BinaryTreeNode<T> PreOrderSearch(T value)
        {
            BinaryTreeNode<T> result = null;
            // root
            if (Value.Equals(value))
                result = this;

            // left child
            else if (left != null)
                result = left.PreOrderSearch(value);

            // right child
            else if (right != null)
                result = right.PreOrderSearch(value);

            return result;
        }

        /// <summary>
        /// Performs an in-order search for the given value.
        /// </summary>
        /// <param name="value">Value to search for.</param>
        /// <returns>Returns the first node containing the value.</returns>
        /// VB: Public Function InOrderSearch(ByVal value As T) As BinaryTreeNode(Of T)
        public BinaryTreeNode<T> InOrderSearch(T value)
        {
            BinaryTreeNode<T> result = null;
            
            // left child
            if (left != null)
                result = left.InOrderSearch(value);

            // root
            else if (Value.Equals(value))
                result = this;

            // right child
            else if (right != null)
                result = right.InOrderSearch(value);

            return result;
        }


        /// <summary>
        /// Performs a post-order search for the given value.
        /// </summary>
        /// <param name="value">Value to search for.</param>
        /// <returns>Returns the first node containing the value.</returns>
        /// VB: Public Function PostOrderSearch(ByVal value As T) As BinaryTreeNode(Of T)
        public BinaryTreeNode<T> PostOrderSearch(T value)
        {
            BinaryTreeNode<T> result = null;

            // left child
            if (left != null)
                result = left.PostOrderSearch(value);

            // right child
            else if (right != null)
                result = right.PostOrderSearch(value);

            // root
            else if (Value.Equals(value))
                result = this;

            return result;
        }

        /// <summary>
        /// Gets the enumerator for the BinaryTreeNode children.
        /// </summary>
        /// <returns>Returns an enumerator.</returns>
        /// VB: Public Function GetEnumerator() As IEnumerator(Of ITreeNode(Of T))
        public IEnumerator<ITreeNode<T>> GetEnumerator()
        {
            bool left = true;
            if (left)
                yield return Left;
            else
                yield return Right;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            bool left = true;
            if (left)
                yield return Left;
            else
                yield return Right;
        }

        /// <summary>
        /// Adds a value to the node if a side is available.
        /// </summary>
        /// <param name="value">Value of new node.</param>
        /// <remarks>Adds the node according to the given pattern: 
        /// if both sides are free, assigns the node to the left side. 
        /// If the left side is free, assigns the node to the left side.
        /// If the right side is free, but not the left, assigns the node to the right side.
        /// If neither side is free, throws an OverflowException.</remarks>
        /// <exception cref="OverflowException">Thrown if a value is added to a node that already has two values.</exception>
        /// VB: Public Sub Add(ByVal value As T)
        public void Add(T value)
        {
            if (left == null)
            {
                Insert(value, Sides.Left);
            }
            else if (right == null)
            {
                Insert(value, Sides.Right);
            }
            else
            {
                throw new OverflowException("Both leaves have values.");
            }
        }

        /// <summary>
        /// Adds a value to the node is a side is available.
        /// </summary>
        /// <param name="node">The new node.</param>
        /// <remarks>Adds the node according to the given pattern: 
        /// if both sides are free, assigns the node to the left side. 
        /// If the left side is free, assigns the node to the left side.
        /// If the right side is free, but not the left, assigns the node to the right side.
        /// If neither side is free, throws an OverflowException.</remarks>
        /// <exception cref="OverflowException">Thrown if a value is added to a node that already has two values.</exception>
        public void Add(BinaryTreeNode<T> node)
        {
            if (left == null)
            {
                Left = node;
            }
            else if (right == null)
            {
                Right = node;
            }
            else
            {
                throw new OverflowException("Both leaves have values.");
            }
        }

        /// <summary>
        /// Clears all items from the node.
        /// </summary>
        /// VB: Public Sub Clear()
        public void Clear()
        {
            left = null;
            right = null;
        }

        /// <summary>
        /// Determines if one of the leaves contains the given value.
        /// </summary>
        /// <param name="value">Value to check for.</param>
        /// <returns>If one or both leaves contains the value, returns true. Otherwise, returns false.</returns>
        /// VB: Public Function Contains(ByVal value As T) As Boolean
        public bool Contains(T value)
        {
            return (left.Value.Equals(value) | right.Value.Equals(value));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Collections.Trees
{
    class BinarySearchTreeNode<T> : BinaryTreeNode<T>
    {
        /********************************************************
         *  Constructors
         ********************************************************/
        public BinarySearchTreeNode() : base() { }
        public BinarySearchTreeNode(T value) : base(value) { }


    }
}

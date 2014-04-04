using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using GSLib.Language;

namespace GSLib.XML
{
    class XmlDocument : ICollection<XmlTreeNode>
    {
        List<XmlTreeNode> nodes;

        public XmlDocument()
        {
            nodes = new List<XmlTreeNode>();
        }

        public XmlDocument(string[] xml, bool ignoreLeadingWhitespace = true)
        {
            foreach (string line in xml)
            {
                DelimInfoCollection tokens;

                // remove whitespace if required
                string l = line;
                if (ignoreLeadingWhitespace) l = l.TrimStart(new char[] { ' ', '\t' });

                // try to tokenize the string
                try
                {
                    tokens = DelimitedStringTokenizer.TokenizeDelimiter(l, '<', '>', new char[0], true, true);
                }
                catch (DelimiterMismatchException ex)
                {
                    throw new XmlException("Angle bracket mismatch.", ex);
                }

                // if the tokenize was successful
                if (tokens != null)
                {
                    foreach (DelimInfo token in tokens)
                    {
                        if (token.Result)
                        {
                            
                        }
                    }
                }
            }
        }

        private XmlTreeNode ParseElement(string element)
        {
            Debug.Assert(element != null);
            Debug.Assert(element != "");

            XmlTreeNode node;
            string[] parts = element.Split(' ');

            string e = parts[0];
            if (e.StartsWith("?XML") | e.StartsWith("?xml"))
            {
                node = new XmlTreeNode(XmlNodeTypes.XmlDeclaration, e);
            }
            else if (e.StartsWith("![CDATA["))
            {
                node = new XmlTreeNode(XmlNodeTypes.CDATA, e);
            }
            else if (e.StartsWith("!--"))
            {
                node = new XmlTreeNode(XmlNodeTypes.Comment, e);
            }
            else
            {
                node = new XmlTreeNode(XmlNodeTypes.Element, e);
            }

            if (parts.Length > 1)
            {
                for (int i = 1; i < parts.Length; i++)
                {
                    // TODO: split attributes at equal sign
                    //Regex.Split(parts[i], "[a-zA-Z0-9_]+=\"*\"
                    // TODO: add each attribute to node
                }
            }

            return node;
        }

        public XmlTreeNode Declaration
        {
            get
            {
                foreach (XmlTreeNode node in nodes)
                {
                    if (node.NodeType == XmlNodeTypes.XmlDeclaration) return node;
                }
                return null;
            }
        }

        public XmlTreeNode Root
        {
            get
            {
                foreach (XmlTreeNode node in nodes)
                {
                    if (node.NodeType == XmlNodeTypes.Element) return node;
                }
                return null;
            }
        }

        public void Add(XmlTreeNode item)
        {
            if (item.NodeType == XmlNodeTypes.XmlDeclaration)
            {
                if (ContainsRootNode()) throw new XmlException("An XML declaration can only be made once.");
            }
            else if (item.NodeType == XmlNodeTypes.Element)
            {
                if (!ContainsDeclaration()) throw new XmlException("An XML declaration must be the first node in the document with no white space before it.");
                if (ContainsRootNode()) throw new XmlException("Only one root node is allowed.");
            }
            else
            {
                if (!ContainsDeclaration()) throw new XmlException("An XML declaration must be the first node in the document with no white space before it.");
            }

            nodes.Add(item);
        }

        public bool ContainsRootNode()
        {
            foreach (XmlTreeNode node in nodes)
            {
                if (node.NodeType == XmlNodeTypes.Element) return true;
            }
            return false;
        }

        public bool ContainsDeclaration()
        {
            foreach (XmlTreeNode node in nodes)
            {
                if (node.NodeType == XmlNodeTypes.XmlDeclaration) return true;
            }
            return false;
        }

        public void Clear()
        {
            nodes.Clear();
        }

        public bool Contains(XmlTreeNode item)
        {
            return nodes.Contains(item);
        }

        public void CopyTo(XmlTreeNode[] array, int arrayIndex)
        {
            nodes.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return nodes.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(XmlTreeNode item)
        {
            return nodes.Remove(item);
        }

        public IEnumerator<XmlTreeNode> GetEnumerator()
        {
            return nodes.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using GSLib.Collections.Trees;
using GSLib.Language;

namespace GSLib.XML
{
    public class XmlTreeNode : TreeNode<string>
    {
        XmlNodeTypes nodeType;
        Dictionary<string, string> attributes;

        public XmlTreeNode() : base()
        {
            nodeType = XmlNodeTypes.None;
        }

        public XmlTreeNode(XmlNodeTypes nodeType, string element) : base(element)
        {
            this.nodeType = nodeType;
        }

        public XmlNodeTypes NodeType
        {
            get
            {
                return nodeType;
            }

        }

        public string GetAttribute(string name)
        {
            if (attributes.ContainsKey(name))
            {
                return attributes[name];
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public void AddAttribute(string name, string value)
        {
            if (attributes.ContainsKey(name)) throw new Exception("Attribute with that name already exists.");
            attributes.Add(name, value);
        }
    }
}

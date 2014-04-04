using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using GSLib.Collections;
using GSLib.Collections.Trees;

namespace GSLib.IO.Compression
{
    /// <summary>
    /// Provides a way of compressing data using Huffman coding.
    /// </summary>
    public class HuffmanCoding : Compression
    {
        #region Helpers
        struct NodeData
        {
            public int weight;
            public byte symbol;
            public bool isInternal;

            public NodeData(byte symbol, int weight)
            {
                this.symbol = symbol;
                this.weight = weight;
                isInternal = false;
            }

            public NodeData(int weight)
            {
                this.symbol = 0;
                this.weight = weight;
                isInternal = true;
            }
        }

        /// <summary>
        /// Translates the Huffman tree into a Bitfield.
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        private Bitfield HuffmanTreeToBitfield(BinaryTreeNode<NodeData> tree)
        {
            Bitfield bits = new Bitfield(0);
            if (tree == null) return bits;

            bool value = (!tree.Value.isInternal);
            bits.Add(value);
            if (value)
            {
                bits.Append(Bitfield.FromByte(tree.Value.symbol));
            }

            bits.Append(HuffmanTreeToBitfield(tree.Left));
            bits.Append(HuffmanTreeToBitfield(tree.Right));

            return bits;
        }

        private BinaryTreeNode<NodeData> BitfieldToHuffmanTree(ref Bitfield bits)
        {
            BinaryTreeNode<NodeData> node = new BinaryTreeNode<NodeData>();

            if (bits[0])
            {
                bits.TrimStart(1);
                try
                {
                    /* read the first 8 bits from the bitfield (bits.ToByte(0)), 
                     * and create a node value from it. */
                    node.Value = new NodeData(bits.ToByte(0), 0);
                    bits.TrimStart(8); // then remove those 8 bits.
                }
                catch (BoundaryOutOfRangeException)
                {
                    throw;
                }
            }
            else
            {
                bits.TrimStart(1);
                node.Value = new NodeData(0);

                node.Left = BitfieldToHuffmanTree(ref bits);
                node.Right = BitfieldToHuffmanTree(ref bits);
            }

            return node;
        }

        private Bitfield SymbolToBitfield(BinaryTreeNode<NodeData> symbolNode)
        {
            Bitfield bits = new Bitfield(0, Bitfield.Endianness.BigEndian);

            BinaryTreeNode<NodeData> current = symbolNode;
            while (current.Parent != null)
            {
                bits.Add(current.Side == BinaryTreeNode<NodeData>.Sides.Right);
                current = current.Parent;
            }

            bits.Reverse();
            return bits;
        }

        #endregion

        /// <summary>
        /// Compresses the given byte array.
        /// </summary>
        /// <param name="bytes">Byte array to compress.</param>
        /// <returns>Returns the resulting compressed byte array.</returns>
        /// <remarks>To make the result fit in a byte array, between 0 and 7 bits are appended to the right of the last bit. The first byte in the compressed byte array is the number of bits padded.
        /// This classes Decompress methods handles removing the padded bits.</remarks>
        public override byte[] Compress(byte[] bytes)
        {
            /* List out weights */
            Dictionary<byte, int> weights = new Dictionary<byte, int>();

            foreach (byte b in bytes)
            {
                if (weights.ContainsKey(b))
                    weights[b]++;
                else
                    weights.Add(b, 1);
            }

            /* Translate to queue */
            PriorityQueue<BinaryTreeNode<NodeData>, int> queue = new PriorityQueue<BinaryTreeNode<NodeData>, int>(priorityDescending: false);
            Dictionary<byte, BinaryTreeNode<NodeData>> symbols = new Dictionary<byte, BinaryTreeNode<NodeData>>();

            foreach (KeyValuePair<byte, int> item in weights)
            {
                BinaryTreeNode<NodeData> node = new BinaryTreeNode<NodeData>(new NodeData(item.Key, item.Value));
                symbols.Add(item.Key, node);
                queue.Enqueue(node, item.Value);
            }

            /* Build the tree */
            while (queue.Count > 1)
            {
                int weight1, weight2;
                BinaryTreeNode<NodeData> left, right, parent;

                left = queue.Dequeue(out weight1);
                right = queue.Dequeue(out weight2);
                parent = new BinaryTreeNode<NodeData>(new NodeData(weight1 + weight2));
                parent.Add(left);
                parent.Add(right);
                queue.Enqueue(parent, weight1 + weight2);
            }

            /* Final node is in the queue. */
            BinaryTreeNode<NodeData> huffmanTree = queue.Dequeue();

            /* Translate to bitfield */
            Bitfield bits = new Bitfield();
            List<byte> data = new List<byte>();

            foreach (byte b in bytes)
            {
                bits.Append(SymbolToBitfield(symbols[b]));
                
                // To avoid running out of memory
                //if (bits.ByteCount > 1)
                //{
                //    data.Add(bits.ToByte());
                //    data.RemoveRange(0, 8);
                //}
            }

            /* Write to file */
            // Translate tree to bitfield
            Bitfield tree = HuffmanTreeToBitfield(huffmanTree);

            // Calculate padding
            byte padCount = (byte)(8 - ((bits.Count + tree.Count) % 8));
            if (padCount == 8) padCount = 0;
            Bitfield pad = Bitfield.FromByte(padCount);

            // Write to final bitfield
            Bitfield result = new Bitfield();
            result.Append(pad);     // add the padding bit count
            result.Append(tree);    // add the Huffman tree
            result.Append(bits);    // add the file bits
            result.PadToByte();

            // return result
            return result.ToByteArray();
        }

        /// <summary>
        /// Decompresses the given byte array and returns the decompressed data as a byte array.
        /// </summary>
        /// <param name="compressedData">Compressed data.</param>
        /// <returns>Returns the decompressed data.</returns>
        public override byte[] Decompress(byte[] compressedData)
        {
            Bitfield bits = Bitfield.FromByteArray(compressedData);

            // Get padding length.
            int trim = bits.ToByte();
            // Trim ends
            bits.TrimStart(8);
            bits.TrimEnd(trim);

            // Get Huffman tree
            BinaryTreeNode<NodeData> huffmanTree = BitfieldToHuffmanTree(ref bits);
           
            // Decompress data
            List<byte> bytes = new List<byte>();
            
            BinaryTreeNode<NodeData> node = huffmanTree;
            while (bits.Count > 0)
            {
                if (bits[0])
                    node = node.Right;
                else
                    node = node.Left;

                bits.TrimStart(1);

                if (!node.Value.isInternal)
                {
                    bytes.Add(node.Value.symbol);
                    node = huffmanTree;
                }
            }

            return bytes.ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

using Algo.Trees.Entities;

namespace Algo.Greedy.Huffman
{
    public sealed class HuffmanWriter<T> : IDisposable
    {
        private static readonly int[] _masks;

        private readonly IReadOnlyDictionary<T, ValueBits> _code;
        private readonly bool _isOwnStream;

        private Stream _stream;
        private byte _currentByte;
        private byte _bitsLeft;

        static HuffmanWriter()
        {
            _masks = new[]
            {
                0b0000_0001,
                0b0000_0011,
                0b0000_0111,
                0b0000_1111,
                0b0001_1111,
                0b0011_1111,
                0b0111_1111,
                0b1111_1111
            };
        }    

        public HuffmanWriter(HuffmanCode<T> code, Stream stream, bool ownStream = false)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            if (!stream.CanWrite)
            {
                throw new ArgumentException(nameof(stream), "Cannot write to the stream");
            }

            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            _code = TranslateCode(code);
            _isOwnStream = ownStream;
            _bitsLeft = 8;
        }

        public void Write(T value)
        {
            ValueBits allBits = _code[value];
            byte bitsToWrite = allBits.Count;
            int code = allBits.Bits;

            while (bitsToWrite > 0)
            {
                int mask;
                byte bitCount;
                if (bitsToWrite > _bitsLeft)
                {
                    int remainder = bitsToWrite - _bitsLeft;
                    bitCount = _bitsLeft;
                    mask = _masks[bitCount - 1];
                    _currentByte |= (byte)((code >> remainder) & mask);
                }
                else
                {
                    int gap = _bitsLeft - bitsToWrite;
                    bitCount = bitsToWrite;
                    mask = _masks[bitCount - 1];
                    _currentByte |= (byte)((code & mask) << gap);
                }

                _bitsLeft -= bitCount;
                bitsToWrite -= bitCount;

                if (_bitsLeft == 0)
                {
                    _stream.WriteByte(_currentByte);
                    _bitsLeft = 8;
                }
            }
        }

        void IDisposable.Dispose()
        {
            if (_bitsLeft != 8)
            {
                _stream.WriteByte(_currentByte);
            }

            if (_isOwnStream && _stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }
        }

        private static IReadOnlyDictionary<T, ValueBits> TranslateCode(HuffmanCode<T> code)
        {
            Dictionary<T, ValueBits> dictionary = new Dictionary<T, ValueBits>();
            ValueBits rootBits = new ValueBits();
            IterateTree(code.Root, dictionary, ref rootBits);
            return dictionary;
        }

        private static bool IterateTree(BinaryTreeNode<T> treeNode, IDictionary<T, ValueBits> dictionary, ref ValueBits current)
        {
            if (treeNode == null)
            {
                return false;
            }

            current.Count++;
            current.Bits <<= 1;

            bool hasChildren = IterateTree(treeNode.Left, dictionary, ref current);

            current.Bits |= 1;
            hasChildren |= IterateTree(treeNode.Right, dictionary, ref current);

            current.Bits >>= 1; 
            current.Count--;

            if (current.Count != 0 && !hasChildren)
            {
                dictionary.Add(treeNode.Data, current);
            }

            return hasChildren;
        }

        private struct ValueBits
        {
            public int Bits { get; set; }

            public byte Count { get; set; } 
        }
    }
}
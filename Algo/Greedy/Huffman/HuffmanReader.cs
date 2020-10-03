using System;
using System.IO;

using Algo.Trees.Entities;

namespace Algo.Greedy.Huffman
{
    public sealed class HuffmanReader<T> : IDisposable
    {
        private const byte EOF = 0xFF;

        private readonly HuffmanCode<T> _code;
        private readonly bool _isOwnStream;

        private Stream _stream;
        private byte _currentBit;
        private byte _currentByte;

        public HuffmanReader(HuffmanCode<T> code, Stream stream, bool ownStream = false)
        {
            _code = code ?? throw new ArgumentNullException(nameof(code));
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            if (!stream.CanRead)
            {
                throw new ArgumentException(nameof(stream), "Cannot read from the stream");
            }

            _isOwnStream = ownStream;
        }

        public T Read()
        {
            if (_currentBit == EOF)
            {
                return _code.EofMarker;
            }

            BinaryTreeNode<T> node = _code.Root;

            while (node.Left != null && node.Right != null)
            {
                if (_currentBit == 0)
                {
                    int data = _stream.ReadByte();
                    if (data == -1)
                    {
                        _currentBit = EOF;
                        return default;
                    }

                    _currentByte = (byte)data;
                    _currentBit = 0b1000_0000;
                }

                node = (_currentByte & _currentBit) == 0 ? node.Left : node.Right;

                _currentBit >>= 1;
            }

            if (_code.EofMarker != null && node.Data?.Equals(_code.EofMarker) == true)
            {
                _currentBit = EOF;
            }

            return node.Data;
        }

        void IDisposable.Dispose()
        {
            if (_isOwnStream && _stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }
        }
    }
}
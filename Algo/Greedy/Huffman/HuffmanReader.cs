using System;
using System.IO;

namespace Algo.Greedy.Huffman
{
    public sealed class HuffmanReader<T> : IDisposable
    {
        private readonly HuffmanCode<T> _code;
        private readonly bool _isOwnStream;
        private Stream _stream;

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
            throw new NotImplementedException();
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
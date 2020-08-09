using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algo.DynamicProgramming.Entities
{
    public sealed class BasedArray<T> : IReadOnlyList<T>
    {
        private readonly int _base;
        private T[] _data;

        public BasedArray(int @base, int length) : this(@base)
        {
            _data = new T[length];
        }

        public BasedArray(IEnumerable<T> data, int @base) : this(@base)
        {
            _data = data?.ToArray() ??
                    throw new ArgumentNullException(nameof(data));
        }

        private BasedArray(int @base)
        {
            _base = @base >= 1 ? @base : throw new ArgumentOutOfRangeException(nameof(@base));
        }

        public T this[int index]
        {
            get
            {
                return _data[index - _base];
            }

            set
            {
                _data[index - _base] = value;
            }
        }

        public int Count => _data.Length;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
             return (IEnumerator<T>)_data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}

using System.Collections.Generic;

namespace Algo.Other.SubArraysMax
{
    internal interface IArrayIterator
    {
        IReadOnlyList<int> FindMaximums(IReadOnlyList<int> array, int chunkSize);
    }
}
using System;
using System.Collections.Generic;

namespace Algo.Other.SubArraysMax
{
    internal sealed class SimpleIterator : IArrayIterator
    {
        IReadOnlyList<int> IArrayIterator.FindMaximums(IReadOnlyList<int> array, int chunkSize)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (chunkSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(chunkSize));
            }

            int[] maximums = new int[array.Count - chunkSize + 1];
            int localMax = -1, ttl = 1;
            for (int index = 0; index < maximums.Length; ++index)
            {
                if (localMax == -1)
                {
                    for (int localIndex = index; localIndex < index + chunkSize; ++localIndex)
                    {
                        if (array[localIndex] > localMax)
                        {
                            localMax = array[localIndex];
                            ttl = localIndex - index + 1;
                        }
                    }
                }
                else
                {
                    int last = array[index + chunkSize - 1];
                    if (last > localMax)
                    {
                        localMax = last;
                        ttl = chunkSize;
                    }
                }

                maximums[index] = localMax;
                if (--ttl == 0)
                {
                    localMax = -1;
                }
            }

            return maximums;
        }
    }
}
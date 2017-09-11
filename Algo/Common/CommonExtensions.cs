using System;
using System.Collections.Generic;

namespace Algo.Common
{
    public static class CommonExtensions
    {
        public static void Swap<T>(this IList<T> data, int index, int otherIndex)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (index == otherIndex)
            {
                return;
            }

            T temp = data[index];
            data[index] = data[otherIndex];
            data[otherIndex] = temp;
        }
    }
}
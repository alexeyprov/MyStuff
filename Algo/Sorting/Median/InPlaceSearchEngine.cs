using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Algo.Common;

namespace Algo.Sorting.Median
{
    internal sealed class InPlaceSearchEngine<T>
        where T : IComparable<T>
    {
        private readonly IList<T> _source;
        private readonly Random _rnd;

        public InPlaceSearchEngine(IEnumerable<T> source)
        {
            IList<T> sourceList = source?.ToArray();

            if (sourceList?.Any() != true)
            {
                throw new ArgumentNullException(nameof(source));
            }

            _source = sourceList;
            _rnd = new Random();
        }

        public T FindMedian()
        {
            return FindElement(0, _source.Count - 1, (_source.Count - 1) / 2);
        }

        private T FindElement(int start, int end, int rank)
        {
            Debug.Assert(start >= 0);
            Debug.Assert(end < _source.Count);
            Debug.Assert(end >= start);
            Debug.Assert(rank >= 0);
            Debug.Assert(rank <= end - start);

            if (start == end)
            {
                Debug.Assert(rank == 0);
                return _source[start];
            }

            T probe = _source[start + _rnd.Next(end - start + 1)];

            int leftIndex = start, 
                rightIndex = end;
            bool isLeftToBeSwapped = false,
                 isRightToBeSwapped = false;

            // move left and right pointers towards each other,
            // swapping elements as needed
            while (leftIndex < rightIndex)
            {
                if (!isLeftToBeSwapped)
                {
                    isLeftToBeSwapped = AdvancePointer(probe, ref leftIndex, Direction.Forward);
                }

                if (!isRightToBeSwapped)
                {
                    isRightToBeSwapped = AdvancePointer(probe, ref rightIndex, Direction.Backward);
                }

                if (isLeftToBeSwapped && isRightToBeSwapped)
                {
                    _source.Swap(leftIndex++, rightIndex--);
                    isLeftToBeSwapped = false;
                    isRightToBeSwapped = false;
                }
            }

            int oldLeft = leftIndex, oldRight = rightIndex;
            AdjustIndexes(ref leftIndex, ref rightIndex, probe);
            Debug.Assert(rightIndex > leftIndex);

            leftIndex = PushOutProbes(probe, start, leftIndex, Direction.Forward);
            rightIndex = PushOutProbes(probe, rightIndex, end, Direction.Backward);
            Debug.Assert(rightIndex > leftIndex);

            if (rank <= leftIndex - start)
            {
                return FindElement(start, leftIndex, rank);
            }
            else if (rank > leftIndex - start && rank < rightIndex - start)
            {
                return probe;
            }

            return FindElement(rightIndex, end, rank - rightIndex + start);
        }

        private bool AdvancePointer(T probe, ref int index, Direction direction)
        {
            int comparison = _source[index].CompareTo(probe);

            // if we are moving right and current is less than probe
            // or moving left and current is more than probe
            // or current is equal to probe for any direction
            if (comparison * (int)direction <= 0) 
            {
                index += (int)direction;
                return false;
            }

            // no iteration has been made, current is to be swapped.
            return true;
        }

        private int PushOutProbes(T probe, int start, int end, Direction direction)
        {
            if (start >= end)
            {
                return direction == Direction.Forward ? end : start;
            }

            int current, destination;
            
            if (direction == Direction.Forward)
            {
                current = start;
                destination = end;
            }
            else
            {
                current = end;
                destination = start;
            }

            while ((destination - current) * (int)direction > 0)
            {
                int comparison = _source[current].CompareTo(probe);

                if (comparison == 0)
                {
                    _source.Swap(current, destination);
                    destination -= (int)direction;
                }
                else
                {
                    // comparison result should be "less" if moving right (1)
                    // or "more" if moving left (-1)
                    Debug.Assert((int)direction * comparison < 0);
                }

                current += (int)direction;
            }

            return destination;
        }

        private void AdjustIndexes(ref int leftIndex, ref int rightIndex, T probe)
        {
            Debug.Assert(leftIndex - rightIndex == 0 || leftIndex - rightIndex == 1);
            bool rollbackLeft = true;
            bool rollbackRight = true;

            if (leftIndex == rightIndex)
            {
                int comparison = _source[leftIndex].CompareTo(probe);

                if (comparison < 0)
                {
                    rollbackLeft = false;
                }

                if (comparison > 0)
                {
                    rollbackRight = false;
                }
            }

            if (rollbackLeft)
            {
                leftIndex--;
            }

            if (rollbackRight)
            {
                rightIndex++;
            }
        }

        private enum Direction
        {
            Forward = 1,
            Backward = -1
        }
    }
}
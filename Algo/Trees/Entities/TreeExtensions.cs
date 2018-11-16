using System;
using System.Collections.Generic;

namespace Algo.Trees.Entities
{
    public static class TreeExtensions
    {
        public static void Print<T>(this BinaryTreeNode<T> root)
        {
            if (root == null)
            {
                return;
            }

            Queue<(BinaryTreeNode<T> node, int row, int column)> queue = new Queue<(BinaryTreeNode<T>, int, int)>();
            queue.Enqueue((root, 0, 0));
            int lastRow = 0,
                lastColumn = 0,
                cellCount = 1,
                cellWidth = Console.WindowWidth;

            while (queue.Count > 0)
            {
                (BinaryTreeNode<T> node, int row, int column) = queue.Dequeue();
                if (row != lastRow)
                {
                    if (lastColumn != (1 << lastRow) - 1)
                    {
                        Console.WriteLine();
                    }

                    lastRow = row;
                    lastColumn = 0;
                    cellCount = 1 << row;
                    cellWidth = Console.WindowWidth / cellCount;
                }

                if (lastColumn < column - 1)
                {
                    // empty cell(s)
                    Console.Write($"{{0, {cellWidth * (column - lastColumn - 1)}}}", string.Empty);
                }

                PrintCenterAligned(node.Data, cellWidth);
                lastColumn = column;

                if (node.Left != null)
                {
                    queue.Enqueue((node.Left, row + 1, 2 * column));
                }

                if (node.Right != null)
                {
                    queue.Enqueue((node.Right, row + 1, 2 * column + 1));
                }
            }

            Console.WriteLine();
        }

        private static void PrintCenterAligned(object data, int width)
        {
            string text = Convert.ToString(data) ?? string.Empty;
            Console.Write($"{{0, {-width}}}", string.Format($"{{0, {(width + text.Length) / 2}}}", text));            
        }
    }
}
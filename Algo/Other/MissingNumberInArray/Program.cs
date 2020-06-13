using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MissingNumberInArray
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            IReadOnlyCollection<int> task = GetRandomTask();
            int solution = Solve(task);
            Verify(task, solution);
        }

        private static IReadOnlyCollection<int> GetRandomTask()
        {
            Random random = new Random();
            int max = random.Next(2000) + 2;
            int skipped = random.Next(max);
            int[] task = Enumerable.Range(0, max + 1).Where(i => i != skipped).ToArray();

            // permute
            for (int i = 0; i < task.Length - 1; ++i)
            {
                int j = random.Next(i + 1, task.Length);
                int temp = task[i];
                task[i] = task[j];
                task[j] = temp;
            }

            Console.WriteLine($"Generated task: [0, {max}] \\ {skipped}");
            return task;
        }

        private static int Solve(IReadOnlyCollection<int> task)
        {
            if (task.Count == 0)
            {
                return 0;
            }

            List<int> oddBitSubtask = new List<int>(task.Count / 2 + 1);
            List<int> evenBitSubtask = new List<int>(task.Count / 2 + 1);

            foreach (int number in task)
            {
                if ((number & 1) == 0)
                {
                    evenBitSubtask.Add(number >> 1);
                }
                else
                {
                    oddBitSubtask.Add(number >> 1);
                }
            }

            return evenBitSubtask.Count <= oddBitSubtask.Count ?
                Solve(evenBitSubtask) << 1 :
                (Solve(oddBitSubtask) << 1) | 1; 
        }

        private static void Verify(IReadOnlyCollection<int> task, int solution)
        {
            BitArray foundNumbers = new BitArray(task.Count + 1);
            foundNumbers[solution] = true;
            foreach (int number in task)
            {
                foundNumbers[number] = true;
            }

            if (foundNumbers.Cast<bool>().Any(b => !b))
            {
                Console.WriteLine($"Solution {solution} is incorrect");
            }
            else
            {
                Console.WriteLine($"Solution {solution} is correct");
            }
        }
    }
}

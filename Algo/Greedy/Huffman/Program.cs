using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Algo.Greedy.Huffman
{
    internal static class Program
    {
        private static void Main()
        {
            RunSimpleTest();
        }

        private static void RunSimpleTest()
        {
            IReadOnlyDictionary<char, double> stats = new Dictionary<char, double>
            {
                { 'a', 45D },
                { 'b', 13D },
                { 'c', 12D },
                { 'd', 16D },
                { 'e', 9D },
                { 'f', 5D }
            };

            HuffmanCode<char> code = new HuffmanCode<char>(stats);
            string test = "beacdafebcadfacbed";
            using (Stream encodedStream = new MemoryStream())
            {
                using (HuffmanWriter<char> writer = new HuffmanWriter<char>(code, encodedStream))
                {
                    foreach (char c in test)
                    {
                        writer.Write(c);
                    }
                }

                Console.WriteLine($"Encoded {test.Length} source characters into {encodedStream.Position} bytes.");

                encodedStream.Seek(0L, SeekOrigin.Begin);

                StringBuilder result = new StringBuilder(test.Length);
                using (HuffmanReader<char> reader = new HuffmanReader<char>(code, encodedStream))
                {
                    char c;
                    while ((c = reader.Read()) != default)
                    {
                        result.Append(c);
                    }
                }

                string decodedTest = result.ToString();
                if (test == decodedTest)
                {
                    Console.WriteLine("Encode/decode test succeeded.");
                }
                else
                {
                    Console.WriteLine($"Encode/decode test FAILED. Original: '{test}'. Decoded: '{decodedTest}'");
                }
            }
        }
    }
}

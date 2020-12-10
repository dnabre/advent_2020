using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


/*
	Solutions found:
	Part 1: 2380
	Part 2: 48358655787008
*/

namespace advent_2020
{
    static class AOC_10
    {
        private const string Part1Input = "aoc_10_input_1.txt";
        private const string Part2Input = "aoc_10_input_2.txt";
        private const string TestInput1 = "aoc_10_test_1.txt";
        private const string TestInput2 = "aoc_10_test_2.txt";


        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 05");
           Part1(args);
            Console.Write("\n");
            Part2(args);
        }
        
        

        private static void Part1(string[] args)
        {
            Console.WriteLine("   Part 1");
            String[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            String[] add_these = {"0",Utility.MaxIntOfString(lines)};
            lines = Utility.AddToArray(lines,add_these);

            int[] adapters = Utility.StringArrayToIntArray(lines);
            int max = adapters[0];
            foreach (int i in adapters)
            {
                max = Math.Max(max, i);
            }

            adapters[1] = max+3;
            Array.Sort(adapters);
            

            

            int one_count = 0;
            int three_count = 0;
            int[] diffs = new int[adapters.Length - 1];
            for (int i = 0; i < diffs.Length; i++)
            {
                diffs[i] = adapters[i + 1] - adapters[i];
                if (diffs[i] == 1) one_count++;
                if (diffs[i] == 3) three_count++;
            }

            
         
            
            
            Console.WriteLine($"\n\tPart 1 Solution: {one_count * three_count}  (one: {one_count} three: {three_count})");
        }

        private static void Part2(string[] args)
        {
            Console.WriteLine("   Part 2:");
            String[] lines = System.IO.File.ReadAllLines(Part2Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            String[] add_these = {"0", Utility.MaxIntOfString(lines)};

            lines = Utility.AddToArray(lines, add_these);

            int[] adapters = Utility.StringArrayToIntArray(lines);
            Array.Sort(adapters);

            /*
             * Doing magic based on the Tribonacci sequence here.
             *
             * I noticed that the example answers were 2^a * 7^b for examples and anything I tried.
             * That lead to Tribonacci sequence. It was admittedly a little trial and error from there
             */

            int e2 = 0;
            int e7 = 0;
            long minus3;
            // Loop over the adapters, ignore first and last which are outlet and device-adapter
            for (int i = 1; i < adapters.Length - 1; i++)
            {
                if (i >= 3)
                {
                    minus3 = adapters[i - 3];
                }
                else
                {
                    minus3 = int.MinValue;
                }


                if ((adapters[i + 1] - minus3) == 4)
                {
                    e7 += 1;
                    e2 -= 2;
                }
                else if (adapters[i + 1] - adapters[i - 1] == 2)
                {
                    e2 += 1;
                }
            }


            long e = Utility.IntPow(2, e2) * Utility.IntPow(7, e7);

            Console.WriteLine($"\n\tPart 2 Solution: 2^{e2} * 7^{e7}= {e}");
        }
    }
}
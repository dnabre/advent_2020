using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;


/*
	Solutions found:
	Part 1:
	Part 2: 
	
*/

namespace advent_2020
{
    static class AOC_23
    {
        private const string Part1Input = "aoc_23_input_1.txt";
        private const string Part2Input = "aoc_23_input_2.txt";
        private const string TestInput1 = "aoc_23_test_1.txt";
        private const string TestInput2 = "aoc_23_test_2.txt";


        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 23");
            var watch = Stopwatch.StartNew();
            Part1();
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = Stopwatch.StartNew();
            Part2();
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }


        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            string[] lines = File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            Console.WriteLine($"\n\tPart 1 Solution: {0}");
        }


        private static void Part2()
        {
            Console.WriteLine("   Part 2");
            string[] lines = File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);


            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }
       
    }
}

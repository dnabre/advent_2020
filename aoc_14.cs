using System;
using System.Text;
using System.Collections.Generic;


/*
	Solutions found:
	Part 1:
	Part 2: 
	
*/

namespace advent_2020
{
    static class AOC_14 {
        private const string Part1Input = "aoc_14_input_1.txt";
        private const string Part2Input = "aoc_14_input_2.txt";
        private const string TestInput1 = "aoc_14_test_1.txt";
        private const string TestInput2 = "aoc_14_test_2.txt";

        public static void Run (string[] args) {
            Console.WriteLine ("AoC Problem 14");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Part1(args);
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
            Part2(args);
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");

        }

        private static void Part1(string[] args) {
            Console.WriteLine("   Part 1");
            string[] lines =  System.IO.File.ReadAllLines(Part2Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);


	    Console.WriteLine($"\n\tPart 1 Solution: {0}");	
        }

        private static void Part2(string[] args) {
            Console.WriteLine("   Part2");
            string[] lines =  System.IO.File.ReadAllLines(Part2Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
	
		Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }
}
}



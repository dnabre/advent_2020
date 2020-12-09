using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;


/*
	Solutions found:
	Part 1: 289
	Part 2: 3640
	
*/

namespace advent_2020
{
    static class AOC_08
{
    private const string Part1Input = "aoc_08_input_1.txt";
    private const string Part2Input = "aoc_08_input_2.txt";
    private const string TestInput1 = "aoc_08_test_1.txt";
    private const string TestInput2 = "aoc_08_test_2.txt";

    

    public static void Run(string[] args)
    {
        Console.WriteLine("AoC Problem 08");
        Part1(args);
 //       Console.Write("\n");
 //       Part2(args);
    }


    private static void Part1(string[] args)
    {
        Console.WriteLine("   Part 1");
        String[] lines = System.IO.File.ReadAllLines(TestInput1);
        Console.WriteLine("\tRead {0} inputs", lines.Length);

        
        
        Console.WriteLine($"\n\tPart 1 Solution: {0}");
    }


    private static void Part2(string[] args)
    {
        Console.WriteLine("   Part 2");
        String[] lines = System.IO.File.ReadAllLines(TestInput1);
        Console.WriteLine("\tRead {0} inputs", lines.Length);

        Console.WriteLine($"\n\tPart 2 Solution: {0}");
    }
}

}
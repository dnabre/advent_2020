using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.AccessControl;


/*
	Solutions found:
	Part 1: 731031916
	Part 2: 
	
*/

namespace advent_2020
{
    
    static class AOC_09
    {
        private const string Part1Input = "aoc_09_input_1.txt";
        private const string Part2Input = "aoc_09_input_2.txt";
        private const string TestInput1 = "aoc_09_test_1.txt";
        private const string TestInput2 = "aoc_09_test_2.txt";

      
        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 09");
            Part1(args);
            Console.Write("\n");
            Part2(args);
        }


        private static HashSet<long> getAllPartsLast5(long[] nums, int start)
        {
            HashSet<long> all_sums = new HashSet<long>();
            long[] five = new long[25];
            for (int q = 0; q <25; q++)
            {
                five[q] = nums[start + q];
            }
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j <25; j++)
                {
                    if (i == j) {
                        continue;
                    }
                    else
                    {
                        all_sums.Add(five[i] + five[j]);
                    }
                }
            }
            return all_sums;
        } 


        private static void Part1(string[] args)
        {
            Console.WriteLine("   Part 1");
            String[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            long[] nums = new long[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                nums[i] = long.Parse(lines[i]);
            }

            long solution = -1;
            for (int n = 25; n < nums.Length; n++)
            {
                HashSet<long> sums = getAllPartsLast5(nums, n - 25);
                if (sums.Contains(nums[n]))
                {
                //    Console.WriteLine($"\t {nums[n]} is a sum");
                }
                else
                {
                    Console.WriteLine($"\t {nums[n]} is NOT sum");
                    solution = nums[n];
                  
                    
                    Console.WriteLine($"\n\tPart 1 Solution: {solution}");
                  return;
                }
            }
            
            

            Console.WriteLine($"\n\tPart 1 Solution: {solution}");
        }

    
        private static void Part2(string[] args)
        {
            Console.WriteLine("   Part 2");
            String[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            // Dynamic programming -> sum_range(nums[], start a, end b)

            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }
    }
}


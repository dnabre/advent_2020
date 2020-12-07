using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;


/*
	Solutions found:
	Part 1: 911
	Part 2: 629
	
*/

namespace advent_2020
{
    static class AOC_06
    {
        private const string Part1Input = "aoc_06_input_1.txt";
        private const string Part2Input = "aoc_06_input_2.txt";
        private const string TestInput1 = "aoc_06_test_1.txt";
        private const string TestInput2 = "aoc_06_test_2.txt";
        
        
        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 06");
            Part1(args);
            Console.Write("\n");
            //Part2(args);
        }



        private static void Part1(string[] args)
        {
            Console.WriteLine("   Part 1");
            String[] lines = System.IO.File.ReadAllLines(TestInput1);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            List<HashSet<char>> group_input_list = new List<HashSet<char>>();

            HashSet<String> group = new HashSet<String>();
                
            foreach (String ln in lines)
            {
                Console.WriteLine(ln);
                if (ln.Equals(""))
                {
                    group_input_list.Add(GroupToCharSet(group));
                    group = new HashSet<String>();
                }
                else
                {
                    group.Add(ln);
                }
                
            }

            if (group.Count > 0)
            {
                group_input_list.Add(GroupToCharSet(group));
            }
            Console.WriteLine($"\n\tPart 1 Solution: {0}");
        }

        private static HashSet<char> GroupToCharSet(HashSet<String> group)
        {
            return null;
        }
        
        
        private static void Part2(string[] args)
        {
            Console.WriteLine("   Part 2");
            String[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            
            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }
    }
}
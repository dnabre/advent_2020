using System;
using System.CodeDom;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;


/*
	Solutions found:
	Part 1: 203
	Part 2: 9007186
	
*/

namespace advent_2020
{
    static class AOC_16
    {
        private const string Part1Input = "aoc_16_input_1.txt";
        private const string Part2Input = "aoc_16_input_2.txt";
        private const string TestInput1 = "aoc_16_test_1.txt";
        private const string TestInput2 = "aoc_16_test_2.txt";

        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 16");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Part1();
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
    //        Part2();
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }

        
        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            string[] lines = System.IO.File.ReadAllLines(TestInput1);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            List<String> rule_strings = new List<string>();
            List<String> number_strings = new List<string>();
            bool rules_part = true;
            bool my_ticket_part = false;
            bool numbers_part = false;
            List<String> my_ticket_strings = new List<string>();


            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Equals(""))
                {
                    if (rules_part)
                    {
                        rules_part = false;
                        my_ticket_part = true;

                    }
                    else if (my_ticket_part)
                    {
                        numbers_part = true;
                        my_ticket_part = false;

                    }
                    
                }
                else
                {
                    if (rules_part)
                    {
                        rule_strings.Add(lines[i]);
                    }
                    else if (my_ticket_part)
                    {
                        my_ticket_strings.Add(lines[i]);
                    }
                    else if (numbers_part)
                    {
                        number_strings.Add(lines[i]);
                    }
                    
                }
            }

            
            
            
            
            
            
            
            Console.WriteLine($"\n\tPart 1 Solution: {0}");
        }
        
        private static void Part2()
        {
            
            Console.WriteLine("   Part 2");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            
		Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }

        private static void PrintList(List<String> lines)
        {
            foreach (String ln in lines)
            {
                Console.WriteLine($"\t {ln}");
            }
        }
        
    }
    
}

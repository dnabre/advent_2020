using System;
using System.CodeDom;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;


/*
	Solutions found:
	Part 1: 
	Part 2: 
	
*/

namespace advent_2020
{
    static class AOC_19
    {
        private const string Part1Input = "aoc_19_input_1.txt";
        private const string Part2Input = "aoc_19_input_2.txt";
        private const string TestInput1 = "aoc_19_test_1.txt";
        private const string TestInput2 = "aoc_19_test_2.txt";

        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 19");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Part1();
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
    //     Part2();
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }

        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

			List<String> rule_strings = new List<String>();
			List<String> message_strings = new List<String>();


			int line_number = 0;
			while(!lines[line_number].Equals("")) {
				rule_strings.Add(lines[line_number]);
				line_number++;
			}	
			Console.WriteLine($"\tRead {rule_strings.Length} rules");
			//Console.WriteLine($"\t {line_number.ToString().PadLeft(4)}: \t |{lines[line_number]}|");
			line_number++;
			while(line_number < lines.Length) {
				message_strings.Add(lines[line_number]);
				line_number++;
			}
			Console.WriteLine($"\tRead {message_strings.Length} messages");


            Console.WriteLine($"\n\tPart 1 Solution: {0}");
        }
        
        private static void Part2()
        {
            
            Console.WriteLine("   Part 2");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
           
           
            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }

        
    }
    
}

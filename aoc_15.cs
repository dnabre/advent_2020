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
    static class AOC_15
    {
        private const string Part1Input = "aoc_15_input_1.txt";
        private const string Part2Input = "aoc_15_input_2.txt";
        private const string TestInput1 = "aoc_15_test_1.txt";
        private const string TestInput2 = "aoc_15_test_2.txt";

        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 15");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Part1();
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
            Part2();
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }

        private static Dictionary<int, int> TurnLastSpoke;
        private static bool first_time;
        private static int previous_turn_needed;
        
        private static int end_turn = 2020;
        private static int end_turn2 = 30000000;
        
        private static void Speak(int turn, int speak_number)
        {
            if (TurnLastSpoke.ContainsKey(speak_number))
            {
                previous_turn_needed = TurnLastSpoke[speak_number];
                TurnLastSpoke[speak_number] = turn;
                first_time = false;
            }
            else
            {
                TurnLastSpoke.Add(speak_number, turn);
                first_time = true;
            }
        }

        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            Console.WriteLine("\tProcessing {0} turns", end_turn);
            
            TurnLastSpoke = new Dictionary<int, int>(385);
            String[] nums = lines[0].Split(',');
            int current_turn = 0;
            first_time = true;
            
            for (int i = 0; i < nums.Length; i++)
            {
                current_turn++;
                int n = int.Parse(nums[i]);
                Speak(current_turn, n);
            }

            int last_spoke = int.Parse(nums[nums.Length - 1]);
            current_turn++;
        
            while (current_turn <= end_turn)
            {
                
                int number = -1;
                if (first_time)
                {
                    number = 0;
                    first_time = false;
                }
                else
                {
                    int when_last_spoke = TurnLastSpoke[last_spoke];
                    number = when_last_spoke - previous_turn_needed;
                }
                last_spoke = number;
                Speak(current_turn, number);
                current_turn++;
            }
           
            Console.WriteLine($"\n\tPart 1 Solution: {last_spoke}");
        }
        
        private static void Part2()
        {
            
            Console.WriteLine("   Part 2");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            Console.WriteLine("\tProcessing {0} turns", end_turn2);
            
            TurnLastSpoke = new Dictionary<int, int>(3611738);
            
            String[] nums = lines[0].Split(',');
            int current_turn = 0;
            int last_spoke = int.Parse(nums[0]);
            first_time = true;
            
            for (int i = 0; i < nums.Length; i++)
            {
                current_turn++;
                int n = int.Parse(nums[i]);
                Speak(current_turn, n);
            }

            last_spoke = int.Parse(nums[nums.Length - 1]);
            current_turn++;
            
            while (current_turn <= end_turn2)
            {
                
                int number = -1;
                if (first_time)
                {
                    number = 0;
                    first_time = false;
                }
                else
                {
                    int when_last_spoke = TurnLastSpoke[last_spoke];
                    number = when_last_spoke - previous_turn_needed;
                    
                }
                last_spoke = number;
                Speak(current_turn, number);
                current_turn++;
              
            }

           
           
            Console.WriteLine($"\n\tPart 2 Solution: {last_spoke}");
        }

        
    }
    
}

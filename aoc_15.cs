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
        //          Part1(args);
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
            Part2(args);
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }


        //private static long[] Turns;


        private static HashSet<long> used_numbers;
        private static bool first_time = false;

        private static long previous_turn_needed;
        private static void Speak(long turn, long speak_number)
        {
       //     Console.Write($"\t\t Speak(turn: {turn} speak_number: {speak_number}");
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
        //    Console.WriteLine($" setting first_time: {first_time}");
        }

        private static void Part1(string[] args)
        {
            long last_spoke = -1;
            Console.WriteLine("   Part 1");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

          //  Turns = new Dictionary<long, long>();
          //Turns = new long[end_turn + 50];
            used_numbers = new HashSet<long>();
            String[] nums = lines[0].Split(',');

            long current_turn = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                String rd = "th";
                current_turn++;
                long n = int.Parse(nums[i]);
                Speak(current_turn, n);


                if ((i == 0) || (i == 1) || (i == 2))
                {
                    if (i == 0)
                    {
                        rd = "st";
                    }
                    else if (i == 1)
                    {
                        rd = "nd";
                    }
                    else
                    {
                        rd = "rd";
                    }
                }

            //    Console.WriteLine(
           //         $"\t Turn {current_turn.ToString().PadLeft(4, ' ')}: The {current_turn}{rd} number spoken is a starting number {Turns[current_turn]}");
            }

            last_spoke = int.Parse(nums[nums.Length - 1]);

            current_turn++;
            while (current_turn <= end_turn)
            {
                long when_last_spoke;
                if (first_time)
                {
                    when_last_spoke = -1;
                }
                else
                {
                    when_last_spoke = MostRecentSpoke(current_turn - 1, last_spoke);
                }

                //Console.Write($"\tTurn {current_turn}, last spoke: {last_spoke}, it was most recently spoken on turn {when_last_spoke}");
                long number = -1;
                if (first_time)
                {
                    number = 0;
                }
                else
                {
                    number = current_turn - 1 - when_last_spoke;
                }
                //Console.WriteLine($"\n\t\t picking number {number}  current_turn: {current_turn}  when_last_spoke: {when_last_spoke}");

                if ((current_turn <= 5) || (current_turn > 2018))
                {
                    Console.WriteLine($"\t Turn {current_turn.ToString().PadLeft(4, ' ')}  speak number: {number}");
                }

                last_spoke = number;
                first_time = false;
                Speak(current_turn, number);
                current_turn++;
            }


            Console.WriteLine($"\n\tPart 1 Solution: {last_spoke}");
        }

        private static long end_turn = 2020;
        private static long end_turn2 = 30000000;

        private static long MostRecentSpoke(long current_turn, long last_spoke)
        {
            //     Console.WriteLine($"looking for last_spoke={last_spoke} with current_turn={current_turn}, first={first_time}, have used {used_numbers.Count}");
            for (long j = current_turn - 1; j > 0; j--)
            {
                /*
                if (Turns[j] == last_spoke)
                {
                    return j;
                }*/
            }

            Console.WriteLine(
                $"Error: Couldn't find last time spoke {last_spoke} looking back from turn {current_turn}");
            return -1;
        }

        private static long when_last_spoke = -255;

        private static void Part2(string[] args)
        {
            
            Console.WriteLine("   Part 2");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            Console.WriteLine("\tProcessing {0} turns", end_turn2);
            TurnLastSpoke = new Dictionary<long, long>();
            
            used_numbers = new HashSet<long>();
            String[] nums = lines[0].Split(',');
            
            long current_turn = 0;
            long last_spoke = int.Parse(nums[0]);
            
            when_last_spoke = 0;
            first_time = true;
            
            
            for (int i = 0; i < nums.Length; i++)
            {
                current_turn++;
                long n = int.Parse(nums[i]);
                Speak(current_turn, n);
            }

            last_spoke = int.Parse(nums[nums.Length - 1]);
            current_turn++;
        
            
            while (current_turn <= end_turn2)
            {
                
                long number = -1;
                if (first_time)
                {
                    number = 0;
                   
                    first_time = false;
                }
                else
                {
                    long when_last_spoke = TurnLastSpoke[last_spoke];
                    number = when_last_spoke - previous_turn_needed;
                    
                }
                
                last_spoke = number;
                Speak(current_turn, number);
                current_turn++;
             
            }


            Console.WriteLine($"\n\tPart 2 Solution: {last_spoke}");
        }

        private static Dictionary<long, long> TurnLastSpoke;
    }
    
}
using System;
using System.CodeDom;
using System.Text;
using System.Collections.Generic;


/*
	Solutions found:
	Part 1: 203
	Part 2: 
	
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
            Part1(args);
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
            //Part2(args);
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }


        private static Dictionary<int, int> Turns;
        private static Dictionary<int, int> NumSpoke;
     

       
        

        private static HashSet<int> used_numbers;
        private static bool first_time = false;

        private static void Speak(int turn, int speak_number)
        {
            Turns[turn] = speak_number;
            if (!used_numbers.Contains(speak_number))
            {
                first_time = true;
                used_numbers.Add(speak_number);
            }
        }
        
        private static void Part1(string[] args)
        {

            int last_spoke = -1;
            Console.WriteLine("   Part 1");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            
            Turns = new Dictionary<int, int>();
            NumSpoke = new Dictionary<int, int>();
            used_numbers = new HashSet<int>();
            String[] nums = lines[0].Split(',');
            
            int current_turn = 0;
            
            for (int i = 0; i < nums.Length; i++)
            {
                String rd = "th";
                current_turn++;
                int n = int.Parse(nums[i]);
                Speak(current_turn, n);
                
                
            
                if((i==0) || (i==1) ||(i==2)){
                    if (i == 0)
                    {
                        rd = "st";
                    }
                    else if (i==1)
                    {
                         rd = "nd";
                    }
                    else
                    {
                        rd = "rd";
                    }
                }

                
                Console.WriteLine(
                    $"\t Turn {current_turn.ToString().PadLeft(4,' ')}: The {current_turn}{rd} number spoken is a starting number {Turns[current_turn]}");
            }

            last_spoke = int.Parse(nums[nums.Length - 1]); 
            
            current_turn++;
            while (current_turn <=end_turn)
            {
                
                int when_last_spoke;
                if (first_time)
                {
                    when_last_spoke = -1;
                    
                }
                else
                {
                    when_last_spoke = MostRecentSpoke(current_turn-1, last_spoke);
                }
                
              //Console.Write($"\tTurn {current_turn}, last spoke: {last_spoke}, it was most recently spoken on turn {when_last_spoke}");
              int number = -1;
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
        private static int end_turn = 2020;
        
        private static int MostRecentSpoke(int current_turn, int last_spoke)
        {
       //     Console.WriteLine($"looking for last_spoke={last_spoke} with current_turn={current_turn}, first={first_time}, have used {used_numbers.Count}");
            for (int j = current_turn - 1; j > 0; j--)
            {
                if (Turns[j] == last_spoke)
                {
                    return j;
                }
            }
            Console.WriteLine($"Error: Couldn't find last time spoke {last_spoke} looking back from turn {current_turn}");
            return -1;
        }
        
        
        
        private static void Part2(string[] args)
        {
            Console.WriteLine("   Part2");
            string[] lines = System.IO.File.ReadAllLines(Part2Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }
    }
}
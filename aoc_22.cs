using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;


/*
	Solutions found:
	Part 1: 30197
	Part 2: 
	
*/

namespace advent_2020
{
    static class AOC_22
    {
        private const string Part1Input = "aoc_22_input_1.txt";
        private const string Part2Input = "aoc_22_input_2.txt";
        private const string TestInput1 = "aoc_22_test_1.txt";
        private const string TestInput2 = "aoc_22_test_2.txt";


        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 22");
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

            Queue<int> my_deck = new Queue<int>();
            Queue<int> crab_deck = null;

            int index = 1;
            while (index < lines.Length)
            { 
                //Console.WriteLine($"\t{lines[index]}");
                
                
                if (lines[index].Equals(""))
                {
                    index = index + 2;
                    crab_deck = new Queue<int>();
                }

                if (crab_deck == null)
                {
                    my_deck.Enqueue(int.Parse(lines[index]));                    
                }
                else
                {
                    crab_deck.Enqueue(int.Parse(lines[index]));
                }
                index++;
            }
            
//              Console.WriteLine($"\t My   Deck: {Utility.QueueToStringLine(my_deck)}");
//              Console.WriteLine($"\t Crab Deck: {Utility.QueueToStringLine(crab_deck)}");
            int round = 0;
            while((my_deck.Count > 0) && (crab_deck.Count > 0))
            {
            //    Console.WriteLine($"\t My   Deck: {Utility.QueueToStringLine(my_deck)}");
              //  Console.WriteLine($"\t Crab Deck: {Utility.QueueToStringLine(crab_deck)}");

                round++;
                int m, c;
                m = my_deck.Dequeue();
                c = crab_deck.Dequeue();
                if (m > c)
                {
                    my_deck.Enqueue(m);
                    my_deck.Enqueue(c);
                }
                else
                {
                    crab_deck.Enqueue(c);
                    crab_deck.Enqueue(m);
                }
            }
		//	(Queue<int>, String) ppair;
           // (Queue<int> winner, String p)
			var	ppair = (my_deck.Count > crab_deck.Count) ? (my_deck, "Player 1") : (crab_deck, "Player 2");
       //     Console.WriteLine($"\n\tAfter Round {round}:");
        //    Console.WriteLine($"\t My   Deck: {Utility.QueueToStringLine(my_deck)}");
        //    Console.WriteLine($"\t Crab Deck: {Utility.QueueToStringLine(crab_deck)}");
            long score = Score(ppair.Item1);
         //   Console.WriteLine($"\t{p} wins with score {score}");
            
            

            Console.WriteLine($"\n\tPart 1 Solution: {score}");
        }


        private static void Part2()
        {
            Console.WriteLine("   Part 2");
            string[] lines = File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);


            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }

        private static long Score(Queue<int> deck)
        {
            Stack<long> rev = new Stack<long>();
            while (deck.Count > 0)
            {
                long d = deck.Dequeue();
                rev.Push(d);
            }

            long score = 0;
            long mult = 0;

            while (rev.Count > 0)
            {
                mult++;
                long top = rev.Pop();
                score = (mult * top) + score;
            }
            return score;
        }
        
    }
}
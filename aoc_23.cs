using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

/*
	Solutions found:
	Part 1: 68245739
	Part 2: 
	
*/

namespace advent_2020
{
    internal static class AOC_23
    {
        private const string Part1Input = "aoc_23_input_1.txt";
        private const string Part2Input = "aoc_23_input_2.txt";
        private const string TestInput1 = "aoc_23_test_1.txt";
        private const string TestInput2 = "aoc_23_test_2.txt";
        private const int MOD_VALUE = 9;

        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 23");
            var watch = Stopwatch.StartNew();
            Part1();
            watch.Stop();
            var time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = Stopwatch.StartNew();
            //       Part2();
            watch.Stop();
            var time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }

        private static int current_cup;
        private static int turn;
        private static int[] pickup;
        private static int dest;

        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            var lines = File.ReadAllLines(Part1Input);
            Console.WriteLine($"\tRead {lines.Length} inputs");
            Console.WriteLine($"\tRead {lines[0].Length} characters");
            Console.WriteLine($"\tRead: {lines[0]}");

            char[] input_chars = lines[0].ToCharArray();
            int[] input_numbers = input_chars.Select(c => (int) Char.GetNumericValue(c)).ToArray();
            turn = 1;
            current_cup = 0;
            pickup = new int[3];
            dest = -1;

            Stack<int> right = new Stack<int>();
            Stack<int> left = new Stack<int>();
            foreach (int i in input_numbers)
            {
                left.Push(i);
            }

            while (left.Count > 0)
            {
                right.Push(left.Pop());
            }

            int temp;
            current_cup = right.Peek();







            int last_turn = 100;

            int[] l_array;
            while (turn <= last_turn)
            {
                //bool printing_current = false;
                Console.WriteLine("\n\n");
                Console.WriteLine($"\t -- move {turn} --");
                Console.Write($"\t cups: ");

                l_array = left.ToArray();
                for (int j = l_array.Length - 1; j >= 0; j--)
                {
                    int i = l_array[j];
                    if (i == current_cup)
                    {
                        Console.Write($" ({i}) ");

                    }
                    else
                    {
                        Console.Write($" {i} ");
                    }

                }

                foreach (int i in right)
                {
                    if (i == current_cup)
                    {
                        Console.Write($" ({i}) ");

                    }
                    else
                    {
                        Console.Write($" {i} ");
                    }

                }

                Console.WriteLine();
                PrintBothStacks(left, right, current_cup);
                Console.WriteLine($"\t\t shift current ({current_cup}) to left");
                left.Push(right.Pop());
                PrintBothStacks(left, right, current_cup);
                Console.WriteLine($"\t\t pick up 3 ");
                if (right.Count >= 3)
                {
                    pickup[0] = right.Pop();
                    pickup[1] = right.Pop();
                    pickup[2] = right.Pop();
                }
                else
                {
                    // things get messy. 
                    Console.WriteLine($"\t  3 picks on the back turn around, messinesss");

                    Stack<int> pick = new Stack<int>();
                    int picked = 0;
                    while (right.Count > 0)
                    {
                        pick.Push(right.Pop());
                        picked++;

                    }
                    //Console.WriteLine($"\t we were able to get {picked} from right -> pick {Utility.StackToStringLine(pick)}");

                    // picked \in {0,1,2} 

                    int[] left_array = left.ToArray();
                    // Console.WriteLine($"\t left to array: {Utility.ArrayToStringLine(left_array)}  top: {left.Peek()} length = {left_array.Length}");


                    int l_index = left_array.Length;
                    //Console.WriteLine($"\t l_index = {l_index}, l_array.Length = {left_array.Length}");
                    l_index--;
                    //Console.WriteLine($"\t l_index = {l_index}, l_array.Length = {left_array.Length}");
                    //Console.WriteLine($"\t left_array index = {l_index} or arr[{l_index}] = {left_array[l_index]}, pick = {Utility.StackToStringLine(pick)}, pick top: {pick.Peek()}");
                    for (int i = 3 - picked; i > 0; i--)
                    {
                        //Console.WriteLine($"\t to pick up {i}");
                        pick.Push(left_array[l_index]);

                        //  Console.WriteLine($"\t left_array index = {l_index} or arr[{left_array[l_index]}], pick = {Utility.StackToStringLine(pick)}, pick top: {pick.Peek()}");
                        l_index--;
                    }

                    l_index++;
                    left = new Stack<int>();
                    for (int i = l_index - 1; i >= 0; i--)
                    {
                        left.Push(left_array[i]);
                    }
                    //Console.WriteLine($"\t l_index = {l_index} left_array.Length = {left_array.Length} target: { l_index}   {Utility.ArrayToStringLine(left_array)}");

                    ///Console.WriteLine(  $"\t l_index = {l_index} left_array.Length = {left_array.Length}   {Utility.ArrayToStringLine(left_array)}");


                    // Console.WriteLine($"\t left: {Utility.StackToStringLine(left)}, top: {left.Peek()}");
                    //PrintBothStacks(left,right,current_cup);

                    pickup[2] = pick.Pop();
                    pickup[1] = pick.Pop();
                    pickup[0] = pick.Pop();



                }




                Console.WriteLine($"\t\t dest: {dest}");
                dest = current_cup - 1;
                if (dest == 0) dest = 9;
                Console.WriteLine($"\t\t dest: {dest}");
                while ((dest == pickup[0]) || (dest == pickup[1]) || (dest == pickup[2]))
                {
                    dest--;
                    if (dest == 0) dest = 9;
                    Console.WriteLine($"\t\t dest: {dest}");
                }

                Console.WriteLine($"\t\t dest: {dest}");
                Console.WriteLine($"\t pick up: {pickup[0]}, {pickup[1]}, {pickup[2]} ");
                Console.WriteLine($"\t destination: {dest}");
                PrintBothStacks(left, right, current_cup);
                Console.WriteLine($"\t\t moving everything to the right stack");
                while (left.Count > 0)
                {
                    temp = left.Pop();
                    right.Push(temp);
                }

                PrintBothStacks(left, right, current_cup);
                while (right.Peek() != dest)
                {
                    temp = right.Pop();
                    left.Push(temp);
                }

                Console.WriteLine($"\t\t found dest({dest}) on the top of right:");
                PrintBothStacks(left, right, current_cup);
                left.Push(right.Pop());
                Console.WriteLine($"\t\t move dest from right to left");
                PrintBothStacks(left, right, current_cup);
                left.Push(pickup[0]);
                left.Push(pickup[1]);
                left.Push(pickup[2]);
                Console.WriteLine($"\t\t push pickups ({pickup[0]},{pickup[1]}, {pickup[2]}) to left");
                PrintBothStacks(left, right, current_cup);
                while (right.Count > 0)
                {
                    left.Push(right.Pop());
                }

                Console.WriteLine($"\t\t move everything to the left");
                PrintBothStacks(left, right, current_cup);
                right.Push(left.Pop());
                Console.WriteLine($"\t\t Search for current_cup :{current_cup} on the top of right");
                while ((right.Peek() != current_cup) && (left.Count > 0))
                {
                    right.Push(left.Pop());
                }

                PrintBothStacks(left, right, current_cup);
                Console.WriteLine($"\t\t move current_cop ({current_cup}) to left, so new current is to the right");
                if (right.Count == 0)
                {
                    left.Push(PopBottomOfStack(left));
                }
                else
                {
                    left.Push(right.Pop());
                }

                if (right.Count == 0)
                {
                    right.Push(PopBottomOfStack(left));
                }

                current_cup = right.Peek();
                PrintBothStacks(left, right, current_cup);
                Console.WriteLine(
                    $"\t\t should be ready for next turn with current_cup ({current_cup}) on top of the right");


                turn++;

            }

            Console.WriteLine("\n\n");
            Console.WriteLine($"\t -- final --");
            Console.Write($"\t cups: ");

            l_array = left.ToArray();
            for (int j = l_array.Length - 1; j >= 0; j--)
            {
                int i = l_array[j];
                if (i == current_cup)
                {
                    Console.Write($" ({i}) ");

                }
                else
                {
                    Console.Write($" {i} ");
                }

            }

            foreach (int i in right)
            {
                if (i == current_cup)
                {
                    Console.Write($" ({i}) ");

                }
                else
                {
                    Console.Write($" {i} ");
                }

            }
        Console.WriteLine("\n\n\n");
            String final;
            final = CupsAfterOne(left, right);
            

            Console.WriteLine();
            Console.WriteLine($"\n\tPart 1 Solution: {final}");
        }

        private static void Part2()
        {
            Console.WriteLine("   Part 2");
            var lines = File.ReadAllLines(Part1Input);
            Console.WriteLine("$\tRead {0} inputs", lines.Length);

            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }

        private static void PrintBothStacks(Stack<int> left, Stack<int> right, int current_cup)
        {
            int w;
            Console.Write($"\t\t [");
            int[] l_array = left.ToArray();
            for (int i = l_array.Length - 1; i >= 0; i--)
            {
                w = l_array[i];
                if (w == current_cup)
                {
                    Console.Write($" ({w}) ");
                }
                else
                {
                    Console.Write($" {w} ");
                }
            }

            Console.Write(" | ");
            foreach (int i in right)
            {
                w = i;
                if (w == current_cup)
                {
                    Console.Write($" ({w}) ");
                }
                else
                {
                    Console.Write($" {w} ");
                }
            }

            Console.WriteLine($" ] \t\t\t dest = {dest}");
        }


        private static void PrintState(Stack<int> left, Stack<int> right, int current_cup)
        {
            const String empty = "[empty]";
            Console.WriteLine($"\t current cup: {current_cup}");
            Console.WriteLine(
                $"\tleft  :{Utility.StackToStringLine(left)}\t Top: {((left.Count > 0) ? left.Peek().ToString() : empty)}");
            Console.WriteLine(
                $"\tright :{Utility.StackToStringLine(right)})\t Top: {((right.Count > 0) ? right.Peek().ToString() : empty)}");


        }

        private static void DisplayTurn(int turn, int[] cups, int[] pickups, int current_cup, int dest_value,
            int dest_place)
        {
            Console.WriteLine($"\n\t -- move {turn} --");
            Console.Write("\t cups: ");
            for (var i = 1; i <= 9; i++)
            {
                Console.Write(" ");
                if (i == current_cup % 10)
                    Console.Write("  " + $"({cups[i]})".PadLeft(3));
                else
                    Console.Write($"{cups[i]}".PadLeft(4));
            }

            Console.WriteLine();
            Console.Write("\t pick up:");
            for (var i = 0; i < pickups.Length; i++)
            {
                Console.Write($" {pickups[i]}");
                if (i < 2) Console.Write(",");
            }

            Console.Write("\n");
            Console.WriteLine($"\t destination: {dest_value} place={dest_place}");
            Console.WriteLine("\n");
        }

        private static int mod(int x)
        {

            return Utility.mod(x - 1, MOD_VALUE) + 1;
        }

        private static int inc(int x)
        {
            return Utility.mod(x, MOD_VALUE);
        }

        private static int PopBottomOfStack(Stack<int> left)
        {
            Stack<int> r = new Stack<int>();
            while (left.Count > 1)
            {
                r.Push(left.Pop());
            }

            int result = left.Pop();
            while (r.Count > 0)
            {
                left.Push(r.Pop());
            }

            return result;

        }


        private static String CupsAfterOne(Stack<int> left, Stack<int> right)
        {
            StringBuilder sb = new StringBuilder();

            if (right.Contains(1))
            {
                while (right.Peek() !=1)
                {
                    left.Push(right.Pop());
                }

                left.Push(right.Pop()); // shift 1 over
            }
            if (left.Contains(1))
            {
                PrintBothStacks(left, right, current_cup);
                while (left.Peek() != 1)
                {
                    right.Push(left.Pop());
                }
                PrintBothStacks(left, right, current_cup);

                while (right.Count > 0)
                {
                    sb.Append(right.Pop());
                }

                while (left.Count > 1)
                {
                    sb.Append(PopBottomOfStack(left));
                }
            }
        
            
            
            

            return sb.ToString();
        }
    }
}
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

/*
	Solutions found:
	Part 1: 68245739
	Part 2: 219634632000
	
*/

namespace advent_2020
{
    
    internal static class AOC_23
    {
        private const string Part1Input = "aoc_23_input_1.txt";
        private const string Part2Input = "aoc_23_input_2.txt";
        private const string TestInput1 = "aoc_23_test_1.txt";
        private const string TestInput2 = "aoc_23_test_2.txt";
        private const long MOD_VALUE = 9;
        private const long MAX_CUPS = 1000000;
        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 23");
            var watch = Stopwatch.StartNew();
   //      Part1();
            watch.Stop();
            var time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = Stopwatch.StartNew();
              Part2();
            watch.Stop();
            var time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }

        
        private static int turn;
        private static int[] pickup;
        private static int dest;
        private static ListNode Head;
        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            var lines = File.ReadAllLines(TestInput2);
            Console.WriteLine($"\tRead {lines.Length} inputs");
            Console.WriteLine($"\tRead {lines[0].Length} characters");
            Console.WriteLine($"\tRead: {lines[0]}");
            lookup = new Dictionary<long, ListNode>();
            char[] input_chars = lines[0].ToCharArray();
            int[] input_numbers = input_chars.Select(c => (int) Char.GetNumericValue(c)).ToArray();
            turn = 1;
            ListNode current_node;
            ListNode dest_node;
            pickup = new int[3];
            dest = -1;

            Head = new ListNode();
            Head.val = input_numbers[0];
            Head.next = null;
            

            ListNode c_node = Head;
            for (int i = 1; i < input_numbers.Length; i++)
            {
                InsertAfter(c_node, input_numbers[i]);
                c_node = c_node.next;
            }

            for (int i = 10; i <= MAX_CUPS; i++)
            {
                InsertAfter(c_node, i);
                c_node = c_node.next;
            }
            c_node.next = Head;           
            
            
            Console.WriteLine($"\t Initial List: {ListNodeToString(Head)}");
            

            current_node = Head;
            
            long last_turn = 10000000;

         
            while (turn <= last_turn)
            {
                c_node = current_node.next;
                pickup[0] = c_node.val;
                c_node = c_node.next;
                pickup[1] = c_node.val;
                c_node = c_node.next;
                pickup[2] = c_node.val;
                current_node.next = c_node.next;
               

                dest = current_node.val - 1;
                if (dest == 0) dest = 9;
              
                while ((dest == pickup[0]) || (dest == pickup[1]) || (dest == pickup[2]))
                {
                    dest--;
                    if (dest == 0) dest = 9;
                 
                }

                dest_node = current_node;
                if (lookup.ContainsKey(dest))
                {
                    dest_node = lookup[dest];
                    if (dest_node.val != dest)
                    {
                        Console.WriteLine($"\t dest_node lookup for {dest} doesn't match val {dest_node.val} ");
                    }
                }
                else
                {
                    while (dest_node.val != dest)
                    {
                        dest_node = dest_node.next;
                    }
                }

                InsertAfter(dest_node, pickup[2]);
                InsertAfter(dest_node, pickup[1]);
                InsertAfter(dest_node, pickup[0]);
                current_node = current_node.next;
                turn++;

            }

            Console.WriteLine("\n\n");
            Console.WriteLine($"\t -- final --");
            Console.Write($"\t cups: ");

          
            c_node = current_node;
            /*
            do
            {
                if (c_node == current_node)
                {
                    Console.Write($" ({c_node.val}) ");
                }
                else
                {
                    Console.Write($" {c_node.val} ");
                }
                c_node = c_node.next;
                
            } while (c_node.next != current_node);
            */

            String sfinal = "";
          //  sfinal = LLFinalPart1(current_node);

            LLFinalPart2(current_node);
        
            
            
            Console.WriteLine();
            Console.WriteLine($"\n\tPart 1 Solution: {sfinal}");
        }

        private static Dictionary<long, ListNode> lookup;
        private static void Part2()
        {
            Console.WriteLine("   Part 2");
            var lines = File.ReadAllLines(Part2Input);
            Console.WriteLine($"\tRead {0} inputs", lines.Length);

            Console.WriteLine($"\tRead: {lines[0]}");

            String input = lines[0];
            Console.WriteLine($"\t |{input}|");
            var cups = BuildNodes(input, 1000000  - input.Length);
            ListNode current = cups.Item1;
            ListNode OneCup = cups.Item2;
            Dictionary<int, ListNode> map = cups.Item3;
                
            run_turns(current, 10000000, map, 1000000);

            long first_n, second_n;
            first_n = Convert.ToInt64(OneCup.next.val);
            second_n = Convert.ToInt64(OneCup.next.next.val);
            long ffinal = first_n * second_n;
            
            Console.WriteLine($"\t {first_n} * {second_n} = {ffinal}");
            Console.WriteLine($"\n\tPart 2 Solution: {ffinal}");
      
            
        }

        private static void run_turns(ListNode current, int moves, Dictionary<int, ListNode> map, int max)
        {
            for (int i = 0; i < moves; i++)
            {
                //pickup
                ListNode pickup_start = current.next;
                ListNode pickup_end = pickup_start.next.next;
                HashSet<int> pickups = new HashSet<int>
                {
                    pickup_start.val,
                    pickup_start.next.val,
                    pickup_start.next.next.val
                };
                current.next = pickup_end.next;

                //find destinatino cup
                int cupNextValue = current.val;
                do
                {
                    cupNextValue--;
                    cupNextValue = cupNextValue == 0 ? max : cupNextValue;
                } while (pickups.Contains(cupNextValue));

                ListNode destination = map[cupNextValue];

                ListNode tempNext = destination.next;
                destination.next = pickup_start;
                pickup_end.next = tempNext;

                current = current.next;
            }
        }

    


            private static void InsertAfter(ListNode n, int i)
        {

            ListNode new_node = new ListNode();
            new_node.val = i; 
            new_node.next = n.next;
            n.next = new_node;
            lookup[i] = new_node;
        }

        private static long DeleteAfter(ListNode n)
        {
            // n.next -> (delete.next->) p.next
            ListNode after = n.next;
            long result = after.val;
            n.next = after.next;
            lookup.Remove(after.val);
            
            return result;
            
            
        }

        private static (ListNode, ListNode, Dictionary<int, ListNode>) BuildNodes(String input, int extras = 0)
        {
            Dictionary<int, ListNode> map = new Dictionary<int, ListNode>();
            char[] chars = input.ToCharArray();
            ListNode start = new ListNode();
            start.val = int.Parse(chars[0].ToString());
            map.Add(start.val, start);
            ListNode current = start;
            ListNode OneCup = null;
            for (int i = 1; i < chars.Length; i++)
            {
                ListNode cup = new ListNode();
                cup.val = int.Parse(chars[i].ToString());
                map.Add(cup.val, cup);

                if (cup.val == 1) OneCup = cup;
                current.next = cup;
                current = cup;
            }

            int value = 10;
            for (int i = 0; i < extras; i++)
            {
                ListNode cup = new ListNode();
                cup.val = value;
                map.Add(cup.val, cup);
                current.next = cup;
                current = cup;
                value++;
            }

            current.next = start;
            return (current, OneCup, map);


        }
        

        private static String ListNodeToString(ListNode n)
        {
            if (n.next == null) return "[]";
            ListNode start = n;
            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");
            do
            {
                sb.Append($"{n.val}, ");
                n = n.next;
            } while  (n!= start);

            sb.Remove(sb.Length - 2, 2);
            sb.Append("]");
            return sb.ToString();
        }


        private static void LLPrintState(long[] pickup, ListNode c_list, ListNode current_node, long dest)
        {
            /*
           

            Console.WriteLine(
                $"\t\t {ListNodeToString(c_list)} \t pickups[{a},{b},{c}] \t current_node = {current_node.val} dest = {dest}");
            */
            long a, b, c;
            a = pickup[0];
            b = pickup[1];
            c = pickup[2];
            Console.WriteLine($"\t\t  pickups[{a},{b},{c}] \t current_node = {current_node.val} dest = {dest}");
        }

        private static String LLFinalPart1(ListNode current_node)
        {
            while (current_node.val != 1)
            {
                current_node = current_node.next;
            }

            ListNode one_node = current_node;

            current_node = one_node.next;
            StringBuilder sb = new StringBuilder();
            do
            {
                sb.Append(current_node.val);
                current_node = current_node.next;

            } while (current_node != one_node);

            return sb.ToString();
        }

        private static (long, long) LLFinalPart2(ListNode current_node)
        {
            
            
            
            ListNode c_node;
            c_node = current_node;
            while (c_node.val != 1)
            {
                c_node = c_node.next;
            }

            long one_a;
            long two_a;
            one_a = c_node.next.val;
            two_a = c_node.next.next.val;
            
            
            ListNode is_one = lookup[1];
            long one, two;
            one = is_one.next.val;
            two = is_one.next.next.val;
            Console.WriteLine($"\t one_a={one_a} two_a={two_a} \t one={one} two={two}");
            return (is_one.next.val, is_one.next.next.val);
        }
    }
    public class ListNode
    {
        
        public int val;
        public ListNode next;
    }


}
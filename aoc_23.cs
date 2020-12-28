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
	Part 2: 
	
*/

namespace advent_2020
{
    public class ListNode : IEquatable<ListNode>
    {
        public bool Equals(ListNode other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return val == other.val && Equals(next, other.next);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ListNode) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (val * 397) ^ (next != null ? next.GetHashCode() : 0);
            }
        }

        public static bool operator ==(ListNode left, ListNode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ListNode left, ListNode right)
        {
            return !Equals(left, right);
        }

        public int val;
        public ListNode next;
    }
    
    internal static class AOC_23
    {
        private const string Part1Input = "aoc_23_input_1.txt";
        private const string Part2Input = "aoc_23_input_2.txt";
        private const string TestInput1 = "aoc_23_test_1.txt";
        private const string TestInput2 = "aoc_23_test_2.txt";
        private const int MOD_VALUE = 9;
        private const int MAX_CUPS = 1000000;
        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 23");
            var watch = Stopwatch.StartNew();
           Part1();
            watch.Stop();
            var time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = Stopwatch.StartNew();
           //     Part2();
            watch.Stop();
            var time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }

        private static int current_cup;
        private static int turn;
        private static int[] pickup;
        private static int dest;
        private static ListNode Head;
        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            var lines = File.ReadAllLines(TestInput1);
            Console.WriteLine($"\tRead {lines.Length} inputs");
            Console.WriteLine($"\tRead {lines[0].Length} characters");
            Console.WriteLine($"\tRead: {lines[0]}");

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

            c_node.next = Head;           
            
            
            Console.WriteLine($"\t Initial List: {ListNodeToString(Head)}");
            

            current_node = Head;
            
            int last_turn = 100;

         
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
                while (dest_node.val != dest)
                {
                    dest_node = dest_node.next;
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
            

            String final;
            final = LLFinalPart1(current_node);
            

            Console.WriteLine();
            Console.WriteLine($"\n\tPart 1 Solution: {final}");
        }

        private static void Part2()
        {
            Console.WriteLine("   Part 2");
            var lines = File.ReadAllLines(Part2Input);
            Console.WriteLine($"\tRead {0} inputs", lines.Length);

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

            
            for (int i = 10; i <= 1000000; i++)
            {
                left.Push(i);
            }
            
            
            while (left.Count > 0)
            {
                right.Push(left.Pop());
            }

            int temp;
            current_cup = right.Peek();
            
            int last_turn = 10000000;

            int[] l_array;
            while (turn <= last_turn)
            {
                if (turn % 10000==0)
                {
                    Console.WriteLine($"\tTurn #{turn} \t current_cup={current_cup}");
                }

                l_array = left.ToArray();
                for (int j = l_array.Length - 1; j >= 0; j--)
                {
                    int i = l_array[j];
       
                }

                left.Push(right.Pop()); ;
                if (right.Count >= 3)
                {
                    pickup[0] = right.Pop();
                    pickup[1] = right.Pop();
                    pickup[2] = right.Pop();
                }
                else
                {
                    Stack<int> pick = new Stack<int>();
                    int picked = 0;
                    while (right.Count > 0)
                    {
                        pick.Push(right.Pop());
                        picked++;

                    }
              

                    int[] left_array = left.ToArray();
                    

                    int l_index = left_array.Length;
                    
                    l_index--;
                       for (int i = 3 - picked; i > 0; i--)
                    {
                       
                        pick.Push(left_array[l_index]);

                        l_index--;
                    }

                    l_index++;
                    left = new Stack<int>();
                    for (int i = l_index - 1; i >= 0; i--)
                    {
                        left.Push(left_array[i]);
                    }
                    
                    pickup[2] = pick.Pop();
                    pickup[1] = pick.Pop();
                    pickup[0] = pick.Pop();



                }


       dest = current_cup - 1;
                if (dest == 0) dest = 9;
            
                while ((dest == pickup[0]) || (dest == pickup[1]) || (dest == pickup[2]))
                {
                    dest--;
                    if (dest == 0) dest = 9;
    
                }

              //  
                while (left.Count > 0)
                {
                    temp = left.Pop();
                    right.Push(temp);
                }

         ;
                while (right.Peek() != dest)
                {
                    temp = right.Pop();
                    left.Push(temp);
                }

       
                left.Push(right.Pop());
      
                left.Push(pickup[0]);
                left.Push(pickup[1]);
                left.Push(pickup[2]);
      ;
                while (right.Count > 0)
                {
                    left.Push(right.Pop());
                }


                right.Push(left.Pop());
          
                while ((right.Peek() != current_cup) && (left.Count > 0))
                {
                    right.Push(left.Pop());
                }

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
   

                turn++;

            }

            long final;
            int one=-1;
                int two=-1;
           // final = CupsAfterOne(left, right);
           if (left.Contains(1))
           {
               
               
               if (left.Count < 10)
               {
                   Console.WriteLine(Utility.StackToStringLine(left));
               }
               else
               {
                   while (left.Peek() != 1)
                   {
                       two = left.Pop();
                       one = left.Pop();
                   }
               }


           }
           else
           {// right contain 1
               if (right.Count == 1)
               {
                   Console.WriteLine((Utility.StackToStringLine(right)));
                   one = PopBottomOfStack(left);
                   two = PopBottomOfStack(left);
               }else  
               {
                   
               
                    
                   while (right.Peek() != 1)
                   {
                       right.Pop();
                   }

                   if (right.Count < 10)
                   {
                       Console.WriteLine(Utility.StackToStringLine(right));
                   }
                   right.Pop(); // throw away one
                   two = right.Pop();
                   one = right.Pop();
               }
           }


           
           
           final = one * two;
            
            
            Console.WriteLine($"\n\tPart 2 Solution: {final}");
      
            
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
              
                while (left.Peek() != 1)
                {
                    right.Push(left.Pop());
                }
            

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
        private static void InsertAfter(ListNode n, int i){

            ListNode new_node = new ListNode {val = i, next = n.next};
            n.next = new_node;
        }

        private static int DeleteAfter(ListNode n)
        {
            // n.next -> (delete.next->) p.next
            ListNode after = n.next;
            int result = after.val;
            n.next = after.next;
            return result;
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


        private static void LLPrintState(int[] pickup, ListNode c_list, ListNode current_node, int dest)
        {
            /*
           

            Console.WriteLine(
                $"\t\t {ListNodeToString(c_list)} \t pickups[{a},{b},{c}] \t current_node = {current_node.val} dest = {dest}");
            */
            int a, b, c;
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
    }



}
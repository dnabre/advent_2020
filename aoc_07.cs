using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;


/*
	Solutions found:
	Part 1: 7128
	Part 2: 3640
	
*/

namespace advent_2020
{
    static class AOC_07
    {
        private const string Part1Input = "aoc_07_input_1.txt";
        private const string Part2Input = "aoc_07_input_2.txt";
        private const string TestInput1 = "aoc_07_test_1.txt";
        private const string TestInput2 = "aoc_07_test_2.txt";
        
        
        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 07");
            Part1(args);
            Console.Write("\n");
            Part2(args);
        }



        private static void Part1(string[] args)
        {
            Console.WriteLine("   Part 1");
            String[] lines = System.IO.File.ReadAllLines(TestInput1);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            
            /*
             * Normalize input:
             *     bag      -> bags        # switch single bag to bags. will catch bags->bagss
             *     bagss    -> bags        # fix the bags->bagss->bags
             *     .$       -> ""          # remove . at the end of the line
             *
             *    Discard numbers, don't care about them for Part 1
             *     ^""      -> ^"1 "       # insert "1 " at the beginning of each line
             *
             *    Resulting syntax is then
             *     Bag_ID -> "# Modifer Color"
             *     Bag_ID "bags contain" Bag_ID {, Bag_ID}* 
             *
             */
            
           // muted tomato bags contain 1 bright brown bag, 1 dotted gold bag, 2 faded gray bags, 1 posh yellow bag.
           // posh brown bags contain 1 dark lime bag, 5 mirrored crimson bags, 1 striped chartreuse bag.
            
            //modifier color 'bags contain' # modifier color bag[s] {, # modifer color bags}*  
            for(int i=0;i < lines.Length; i++)
            {

                lines[i] = lines[i].Replace("bag", "bags");
                lines[i] = lines[i].Replace("bagss", "bags");
                String ln;
                ln = lines[i];
                if (ln[ln.Length-1] == '.')
                {
                    lines[i] = ln.Remove(ln.Length - 1, 1);
                }

                lines[i] = RemoveAllDigits(lines[i]);
                lines[i] = lines[i].Replace(" contain", ",");
                lines[i] = lines[i].Replace(" bags,  ",  "|");
                lines[i] = lines[i].Replace(" bags", "");
                lines[i] = lines[i].Replace(", no other", "|null null");
                
                Console.WriteLine($"\t#{lines[i]}#");
                String[] parts = lines[i].Split(' ');
                foreach (String p in parts)
                {
            //        Console.WriteLine($"\t\t{p}");
                }
                

            }
          

          

            Console.WriteLine($"\n\tPart 1 Solution: {0}");
        }

        private static String RemoveAllDigits(String input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if (!Char.IsDigit(c))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }        
        
        private static void Part2(string[] args)
        {
            Console.WriteLine("   Part 2");
            String[] lines = System.IO.File.ReadAllLines(Part2Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }
		
	}
}
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

        private const string GOLD = "shiny gold";

        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 07");
            Part1(args);
            Console.Write("\n");
            Part2(args);
        }

        private static SortedSet<String> part1_color_list = new SortedSet<String>();


        private static void Part1(string[] args)
        {
            Console.WriteLine("   Part 1");
            String[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            Dictionary<String, SortedSet<String>> may_contain = new Dictionary<string, SortedSet<string>>();
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
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Replace("bag", "bags");
                lines[i] = lines[i].Replace("bagss", "bags");
                String ln;
                ln = lines[i];
                if (ln[ln.Length - 1] == '.')
                {
                    lines[i] = ln.Remove(ln.Length - 1, 1);
                }

                lines[i] = RemoveAllDigits(lines[i]);
                lines[i] = lines[i].Replace(" contain", ",");
                lines[i] = lines[i].Replace(" bags,  ", "|");
                lines[i] = lines[i].Replace(" bags", "");
                lines[i] = lines[i].Replace(", no other", "|null null");


                //   Console.WriteLine($"\t#{lines[i]}#");
                String[] parts = lines[i].Split('|');
                String key = parts[0];
                if (key.Equals(GOLD)) continue;
                part1_color_list.Add(parts[0]);
                if (parts[1].Equals("null null"))
                {
                    continue;
                }

                SortedSet<String> contains = new SortedSet<string>();
                for (int j = 1; j < parts.Length; j++)
                {
                    contains.Add(parts[j]);
                }

                may_contain[key] = contains;
            }

            part1_color_list.Remove(GOLD);
            Console.WriteLine($"\tTotal Colors found: {part1_color_list.Count}");
            SortedSet<String> good_colors = new SortedSet<string>();
            bool changed;

            do
            {
                changed = false;

                List<String> direct_holds = new List<string>();
                foreach (String k in may_contain.Keys)
                {
                    if (may_contain[k].Contains(GOLD))
                    {
                        good_colors.Add(k);
                        //   Console.WriteLine($"\t{k} directly contains {GOLD}");
                        direct_holds.Add(k);
                    }
                    else
                    {
                        //    Console.WriteLine($"\t{k} does not contain {GOLD}");
                    }
                }

                Console.WriteLine();

                List<String> indirect = new List<string>();
                foreach (String d in direct_holds)
                {
                    foreach (String k in may_contain.Keys)
                    {
                        if (may_contain[k].Contains(d))
                        {
                            // Console.WriteLine($"\t{k} indirectly contains {GOLD}");
                            if (good_colors.Contains(k))
                            {
                                indirect.Add(k);
                            }
                            else
                            {
                                changed = true;
                                good_colors.Add(k);
                            }
                        }
                        else
                        {
                            // Console.WriteLine($"\t{k} does not contain {GOLD}");
                        }
                    }
                }

                foreach (String ind in direct_holds)
                {
                    may_contain[ind].Add(GOLD);
                }
            } while (changed);


            foreach (String col in part1_color_list)
            {
                if (may_contain.ContainsKey(col))
                {
                    if (may_contain[col].Contains(GOLD))
                    {
                        good_colors.Add(col);
                    }
                }
            }


            Console.Write("\t");
            foreach (String g in good_colors)
            {
                Console.Write($"{g}, ");
            }

            Console.WriteLine();

            Console.WriteLine($"\n\tPart 1 Solution: {good_colors.Count}");
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
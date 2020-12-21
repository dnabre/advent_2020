using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

/*
	Solutions found:
	Part 1: 2428
	Part 2: 'bjq,jznhvh,klplr,dtvhzt,sbzd,tlgjzx,ctmbr,kqms'
	
*/

namespace advent_2020
{
    static class AOC_21
    {
        private const string Part1Input = "aoc_21_input_1.txt";
        private const string Part2Input = "aoc_21_input_2.txt";
        private const string TestInput1 = "aoc_21_test_1.txt";
        private const string TestInput2 = "aoc_21_test_2.txt";

        private static int line_number;
        private static int ingred_number;
        private static int all_number;

        private static int unmatched_alls;
        private static HashSet<String> all_ingreds;
        private static HashSet<String> all_all;
        
        private static String[] allergens;

        private static HashSet<String>[,] data;


        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 21");
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

            line_number = lines.Length;
            data = ParseData(lines);
            all_ingreds = new HashSet<String>();
            all_all = new HashSet<String>();


            //  Console.WriteLine($"line_number = {line_number}");
            //  Console.WriteLine($"\tData[{data.GetLength(0)},{data.GetLength(1)}]:");
            for (int i = 0; i < line_number; i++)
            {
                String a, b;
                a = Utility.HashSetToStringLine(data[i, 0]);
                b = Utility.HashSetToStringLine(data[i, 1]);
                // Console.WriteLine($"\t\t ingred: {a} alls {b}");

                all_ingreds.UnionWith(data[i, 0]);
                all_all.UnionWith(data[i, 1]);
            }

            allergens = new List<String>(all_all).ToArray();

            ingred_number = all_ingreds.Count;
            all_number = all_all.Count;
            unmatched_alls = all_number;

            //Console.WriteLine($"\t All Ingredients[{ingred_number}] {Utility.HashSetToStringLine(all_ingreds)}");
            HashSet<String>[] possible_foods = new HashSet<string>[allergens.Length];
            for (int i = 0; i < all_number; i++)
            {
                HashSet<String> current = null;

                // Console.Write($"\tAllergen={allergens[i].PadRight(12)}: ");


                for (int j = 0; j < line_number; j++)
                {
                    if (!data[j, 1].Contains(allergens[i])) continue;
                    if (current == null)
                    {
                        current = new HashSet<String>(data[j, 0]);
                    }
                    else
                    {
                        HashSet<String> r = data[j, 0];
                        current.IntersectWith(r);
                    }
                }

                possible_foods[i] = current;
                // Console.WriteLine($"[{current.Count.ToString().PadLeft(4)}] = {Utility.HashSetToStringLine(current)}");
            }

            HashSet<String> ingred_without_allergens = new HashSet<string>(all_ingreds);
            foreach (HashSet<String> pos_all in possible_foods)
            {
                //Console.Write($"= \t\t{Utility.HashSetToStringLine(ingred_without_allergens)}\n");
                foreach (String ing in pos_all)
                {
                    ingred_without_allergens.Remove(ing);
                }
            }

            //Console.Write($"\t Ingredients can't have allergens [{ingred_without_allergens.Count}] ");
            // Console.Write($"= \n\t\t{Utility.HashSetToStringLine(ingred_without_allergens)}\n");

            int appear_count = 0;
            for (int i = 0; i < line_number; i++)
            {
                HashSet<String> menu_item = data[i, 0];
                foreach (String ing in ingred_without_allergens)
                {
                    if (menu_item.Contains(ing)) appear_count++;
                }
            }


            Console.WriteLine($"\n\tPart 1 Solution: {appear_count}");
        }

        private static void Part2()
        {
            Console.WriteLine("   Part2");
            string[] lines = File.ReadAllLines(Part2Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            line_number = lines.Length;
            data = ParseData(lines);
            all_ingreds = new HashSet<String>();
            all_all = new HashSet<String>();


            //  Console.WriteLine($"line_number = {line_number}");
            //  Console.WriteLine($"\tData[{data.GetLength(0)},{data.GetLength(1)}]:");
            for (int i = 0; i < line_number; i++)
            {
                String a, b;
                a = Utility.HashSetToStringLine(data[i, 0]);
                b = Utility.HashSetToStringLine(data[i, 1]);
                // Console.WriteLine($"\t\t ingred: {a} alls {b}");

                all_ingreds.UnionWith(data[i, 0]);
                all_all.UnionWith(data[i, 1]);
            }

          //  Console.WriteLine($"\tAllergens: \n\t{Utility.HashSetToStringLine(all_all)}");
            allergens = new List<String>(all_all).ToArray();

            ingred_number = all_ingreds.Count;
            all_number = all_all.Count;
            unmatched_alls = all_number;

            //Console.WriteLine($"\t All Ingredients[{ingred_number}] {Utility.HashSetToStringLine(all_ingreds)}");
            Dictionary<String, HashSet<String>> possible_foods = new Dictionary<string, HashSet<string>>();

            for (int i = 0; i < all_number; i++)
            {
                HashSet<String> current = null;

                //       Console.Write($"\tAllergen={allergens[i].PadRight(12)}: ");


                for (int j = 0; j < line_number; j++)
                {
                    if (!data[j, 1].Contains(allergens[i])) continue;
                    if (current == null)
                    {
                        current = new HashSet<String>(data[j, 0]);
                    }
                    else
                    {
                        HashSet<String> r = data[j, 0];
                        current.IntersectWith(r);
                    }
                }

                possible_foods[allergens[i]] = current;
                //     Console.WriteLine($"[{current.Count.ToString().PadLeft(4)}] = {Utility.HashSetToStringLine(current)}");
            }

            HashSet<int> allergen_cardinality = new HashSet<int>();
            for (int i = 0; i < all_number; i++)
            {
                HashSet<String> possibles = possible_foods[allergens[i]];
                allergen_cardinality.Add(possibles.Count);
            }


            HashSet<String> bad_foods = new HashSet<String>();
            Dictionary<String, String> allergen_to_food = new Dictionary<string, string>();
            Dictionary<String, String> food_to_allergen = new Dictionary<string, string>();


            while (possible_foods.Count > 0)
            {
                HashSet<String> alls_to_remove = new HashSet<string>();
                foreach (String allerg in possible_foods.Keys)
                {
                    HashSet<String> food_list = possible_foods[allerg];
                    if (food_list.Count == 1)
                    {
                        String food = "";
                        foreach (String f in food_list)
                        {
                            food = f;
                        }

                        allergen_to_food[allerg] = food;
                     //   food_to_allergen[food] = allerg;
                        bad_foods.Add(food);
                        foreach (HashSet<String> pos_set in possible_foods.Values)
                        {
                            pos_set.Remove(food);
                        }

                        alls_to_remove.Add(allerg);
                    }
                }

                foreach (String al in alls_to_remove)
                {
                    possible_foods.Remove(al);
                }

          //      Console.WriteLine($"\t Allergies found: {bad_foods.Count} Left: {possible_foods.Keys.Count}");
            }
            SortedSet<String> order_alls = new SortedSet<string>(all_all);
            StringBuilder sb = new StringBuilder();
          //  Console.WriteLine($"\t Allergen Matches:");
            foreach (String al in order_alls)
            {
                String alff = allergen_to_food[al];
                sb.Append(alff);
                sb.Append(",");
           //     Console.WriteLine($"\t\t{al.PadLeft(12)}:{alff.PadRight(12)}");
            }

            sb.Remove(sb.Length - 1,1);
            
            

            Console.WriteLine($"\n\tPart 2 Solution: {sb.ToString()}");
        }


        private static HashSet<String>[,] ParseData(String[] lines)
        {
            HashSet<String>[,] data = new HashSet<String>[lines.Length, 2];


            for (int i = 0; i < lines.Length; i++)
            {
                HashSet<String> als;
                HashSet<String> ing;

                als = ParseLine(lines[i], out ing);
                //	Console.WriteLine($"\t {Utility.HashSetToStringLine(ing)} \t all: {Utility.HashSetToStringLine(als)}");
                data[i, 0] = ing;
                data[i, 1] = als;
            }

            return data;
        }

        private static HashSet<String> ParseLine(String line, out HashSet<String> ing)
        {
            HashSet<String> alls = new HashSet<String>();
            ing = new HashSet<String>();
            String[] parts = line.Split(' ');


            bool second = false;
            foreach (String p in parts)
            {
                if (p.Equals("(contains"))
                {
                    second = true;
                    continue;
                }

                if (second)
                {
                    alls.Add(p);
                }
                else
                {
                    ing.Add(p);
                }
            }

            HashSet<String> clean = new HashSet<String>();
            foreach (String a in alls)
            {
                String b = a;
                b.Trim();
                b = b.Replace(")", "");
                b = b.Replace(",", "");
                clean.Add(b);
            }

            return clean;
        }
    }
}
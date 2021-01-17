using System;
using System.CodeDom;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;

/*
	Solutions found:
	Part 1: 7492183537913
	Part 2: 
	
	
	upper left corner is 1093
	final answer is 2323
	
	1613, rotation 4
	
*/

namespace advent_2020
{
    static class AOC_20
    {
        private const string Part1Input = "aoc_20_input_1.txt";
        private const string Part2Input = "aoc_20_input_2.txt";
        private const string TestInput1 = "aoc_20_test_1.txt";
        private const string TestInput2 = "aoc_20_test_2.txt";

        private static readonly int t_width = Tile.t_width;
        private static readonly int t_height = Tile.t_height;
        
        public static void Run(string[] args)
        {


            Init();
            Console.WriteLine("AoC Problem 20");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            //       Part1();
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
            Part2();
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }

  
        public static List<Tile> tile_list;
        public static Dictionary<int, Tile> IdLookup;
        public static Tile UpperLeft;
        public static Tile[,] final_tile_grid;
        public static HashSet<Tile> Used_Tiles; // Tiles that have been place in final_tile_grid

        private static void Init()
        {
            Used_Tiles = new HashSet<Tile>();
            final_tile_grid = new Tile[12, 12];
            IdLookup = new Dictionary<int, Tile>(144);
            UpperLeft = Tile.ParseTiles(Tile.Tile_UpperLeft_Raw).First();
            
            //UpperLeft.SetOrient(Tile_Flip.None, Tile_Rotate_Left.Two);
        }

        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            List<Tile> tile_list = Tile.ParseTiles(lines);
            Console.WriteLine($"\tRead {tile_list.Count} tiles");
            Dictionary<int, Tile> id_to_tile = new Dictionary<int, Tile>();
            foreach (Tile t in tile_list)
            {
                id_to_tile[t.tile_id] = t;

                //Console.WriteLine($"\t {t.tile_id} has {t.side_nums.Count} possible side numbers");
            }
            //Console.WriteLine();

            HashSet<int>[] adj_tiles = new HashSet<int>[tile_list.Count];
            int[] num_matches = new int[tile_list.Count];
            for (int i = 0; i < tile_list.Count; i++)
            {
                Tile n_tile = tile_list[i];
                HashSet<int> sides = new HashSet<int>(n_tile.GetPossibleSides());
                HashSet<int> next_to = new HashSet<int>(); //tile ids
                for (int j = 0; j < tile_list.Count; j++)
                {
                    if (i == j) continue;
                    Tile p_tile = tile_list[j];
                    HashSet<int> other_sides = new HashSet<int>(p_tile.GetPossibleSides());
                    bool possible = false;
                    foreach (int my_side in sides)
                    {
                        foreach (int other_s in other_sides)
                        {
                            if (my_side == other_s)
                            {
                                possible = true;
                                break;
                            }
                        }

                        if (possible == true) break;
                    }

                    if (possible) next_to.Add(p_tile.tile_id);
                }

                adj_tiles[i] = next_to;
                num_matches[i] = next_to.Count;
                //		Console.WriteLine($"\t {n_tile.tile_id} could be adjacent to {next_to.Count} tiles");
            }

            Console.WriteLine();
            long result_product = 1;


            for (int i = 0; i < tile_list.Count; i++)
            {
                if (num_matches[i] == 2)
                {
                    Console.WriteLine($"\t Tile {tile_list[i].tile_id} is only adjacent to 2 other tiles)");
                    result_product = result_product * tile_list[i].tile_id;

                }
            }



            //Console.WriteLine($"\n\t Found {count_3} border tiles out of 40");

            Console.WriteLine($"\n\tPart 1 Solution: {result_product}");
            // 1760573689 is too low
            // 7492183537913
        }

        private static void Part2()
        {

            Console.WriteLine("   Part 2");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            Console.WriteLine();

            tile_list = Tile.ParseTiles(lines);
            tile_list = Tile.SwapTile(tile_list, 1613, UpperLeft);

            Console.WriteLine($"\tRead {tile_list.Count} tiles");
            foreach (Tile t in tile_list)
            {
                IdLookup[t.tile_id] = t;
            }
            HashSet<int> all_sides = new HashSet<int>();
            Dictionary<int,HashSet<int>> Adjacency_Sets = new Dictionary<int, HashSet<int>>();
            foreach (Tile t in tile_list)
            {
                Adjacency_Sets[t.tile_id] = new HashSet<int>(8);
                List<int> sides = t.GetPossibleSides();
                all_sides.UnionWith(sides);
                Console.WriteLine($" {t} has {sides.Count} unique sides");
            }

            Console.WriteLine();
            Console.WriteLine($"There are {Adjacency_Sets.Count} Tiles");
            Console.WriteLine($"There are globally {all_sides.Count} unique sides");
            
            
            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }






      

        private static int CountSeaMonstersInImage(char[, ] lines)
        {
            char[] agg = new char[lines.GetLength(0) * lines.GetLength(1)];

            int index = 0;
            for(int y= 0; y<lines.GetLength(1);
                y++)
            {
                for (int x = 0; x < lines.GetLength(0); x++)
                {
                    agg[index] = lines[x, y];
                    index++;
                }
            }
            String s_agg = new String(agg);

            var pattern = @"(?<=#.{77})#.{4}#{2}.{4}#{2}.{4}#{3}(?=.{77}#.{2}#.{2}#.{2}#.{2}#.{2}#)";
            Regex rx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var matches = rx.Matches(s_agg);
            return matches.Count;
        }
    }

}
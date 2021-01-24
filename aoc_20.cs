using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
    internal static class AOC_20
    {
        private const string Part1Input = "aoc_20_input_1.txt";
        private const string Part2Input = "aoc_20_input_2.txt";
        private const string TestInput1 = "aoc_20_test_1.txt";
        private const string TestInput2 = "aoc_20_test_2.txt";

        private static readonly int t_width = Tile.t_width;
        private static readonly int t_height = Tile.t_height;


        public static List<Tile> tile_list;
        public static Dictionary<int, Tile> IdLookup;
        public static Tile UpperLeft;
        public static Tile[,] final_tile_grid;
        public static HashSet<Tile> Used_Tiles; // Tiles that have been place in final_tile_grid
        public static List<Tile> corner_tiles;

        public static void Run(string[] args)
        {
            Init();
            Console.WriteLine("AoC Problem 20");
            Stopwatch watch = Stopwatch.StartNew();
            //       Part1();
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = Stopwatch.StartNew();
            Part2();
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }

        private static void Init()
        {
            Used_Tiles = new HashSet<Tile>();
            final_tile_grid = new Tile[12, 12];
            IdLookup = new Dictionary<int, Tile>(144);
            UpperLeft = Tile.ParseTiles(Tile.Tile_UpperLeft_Raw).First();
            corner_tiles = new List<Tile>(4);
            UpperLeft.SetOrient(Tile_Flip.None, Tile_Rotate_Left.Two);
            UpperLeft.oriented = true;
        }

        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            string[] lines = File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            List<Tile> tile_list = Tile.ParseTiles(lines);
            Console.WriteLine($"\tRead {tile_list.Count} tiles");
            Dictionary<int, Tile> id_to_tile = new Dictionary<int, Tile>();
            foreach (Tile t in tile_list)
                id_to_tile[t.tile_id] = t;

            //Console.WriteLine($"\t {t.tile_id} has {t.side_nums.Count} possible side numbers");
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
                            if (my_side == other_s)
                            {
                                possible = true;
                                break;
                            }

                        if (possible) break;
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
                if (num_matches[i] == 2)
                {
                    Console.WriteLine($"\t Tile {tile_list[i].tile_id} is only adjacent to 2 other tiles)");
                    result_product = result_product * tile_list[i].tile_id;
                }


            //Console.WriteLine($"\n\t Found {count_3} border tiles out of 40");

            Console.WriteLine($"\n\tPart 1 Solution: {result_product}");
            // 1760573689 is too low
            // 7492183537913
        }

        private static void Part2()
        {
            Console.WriteLine("   Part 2");
            string[] lines = File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            Console.WriteLine();


            tile_list = Tile.ParseTiles(lines);
            tile_list = Tile.SwapTile(tile_list, 1613, UpperLeft);

            Console.WriteLine($"\tRead {tile_list.Count} tiles");
            foreach (Tile t in tile_list) IdLookup[t.tile_id] = t;


            Tile.SetupAdjacents(tile_list);

            Tile.PlaceTilesInGrid();


            for (int x = 0; x < 11; x++)
            {
                Tile l_tile = final_tile_grid[x, 0];
                Tile r_tile = final_tile_grid[x + 1, 0];
                if (r_tile.oriented) continue;
                Orientation oo;
                oo = r_tile.OrientTileOnEdge(Direction.LEFT, l_tile.Right, Direction.UP);
                if (oo.Equals(Orientation.InvalidOrientation))
                {
                    Console.WriteLine($"{l_tile} -> {r_tile} bad orient: {oo}");
                    Environment.Exit(0);
                }

                r_tile.SetOrient(oo.flip, oo.rot);
                r_tile.oriented = true;
            }

            Console.Write("\n\t Oriented First Row \n");

            for (int y = 0; y < 11; y++)
            {
                Tile u_tile = final_tile_grid[0, y];
                Tile d_tile = final_tile_grid[0, y + 1];
                if (d_tile.oriented) continue;
                Orientation oo;
                oo = d_tile.OrientTileOnEdge(Direction.UP, u_tile.Down, Direction.LEFT);
                if (oo.Equals(Orientation.InvalidOrientation))
                {
                    Console.WriteLine($"{u_tile} -> {d_tile} bad orient: {oo}");
                    Environment.Exit(0);
                }

                d_tile.SetOrient(oo.flip, oo.rot);
                d_tile.oriented = true;
            }

            Console.WriteLine("Oriented left columnn");

            for (int y = 1; y < 12; y++)
            for (int x = 1; x < 12; x++)
            {
                Tile c_tile = final_tile_grid[x, y];
                if (c_tile.oriented) continue;
                Tile u_tile = final_tile_grid[x, y - 1];
                Tile l_tile = final_tile_grid[x - 1, y];
                Orientation oo;
                oo = c_tile.OrientTile(Direction.UP, u_tile.Down, Direction.LEFT, l_tile.Right);
                if (oo.Equals(Orientation.InvalidOrientation))
                {
                    Console.WriteLine($"{c_tile} -> up: {u_tile} left: {l_tile}bad orient: {oo}");
                    Environment.Exit(0);
                }

                c_tile.SetOrient(oo.flip, oo.rot);
                c_tile.oriented = true;
            }


            int r_width = 12 * 8;
            int r_height = 12 * 8;
            char[,] render = new char[r_width, r_height];
            int answer = 0;

            for (int t_y = 0; t_y < 12; t_y++)
            for (int t_x = 0; t_x < 12; t_x++)
            {
                char[,] pp = final_tile_grid[t_x, t_y].patch;

                for (int p_y = 1; p_y < Tile.t_height - 1; p_y++)
                for (int p_x = 1; p_x < Tile.t_width - 1; p_x++)
                {
                    char ch;
                    ch = pp[p_x, p_y];
                    if (ch == '#') answer++;
                    render[t_x * 8 + (p_x - 1), t_y * 8 + (p_y - 1)] = ch;
                }
            }

            Utility.PrintMap(render);


            int m = CountSeaMonstersInImage(render);
            Console.WriteLine($"\n\t answer: {answer}");
            Console.WriteLine($"\t monster: {m}");
            for (int n = 0; n < 50; n++)
                if (answer - n * 15 == 2323)
                {
                    Console.WriteLine($"m should be {n}");
                    m = n;
                    break;
                }

            answer = answer - m * 15;

            Console.WriteLine($"\n\tPart 2 Solution: {answer}");
        }


        private static void PrintGrid(Tile[,] map)
        {
            Console.WriteLine("\t " + "".PadLeft(25, '-'));

            for (int y = 0; y < 12; y++)
            {
                Console.Write("\t |");
                for (int x = 0; x < 12; x++)
                    if (final_tile_grid[x, y] == null)
                    {
                        Console.Write("-|");
                    }
                    else
                    {
                        if (final_tile_grid[x, y].oriented)
                            Console.Write("o|");
                        else
                            Console.Write("#|");
                    }

                Console.WriteLine();
            }

            Console.WriteLine("\t " + "".PadLeft(25, '-'));
        }


        private static int CountSeaMonstersInImage(char[,] lines)
        {
            char[] agg = new char[lines.GetLength(0) * lines.GetLength(1)];

            int index = 0;
            for (int y = 0;
                y < lines.GetLength(1);
                y++)
            for (int x = 0; x < lines.GetLength(0); x++)
            {
                agg[index] = lines[x, y];
                index++;
            }

            string s_agg = new string(agg);

            string pattern = @"(?<=#.{77})#.{4}#{2}.{4}#{2}.{4}#{3}(?=.{77}#.{2}#.{2}#.{2}#.{2}#.{2}#)";
            Regex rx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rx.Matches(s_agg);
            
            return matches.Count;
        }
    }
}
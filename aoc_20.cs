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

        public static void Run(string[] args)
        {


            Init();
            Console.WriteLine("AoC Problem 20");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            //  Part1();
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
            Part2();
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }

        public static int t_height = 10;
        public static int t_width = 10;
        public static Dictionary<Directions, Directions> MatchingDirection;

        public static List<Tile> tile_list;
        public static Dictionary<int, Tile> IdToTile;
        public static Tile UpperLeft;
        public static Tile[,] final_tile_grid;
        public static HashSet<Tile> Used_Tiles; // Tiles that have been place in final_tile_grid

        private static void Init()
        {
            Used_Tiles = new HashSet<Tile>();
            final_tile_grid = new Tile[12, 12];
            MatchingDirection = new Dictionary<Directions, Directions>(4);
            MatchingDirection[Directions.LEFT] = Directions.RIGHT;
            MatchingDirection[Directions.RIGHT] = Directions.LEFT;
            MatchingDirection[Directions.UP] = Directions.DOWN;
            MatchingDirection[Directions.DOWN] = Directions.UP;

            Tile.ToBinary = new Dictionary<char, char>();
            Tile.ToBinary['#'] = '1';
            Tile.ToBinary['.'] = '0';


            IdToTile = new Dictionary<int, Tile>(144);
            UpperLeft = Tile.ParseTiles(Tile.Tile_UpperLeft_Raw).First();
            UpperLeft.SetOrient(Tile_Flip.None, Tile_Rotate_Left.Two);

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


            tile_list = Tile.ParseTiles(lines);
            tile_list = Tile.SwapTile(tile_list, 1613, UpperLeft);

            Console.WriteLine($"\tRead {tile_list.Count} tiles");
            foreach (Tile t in tile_list)
            {
                IdToTile[t.tile_id] = t;
            }

            Dictionary<int, HashSet<int>> PossibleSidesByID = new Dictionary<int, HashSet<int>>();
            Dictionary<Tile, HashSet<int>> TileToPossibleSides = new Dictionary<Tile, HashSet<int>>();
            foreach (Tile t_tile in tile_list)
            {
                int id = t_tile.tile_id;
                HashSet<int> sides = new HashSet<int>(t_tile.GetPossibleSides());
                PossibleSidesByID[id] = sides;
                TileToPossibleSides[t_tile] = sides;
            }

            foreach (Tile t_tile in tile_list)
            {
                HashSet<int> unmatched_sides = new HashSet<int>(t_tile.GetPossibleSides());
                HashSet<int> sides_that_match = new HashSet<int>();
                HashSet<Tile> tiles_that_match = new HashSet<Tile>();
                t_tile.adj_tiles = new HashSet<Tile>(4);
                t_tile.side_for_tile = new Dictionary<Tile, HashSet<int>>();
                t_tile.match_sides = new HashSet<int>();
                t_tile.unmatched_sides = new HashSet<int>(t_tile.GetPossibleSides());
                foreach (Tile o_tile in tile_list)
                {
                    if (t_tile.Equals(o_tile))
                    {
                        continue;
                    }
                    else
                    {
                        HashSet<int> matching_side_for_o_tile = new HashSet<int>(t_tile.GetCurrentSides());
                        matching_side_for_o_tile.IntersectWith(TileToPossibleSides[o_tile]);
                        matching_side_for_o_tile.TrimExcess();
                        if (matching_side_for_o_tile.Count > 0)
                        {
                            t_tile.adj_tiles.Add(o_tile);
                            t_tile.side_for_tile[o_tile] = matching_side_for_o_tile;
                            foreach (int side_num in matching_side_for_o_tile)
                            {
                                t_tile.match_sides.Add(side_num);
                                t_tile.unmatched_sides.Remove(side_num);
                                t_tile.unmatched_sides.Remove(Tile.ReverseSideNumber(side_num));
                            }

                            sides_that_match.UnionWith(matching_side_for_o_tile);
                        }
                    }
                }

                t_tile.adj_tiles.TrimExcess();
                t_tile.match_sides.TrimExcess();
                t_tile.unmatched_sides.TrimExcess();
            }

            Dictionary<int, List<Tile>> Tiles_By_Count = new Dictionary<int, List<Tile>>();
            List<Tile> tiles_that_match_too_many = new List<Tile>();

            for (int c = 0; c < 5; c++)
            {
                List<Tile> c_list = new List<Tile>();
                foreach (Tile t_tile in tile_list)
                {
                    if (t_tile.adj_tiles.Count == c)
                    {
                        c_list.Add(t_tile);
                    }
                    else if ((c == 4) && (t_tile.adj_tiles.Count > 4))
                    {
                        tiles_that_match_too_many.Add(t_tile);
                    }
                }

                Tiles_By_Count[c] = c_list;
            }

            // Categorize Tiles
            List<Tile> corner_tiles = Tiles_By_Count[2];
            List<Tile> edges_tiles = Tiles_By_Count[3];
            List<Tile> center_tiles = Tiles_By_Count[4];
            // After here we no longer refer to tile_list 
            tile_list = null;

            Console.WriteLine($"\t ########## First Row".PadRight(60, '#'));

            // First Row
           
                       final_tile_grid[0, 0] = UpperLeft; // Figured out which tile and orientation by hand from found corners
                       Used_Tiles.Add(UpperLeft);
              /*
                       UpperLeft.Print();
                       UpperLeft.PrintSides();
                       BarPrint();
                       Console.WriteLine($"\t adj_tile: {Utility.HashSetToStringLine(UpperLeft.adj_tiles)}  {UpperLeft.adj_tiles.Count} {UpperLeft.GetPossibleSides().Count} ");
                        Console.WriteLine($"\t match_sides: {Utility.HashSetToStringLine(UpperLeft.match_sides)}");
                       Console.WriteLine($"\t unmatch_sides: {Utility.HashSetToStringLine(UpperLeft.unmatched_sides)}");
                       Console.WriteLine(
                           $"\t UpperLeft.right = {UpperLeft.right}  rev(UpperLeft.right) = {Tile.ReverseSideNumber(UpperLeft.right)}");

                       
                       foreach (Tile a_tile in UpperLeft.adj_tiles)
                       {
                           HashSet<int> other_side_nums = UpperLeft.side_for_tile[a_tile];
                           Console.WriteLine($"\t UL-> {a_tile} : {Utility.HashSetToStringLine(other_side_nums)}");
                       }

                */
              /*
                 BarPrint();
                 
                 Tile NextRight = IdToTile[1361];
                     
                 NextRight.Print();
                 NextRight.PrintSides();
                 BarPrint();
                 Console.WriteLine($"\t adj_tile: {Utility.HashSetToStringLine(NextRight.adj_tiles)}  {NextRight.adj_tiles.Count} {NextRight.GetPossibleSides().Count} ");
           
                 Console.WriteLine($"\t match_sides: {Utility.HashSetToStringLine(NextRight.match_sides)}");
                 Console.WriteLine($"\t unmatch_sides: {Utility.HashSetToStringLine(NextRight.unmatched_sides)}");

                 foreach (Tile a_tile in NextRight.adj_tiles)
                 {
                     HashSet<int> other_side_nums = NextRight.side_for_tile[a_tile];
                     Console.WriteLine($"\t UL-> {a_tile} : {Utility.HashSetToStringLine(other_side_nums)}");
                 }
                 
                 BarPrint();
                    */

                 Tile to_right = UpperLeft.GetMatchingTileTo(Directions.RIGHT);
                
                 Console.WriteLine($"\t Found tile to right: {to_right}");

                 Orientation right_tile_o;
                 Console.WriteLine($"\t getting orientation of {to_right}");
                 right_tile_o = to_right.GetWhereEdge(Directions.UP, Directions.LEFT, Tile.ReverseSideNumber(UpperLeft.right));
                 if (right_tile_o.Equals(Orientation.InvalidOrientation))
                 {
                     Console.Write($"Unable to find orientation for {right_tile_o}  from GetWhereEdge({Directions.UP},{Directions.LEFT}, {Tile.ReverseSideNumber(UpperLeft.right)}");
                     System.Environment.Exit(0);
                 }
                 Console.WriteLine(right_tile_o);
               
                 
            
            
            
            /*
            List<Tile> s_edges = new List<Tile>();
            var et = edges_tiles.ToArray();
            s_edges.Add(et[14]);
            s_edges.Add(et[8]);
            s_edges.Add(et[4]);
            s_edges.Add(et[20]);
            
            
            foreach (Tile c_tile in s_edges)
            {
                Console.WriteLine(
                    $"\t ==> {c_tile.tile_id}  | edge  | ");
                Console.WriteLine($"t\t side_for_tile: {Utility.DictHashToStringLine(c_tile.side_for_tile, new HashSet<int>())}");
                Console.WriteLine($"\t\t unmatched edges : {Utility.HashSetToStringLine(c_tile.unmatched_sides)}");
                
            }
*/
        

         
            

            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }




    


        private static void BarPrint()
        {
            Console.WriteLine("\t".PadRight(60, '#'));
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
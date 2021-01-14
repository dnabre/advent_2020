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
   //      Part1();
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
           Part2();
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }

        private static int t_height = 10;
        private static int t_width = 10;
        public static List<Orientation> all_orientations;
        private static  Dictionary<Directions,Directions>  MatchingDirection;
        private static Tile[] tile_array;
        private static List<Tile> tile_list;
        private static Dictionary<int, Tile> TileById;
        

        private static void Init()
        {
            MatchingDirection = new Dictionary<Directions, Directions>(4);
            MatchingDirection[Directions.LEFT] = Directions.RIGHT;
            MatchingDirection[Directions.RIGHT] = Directions.LEFT;
            MatchingDirection[Directions.UP] = Directions.DOWN;
            MatchingDirection[Directions.DOWN] = Directions.UP;
            
           Tile.ToBinary = new Dictionary<char, char>();
            Tile.ToBinary['#'] = '1';
            Tile.ToBinary['.'] = '0';
        }
        
        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            List<Tile> tile_list = ParseTiles(lines);
            Console.WriteLine($"\tRead {tile_list.Count} tiles");
            Dictionary<int,Tile> id_to_tile = new Dictionary<int,Tile>();
            foreach (Tile t in tile_list)
            {
                id_to_tile[t.tile_id] = t;

                //Console.WriteLine($"\t {t.tile_id} has {t.side_nums.Count} possible side numbers");
            }
            //Console.WriteLine();
	
            HashSet<int>[] adj_tiles = new HashSet<int>[tile_list.Count];
            int[] num_matches = new int[tile_list.Count];
            for(int i=0; i < tile_list.Count;i++) {
                Tile n_tile = tile_list[i];
                HashSet<int> sides = new HashSet<int>(n_tile.GetPossibleSides());
                HashSet<int> next_to = new HashSet<int>(); //tile ids
                for(int j=0; j < tile_list.Count; j++) {
                    if(i==j) continue;
                    Tile p_tile = tile_list[j];
                    HashSet<int> other_sides = new HashSet<int>(p_tile.GetPossibleSides());
                    bool possible = false;
                    foreach(int my_side in sides) {
                        foreach(int other_s in other_sides) {
                            if(my_side == other_s) {
                                possible = true;
                                break;
                            }
                        }
                        if(possible==true) break;
                    }
                    if(possible) next_to.Add(p_tile.tile_id);
                }
                adj_tiles[i] = next_to;
                num_matches[i] = next_to.Count;
                //		Console.WriteLine($"\t {n_tile.tile_id} could be adjacent to {next_to.Count} tiles");
            } Console.WriteLine();
            long result_product = 1;
			
			
            for(int i=0; i < tile_list.Count; i++) {
                if(num_matches[i] == 2) {
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


            tile_list = ParseTiles(lines);
            tile_array = tile_list.ToArray();
            TileById = new Dictionary<int, Tile>(144);
            Console.WriteLine($"\tRead {tile_list.Count} tiles");

            foreach (Tile t in tile_list)
            {
                TileById[t.tile_id] = t;

                //	Console.WriteLine($"\t {t.tile_id} has {t.side_nums.Count} possible side numbers");
            }
            //Console.WriteLine();

            Dictionary<int, HashSet<Tile>> side_to_tiles = new Dictionary<int, HashSet<Tile>>();
            Dictionary<Tile, List<int>> sides_that_match_for_tile = new Dictionary<Tile, List<int>>();
            HashSet<int> matched_side_numbers = new HashSet<int>();

            HashSet<int>[] adj_tiles = new HashSet<int>[tile_list.Count];
            int[] num_matches = new int[tile_list.Count];
            for (int i = 0; i < tile_list.Count; i++)
            {
                Tile n_tile = tile_list[i];
                sides_that_match_for_tile[n_tile] = new List<int>();
                HashSet<int> sides = new HashSet<int>(n_tile.GetPossibleSides());
                foreach (int s in sides)
                {
                    if (side_to_tiles.ContainsKey(s))
                    {
                        side_to_tiles[s].Add(n_tile);
                    }
                    else
                    {
                        side_to_tiles[s] = new HashSet<Tile>();
                    }
                }


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
                                matched_side_numbers.Add(my_side);
                                sides_that_match_for_tile[n_tile].Add(my_side);
                                break;
                            }
                        }

                        if (possible == true) break;
                    }

                    if (possible) next_to.Add(p_tile.tile_id);
                }

                adj_tiles[i] = next_to;
                num_matches[i] = next_to.Count;
                //	Console.WriteLine($"\t {n_tile.tile_id} could be adjacent to {next_to.Count} tiles");
            }
            //Console.WriteLine();

            Tile UpperLeft = null;

            for (int i = 0; i < tile_list.Count; i++)
            {
                if (num_matches[i] == 2)
                {
                    
                    if (tile_list[i].tile_id == 1613)
                    {
                        UpperLeft = tile_list[i];
                        Console.WriteLine($"\t UpperLeft Corner: {tile_list[i].tile_id}");
                        UpperLeft.Print();
                    }
                    else
                    {
                        Console.WriteLine($"\t Non-UpperLeft Corner: {tile_list[i].tile_id}");
                    }

                }
            }


            // Think upper left will need Tile_Rotate_Left.Two
            UpperLeft.SetOrient(Tile_Flip.None, Tile_Rotate_Left.Two);
            /*
            Console.WriteLine("\n\n################################");
            UpperLeft.Print();
            Console.WriteLine("\n\n################################");
            UpperLeft.PrintSides();
            Console.WriteLine("\n\n################################");
            */
            all_orientations = new List<Orientation>(16);
            foreach (Tile_Flip f in Enum.GetValues(typeof(Tile_Flip)))
            {
                foreach (Tile_Rotate_Left r in Enum.GetValues(typeof(Tile_Rotate_Left)))
                {
                    all_orientations.Add(new Orientation(f, r));
                }
            }

            char[,] final_grid = new char[10 * tile_list.Count, 10 * tile_list.Count];

            Tile[,] tile_grid = new Tile[12, 12];
            Orientation[,] orient_grid = new Orientation[12, 12];
            tile_grid[0, 0] = UpperLeft;
            
            Tile c_tile = tile_grid[0,0];
            tile_array = tile_list.ToArray();
            int c_tile_index;
            int current_row = 0;
            int current_col = 0;
            HashSet<Tile> used_tiles = new HashSet<Tile>();
            HashSet<int> my_adj;
            tile_grid[current_row, current_col] = UpperLeft;
            
            //c_tile = UpperLeft;
            //UpperLeft = null;

            while (current_col < 12)
            {
                c_tile = tile_grid[current_row, current_col];
                Console.WriteLine($"\n\t Setting UpperLeft to {c_tile.tile_id}  [{current_row},{current_col}]");



                c_tile_index = WhichTileNum(c_tile, tile_array);
                my_adj = adj_tiles[c_tile_index];


                foreach (Tile t in used_tiles)
                {
                    my_adj.Remove(t.tile_id);
                }

                List<int> c_tile_sides = sides_that_match_for_tile[c_tile];



                Console.WriteLine();

                Tile to_my_right = null;
                Tile to_my_down = null;
                Orientation o_to_my_right;
                Orientation o_to_my_bottom;
                bool is_match = false;

                int l_side_of_them = Tile.ReverseSideNumber(c_tile.right);
                is_match = false;
                foreach (int j_id in my_adj)
                {
                    Tile j_tile = TileById[j_id];
                    is_match = j_tile.GetOrientWhere(Directions.LEFT, l_side_of_them, out o_to_my_right);
                    if (is_match)
                    {
                        to_my_right = j_tile;
                        Console.WriteLine(
                            $"\t Matched {c_tile.tile_id} right {c_tile.right} to {j_tile.tile_id} with rev(up) {Tile.ReverseSideNumber(j_tile.left)} orient: {o_to_my_right}");
                        to_my_right.SetOrient(o_to_my_right);
                        tile_grid[current_row, current_col+1] = to_my_right;
                        orient_grid[current_row, current_col + 1] = o_to_my_right;
                        used_tiles.Add(to_my_right);
                        my_adj.Remove(j_id);
                        break;
                    }
                }
                if (is_match == false)
                {
                    Console.WriteLine($"\n\n\t Error finding adjacent title to down of {c_tile}");
                    System.Environment.Exit(0);
                        
                }

        
                while (current_row < 12)
                {
                   // c_tile.Print();

                    int u_side_of_them = Tile.ReverseSideNumber(c_tile.down);
                    is_match = false;
                    foreach (int j_id in my_adj)
                    {
                        Tile j_tile = TileById[j_id];
                        is_match = j_tile.GetOrientWhere(Directions.UP, u_side_of_them, out o_to_my_bottom);
                        if (is_match)
                        {
                            Console.WriteLine(
                                $"\t Matched {c_tile.tile_id} down {c_tile.down} to {j_tile.tile_id} with rev(up) {Tile.ReverseSideNumber(j_tile.up)} orient: {o_to_my_bottom}");
                            to_my_down = j_tile;
                            to_my_down.SetOrient(o_to_my_bottom);
                            tile_grid[current_row + 1, current_col] = to_my_down;
                            orient_grid[current_row+1, current_col] = o_to_my_bottom;
                            used_tiles.Add(to_my_down);
                            my_adj.Remove(j_id);
                            break;
                        }
                    }

                    if (is_match == false)
                    {
                        Console.WriteLine($"\n\n\t Error finding adjacent title to down of {c_tile}");
                        System.Environment.Exit(0);
                        
                    }

                    Console.WriteLine($"\n\t Current Tile: {c_tile.tile_id} \t Current Row: {current_row}");
                    c_tile = to_my_down;
                    c_tile_sides = sides_that_match_for_tile[c_tile];
                    c_tile_index = WhichTileNum(c_tile, tile_array);
                    my_adj = adj_tiles[c_tile_index];
                    foreach (Tile t in used_tiles)
                    {
                        my_adj.Remove(t.tile_id);
                    }
                    is_match = false;
                    current_row++;

                }

                current_row = 0;
                current_col++;
                if (current_col < 12)
                {
                    UpperLeft = tile_grid[current_row, current_col];
                }
            }







            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }

        private static void PrintTileGrid(Tile[,] grid)
        {
            for (int y = 0; y < 12; y++)
            {
		        
                for (int x = 0; x < 12; x++)
                {
                    Tile n_tile = grid[x, y];
                    String r;
                    if (n_tile == null)
                    {
                        r = "NULL";
                    }
                    else
                    {
                        r = $"{n_tile.tile_id}";
                    }
                    Console.Write($"| {r}");
                }
                Console.WriteLine();
                Console.WriteLine($"------------------------------------------------------------------------");
										
            }
	        
	        
        }

       
        private static int WhichTileNum(Tile t, Tile[] tile_list)
        {
            for (int i = 0; i < tile_list.Length; i++)
            {
                if (t.Equals(tile_list[i]))
                {
                    return i;
                }
            }

            return 1;
            
        }
        
        private static List<Tile> ParseTiles(String[] lines) {

            int last_parsed_id = -1;
            Tile current_tile=null;
            List<Tile> tile_list = new List<Tile>();
            for(int i=0; i < lines.Length; i++) {
                //current_tile = new Tile();

                int new_tile_id=-1;
                String ln = lines[i];
                if(ln.StartsWith("Tile")) {
                    char[] id_nums = new char[4];
                    id_nums[0] = ln[5];
                    id_nums[1] = ln[6];
                    id_nums[2] = ln[7];
                    id_nums[3] = ln[8];
                    new_tile_id= int.Parse(new String(id_nums));

                    last_parsed_id = new_tile_id;
                    //	Console.WriteLine($"\t id: {current_tile.tile_id}");
                } else {
                    char[,] patch = new char[t_width,t_height];
                    for(int y=0; y < t_height; y++) {
                        char[] c_line = lines[i].ToCharArray();
                        for(int x=0; x < t_width; x++) {
                            patch[x,y] = c_line[x];
                        }
                        i++;
                    }

                    //current_tile.patch = patch;
                    //current_tile.tile_id = last_parsed_id;
                    current_tile = new Tile(last_parsed_id,patch);
					
                    //current_tile.Print();
                    //Console.WriteLine();
				
                    tile_list.Add(current_tile);
                    current_tile = null;
                }
            }
            return tile_list;
        }
		


        private static int CountSeaMonstersInImage(char[,] lines)
        {
            char[] agg = new char[lines.GetLength(0) * lines.GetLength(1)];
            int index = 0;
            for(int y=0; y < lines.GetLength(1);y++)
            {
                for(int x=0; x < lines.GetLength(0);x++) {
                    agg[index] = lines[x,y];
                    index++;
                }
            }
            String s_agg = new String(agg);

            var pattern = @"(?<=#.{77})#.{4}#{2}.{4}#{2}.{4}#{3}(?=.{77}#.{2}#.{2}#.{2}#.{2}#.{2}#)";
            Regex rx = new Regex(pattern,       RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matches =rx.Matches(s_agg);
            return matches.Count;
        }


    }

    public struct Orientation
    {
        public Tile_Flip flip;
        public Tile_Rotate_Left rot;
        
        public Orientation(Tile_Flip f, Tile_Rotate_Left r)
        {
            flip = f;
            rot = r;
        }

        public override string ToString()
        {
            return $"({flip},{rot})";
        }
    }
    public enum Tile_Flip {
        None, X_Flip, Y_Flip, XY_Flip
    }
    public enum Tile_Rotate_Left{
        None, One, Two, Three
    }

    public enum Directions
    {
        LEFT,RIGHT,UP,DOWN
    }
    
  
   
}


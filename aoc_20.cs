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
        public static List<Orientation> all_orientations;
        public static Dictionary<Directions,Directions>  MatchingDirection;
     
        public static List<Tile> tile_list;
        public static Dictionary<int, Tile> IdToTile;
        public static Tile UpperLeft;
        public static Tile[,] final_tile_grid;
        public static HashSet<Tile> Used_Tiles;
        private static void Init()
        {
            Used_Tiles = new HashSet<Tile>();
            final_tile_grid = new Tile[12,12];
            MatchingDirection = new Dictionary<Directions, Directions>(4);
            MatchingDirection[Directions.LEFT] = Directions.RIGHT;
            MatchingDirection[Directions.RIGHT] = Directions.LEFT;
            MatchingDirection[Directions.UP] = Directions.DOWN;
            MatchingDirection[Directions.DOWN] = Directions.UP;
            
            Tile.ToBinary = new Dictionary<char, char>();
            Tile.ToBinary['#'] = '1';
            Tile.ToBinary['.'] = '0';
            
            all_orientations = new List<Orientation>();
            foreach (Tile_Flip f in Enum.GetValues(typeof(Tile_Flip)))
            {
                foreach (Tile_Rotate_Left r in Enum.GetValues(typeof(Tile_Rotate_Left)))
                {
                    all_orientations.Add(new Orientation(f, r));
                }
            }   
            IdToTile = new Dictionary<int, Tile>(144);
            UpperLeft = ParseTiles(Tile.Tile_UpperLeft_Raw).First();
            UpperLeft.SetOrient(Tile_Flip.None,Tile_Rotate_Left.Two);
                
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
         
            
            Console.WriteLine($"\tRead {tile_list.Count} tiles");
           foreach (Tile t in tile_list)
            {
                IdToTile[t.tile_id] = t;
            }
            
            Dictionary<int,HashSet<int>> PossibleSidesByID = new Dictionary<int, HashSet<int>>(); 
            Dictionary<Tile,HashSet<int>> TileToPossibleSides = new Dictionary<Tile, HashSet<int>>();
            foreach (Tile t_tile in tile_list)
            {
                int id = t_tile.tile_id;
                HashSet<int> sides = new HashSet<int>(t_tile.GetPossibleSides());
                PossibleSidesByID[id] = sides;
                TileToPossibleSides[t_tile] = sides;
            }

            foreach (Tile t_tile in tile_list)
            {
                HashSet<int> sides_that_match = new HashSet<int>();
                HashSet<Tile> tiles_that_match = new HashSet<Tile>();
                foreach (Tile o_tile in tile_list)
                {   if (t_tile.Equals(o_tile))
                    {
                        continue;
                    }
                    else
                    {
                        HashSet<int> my_possible_sides = new HashSet<int>(t_tile.GetCurrentSides());
                        my_possible_sides.IntersectWith(TileToPossibleSides[o_tile]);
                        if (my_possible_sides.Count > 0)
                        {
                            sides_that_match.UnionWith(my_possible_sides);
                            tiles_that_match.Add(o_tile);
                        }
                    }
                }
                t_tile.adj_tiles = tiles_that_match;
            }

            Dictionary<int,List<Tile>> Tiles_By_Count = new Dictionary<int, List<Tile>>();
            List<Tile> tiles_that_match_too_many = new List<Tile>();
            
            for (int c = 0; c < 5; c++)
            {
                List<Tile> c_list  = new List<Tile>();
                foreach (Tile t_tile in tile_list)
                {
                    if (t_tile.adj_tiles.Count == c)
                    {
                        c_list.Add(t_tile);
                    } else if ((c == 4) && (t_tile.adj_tiles.Count > 4))
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

   
    
  
   
}


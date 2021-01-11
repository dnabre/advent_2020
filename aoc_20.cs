using System;
using System.CodeDom;
using System.Text;
using System.Collections.Generic;
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

//			System.Diagnostics.Process.Start("clear");
			
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

        private static int t_height = 10;
        private static int t_width = 10;
        private static List<Orientation> all_orientations;
        private static  Dictionary<Directions,Directions>  MatchingDirection;
        private static Tile[] tile_array;
        private static List<Tile> tile_list;
        private static Dictionary<int, Tile> TileById;
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
                HashSet<int> sides = n_tile.side_nums;
                HashSet<int> next_to = new HashSet<int>(); //tile ids
                for(int j=0; j < tile_list.Count; j++) {
                    if(i==j) continue;
                    Tile p_tile = tile_list[j];
                    HashSet<int> other_sides = p_tile.side_nums;
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
            MatchingDirection = new Dictionary<Directions, Directions>(4);
            MatchingDirection[Directions.LEFT] = Directions.RIGHT;
            MatchingDirection[Directions.RIGHT] = Directions.LEFT;
            MatchingDirection[Directions.UP] = Directions.DOWN;
            MatchingDirection[Directions.DOWN] = Directions.UP;
            

            
            
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
	
            Dictionary<int,HashSet<Tile>> side_to_tiles = new Dictionary<int, HashSet<Tile>>();
            Dictionary<Tile,List<int>> sides_that_match_for_tile = new Dictionary<Tile, List<int>>();
            HashSet<int> matched_side_numbers = new HashSet<int>();	
			
            HashSet<int>[] adj_tiles = new HashSet<int>[tile_list.Count];
            int[] num_matches = new int[tile_list.Count];
            for(int i=0; i < tile_list.Count;i++) {
                Tile n_tile = tile_list[i];
                sides_that_match_for_tile[n_tile] = new List<int>();
                HashSet<int> sides = n_tile.side_nums;
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
                for(int j=0; j < tile_list.Count; j++) {
                    if(i==j) continue;
                    Tile p_tile = tile_list[j];
                    HashSet<int> other_sides = p_tile.side_nums;
                    bool possible = false;
                    foreach(int my_side in sides) {
                        foreach(int other_s in other_sides) {
                            if(my_side == other_s) {
                                possible = true;
                                matched_side_numbers.Add(my_side);
                                sides_that_match_for_tile[n_tile].Add(my_side);
                                break;
                            }
                        }
                        if(possible==true) break;
                    }
                    if(possible) next_to.Add(p_tile.tile_id);
                }
                adj_tiles[i] = next_to;
                num_matches[i] = next_to.Count;
                //	Console.WriteLine($"\t {n_tile.tile_id} could be adjacent to {next_to.Count} tiles");
            }
            //Console.WriteLine();
            
            Tile UpperLeft=null;
			
            List<Tile> corners = new List<Tile>(4);
            List<Tile> edges = new List<Tile>(4*10);
            Console.WriteLine($"\t side numbers which match count {matched_side_numbers.Count}");
			
            for (int i = 0; i < tile_list.Count; i++)
            {
                if (num_matches[i] == 2)
                {
                    if (tile_list[i].tile_id == 1613)
                        UpperLeft = tile_list[i];
                    corners.Add(tile_list[i]);
                }
            }

	
            all_orientations = new List<Orientation>(16);
            foreach (Tile_Flip f in Enum.GetValues(typeof(Tile_Flip)))
            {
                foreach (Tile_Rotate_Left r in Enum.GetValues(typeof(Tile_Rotate_Left)))
                {
                    all_orientations.Add(new Orientation(f, r));
                }
            }
            char[,] final_grid = new char[10*tile_list.Count, 10*tile_list.Count];
            Tile[,] tile_grid = new Tile[12,12];
            Orientation[,] orient_grid = new Orientation[12,12];
            tile_grid[0, 0] = UpperLeft;

            tile_array = tile_list.ToArray();
            int n = WhichTileNum(UpperLeft, tile_array);
            HashSet<int> my_adj = adj_tiles[n];
            Console.WriteLine($"\n\t Adjacent Tiles: {Utility.HashSetToStringLine(my_adj)}");
            List<int> my_m_sides = sides_that_match_for_tile[UpperLeft];
            Console.WriteLine($"\t {Utility.ListToStringLine(my_m_sides)}");
            Tile a_tile = TileById[3769];
            Tile b_tile = TileById[1361];
            a_tile.Print();
            b_tile.Print();
            
            
            
            

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
		
        private class Tile {
            public int tile_id;
            public char[,] patch;
            public HashSet<int> side_nums;
		
            public Tile_Flip flip = Tile_Flip.None;
            public Tile_Rotate_Left rot = Tile_Rotate_Left.None;
		
            public char[,] GetOnlyPatch(Tile_Flip flip, Tile_Rotate_Left rot) {			
                char[,] n_patch = new char[t_width-1,t_height-1];
                for(int y=1; y <t_height-1; y++) {
                    for( int x=1; x < t_width-1; x++) {
                        n_patch[x-1,y-1] = patch[x,y];
                    }
                }

                return n_patch;
            }

            public char[,] GetOrient(Orientation o)
            {
                return GetOrient(o.flip, o.rot);
            }
				
            public char[,] GetOrient(Tile_Flip flip, Tile_Rotate_Left rot)
            {
                char[,] grid = (char[,]) patch.Clone();
                while (rot != Tile_Rotate_Left.None)
                {
					
                    char[,] new_grid = new char[t_width, t_height];
                    for (int y = 0; y < t_height; y++)
                    {
                        for (int x = 0; x < t_width; x++)
                        {
                            new_grid[  y , t_height -1 -x  ] = grid[x,y];
                        }
                    }

                    grid = new_grid;
                    rot--;
					
                }



				
                if ((flip == Tile_Flip.X_Flip) || (flip == Tile_Flip.XY_Flip))
                {
                    char[,] new_grid = new char[t_width, t_height];
                    for (int y = 0; y < t_height; y++)
                    {
                        for (int x = 0; x < t_width; x++)
                        {
                            new_grid[t_width -1 - x, y] = grid[x, y];
                        }
                    }

                    grid = new_grid;
                }

                if ((flip == Tile_Flip.Y_Flip) || (flip == Tile_Flip.XY_Flip))
                {
                    char[,] new_grid = new char[t_width, t_height];
                    for (int y = 0; y < t_height; y++)
                    {
                        for (int x = 0; x < t_width; x++)
                        {
                            new_grid[x, t_height -1 - y] = grid[x, y];
                        }
                    }

                    grid = new_grid;
                }


                return grid;
            }
 				

            public Tile(int id, char[,] pp) {
                this.tile_id = id;
                this.patch = pp;
				
                List<String> side_strings = GetSides();
                List<int> side_ints = BinaryStringListToInt(side_strings);
                side_nums = new HashSet<int>(side_ints);

            }

            static public void PrintPatch(char[,] pp)
            {
                for(int y=0; y < t_height; y++) {
                    Console.Write($"\t ");
                    for(int x=0; x < t_width; x++) {
                        Console.Write(pp[x,y]);
                    }
                    Console.WriteLine();
                }	
            }
				
            public void Print() {
                String id_tag = "id: ";

                Console.WriteLine($"\t {id_tag.PadRight(5)}{tile_id.ToString().PadLeft(5)}");
                PrintPatch(this.patch);
            }

            public int GetSideNum(char[,] patch, Directions facing)
            {
                StringBuilder sb = new StringBuilder(12);
				
                switch (facing)
                {
                    case Directions.UP:
                        for (int x = 0; x < t_width; x++)
                        {
                            sb.Append(patch[x, 0]);
                        }
                        break;
                    case Directions.DOWN: 
                        for (int x = 0; x < t_width; x++)
                        {
                            sb.Append(patch[x, t_height-1]);
                        }
                        break;
                    case Directions.LEFT:
                        for (int y = 0; y < t_height; y++)
                        {
                            sb.Append(patch[0, y]);
                        }
                        break;
                    case Directions.RIGHT:
                        for (int y = 0; y < t_height; y++)
                        {
                            sb.Append(patch[t_width-1, y]);
                        }
                        break;
                    default:
                        throw new ArgumentException($"{facing} is not valid Directions");
                }

                String s = sb.ToString();
                return BinaryStringToInt(s);
            }
			
			
            public bool GetOrientWhere(Directions facing, int side_id, out Orientation orient)
            {
                foreach (Orientation o in all_orientations)
                {
                    char[,] patch;
                    int n = -1;
                    patch = GetOrient(o);
                    n = GetSideNum(patch, facing);
                    if (n == side_id)
                    {
                        orient = o;
                        return true;

                    }

                }

                orient = new Orientation(Tile_Flip.None, Tile_Rotate_Left.None);
                return false;
            }
			
            public List<String> GetSides() {
				
                StringBuilder top = new StringBuilder(10);
                StringBuilder bottom = new StringBuilder(10);
                StringBuilder left = new StringBuilder(10);
                StringBuilder right = new StringBuilder(10);

                for(int x = 0; x < t_width; x++) {
                    top.Append(patch[x,0]);
                    bottom.Append(patch[x,9]);
                    left.Append(patch[0,x]);
                    right.Append(patch[9,x]);
                }
                List<String> result = new List<String>(4);
                char[] rev;
                String ss;

                result.Add(top.ToString());
                result.Add(bottom.ToString());
                result.Add(left.ToString());
                result.Add(right.ToString());
                List<String> r_strs = new List<String>();
                foreach(String st in result) {
                    rev = st.ToCharArray();
                    Array.Reverse(rev);
                    ss = new String(rev);
                    r_strs.Add(ss);
                }
                result.AddRange(r_strs);

                List<String> bi_result = new List<String>();
                foreach(String s in result) {
                    bi_result.Add(HashDotToBinary(s));
                }
                return bi_result;
            }

            public List<int> GetSideNums()
            {
                List<String> s_sides = this.GetSides();
                return BinaryStringListToInt(s_sides);
            }
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


        private static String HashDotToBinary(String s) {
            char[] from = s.ToCharArray();
            char[] c_to = new char[from.Length];
            for(int i=0; i < from.Length; i++ ) {
                char ch = from[i];
                if(ch=='#') {
                    c_to[i] = '1';
                } else {
                    c_to[i] = '0';
                }
            }
            return new String(c_to);
        }

        private static int BinaryStringToInt(String s)
        {
            int n = Convert.ToInt32(s, 2);
            return n;
        }
        private static List<int> BinaryStringListToInt(List<String> sides) {
            List<int> result  = new List<int>(sides.Count);
            foreach (String s in sides)
            {
                int n = Convert.ToInt32(s, 2);
                result.Add(n);
            }

            return result;
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


using System;
using System.CodeDom;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;

/*
	Solutions found:
	Part 1: 7492183537913
	Part 2: 
	
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
            Part1();
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
    //        Part2();
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }

		private static int t_height = 10;
		private static int t_width = 10;


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

			int count_3 = 0;
			for (int i = 0; i < tile_list.Count; i++)
			{
				if (num_matches[i] == 3)
				{
					Console.WriteLine($"\t Tile {tile_list[i].tile_id} is only adjacent to 3 other tiles");
					count_3++;
				}
			}

			Console.WriteLine($"\n\t Found {count_3} border tiles out of 40");
            
            Console.WriteLine($"\n\tPart 1 Solution: {result_product}");
			// 1760573689 is too low
			// 7492183537913
        }
        
        private static void Part2()
        {
            
            Console.WriteLine("   Part 2");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
           
           
            Console.WriteLine($"\n\tPart 2 Solution: {0}");
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


				public Tile(int id, char[,] pp) {
					this.tile_id = id;
					this.patch = pp;
				
					List<String> side_strings = GetSides();
						List<int> side_ints = BinaryStringListToInt(side_strings);
					side_nums = new HashSet<int>(side_ints);

				}
		
			public void Print() {
				String id_tag = "id: ";

				Console.WriteLine($"\t {id_tag.PadRight(5)}{tile_id.ToString().PadLeft(5)}");
				for(int y=0; y < t_height; y++) {
					Console.Write($"\t ");
					for(int x=0; x < t_width; x++) {
						Console.Write(patch[x,y]);
					}
					Console.WriteLine();
				}
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
    
}

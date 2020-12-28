using System;
using System.CodeDom;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;

/*
	Solutions found:
	Part 1: 
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
			System.Diagnostics.Process.Start("clear");
			
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
            string[] lines = System.IO.File.ReadAllLines(TestInput1);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

			List<Tile> tile_list = ParseTiles(lines);

			Tile first = tile_list[0];

			Console.WriteLine();
			first.Print();
			Console.WriteLine();

			List<String> sides = first.GetSides();
			foreach(String s in sides) {
				Console.WriteLine($"\t {s}");
			}


			Console.WriteLine();
			Console.WriteLine($"\tRead {tile_list.Count} tiles");




  
            
            Console.WriteLine($"\n\tPart 1 Solution: {0}");
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
				current_tile = new Tile();
				String ln = lines[i];
				if(ln.StartsWith("Tile")) {
					char[] id_nums = new char[4];
					id_nums[0] = ln[5];
					id_nums[1] = ln[6];
					id_nums[2] = ln[7];
					id_nums[3] = ln[8];
					current_tile.tile_id = int.Parse(id_nums);
					last_parsed_id = current_tile.tile_id;
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
					current_tile.patch = patch;
					current_tile.tile_id = last_parsed_id;
					//current_tile.Print();
					//Console.WriteLine();
				
					tile_list.Add(current_tile);
					current_tile = new Tile();
				}
			}
			return tile_list;
		}


		        private class Tile {
			public int tile_id;
			public char[,] patch;
		
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
				//top,bottom,left,right;
				StringBuilder top = new StringBuilder(10);
				StringBuilder bottom = new StringBuilder(10);
				StringBuilder left = new StringBuilder(10);
				StringBuilder right = new StringBuilder(10);

				for(int x = 0; x < t_width; x++) {
					top.Append(patch[x,0]);;
					bottom.Append(patch[x,9]);
					left.Append(patch[0,x]);
					right.Append(patch[9,x]);
				}
				List<String> result = new List<String>(4);
				result.Add(top.ToString());
				result.Add(bottom.ToString());
				result.Add(left.ToString());
				result.Add(right.ToString());
				List<String> bi_result = new List<String>(4);
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
			List<int> result  = new List<int>(sides.Length);
			


		}
    }
    
}

using System;
using System.Text;
using System.Collections.Generic;


/*
	Solutions found:
	Part 1:
	Part 2: 
	
*/

namespace advent_2020
{
	static class AOC_11 {
		private const string Part1Input = "aoc_11_input_1.txt";
		private const string Part2Input = "aoc_11_input_2.txt";
		private const string TestInput1 = "aoc_11_test_1.txt";
		private const string TestInput2 = "aoc_11_test_2.txt";

		public static void Run (string[] args) {
			Console.WriteLine ("AoC Problem 11");
			Part1(args);
			Console.Write("\n");
			Part2(args);
		}

	private static void Part1(string[] args) {
			Console.WriteLine("   Part 1");
			string[] lines =  System.IO.File.ReadAllLines(TestInput1);
			Console.WriteLine("\tRead {0} inputs", lines.Length);
			int width = lines[0].Length;
			int height = lines.Length;
			Console.WriteLine($"\t {(width,height)}");


			
			char[,] map = new char[width,height];
			for(int y=0; y < height; y++) {
				for(int x=0; x < width; x++) {
					map[x,y] = lines[y][x];
				}
			}
			char[,] map2 = MapDub(map);
			map2[3,4] = '@';
			Utility.PrintMap(map);
			Utility.PrintMap(map2);
			
			Console.WriteLine($"\n\tPart 1 Solution: {0}");	
		}

	private static void Part2(string[] args) {
			Console.WriteLine("   Part 2:");
			string[] lines =  System.IO.File.ReadAllLines(Part2Input);
			Console.WriteLine("\tRead {0} inputs", lines.Length);
			


		
			Console.WriteLine($"\n\tPart 2 Solution: {0}");
	}

	private static char[,] MapDub(char[,] map) {
		return (char[,]) map.Clone();
	}

	private static char  UpdateCell(char[,] map,int  x, int y) {
			char new_cell;
			char cell = map[x,y];
			if(cell == '.') {
				return cell;
			}

			if(cell == 'L') {


			} else if (cell == '#') {

			} else {
				throw new ArgumentException($"Cell is {cell} when expected '.', 'L', '#' ");
			}



			return ' ';
	}
	}
	

}
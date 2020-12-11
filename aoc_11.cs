using System;
using System.Text;
using System.Collections.Generic;


/*
	Solutions found:
	Part 1: 191
	Part 2: 1478615040
	
*/

namespace advent_2020
{
	static class AOC_03 {
		private const string Part1Input = "aoc_03_input_1.txt";
		private const string Part2Input = "aoc_03_input_2.txt";
		private const string TestInput = "aoc_03_test_1.txt";

		public static void Run (string[] args) {
			Console.WriteLine ("AoC Problem 03");
			Part1(args);
			Console.Write("\n");
			Part2(args);
		}

	private static void Part1(string[] args) {
			Console.WriteLine("   Part 1");
			(int x,int y) step =(3,1);

			string[] lines =  System.IO.File.ReadAllLines(Part1Input);
			Console.WriteLine("\tRead {0} inputs", lines.Length);
			char[,] panel = ParsePanel(lines);
			int tree_count = CheckTreeCount(panel, step);
			Console.WriteLine($"\n\tPart 1 Solution: {tree_count}");
			
		}

	private static void Part2(string[] args) {
			Console.WriteLine("   Part 2:");
			string[] lines =  System.IO.File.ReadAllLines(Part2Input);
			Console.WriteLine("\tRead {0} inputs", lines.Length);
			char[,] panel = ParsePanel(lines);
			(int x, int y)[] steps = {(1,1), (3,1), (5,1),(7,1),(1,2)};

			int tree_product = 1;
			foreach((int x,int y) s in steps) {
				int tree_count = CheckTreeCount(panel,s);
				tree_product = tree_product * tree_count;
				Console.WriteLine($"\tStep {s} has tree count={tree_count}");
			}
		
			Console.WriteLine($"\n\tPart 2 Solution: {tree_product}");
	}	
	
	private static String PanelToString(char[,] panel) {
			var sb = new StringBuilder();
			for(int y=0; y < panel.GetLength(1); y++) {
				sb.Append('\t');
				for(int x=0; x < panel.GetLength(0) ; x++) {
					sb.Append(panel[x,y]);
				}
				sb.Append('\n');
			}
			return sb.ToString();

		}
	private static int CheckTreeCount(char[,] panel, (int x,int y) step) {
			(int x, int y) loc = (0,0);
			int tree_count = 0;
			int height = panel.GetLength(1);
			int width = panel.GetLength(0);
			while(loc.y < height) {
				if(panel[loc.x,loc.y] == '#') {
					tree_count++;
				}
				loc = ((loc.x + step.x) % width, loc.y + step.y);
			}
			return tree_count;
	}

	private static char[,] ParsePanel(String[] lines) {
			int width = lines[0].Length;
			int height = lines.Length;
			char[,] panel = new char[width,height];
			for(int y=0; y < height; y++) {
				char[] c_array = lines[y].ToCharArray();
				for(int x=0; x <width; x++) {
					panel[x,y] = c_array[x];
				}
			}
			return panel;
		}
	}
}
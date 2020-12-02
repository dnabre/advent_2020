using System;
using System.Collections.Generic;

/*
	Solutions found:
	Part 1: 888331
	Part 2: 130933530

*/

namespace advent_2020
{
	static class AOC_01 {
		private const string Part1Input = "aoc_01_input_1.txt";
		private const string Part2Input = "aoc_01_input_2.txt";
		private const int TargetSum = 2020;

		public static void Run (string[] args) {
			Console.WriteLine ("AoC Problem 01");
			Part1(args);
			Console.Write("\n");
			Part2(args);
		}

		private static void Part1(string[] args) {
			Console.WriteLine("   Part 1");
			string[] lines = System.IO.File.ReadAllLines(Part1Input);
			Console.Write("\t Read {0} values\n", lines.Length);
			HashSet<int> val = new HashSet<int>();

			foreach (string s in lines) {
				int i = int.Parse(s);
				int needValue = TargetSum - i;
				if(val.Contains(needValue)){
					
					Console.Write("\t {0} + {1} = {2} \n", i, needValue, TargetSum);
					Console.Write("\t {0} * {1} = {2} \n", i, needValue, i * needValue);
					return;
				}
				val.Add(i);
			}
			Console.Write("\t Read {0} values. More target values found.\n", val.Count);
		}


		private static void Part2(string[] args) {
			Console.WriteLine("   Part 2:");
			string[] lines = System.IO.File.ReadAllLines(Part2Input);
			Console.Write("\t Read {0} values\n", lines.Length);
			int[] values = new int[lines.Length];
			HashSet<int> val = new HashSet<int>();
			int i =0;
			foreach(string s in lines) {
				int x = int.Parse(s);
				values[i] = x;
				val.Add(x);
				i++;
			}
	
			for(int x=0; x < values.Length; x++){
				for(int y=0; y < values.Length; y++){
					if(x==y) continue;
					int needValue = TargetSum - values[x] - values[y];
					if(val.Contains(needValue)){
						Console.Write("\t {0} + {1} + {2} = {3} \n", values[x], values[y], needValue, TargetSum);
						Console.Write("\t {0} * {1} * {2} = {3} \n", values[x], values[y], needValue, values[x] * values [y] * needValue);
						return;
					}
				}
			}
		}
	}
}
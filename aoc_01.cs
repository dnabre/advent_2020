using System;
using System.Collections.Generic;

/*
	Solutions found:
	Part 1: 888331
	Part 2: 130933530

*/

namespace advent_2020
{
	class AOC_01 {
		public static string part_1_input = "aoc_01_input_1.txt";
		public static string part_2_input = "aoc_01_input_2.txt";


		public static int target_sum = 2020;
		public static void run (string[] args) {
			Console.WriteLine ("AoC Problem 01");
			part1(args);
			Console.Write("\n\n\n");
			part2(args);
		}



		public static void part1(string[] args) {
			Console.WriteLine("   Part 1");
			string[] lines = System.IO.File.ReadAllLines(part_1_input);
			Console.Write("\t Read {0} values\n", lines.Length);
			HashSet<int> val = new HashSet<int>();

			foreach (string s in lines) {
				int i = Int32.Parse(s);
				int need_value = target_sum - i;
				if(val.Contains(need_value)){
					Console.WriteLine();
					Console.Write("\t {0} + {1} = {2} \n", i, need_value, target_sum);
					Console.Write("\t {0} * {1} = {2} \n", i, need_value, i * need_value);
					return;
				}
				val.Add(i);
			}
			Console.Write("\t Read {0} values. More target values found.\n", val.Count);
		}


		public static void part2(string[] args) {
			Console.WriteLine("   Part 2:");
			string[] lines = System.IO.File.ReadAllLines(part_2_input);
			Console.Write("\t Read {0} values\n", lines.Length);
			int[] values = new int[lines.Length];
			HashSet<int> val = new HashSet<int>();
			int i =0;
			foreach(string s in lines) {
				int x = Int32.Parse(s);
				values[i] = x;
				val.Add(x);
				i++;
			}
	
			for(int x=0; x < values.Length; x++){
				for(int y=0; y < values.Length; y++){
					if(x==y) continue;
					int need_value = target_sum - values[x] - values[y];
					if(val.Contains(need_value)){
						Console.WriteLine();
						Console.Write("\t {0} + {1} + {2} = {3} \n", values[x], values[y], need_value, target_sum);
						Console.Write("\t {0} * {1} * {2} = {3} \n", values[x], values[y], need_value, values[x] * values [y] * need_value);
						return;
					}
				}
			}
		}
	}
}
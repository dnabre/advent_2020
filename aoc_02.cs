using System;
using System.Text;
using System.Collections.Generic;

/*
	Solutions found:
	Part 1: 465
	Part 2: 294
	
*/

namespace advent_2020
{
	static class AOC_02 {
		private const string Part1Input = "aoc_02_input_1.txt";
		private const string Part2Input = "aoc_02_input_2.txt";
		private const string TestInput = "aoc_02_test_1.txt";

		public static void Run (string[] args) {
			Console.WriteLine ("AoC Problem 02");
			Part1(args);
			Console.Write("\n");
			Part2(args);
		}



	private static void Part1(string[] args) {
			Console.WriteLine("   Part 1");
			string[] lines =  System.IO.File.ReadAllLines(Part1Input);
			Console.WriteLine("\tRead {0} inputs", lines.Length);
			/*
			foreach(String ln in lines) {
				Password p = ParseLine(ln);
				Console.Write($"\t{p}");
				Console.Write("\t\t");
				bool good = IsValidPart1(p);
				Console.WriteLine(good);
			}
			*/
			int GoodCount = 0;
			int BadCount  = 0;
			foreach(String line in lines) {
				Password p = ParseLine(line);
				bool good = IsValidPart1(p);
				if(good) GoodCount++;
				else BadCount++;
			}
			if((GoodCount+BadCount) != lines.Length){
				Console.WriteLine($"\tError: counts don't add up");	
			}
			Console.WriteLine($"\tTested: {lines.Length}, Good: {GoodCount}, Bad: {BadCount}");
			Console.WriteLine($"\n\tPart 1 Solution: {GoodCount}");
		}
	private static void Part2(string[] args) {
			Console.WriteLine("   Part 2:");
			string[] lines =  System.IO.File.ReadAllLines(Part2Input);
			Console.WriteLine("\tRead {0} inputs", lines.Length);
			
			int GoodCount = 0;
			int BadCount  = 0;
			foreach(String line in lines) {
				Password p = ParseLine(line);
				bool good = IsValidPart2(p);
				if(good) GoodCount++;
				else BadCount++;
			}
			if((GoodCount+BadCount) != lines.Length){
				Console.WriteLine($"\tError: counts don't add up");	
			}
			Console.WriteLine($"\tTested: {lines.Length}, Good: {GoodCount}, Bad: {BadCount}");
			Console.WriteLine($"\n\tPart 2 Solution: {GoodCount}");
		}	


		private static bool IsValidPart1(Password pass) {
			int count = CountLetter(pass.word, pass.letter);
			if(count < pass.start_range) {
				return false;
			}
			if(count > pass.end_range) {
				return false;
			}
			return true;
		}

		private static Password ParseLine(String input) {
			Password p = new Password();
			String[] sp = input.Split(' ');
			
			
			p.letter = sp[1][0];
			p.word = sp[2];
			String[] range = sp[0].Split('-');
			
			p.start_range = int.Parse(range[0]);
			p.end_range = int.Parse(range[1]);
			return p;
		}

		 

		

	




		private static int CountLetter(string s, char c){
			int count = 0;
			foreach(char ch in s) {
				if(ch == c) count++;
			}
			return count;
		}


		private static bool IsValidPart2(Password pass) {
			int match = 0;
			if(pass.word[pass.start_range-1] == pass.letter)
			{
				match++;
			} 
			if(pass.word[pass.end_range-1] == pass.letter) {
				match++;
			}
			return (match == 1);
		}

	}
	struct Password {
			public int start_range;
			public int end_range;
			public char letter;
			public String word;
			public override String ToString() {
				var sb = new StringBuilder();
				sb.Append($"{start_range}-{end_range} {letter}:");
				sb.Append(" ");
				sb.Append(word);
				return sb.ToString();
			}
		}
}
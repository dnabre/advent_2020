using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;


/*
	Solutions found:
	Part 1: 7128
	Part 2: 3640
	
*/

namespace advent_2020
{
    static class AOC_06
    {
        private const string Part1Input = "aoc_06_input_1.txt";
        private const string Part2Input = "aoc_06_input_2.txt";
        private const string TestInput1 = "aoc_06_test_1.txt";
        private const string TestInput2 = "aoc_06_test_2.txt";
        
        
        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 06");
            Part1(args);
            Console.Write("\n");
            Part2(args);
        }



        private static void Part1(string[] args)
        {
            Console.WriteLine("   Part 1");
            String[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            List<HashSet<char>> group_list = new List<HashSet<char>>();

			HashSet<char> group = new HashSet<char>();

			foreach(String ln in lines) {
				if(ln.Equals("")) {
					group_list.Add(group);
					group = new HashSet<char>();

				} else {
					foreach(char ch in ln) {
						group.Add(ch);
					}
				}



			}
			if(group.Count > 0) {
				group_list.Add(group);
				group = new HashSet<char>();
			}
			int group_sum = 0;
			foreach(HashSet<char> h in group_list) {
				//DisplaySet(h); Console.WriteLine($"\t{h.Count}");
				group_sum += h.Count;
			}




            Console.WriteLine($"\n\tPart 1 Solution: {group_sum}");
        }

        
        
        private static void Part2(string[] args)
        {
            Console.WriteLine("   Part 2");
            String[] lines = System.IO.File.ReadAllLines(Part2Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

			String[] new_lines = new String[lines.Length+1];
			for(int i=0; i < lines.Length; i++) {
				new_lines[i] = lines[i];
			}
			new_lines[lines.Length] = "";
			lines=new_lines; new_lines = null;

			List<int> all_count = new List<int>();
			int group_sum = 0;
			List<SortedSet<char>> group = new List<SortedSet<char>>();

			

			for(int i=0; i < lines.Length; i++) {
				if(lines[i].Equals("")) {
					//end of group	
					SortedSet<char> all_answers = new SortedSet<char>();
					
					foreach(SortedSet<char> person_answers in group) {
						all_answers.UnionWith(person_answers);
					}
					
					foreach(SortedSet<char> person_answers in group) {
						all_answers.IntersectWith(person_answers);
					}
					all_count.Add(all_answers.Count);
					
					group = new List<SortedSet<char>>();
				} else {
					SortedSet <char> person = new SortedSet<char>();
					foreach(char ch in lines[i]) {
						person.Add(ch);
					}
					
					if(person.Count == 0 ) {
						Console.WriteLine($"\t person without any answers in line {i}: {lines[i]}");
					}
					group.Add(person);
				}
			}
			Console.WriteLine($"\tProcessed {all_count.Count} groups");
			foreach(int g in all_count){
			//	Console.WriteLine($"\t group count = {g}");
				group_sum += g;
			}
 
 


            Console.WriteLine($"\n\tPart 2 Solution: {group_sum}");
        }
		static private void DisplaySet<T>(SortedSet<T> collection)
		{
			Console.Write("\t{");
			foreach (T i in collection)
			{
				Console.Write(" {0}", i);
			}
			Console.Write(" }");
		}
	}
}
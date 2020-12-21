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

     static class AOC_21
    {
        private const string Part1Input = "aoc_21_input_1.txt";
        private const string Part2Input = "aoc_21_input_2.txt";
        private const string TestInput1 = "aoc_21_test_1.txt";
        private const string TestInput2 = "aoc_21_test_2.txt";



        public static void Run(string[] args)
        {
			System.Diagnostics.Process.Start("clear").Start();
            Console.WriteLine("AoC Problem 21");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Part1();
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
     //       Part2();
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }
	

	private static HashSet<String>[,] ParseData(String[] lines) {
			HashSet<String>[,] data = new HashSet<String>[lines.Length,2];
			
			
			for(int i=0; i < lines.Length; i++) {
				
				HashSet<String> als;
				HashSet<String> ing;
				
				als  = ParseLine(lines[i], out ing);
			//	Console.WriteLine($"\t {Utility.HashSetToStringLine(ing)} \t all: {Utility.HashSetToStringLine(als)}");
				data[i,0] = ing;
				data[i,1] = als;
			}
			return data;
	}

	private static HashSet<String> ParseLine(String line, out HashSet<String> ing){
		
			HashSet<String> alls = new HashSet<String>();		
			ing = new HashSet<String>();
			String[] parts = line.Split(' ');

		
			bool second = false;
			foreach(String p in parts) {
				if(p.Equals("(contains")) {
					second=true;
					continue;
				}
				
				if(second) {
					alls.Add(p);
				} else {
					ing.Add(p);
				}
			}
			HashSet<String> clean = new HashSet<String>();
			foreach(String a in alls ) {
				String b = a;
				b.Trim();
				b = b.Replace(")", "");
				b = b.Replace(",","");
				clean.Add(b);
			}

			return clean;
	}

	private static int line_number;
	private static int ingred_number;
	private static int all_number;
	private static int unmatched_alls;
	private static HashSet<String> all_ingreds;
	private static HashSet<String> all_all;
	private static Dictionary<String,HashSet<String>> poss_alls_by_ingred;
	private static 	Dictionary<String,HashSet<String>> poss_ingreds_by_all;
	private static Dictionary<String,String> final_ing_to_al;
	private static HashSet<String>[,] data;
	


	private static void Part1()
        {
            Console.WriteLine("   Part 1");
            string[] lines = System.IO.File.ReadAllLines(TestInput1);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
			
			line_number = lines.Length;
			data = ParseData(lines);
			all_ingreds = new HashSet<String>();
			all_all = new HashSet<String>();

			

			Console.WriteLine($"\tData:");
			for(int i=0; i < line_number; i++) {
				String a,b;
				a = Utility.HashSetToStringLine(data[i,0]);
				b = Utility.HashSetToStringLine(data[i,1]);
				Console.WriteLine($"\t\t ingred: {a} alls {b}");

				all_ingreds.UnionWith(data[i,0]);
				all_all.UnionWith(data[i,1]);
			}

			ingred_number = all_ingreds.Count;
			all_number = all_all.Count;
			unmatched_alls = all_number;

			Console.WriteLine($"\n\n\t ingreds: {ingred_number} alls {all_number}");
			Console.WriteLine($"\t ingreds: {Utility.HashSetToStringLine(all_ingreds)}");
			Console.WriteLine($"\t all    : {Utility.HashSetToStringLine(all_all)}");

			poss_alls_by_ingred = new Dictionary<String,HashSet<String>>();
			poss_ingreds_by_all = new Dictionary<String,HashSet<String>>();
			foreach(String ing in all_ingreds) {
				poss_alls_by_ingred[ing] = new HashSet<String>();
			}
			foreach(String all in all_all) {
				poss_ingreds_by_all[all] = new HashSet<String>();
			}
			for(int i=0; i < line_number; i++) {
				var ing_set = data[i,0];
				var all_set = data[i,1];
				foreach(String ing in ing_set) {
					foreach(String al in all_set) {
						poss_alls_by_ingred[ing].Add(al);
						poss_ingreds_by_all[al].Add(ing);
					}
				}
			}

			Console.WriteLine();

			final_ing_to_al = new Dictionary<String,String>();
			
			String[] a_ingred = Utility.HashSetToArray(all_ingreds);

			int loop_count = 3;
		//	while(unmatched_alls > 0) {
			while(loop_count > 0) {
				Console.WriteLine("\t#####################################");
				loop_count--;
				String ingred_match="null";
				String all_match="null";
				a_ingred = Utility.HashSetToArray(all_ingreds);
				bool match_found = false;
				foreach(String ing in a_ingred) {
					HashSet<String> alls_for = poss_alls_by_ingred[ing];
					int n;
					n = alls_for.Count;
					Console.WriteLine($"\t {ing} -> [{n}]");
					
					if((n==1)&&!match_found) {
						ingred_match = ing;
						foreach(String aa in all_all){
								//all_match = alls_for.ToArray()[0];
								if(alls_for.Contains(aa)){
									all_match = aa;
									match_found = true;
									break;
								}
						}
					

				
					}
					
				}
				if(match_found)				UpdateMatch(ingred_match, all_match);
			}



            Console.WriteLine($"\n\tPart 1 Solution: {0}");
        }

		private static void UpdateMatch(String ingred, String alg) {
				Console.WriteLine($"\t matching {ingred} to {alg} ");
				final_ing_to_al[ingred] = alg;
				unmatched_alls--;
				all_all.Remove(alg);
				all_ingreds.Remove(ingred);
				poss_alls_by_ingred[ingred].Remove(alg);
				poss_ingreds_by_all[alg].Remove(ingred);

		}
        
	private static void Part2()
        {
            
            Console.WriteLine("   Part 2");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
     
     	    Console.WriteLine($"\n\tPart 2 Solution: {0}");
            
        }
    }
}

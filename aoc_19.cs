using System;
using System.CodeDom;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;


/*
	Solutions found:
	Part 1: 285
	Part 2: 
	
*/

namespace advent_2020
{
    static class AOC_19
    {
        private const string Part1Input = "aoc_19_input_1.txt";
        private const string Part2Input = "aoc_19_input_2.txt";
        private const string TestInput1 = "aoc_19_test_1.txt";
        private const string TestInput2 = "aoc_19_test_2.txt";


        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 19");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Part1();
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
    //     Part2();
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }



        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

			List<String> rule_strings = new List<String>();
			List<String> message_strings = new List<String>();


			int line_number = 0;
			while(!lines[line_number].Equals("")) {
				rule_strings.Add(lines[line_number]);
				line_number++;
			}	
			Console.WriteLine($"\tRead {rule_strings.Count} rules");

			//Console.WriteLine($"\t {line_number.ToString().PadLeft(4)}: \t |{lines[line_number]}|");
			
			line_number++;
			while(line_number < lines.Length) {
				message_strings.Add(lines[line_number]);
				line_number++;
			}
			Console.WriteLine($"\tRead {message_strings.Count} messages");
			rule_array = new String[rule_strings.Count];
			String[] message_array = message_strings.ToArray();

			String[] p_process = rule_strings.ToArray();
			for(int i=0; i < p_process.Length; i++) {
				String[] parts = p_process[i].Split(':');
				int index = int.Parse(parts[0]);
				rule_array[index] = parts[1].Trim();
			}
			//valid_messages = new HashSet<String>();
			
			Console.WriteLine();
			for(int j=0; j < Math.Min(7,rule_array.Length); j++ ) {
				Console.WriteLine($"\t {j.ToString().PadLeft(4)}: #{rule_array[j]}#");
			}

			//Console.WriteLine($"#{rule_array[0]}#");
			
			
			valid_messages = GenMessages(rule_array[0], 0);
			//Console.WriteLine(Utility.HashSetToStringLine(valid_messages));
			Console.WriteLine($"\tGenerated {valid_messages.Count} valid messages");
			
			string[] poss = System.IO.File.ReadAllLines("poss.txt");
			Console.WriteLine($"\t Testing againt poss: {poss.Length}");
			HashSet<int> poss_length = new HashSet<int>();
			HashSet<String> pos_set = new HashSet<string>(poss);
			foreach (String p in poss)
			{
				if (!valid_messages.Contains(p))
				{
					poss_length.Add(p.Length);
				}
			}
			
			Console.WriteLine($"poss lenghts {Utility.HashSetToStringLine(poss_length)}");
			
			poss_length = new HashSet<int>();
			foreach (String v in valid_messages)
			{
				poss_length.Add(v.Length);
			}
			Console.WriteLine($"valid lenghts {Utility.HashSetToStringLine(poss_length)}");

			int ours = 0;
			foreach (String v in valid_messages)
			{
				if (pos_set.Contains(v)) ours++;
			}
			Console.WriteLine($"Our valid messages in poss {ours}");
			int mx = 0;
			int v_pos = 0;
			int valid_count = 0;
			foreach (String m in message_strings)
			{
				if (valid_messages.Contains(m))
				{
					valid_count++;
				}

				if (pos_set.Contains(m))
				{
					v_pos++;
				}


				if (valid_messages.Contains(m) && pos_set.Contains(m) && (mx < 5))
				{
					Console.WriteLine($"\t\t\t {m}");
					mx++;
				}
			}

			

            Console.WriteLine($"\n\tPart 1 Solution: {valid_count} {v_pos}");
        }
        
        private static void Part2()
        {
            
            Console.WriteLine("   Part 2");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
           
           
            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }

		private static String[] rule_array;
		private static HashSet<String> valid_messages;

		private static HashSet<String> SpecialCase(int i_rule)
		{
			//  0: 4 1 5
			//  8: 42
			// 44: 5 | 92
			if (i_rule == 8)
			{
				return GenMessages(rule_array[42], 42);
			}

			if (i_rule == 44)
			{
				HashSet<String> one, two;
				one = GenMessages(rule_array[5], 5);
				two = GenMessages(rule_array[92], 92);
				one.UnionWith(two);
				return one;
			}

			throw new ArgumentException($"Handing special rule, {i_rule} is not special");
			
		}
		private static HashSet<String> GenMessages(String s_rule, int i_rule)
		{
			if (IsSpecialRule(i_rule))
			{
				return SpecialCase(i_rule);
			}
			String rule;
			if (i_rule >= 0)
			{
				rule = rule_array[i_rule];
			}
			else
			{
				rule = s_rule;
			}

			HashSet<String> result = new HashSet<String>();
			if(rule.Contains("a") || rule.Contains("b") ){
					if(rule.Equals("\"a\"" )) {
						result.Add("a");
					} else if (rule.Equals("\"b\"" )) {
						result.Add("b");
					}
					return result;
			}

			if (rule.Contains("|"))
			{
				String[] parts = rule.Split('|');
				parts[0] = parts[0].Trim();
				parts[1] = parts[1].Trim();
				HashSet<String> one = GenMessages(parts[0], -1);
				HashSet<String> two = GenMessages(parts[1], -1);
				result.UnionWith(one);
				result.UnionWith(two);
				return result;
			}
			// Rule form is only "# #" now
			HashSet<String> first;
			HashSet<String> second;
			rule.Trim();
			String[] t_num = rule.Split(' ');
			int i_first, i_second;
			t_num[0].Trim();
			t_num[1].Trim();
			i_first = int.Parse(t_num[0]);
			i_second = int.Parse(t_num[1]);
			second = GenMessages(rule_array[i_first], i_first);
			first = GenMessages(rule_array[i_second], i_second);
			foreach (String f in first)
			{
				foreach (String s in second)
				{
					result.Add(f + s);
				}
			}

			return result;
		}

		private static bool IsSpecialRule(int i)
		{
			
			//If rule form is exception
			if (i == 8) return true;
			if (i == 44) return true;
			return false;
		}
		
		
    }
    
    
    
}

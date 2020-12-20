using System;
using System.CodeDom;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;


/*
	Solutions found:
	Part 1: 
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
            string[] lines = System.IO.File.ReadAllLines(TestInput1);
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



			valid_messages = GenMessages(4);
			Console.WriteLine(Utility.HashSetToStringLine(valid_messages));
			valid_messages = GenMessages(5);
			Console.WriteLine(Utility.HashSetToStringLine(valid_messages));
			

            Console.WriteLine($"\n\tPart 1 Solution: {0}");
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

		private static HashSet<String> GenMessages(int i_rule) {
			String rule = rule_array[i_rule];
			HashSet<String> result = new HashSet<String>();
			if(rule.Contains('a') || rule.Contains('b') ){
					if(rule.Equals("\"a\"" )) {
						result.Add("a");
					} else if (rule.Equals("\"b\"" )) {
						result.Add("b");
					}
					return result;
			}

			if(rule.Contains('|')) {

				//Two possible messages



			} else {

				//One possible message


			}


			return result;
		}
        
    }
    
}

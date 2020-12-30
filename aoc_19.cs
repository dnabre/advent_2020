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
            //      Part1();
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
            Part2();
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
           

            String[] p_process = rule_strings.ToArray();
            for(int i=0; i < p_process.Length; i++) {
                String[] parts = p_process[i].Split(':');
                int index = int.Parse(parts[0]);
                rule_array[index] = parts[1].Trim();
            }
		
			
            Console.WriteLine();
			
			
            valid_messages = GenMessages(rule_array[0], 0);
		
            Console.WriteLine($"\tGenerated {valid_messages.Count} valid messages");
			

		
            int valid_count = 0;
            foreach (String m in message_strings)
            {
                if (valid_messages.Contains(m))
                {
                    valid_count++;
                }

            }

			

            Console.WriteLine($"\n\tPart 1 Solution: {valid_count}");
        }
        
  

        
        private static void Part2()
        {
            Console.WriteLine("   Part 2");
            string[] lines = System.IO.File.ReadAllLines(Part2Input);
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


		

            String start_rule = rule_strings[0];

            Console.WriteLine($"\t Starting Rule:\n\t\t\t {start_rule}");
            rule_strings.Remove(start_rule);

            int term_a = -1;
            int term_b = -1;
            String term_a_s = "";
            String term_b_s = "";
            foreach (String r in rule_strings)
            {
                String[] parts = r.Split(':');
                if (r.Contains("a"))
                {
                    term_a_s = r;
                    term_a = int.Parse(parts[0]);
                }

                if (r.Contains("b"))
                {
                    term_b_s = r;
                    term_b = int.Parse(parts[0]);
                }
            }

            rule_strings.Remove(term_a_s);
            rule_strings.Remove(term_b_s);
			
            Console.WriteLine($"\t Terminals:");
            Console.WriteLine($"\t\t\t {term_a} :: {term_a_s}");
            Console.WriteLine($"\t\t\t {term_b} :: {term_b_s}");

            
           

            RuleLookup = new Dictionary<int, string>(rule_strings.Count);
            foreach (String r in rule_strings)
            {
                String[] Parts = r.Split(':');
                int r_num = int.Parse(Parts[0]);
                String rule_string = Parts[1];

                RuleLookup[r_num] = rule_string;
                //    Console.WriteLine($"\t Storing Rule#: {r_num} \t {rule_string}");
            }

            foreach (String m_input in message_strings)
            {

                Console.WriteLine($"\n\t {message_strings[0]}");
                bool match = CYK(rule_strings, start_rule, term_a, term_b, message_strings[0]);


                Console.WriteLine();


                Console.WriteLine($"\n\t {message_strings[0]} \t match: {match} ");
                System.Environment.Exit(0);
            }

            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }


        private static Dictionary<int, String> RuleLookup;

        private static bool CYK(List<String> rule_strings, String start_rule, int a_term, int b_term,
            String test_string)
        {

            int n = test_string.Length;
            int number_rules = rule_strings.Count;
            Console.WriteLine($"\t Checking string {test_string} of length {$"n"}");
            Console.WriteLine($"\t number of rules: {number_rules}");

            LinkedList<int> terms = new LinkedList<int>();
            foreach (char c in test_string)
            {
                if (c == 'a') terms.AddLast(a_term);
                else if (c == 'b') terms.AddLast(b_term);
                else
                {
                    Console.WriteLine($"\t parse failed, expect 'a' or 'b', saw '{c}' in {test_string}");
                    return false;
                }
            }

            int previous_terms;
            while (terms.Count > 1)
            {
                previous_terms = terms.Count;
                LinkedList<int> new_list = new LinkedList<int>();
                Console.WriteLine($"\t Processing Terms: {Utility.LinkdedListToStringLine(terms)}");
                
                
                while (terms.Count > 0)
                {
                    int left = terms.First.Value;
                    terms.RemoveFirst();
                    int right = terms.First.Value;
                    terms.RemoveFirst();
                    String rule_string = $"{left} {right}";
                    int rule_number_match = -1;
                    foreach (int r_num in RuleLookup.Keys)
                    {
                        String p_rule = RuleLookup[r_num];
                        if (p_rule.Contains('|'))
                        {
                            String[] pparts = p_rule.Split('|');
                            String r_first = pparts[0].Trim();
                            String r_second = pparts[1].Trim();
                            if (rule_string.Equals(r_first))
                            {
                                Console.WriteLine(
                                    $"\t\t l_exact match '{rule_string}' to '{r_first}' in \"{p_rule}\", rule number {r_num}");

                                rule_number_match = r_num;
                                break;
                            }
                            if (rule_string.Equals(r_second))
                            {
                                Console.WriteLine(
                                    $"\t\t r_exact match '{rule_string}' to '{r_second}' in \"{p_rule}\", rule number {r_num}");
                                rule_number_match = r_num;
                                break;
                            }
                        }
                        else
                        {
                            String r_first = p_rule.Trim();
                            
                            if (rule_string.Equals(r_first))
                            {
                                Console.WriteLine(
                                    $"\t o_exact match '{rule_string}' to '{r_first}' in \"{p_rule}\", rule number {r_num}");
                                rule_number_match = r_num;
                                break;
                            }

                                
                        }

                    
                    }

                    if (rule_number_match < 0)
                    {
                        Console.WriteLine($"\t No rule matches {rule_string}");
                        return false;
                    }
                    else
                    {
                        new_list.AddLast(rule_number_match);
                    }
                }

                Console.WriteLine($"\t Reduce {previous_terms} to {new_list.Count} terms");
                terms = new_list;
            }

            Console.WriteLine($"\n\t Final reduction is to {Utility.LinkdedListToStringLine(terms)}");
            return false;
        }


        private static String[] rule_array;
        private static HashSet<String> valid_messages;
        
        private static HashSet<String> GenMessages(String s_rule, int i_rule)
        {
            
            String rule;
            if(i_rule < 0) {
                int ii_rule;
                bool g_i;
                g_i = int.TryParse(s_rule, out ii_rule);
               
                if((g_i) && ii_rule < 136) {
                    i_rule = ii_rule;
                }
            }
            
            if (i_rule >= 0)
            {
                rule = rule_array[i_rule];
            }
            else
            {
                rule = s_rule;
            }

            
            HashSet<String> result = new HashSet<String>();
            if (rule.Contains("a") || rule.Contains("b"))
            {
                if (rule.Equals("\"a\""))
                {
                    result.Add("a");
                }
                else if (rule.Equals("\"b\""))
                {
                    result.Add("b");
                }

                return result;
            }

            if (rule.Contains("|"))
            {
                String[] parts = rule.Split('|');
                for (int i = 0; i < parts.Length; i++)
                {
                    parts[i] = parts[i].Trim();
                }
                result = new HashSet<String>();
                foreach (String ru in parts)
                {
                    HashSet<String> p_set;
                    // Console.WriteLine($"\t\t[p]->{ru}");
                    p_set = GenMessages(ru, -1);
                    result.UnionWith(p_set);
                }
                              
                return result;
            }

            // Rule form is only "# #" now
            result = new HashSet<String>();
            rule = rule.Trim();
            String[] t_num = rule.Split(' ');
            if (t_num.Length == 1)
            {
                String g = t_num[0].Trim();
                //    Console.WriteLine($"\t\t[g]-> {g}");
                return GenMessages(g, -1);
            }


            Stack<String> stk = new Stack<string>();
            HashSet<String> right = new HashSet<string>();
            right.Add("");
            foreach (String r in t_num)
            {
                String r2 = r.Trim();
                stk.Push(r2);
            }

            while (stk.Count > 0)
            {
                String ru = stk.Pop();
                //  Console.WriteLine($"\t\t[s]-> {ru}");
                HashSet<String> n_s =GenMessages(ru, -1);
            
                HashSet<String> left = new HashSet<string>();
                foreach (String l_s in n_s)
                {
                    foreach (String e_s in right) 
                    {
                        left.Add(l_s + e_s);
                    }

                }

                right = left;

            }

            return right;
        
        }

 
    }
    
    
    
}
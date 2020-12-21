using System;
using System.CodeDom;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Data;
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
            String[] message_array = message_strings.ToArray();

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
		
            Console.WriteLine($"\t Rule  8:{rule_array[8]} ");
            Console.WriteLine($"\t Rule 11:{rule_array[11]} ");
            Console.WriteLine();
			
			
            valid_messages = GenMessages(rule_array[0], 0);
		
            Console.WriteLine($"\tGenerated {valid_messages.Count} valid messages");
			

		    HashSet<int> v_length = new HashSet<int>();
            foreach (String v in valid_messages)
            {
                v_length.Add(v.Length);
            }
            Console.WriteLine(Utility.HashSetToStringLine(v_length));
            int valid_count = 0;
            foreach (String m in message_strings)
            {
                if (valid_messages.Contains(m))
                {
                    valid_count++;
                }

            }

			

            Console.WriteLine($"\n\tPart 2 Solution: {valid_count}");
        }
        
        private static String[] rule_array;
        private static HashSet<String> valid_messages;
        private static int d_count= 20;
       
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
                    Console.WriteLine($"\t\t[p]->{ru}");
                    p_set = GenMessages(ru, -1);
                    result.UnionWith(p_set);
                }
                                
                /*
                HashSet<String> one = GenMessages(parts[0], -1);
         
                foreach (String o_s in one)
                {
                
                    result.Add(o_s);
                }
               
                
                HashSet<String> two = GenMessages(parts[1], -1);

                result = new HashSet<String>();
                foreach (String o in one)
                {
                         result.Add(o);
                    
                }

                foreach (String t in two)
                {
                    result.Add(t);
                    
                }
                */
                return result;
            }

            // Rule form is only "# #" now
        result = new HashSet<String>();
        rule = rule.Trim();
        String[] t_num = rule.Split(' ');
        if (t_num.Length == 1)
        {
            String g = t_num[0].Trim();
            Console.WriteLine($"\t\t[g]-> {g}");
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
            Console.WriteLine($"\t\t[s]-> {ru}");
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
        
        
        HashSet<String> first;
        HashSet<String> second;
        HashSet<String> third;
        rule.Trim();
        
        int i_first, i_second, i_third;
        int n_t = t_num.Length;
        t_num[0].Trim();
        i_first = int.Parse(t_num[0]);
        first = GenMessages(rule_array[i_first], i_first);
        
        if (n_t == 1)
        {
            foreach (String o_s in first)
            {
                    result.Add(o_s);
        
            }

            return result;

        }

        t_num[1].Trim();
        i_second = int.Parse(t_num[1]);
        second = GenMessages(rule_array[i_second], i_second);
        
        result = new HashSet<string>();
        if (n_t == 2)
        {
            foreach (String f in first)
            {
                foreach (String s in second)
                {
                    String q = f + s;
        
                        result.Add(q);
                }
            }
            return result;
        }
			
        t_num[2].Trim();
        i_third = int.Parse(t_num[2]);
        third = GenMessages(rule_array[i_third], i_third);
        
        foreach (String f in first)
        {
            foreach (String s in second)
            {
                foreach (String t in third)
                { 
						
                    String q = f + s + t;
                    result.Add(q);
                }
            }
        }
        return result;
    }

    private static List<String> CrossProduct(List<String> one, List<String> two)
    {
    List<String> result = new List<string>();
        foreach (String o in one)
    {
        foreach (String t in two)
        {
            String q = o + t;
            if(q.Length <= 96)
                result.Add(q);
        }
    }
    return result;
    } 
}
    
    
    
}
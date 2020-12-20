using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;


/*
	Solutions found:
	Part 1: 27802
	Part 2: 279139880759
	
*/

namespace advent_2020
{
	
    static class AOC_16
    {
        private const string Part1Input = "aoc_16_input_1.txt";
        private const string Part2Input = "aoc_16_input_2.txt";
        private const string TestInput1 = "aoc_16_test_1.txt";
        private const string TestInput2 = "aoc_16_test_2.txt";
        
        private const string TestInput3 = "aoc_16_test_3.txt";

        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 16");
            var watch = Stopwatch.StartNew();
            Part1();
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = Stopwatch.StartNew();
            Part2();
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }

        
        
        private class Rule
        {
            public int l_start;
            public int l_end;
            public int r_start;
            public int r_end;

            public String name;

            public HashSet<int> possible_fields;

            public int field_num;

            private void Init(int lStart, int lEnd, int rStart, int rEnd, String name)
            {
                l_start = lStart;
                l_end = lEnd;
                r_start = rStart;
                r_end = rEnd;
                this.name = name;
                possible_fields = new HashSet<int>();
                field_num = -1; //Unmatched to field
            }
            
            public Rule(int lStart, int lEnd, int rStart, int rEnd, String name)
            {
                Init(lStart,lEnd,rStart,rEnd, name);
            }

            public Rule(String parse)
            {
                int lStart=-1, lEnd=-2;
                int rStart = -3, rEnd = -4;
                String name = "null";
                
               // Console.WriteLine($"\t{parse}");
                String[] parts;
                parts = parse.Split(':');
                name = parts[0];
                parts = parts[1].Split(' ');
                String range_one = parts[1];
                String range_two = parts[3];
                //Console.WriteLine($"\t r1: {range_one}  | r2: {range_two}");
                String[] r1 = range_one.Split('-');
                String[] r2 = range_two.Split('-');

                lStart = int.Parse(r1[0]);
                lEnd = int.Parse(r1[1]);

                rStart = int.Parse(r2[0]);
                rEnd = int.Parse(r2[1]);
                
                
                Init(lStart,lEnd,rStart,rEnd, name);
            }

            public bool ApplyRule(int target)
            {
                if ((l_start <= target) && (target <= l_end))
                    return true;
                if ((r_start <= target) && (target <= r_end))
                    return true;
                return false;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\t");
                sb.Append(name);
                sb.Append(" : ");
                sb.Append($" {l_start} <= x <= {l_end}   ||   {r_start} <= x <= {r_end}");
                return sb.ToString();

            }
        }
        
        
        
        
        
        
        
        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            string[] lines = File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            List<String> rule_strings = new List<string>();
            List<String> number_strings = new List<string>();
            bool rules_part = true;
            bool my_ticket_part = false;
            bool numbers_part = false;
            List<String> my_ticket_strings = new List<string>();


            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Equals(""))
                {
                    if (rules_part)
                    {
                        rules_part = false;
                        my_ticket_part = true;

                    }
                    else if (my_ticket_part)
                    {
                        numbers_part = true;
                        my_ticket_part = false;

                    }
                    
                }
                else
                {
                    if (rules_part)
                    {
                        rule_strings.Add(lines[i]);
                    }
                    else if (my_ticket_part)
                    {
                        my_ticket_strings.Add(lines[i]);
                    }
                    else if (numbers_part)
                    {
                        if (lines[i].StartsWith("nearby")) continue;
                        number_strings.Add(lines[i]);
                    }
                    
                }
            }
            
            
            List<Rule> rules = new List<Rule>(rule_strings.Count);
            List<int>[] tickets = new List<int>[number_strings.Count];
            
            
            foreach (String r_line in rule_strings)
            {
                Rule r = new Rule(r_line);
                rules.Add(r);
            }
            
            for (int i = 0; i < tickets.Length; i++)
            {
                List<int> L = new List<int>();
                String[] parts = number_strings[i].Split(',');
                foreach (String p in parts)
                {
                    L.Add(int.Parse(p));
                }

                tickets[i] = L;
            }
            

            
            
            
            int rule_count = rules.Count;
           
            
            
            List<int> invalid_numbers = new List<int>();
            
            
            for (int i=0; i < tickets.Length; i++)
            {
              //  Console.WriteLine(Utility.ListToStringLine(tickets[i]));
                foreach (int n in tickets[i])
                {
                    int count = 0;
                    foreach (Rule r in rules)
                    {
                        if (r.ApplyRule(n)) count++;
                    }
                   // Console.WriteLine($"\t#{n} matches {count} rules");
                    if(count == 0) invalid_numbers.Add(n); 
                }
                
            }
            
            //Console.WriteLine($"\t invalid numbers: {Utility.ListToStringLine(invalid_numbers)}");
            
            
            Console.WriteLine($"\n\tPart 1 Solution: {Utility.ListProduct(invalid_numbers)}");
        }

        private static void Part2()
        {

            Console.WriteLine("   Part 2");
            string[] lines = File.ReadAllLines(Part2Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            List<String> rule_strings = new List<string>();
            List<String> number_strings = new List<string>();
            bool rules_part = true;
            bool my_ticket_part = false;
            bool numbers_part = false;
            List<String> my_ticket_strings = new List<string>();


            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Equals(""))
                {
                    if (rules_part)
                    {
                        rules_part = false;
                        my_ticket_part = true;

                    }
                    else if (my_ticket_part)
                    {
                        numbers_part = true;
                        my_ticket_part = false;

                    }

                }
                else
                {
                    if (rules_part)
                    {
                        rule_strings.Add(lines[i]);
                    }
                    else if (my_ticket_part)
                    {
                        my_ticket_strings.Add(lines[i]);
                    }
                    else if (numbers_part)
                    {
                        if (lines[i].StartsWith("nearby")) continue;
                        number_strings.Add(lines[i]);
                    }

                }
            }

            Console.WriteLine("\tRead {0} tickets", number_strings.Count);
            List<Rule> rules = new List<Rule>(rule_strings.Count);
            List<int>[] list_tickets = new List<int>[number_strings.Count];

            Dictionary<String,Rule> rules_by_name = new Dictionary<string, Rule>();
            foreach (String r_line in rule_strings)
            {
                Rule r = new Rule(r_line);
                rules.Add(r);
                rules_by_name[r.name] = r;
            }

            Rule[] rule_set = rules.ToArray();

            int number_of_fields = rules.Count;
            for (int i = 0; i < list_tickets.Length; i++)
            {
                List<int> L = new List<int>();
                String[] parts = number_strings[i].Split(',');
                foreach (String p in parts)
                {
                    L.Add(int.Parse(p));
                }

                list_tickets[i] = L;
            }

            int rule_count = rules.Count;

            HashSet<List<int>> invalid_tickets = new HashSet<List<int>>();
            for (int i = 0; i < list_tickets.Length; i++)
            {
                foreach (int n in list_tickets[i])
                {
                    int count = 0;
                    foreach (Rule r in rules)
                    {
                        if (r.ApplyRule(n)) count++;
                    }

                    if (count == 0) invalid_tickets.Add(list_tickets[i]);
                }
            }

            HashSet<List<int>> tickets_hs = new HashSet<List<int>>(list_tickets);
            foreach(List<int> bad in invalid_tickets)
            {
                tickets_hs.Remove(bad);
            }

            Console.WriteLine($"\t\tDiscarding : {invalid_tickets.Count} invalid tickets");
            Console.WriteLine($"\t\tLeaving    : {tickets_hs.Count} valid tickets");
         //   Console.WriteLine($"\t\tRules      : {rule_set.Length}");




            int[,] a_tickets = new int[rule_count, tickets_hs.Count];


            int tick_count = 0;
            foreach (List<int> l_ticket in tickets_hs)
            {
                int field_index = 0;
                foreach (int f_value in l_ticket)
                {
                    a_tickets[field_index, tick_count] = f_value;
                    field_index++;
                }

                tick_count++;
            }

            int ticket_count = a_tickets.GetLength(1);
            int field_count = a_tickets.GetLength(0);
          

            int[,] valid_rule_by_field_count = new int[rule_count, field_count];
            for (int r = 0; r < rule_set.Length; r++)
            {
                Rule rule = rule_set[r];
                for (int t = 0; t < ticket_count; t++)
                {
                    for (int f = 0; f < field_count; f++)
                    {
                        bool match = false;
                        int value = a_tickets[f, t];
                        match = rule.ApplyRule(value);
                        if (match)
                        {
                            valid_rule_by_field_count[r, f]++;
                        }
                    }
                }
            }

           


            for (int r = 0; r < rule_count; r++)
            {
                Rule rule = rule_set[r];
                for (int f = 0; f < field_count; f++)
                {
                    int current_field = f;
                    bool field_valid = true; //assume field could be valid for this rule
                    for (int t = 0; t < ticket_count; t++)
                    {
                        int value;
                        value = a_tickets[current_field, t];
                        if (!rule.ApplyRule(value))
                        {
                            // At lease one ticket's field value doesn't match this rule for that field
                            field_valid = false;
                            // No need to check other tickets
                            break;
                        }
                    }

                    if (field_valid)
                    {
                        rule.possible_fields.Add(current_field);
                    }
                }
            }
            HashSet<Rule> final_ruleset = new HashSet<Rule>();
            Dictionary<Rule,int> rule_to_field_number = new Dictionary<Rule, int>();
            HashSet<int> matched_fields = new HashSet<int>();
         
            for (int r = 0; r < rule_count; r++)
            {
                Rule rule = rule_set[r];
                int count = rule.possible_fields.Count;
             //  Console.WriteLine($"\t rule-> {rule.name} might apply to fields: {count}");
                if (count == 1)
                {
                    int field_num = rule.possible_fields.ToArray()[0];
                    rule.field_num = field_num;
                    rule_to_field_number[rule] = field_num;
                    matched_fields.Add(field_num);
                    final_ruleset.Add(rule);
                 
                }
               
                 
                   
                
             }
          
            
            
            while (final_ruleset.Count < rule_count)
            {
            
                List<int> finalized_fields = new List<int>();
                foreach (Rule r in rules)
                {
                    if (r.possible_fields.Count == 1)
                    {
                        finalized_fields.Add(r.field_num);
                    }
                }
                List<int> fields_to_add = new List<int>();
                foreach (Rule r in rules)
                {
                    if (r.possible_fields.Count > 1)
                    {
                        foreach (int f in finalized_fields)
                        {
                            r.possible_fields.Remove(f);
                            if (r.possible_fields.Count == 1)
                            {
                                int new_field = r.possible_fields.ToArray()[0];
                                r.field_num = new_field;
                                fields_to_add.Add(new_field);
                                rule_to_field_number[r] = f;
                                matched_fields.Add(f);
                                final_ruleset.Add(r);
                               
                                break;
                            }
                        }

                        foreach (int f in fields_to_add)
                        {
                            finalized_fields.Add(f);
                        }
                        fields_to_add = new List<int>();
                    }
                }
            }

            HashSet<Rule> answer_rules = new HashSet<Rule>();
            for (int i = 0; i < field_count; i++)
            {
                for (int r = 0; r < rule_count; r++)
                {
                    if (rule_set[r].field_num == i)
                    {
                        String rule_name = rule_set[r].name;
                       // int field_num = rule_set[r].field_num;
               
                        if (rule_name.StartsWith("departure"))
                        {
                            answer_rules.Add(rule_set[r]);
                        }
                        break;
                    }
                }
            }


            List<int> my_ticket_numbers = parseCSV(my_ticket_strings.ToArray()[1]);

            int[] mine = my_ticket_numbers.ToArray();
            long answer = 1;
            foreach (Rule r in answer_rules)
            {
                int field = r.field_num;
                long v = mine[field];
              answer *= v;
            }
            
            
     
            
		    Console.WriteLine($"\n\tPart 2 Solution: {answer}");
        }



        private static List<int> parseCSV(String line, char separator = ',')
        {
            List<int> result = new List<int>();
            String[] parts = line.Split(separator);
            foreach (String p in parts)
            {
                result.Add(int.Parse(p));
            }
            return result;
        }
        
        
    }
    
}

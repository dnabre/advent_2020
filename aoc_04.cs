using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


/*
	Solutions found:
	Part 1: 204
	Part 2: 179
	
*/

namespace advent_2020
{
    static class AOC_04
    {
        private const string Part1Input = "aoc_04_input_1.txt";
        private const string Part2Input = "aoc_04_input_2.txt";
        private const string TestInput1 = "aoc_04_test_1.txt";
        private const string TestInput2 = "aoc_04_test_2.txt";

        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 04");
            Part1(args);
            Console.Write("\n");
            Part2(args);
        }

        private static List<Dictionary<String, String>> ParseRecordFromLines(String[] lines)
        {
            List<HashSet<String>> record_list = ParseRecords(lines);
            List<Dictionary<String, String>> record_field_list = ParseRecordsWithFields(record_list);
            Console.WriteLine($"\tFields parsed for {record_field_list.Count} records");
            return record_field_list;
        }


        private static void Part1(string[] args)
        {
            Console.WriteLine("   Part 1");
            Console.WriteLine($"\tRequired Fields {required_fields.Length}");
            String[] lines = File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            var record_field_list = ParseRecordFromLines(lines);
            int valid_count = 0;
            foreach (Dictionary<String, String> rec in record_field_list)
            {
                if (TestRecord(rec))
                {
                    valid_count++;
                }
            }

            Console.WriteLine($"\n\tPart 1 Solution: {valid_count}");
        }

        private static void Part2(string[] args)
        {
            Console.WriteLine("   Part 2:");
            Console.WriteLine($"\tRequired Fields {required_fields.Length}");
            String[] lines = File.ReadAllLines(Part2Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            var record_field_list = ParseRecordFromLines(lines);
            int valid_count = 0;
            foreach (Dictionary<String, String> rec in record_field_list)
            {
                if (TestRecord2(rec))
                {
                    valid_count++;
                }
            }

            Console.WriteLine($"\n\tPart 2 Solution: {valid_count}");
        }

        private static String RecordToString(HashSet<String> rec)
        {
            StringBuilder sb = new StringBuilder();
            foreach (String field in rec)
            {
                sb.Append(field);
                sb.Append("|");
            }

            return sb.ToString();
        }

        private static String RecordToString(Dictionary<String, String> rec)
        {
            StringBuilder sb = new StringBuilder();
            foreach (String k in rec.Keys)
            {
                String val = rec[k];
                sb.Append($"{k}:{val}|");
            }

            return sb.ToString();
        }


        private static bool IsYearBetween(String start, String end, String target)
        {
            int i_start = int.Parse(start);
            int i_end = int.Parse(end);
            int i_target = int.Parse(target);
            if (i_target < i_start) return false;
            if (i_target > i_end) return false;
            return true;
        }

        private static bool IsHeightValid(String input)
        {
            //	hgt (Height) - a number followed by either cm or in:
            //	If cm, the number must be at least 150 and at most 193.
            //	If in, the number must be at least 59 and at most 76.
            String h_unit;
            int h_number;
            ParseHeight(input, out h_unit, out h_number);
            if (h_unit.Equals("cm"))
            {
                if (h_number < 150) return false;
                if (h_number > 193) return false;
            }
            else
            {
                if (h_number < 59) return false;
                if (h_number > 76) return false;
            }

            return true;
        }

        private static bool IsValueColor(String input)
        {
            //	hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
            if (input.Length != 7) return false;
            if (input[0] != '#') return false;
            for (int i = 1; i < input.Length; i++)
            {
                char ch = input[i];
                if (Char.IsDigit(ch))
                {
                    continue;
                }
                else
                {
                    if ("abcdef".Contains(ch.ToString()))
                    {
                        continue;
                    }
                    else return false;
                }
            }

            return true;
        }

        private static bool IsValidEyeColor(String input)
        {
            //ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
            String[] valid_colors = {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
            foreach (String c in valid_colors)
            {
                if (input.Equals(c)) return true;
            }

            return false;
        }

        private static bool IsValidPID(String input)
        {
            //	pid (Passport ID) - a nine-digit number, including leading zeroes.
            if (input.Length != 9) return false;
            foreach (char c in input)
            {
                if (!Char.IsDigit(c)) return false;
            }

            return true;
        }

        private static bool TestRecord2(Dictionary<String, String> fields)
        {
            bool DEBUG = false;


            if (TestRecord(fields) == false)
            {
                if (DEBUG) Console.WriteLine($" Fails Part 1 Test: {RecordToString(fields)}");
                return false;
            }

            // 	byr (Birth Year) - four digits; at least 1920 and at most 2002.
            if (!IsYearBetween("1920", "2002", fields["byr"]))
            {
                if (DEBUG) Console.WriteLine($"Failed byr test 1920 <= {fields["byr"]} <= 2002");
                return false;
            }

            //  iyr (Issue Year) - four digits; at least 2010 and at most 2020.
            if (!IsYearBetween("2010", "2020", fields["iyr"]))
            {
                if (DEBUG) Console.WriteLine($"Failed iyr test 2010 <= {fields["iyr"]} <= 2020");
                return false;
            }

            //	eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
            if (!IsYearBetween("2020", "2030", fields["eyr"]))
            {
                if (DEBUG) Console.WriteLine($"Failed eyr test 2020 <= {fields["eyr"]} <= 2030");
                return false;
            }

            //	hgt (Height) - a number followed by either cm or in:
            //	If cm, the number must be at least 150 and at most 193.
            //	If in, the number must be at least 59 and at most 76.
            if (!IsHeightValid(fields["hgt"]))
            {
                if (DEBUG) Console.WriteLine($"Failed hgt test 150cm or 59in  <= {fields["hgt"]} <= 193cm or 76in");
                return false;
            }

            //	hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
            if (!IsValueColor(fields["hcl"]))
            {
                if (DEBUG) Console.WriteLine($"Failed hcl {fields["hcl"]} is not a valid color");
                return false;
            }

            //	ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
            if (!IsValidEyeColor(fields["ecl"]))
            {
                if (DEBUG) Console.WriteLine($"Failed ecl {fields["ecl"]} is not a valid ecl color");
                return false;
            }

            //	pid (Passport ID) - a nine-digit number, including leading zeroes.
            if (!IsValidPID(fields["pid"]))
            {
                if (DEBUG) Console.WriteLine($"Failed pid {fields["pid"]} is not a valid pid");
                return false;
            }

            return true;
        }

        private static void ParseHeight(String input, out String hunit, out int ht)
        {
            StringBuilder sb_num = new StringBuilder();
            StringBuilder sb_unit = new StringBuilder();
            foreach (char c in input)
            {
                if (Char.IsDigit(c)) sb_num.Append(c);
                else sb_unit.Append(c);
            }

            ht = int.Parse(sb_num.ToString());
            hunit = sb_unit.ToString();
            return;
        }

        private static bool TestRecord(Dictionary<String, String> fields)
        {
            foreach (String req in required_fields)
            {
                if (!fields.ContainsKey(req))
                {
                    return false;
                }
            }

            return true;
        }


        private static List<Dictionary<String, String>> ParseRecordsWithFields(List<HashSet<String>> record_list)
        {
            List<Dictionary<String, String>> field_dicts = new List<Dictionary<String, String>>();
            foreach (HashSet<String> record in record_list)
            {
                Dictionary<String, String> fields;
                fields = ParseFields(record);
                if (fields.Count == 0)
                {
                    Console.WriteLine($"\tRecord without fields {record}");
                    Environment.Exit(1);
                }

                //Console.WriteLine($"\t\tparsed {fields.Count} fields from record");
                field_dicts.Add(fields);
            }

            return field_dicts;
        }


        private static Dictionary<String, String> ParseFields(HashSet<String> record)
        {
            Dictionary<String, String> fields = new Dictionary<String, String>();
            foreach (String raw_field in record)
            {
                //Console.WriteLine($"\t{raw_field}");
                String[] parts = raw_field.Split(':');
                if (parts.Length != 2)
                {
                    Console.WriteLine($"\t error in parsing field {raw_field}. split into {parts.Length} pieces");
                    Environment.Exit(0);
                }

                fields[parts[0]] = parts[1];
            }

            return fields;
        }


        private static List<HashSet<String>> ParseRecords(String[] lines)
        {
            var records = new List<HashSet<String>>();

            var current_record = new HashSet<String>();
            for (int i = 0; i < lines.Length; i++)
            {
                String c_line = lines[i];
                if (c_line.Equals(""))
                {
                    records.Add(current_record);
                    current_record = new HashSet<String>();
                }
                else
                {
                    String[] fields = c_line.Split(' ');
                    foreach (String f in fields)
                    {
                        current_record.Add(f);
                    }
                }
            }

            if (current_record.Count > 0)
            {
                records.Add(current_record);
            }

            Console.WriteLine($"\tParsed {records.Count} records");
            return records;
        }

        private static String[] required_fields =
        {
            "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"
            //											,"cid"
        };


        /*
            byr (Birth Year)
            iyr (Issue Year)
            eyr (Expiration Year)
            hgt (Height)
            hcl (Hair Color)
            ecl (Eye Color)
            pid (Passport ID)
            cid (Country ID)
        */
    }
}
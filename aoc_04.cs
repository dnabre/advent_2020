using System;
using System.Text;
using System.Collections.Generic;


/*
	Solutions found:
	Part 1: 204
	Part 2: 1478615040
	
*/

namespace advent_2020
{
	static class AOC_04 {
		private const string Part1Input = "aoc_04_input_1.txt";
		private const string Part2Input = "aoc_04_input_2.txt";
		private const string TestInput = "aoc_04_test_1.txt";

		public static void Run (string[] args) {
			Console.WriteLine ("AoC Problem 04");
			Part1(args);
//			Console.Write("\n");
//			Part2(args);
		}

	private static void Part1(string[] args) {
			Console.WriteLine("   Part 1");
			Console.WriteLine($"\tRequired Fields {required_fields.Length}");
			String[] lines =  System.IO.File.ReadAllLines(Part1Input);
			Console.WriteLine("\tRead {0} inputs", lines.Length);
			List<HashSet<String>> record_list = ParseRecords(lines);
			List<Dictionary<String,String>> record_field_list = ParseRecordsWithFields(record_list);
			Console.WriteLine($"\tFields parsed for {record_field_list.Count} records");
			
			int valid_count = 0;
			foreach(Dictionary<String,String> rec in record_field_list) {
				if(TestRecord(rec)) {
					valid_count++;
				}
			}



			Console.WriteLine($"\n\tPart 1 Solution: {valid_count}");
			
		}

	private static void Part2(string[] args) {
			Console.WriteLine("   Part 2:");
			
			Console.WriteLine($"\n\tPart 2 Solution: {0}");
		}	
		
	private static String RecordToString(HashSet<String> rec) {
		StringBuilder sb = new StringBuilder();
		foreach(String field in rec) {
			sb.Append(field);
			sb.Append("|");
		}
		return sb.ToString();
	}

	private static bool TestRecord(Dictionary<String,String> fields) {
		foreach(String req in required_fields) {
			if(!fields.ContainsKey(req)) {
				return false;
			}
		}
		return true;
	}

	private static List<Dictionary<String,String>> ParseRecordsWithFields(List<HashSet<String>> record_list) {
			List<Dictionary<String,String>> field_dicts = new List<Dictionary<String,String>>();
			foreach(HashSet<String> record in record_list) {
				Dictionary<String,String> fields;
				fields = ParseFields(record);
				if(fields.Count == 0) {
					Console.WriteLine($"\tRecord without fields {record}");
					System.Environment.Exit(1);
				}
				//Console.WriteLine($"\t\tparsed {fields.Count} fields from record");
				field_dicts.Add(fields);
			}
			return field_dicts;
	}

	

	private static Dictionary<String,String> ParseFields(HashSet<String> record) {
		Dictionary<String,String> fields = new Dictionary<String,String>();
		foreach(String raw_field in record) {
			//Console.WriteLine($"\t{raw_field}");
			String[] parts = raw_field.Split(":");
			if(parts.Length != 2) {
				Console.WriteLine($"\t error in parsing field {raw_field}. split into {parts.Length} pieces");
				System.Environment.Exit(0);
			}
			fields[parts[0]] = parts[1];
		}
		return fields;
	}



	private static List<HashSet<String>> ParseRecords(String[] lines) {
			var records = new List<HashSet<String>>();

			var current_record = new HashSet<String>();
			for(int i=0; i < lines.Length; i++) {
				String c_line = lines[i];
				if(c_line.Equals("")) {
					records.Add(current_record);
					current_record = new HashSet<String>();
				} else {
					String[] fields = c_line.Split(" ");
					foreach(String f in fields) {
						current_record.Add(f);
					}

				}
			}
			if(current_record.Count > 0) {
				records.Add(current_record);
			}
			Console.WriteLine($"\tParsed {records.Count} records");
			return records;
			}

	private static String[] required_fields = {"byr","iyr","eyr","hgt","hcl","ecl","pid"
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

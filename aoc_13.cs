using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;


/*
	Solutions found:
	Part 1: 2238
	Part 2: 
	
*/

namespace advent_2020
{
    static class AOC_13 {
        private const string Part1Input = "aoc_13_input_1.txt";
        private const string Part2Input = "aoc_13_input_2.txt";
        private const string TestInput1 = "aoc_13_test_1.txt";
        private const string TestInput2 = "aoc_13_test_2.txt";

        public static void Run (string[] args) {
            Console.WriteLine ("AoC Problem 13");
            //Part1(args);
            Console.Write("\n");
           Part2(args);
        }

        private static void Part1(string[] args) {
            Console.WriteLine("   Part 1");
            string[] lines =  System.IO.File.ReadAllLines(Part2Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
			//Console.WriteLine($"\t{lines[0]}");
			long target;
			target = long.Parse(lines[0]);
			//Console.WriteLine($"\t target = {target}");
			String inputs = lines[1];
			//Console.WriteLine($"\t{lines[1]}");
			List<long> buses = new List<long>();
			String[] parts = inputs.Split(',');
			foreach(String p in parts) {
				if(p.Equals("x"))	continue;
				long n = long.Parse(p);
				
				buses.Add(n);
			}

			Dictionary<long,long> bus_back = new Dictionary<long,long>();
			List<long> next_stop= new List<long>(buses.Count);
			long next = -1;
			foreach(long bus in buses) {
				next = GetNextStopAfter(bus,target);
			//	Console.WriteLine($"\ttarget: {target} \tbus: {bus} \tnext_stop: {next}");
				bus_back[next] = bus;
				next_stop.Add(next);
			}
			long min_time = next;
			foreach(long i in next_stop) {
				min_time = Math.Min(i,min_time);
			}
			long next_bus = bus_back[min_time];
			long minutes_until = min_time - target;
			Console.WriteLine();
			Console.WriteLine($"\t Next bus stop is {next_bus} at time {min_time}, {minutes_until} minutes in the future");

            Console.WriteLine($"\n\tPart 1 Solution: {next_bus * minutes_until}");	
        }

        private static void Part2(string[] args) {
            Console.WriteLine("   Part2");
            string[] lines =  System.IO.File.ReadAllLines(Part2Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
			Console.WriteLine($"\t{lines[0]}");
			long target;
			long x_count = 0;
			target = long.Parse(lines[0]);
			target = 1068781;
			Console.WriteLine($"\t target = {target}");
			String inputs = lines[1];
			Console.WriteLine($"\t{lines[1]}");
			List<long> buses = new List<long>();
			List<long> minute_after = new List<long>();
			String[] parts = inputs.Split(',');
			long timestamp_n = -1;
			foreach(String p in parts)
			{
				timestamp_n++;
				if(p.Equals("x"))
				{
					x_count++;
					continue;
					
				}
				long n = long.Parse(p);
				buses.Add(n);
				minute_after.Add(timestamp_n);
				Console.WriteLine($"\t Bus {n} is at t+{timestamp_n} ");
			}

			long[] bus_id = buses.ToArray();
			long[] minutes = minute_after.ToArray();
			long answer = -1;

			
			long increment = 1;
			
			
			
			while (answer < 0)
			{
				int good_count = 0;
				for (int i = 0; i < bus_id.Length; i++)
				{
					long r = WhichTime(bus_id[i], target);
			//		Console.WriteLine($"\t{target} % {bus_id[i]} = {r}   {minutes[i]}"); 
					if (r == minutes[i])
					{
						good_count++;
									
						
					}
					
				}
			//		Console.WriteLine();

				
				if (good_count == bus_id.Length)
				{
					answer = target;
					break;
				}
				else
				{
					target += increment;
					if (target <= 0)
					{
						Console.WriteLine($"Overflow on target = {target}");
						System.Environment.Exit(0);
					}
				}
				
			}
			
			
			
			
			
			
			Console.WriteLine($"\n\tPart 2 Solution: {target}");
        }

		private static long GetNextStopAfter(long bus, long target) {
			long next = bus;
			while(next<target) {
				next += bus;
			}


			return next;
		}

		private static long WhichTime(long b, long target)
		{
			long m1,m2;
			m1 = target % b;
			m2 = Math.Abs(m1 - b);
			return Math.Min(m1, m2);

		}
		
		private static long GetBus(long timestamp, long minutes_after, long[] bus)
		{
			for (int i = 0; i < bus.Length; i++)
			{
				if (bus[i] == -1)
				{
					continue;
				}

				long m;
				m = timestamp % bus[i];
				if (minutes_after == m)
				{
					return bus[i];
				}
			}
			return -1;
		}
		
    }
    
}
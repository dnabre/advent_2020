using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

/*
	Solutions found:
	Part 1: 2679568 
	Part 2: Finished 2021/01/23 05:49PM MST
	
*/

namespace advent_2020 {
  static class AOC_25 {
    private const string Part1Input = "aoc_25_input_1.txt";
    private const string Part2Input = "aoc_25_input_2.txt";
    private const string TestInput1 = "aoc_25_test_1.txt";
    private const string TestInput2 = "aoc_25_test_2.txt";
    
    public static void Run(string[] args) {
      Console.WriteLine("AoC Problem 25");
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

    private static long card_public;
	private static long door_public; 
    
	private static long divider =20201227L;
	private static long subject_number = 7L;

    private static void Part1() {
      Console.WriteLine($"   Part 1");
      string[] lines = File.ReadAllLines(Part1Input);
      Console.WriteLine($"\tRead {0} inputs", lines.Length);
	
	  card_public = long.Parse(lines[0]);
	  door_public = long.Parse(lines[1]);
	  Console.WriteLine($"\tCard Public Key: {card_public.ToString().PadLeft(32)}");
	  Console.WriteLine($"\tDoor Public Key: {door_public.ToString().PadLeft(32)}\n");

		long ls;
		ls = find_loop_size(card_public);
		Console.WriteLine($"\tCard Public Key, loop: {ls.ToString().PadLeft(5)}  key: {card_public.ToString().PadLeft(12)}");
		long ls2;
		ls2 = find_loop_size(door_public);
		Console.WriteLine($"\tDoor Public Key, loop: {ls2.ToString().PadLeft(5)}  key: {door_public.ToString().PadLeft(12)}");

		long r1;
		r1 = apply_loop(ls2, card_public);
		Console.WriteLine($"\tApplying loop size = {ls2.ToString().PadLeft(5)} to Card Public Key = " + 
			$"{card_public.ToString().PadLeft(12)} yields: {r1.ToString().PadLeft(12)}");
		
		long r2;
		r2 = apply_loop(ls, door_public);
		Console.WriteLine($"\tApplying loop size = {ls.ToString().PadLeft(5)} to Door Public Key = " +
 			$"{door_public.ToString().PadLeft(12)} yields: {r2.ToString().PadLeft(12)}");




      Console.WriteLine($"\n\tPart 1 Solution: {r2}");
    }


	private static long apply_loop(long loop_size, long subject) {
			long value = 1;
			for(int i=0; i< loop_size;i++) {
				value = value * subject;
				value = value % divider;
			}
			return value;

	}
	
	
	private static long find_loop_size(long target) {
		long value =1;
		int count = 0;
		while (true) {
			count++;
			value = value * subject_number;
			value = value % divider;
		
			if(value == target) return count;
		}
	}



    private static void Part2() {
      Console.WriteLine("   Part 2");
      string[] lines = File.ReadAllLines(Part1Input);
      Console.WriteLine("$\tRead {0} inputs", lines.Length);

      Console.WriteLine($"\n\tPart 2 Solution: {0}");
    }
    
    
  }
}

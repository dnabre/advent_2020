using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;

/*
	Solutions found:
	Part 1:
	Part 2: 
	
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

    private static void Part1() {
      Console.WriteLine($"   Part 1");
      string[] lines = File.ReadAllLines(TestInput1);
      Console.WriteLine($"\tRead {0} inputs", lines.Length);
	
	  card_public = long.Parse(lines[0]);
	  door_public = long.Parse(lines[1]);
	  Console.WriteLine($"\tCard Public Key: {card_public.ToString().PadLeft(32)}");
	  Console.WriteLine($"\tDoor Public Key: {door_public.ToString().PadLeft(32)}\n");


		for(int i = 5; i < 10; i++) {
			for(int s = 5; s < 10; s++) {
				long t = loop_key(i,s);
				long q = loop_key2(i,s);
			if(t==5764801L) Console.WriteLine($"\t\t {t} is loop {i} subject {s}");
			Console.WriteLine($"\t loop {i}, subject {s} = {t.ToString().PadLeft(16)} \t {q.ToString().PadLeft(16)}");

			}
		}


      Console.WriteLine($"\n\tPart 1 Solution: {0}");
    }

	private static long loop_key(long n,long subject) {
		long value =1;
		for(long i=0; i<n; i++) {
			value = value*subject;
			value = value % divider;
		}
		return value;
	}
	private static long loop_key2(long n, long s) {
		BigInteger v = new BitInteger(n);
		BigInteger subject = new BigInteger(s);
		BigInteger r;

		r = subject.ModPower(n, divider);
		return (long)r;
	}

    private static void Part2() {
      Console.WriteLine("   Part 2");
      string[] lines = File.ReadAllLines(Part1Input);
      Console.WriteLine("$\tRead {0} inputs", lines.Length);

      Console.WriteLine($"\n\tPart 2 Solution: {0}");
    }
    
    
  }
}

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
  static class AOC_24 {
    private const string Part1Input = "aoc_24_input_1.txt";
    private const string Part2Input = "aoc_24_input_2.txt";
    private const string TestInput1 = "aoc_24_test_1.txt";
    private const string TestInput2 = "aoc_24_test_2.txt";
    
    public static void Run(string[] args) {
      Console.WriteLine("AoC Problem 24");
      var watch = Stopwatch.StartNew();
     Part1();
      watch.Stop();
      long time_part_1 = watch.ElapsedMilliseconds;
      Console.Write("\n");
      watch = Stopwatch.StartNew();
  //    Part2();
      watch.Stop();
      long time_part_2 = watch.ElapsedMilliseconds;
      Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
    }

	public struct Hex : IEquatable<Hex>
	{
		public bool Equals(Hex other)
		{
			return x == other.x && y == other.y && z == other.z;
		}

		public override bool Equals(object obj)
		{
			return obj is Hex other && Equals(other);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = x;
				hashCode = (hashCode * 397) ^ y;
				hashCode = (hashCode * 397) ^ z;
				return hashCode;
			}
		}

		public static bool operator ==(Hex left, Hex right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Hex left, Hex right)
		{
			return !left.Equals(right);
		}

		public int x,y,z;
		public Hex(int x_, int y_, int z_) {
			x = x_;
			y = y_;
			z = z_;
		}
		public override string  ToString() {
			return $"({x},{y},{z})";
		}
	}

    
	

    private static void Part1() {
    Console.WriteLine($"   Part 1");
      string[] lines = File.ReadAllLines(TestInput1);
      Console.WriteLine($"\tRead {lines.Length} inputs");

		Hex h1 = new Hex(1,2,3);
		Hex h2 = new Hex(4,2,4);
		Hex h3 = new Hex(1,2,3);
		Console.WriteLine(h1);
		Console.WriteLine($"\t {h2}");
		Console.WriteLine(h3);

		Console.WriteLine(h1 == h3);
		Console.WriteLine(h2== h3);
		


		

      Console.WriteLine($"\n\tPart 1 Solution: {0}");
    }





    private static void Part2() {
      Console.WriteLine("   Part 2");
      string[] lines = File.ReadAllLines(Part1Input);
      Console.WriteLine("$\tRead {0} inputs", lines.Length);

      Console.WriteLine($"\n\tPart 2 Solution: {0}");
    }
    
    
  }
}

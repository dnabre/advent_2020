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
  static class AOC_23 {
    private const string Part1Input = "aoc_23_input_1.txt";
    private const string Part2Input = "aoc_23_input_2.txt";
    private const string TestInput1 = "aoc_23_test_1.txt";
    private const string TestInput2 = "aoc_23_test_2.txt";
    private const int MOD_VALUE = 9;
    
    public static void Run(string[] args) {
      Console.WriteLine("AoC Problem 23");
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

    
    
    private static void Part1() {
      Console.WriteLine($"   Part 1");
      string[] lines = File.ReadAllLines(TestInput1);
      Console.WriteLine($"\tRead {0} inputs", lines.Length);

      Console.WriteLine($"\n\tPart 1 Solution: {0}");
    }

    private static void Part2() {
      Console.WriteLine("   Part 2");
      string[] lines = File.ReadAllLines(Part1Input);
      Console.WriteLine("$\tRead {0} inputs", lines.Length);

      Console.WriteLine($"\n\tPart 2 Solution: {0}");
    }
    private static void DisplayTurn(int turn, int[] cups, int[] pickups, int current_cup, int dest_value, int dest_place) {
      Console.WriteLine($"\n\t -- move {turn} --");
      Console.Write($"\t cups: ");
      for (int i = 1; i <= 9; i++) {
        Console.Write(" ");
        if (i == current_cup % 10) {
          Console.Write("  " + $"({cups[i]})".PadLeft(3));
        } else {
          Console.Write($"{cups[i]}".PadLeft(4));
        }
      }
      Console.WriteLine();
      Console.Write($"\t pick up:");
      for (int i = 0; i < pickups.Length; i++) {
        Console.Write($" {pickups[i]}");
        if (i < 2) Console.Write(",");
      }
    
      Console.Write("\n");
      Console.WriteLine($"\t destination: {dest_value} place={dest_place}");
      Console.WriteLine("\n");
    }

    private static int GetDestPlace(int[] cups, int dest_value) {
      for (int i = 1; i < MOD_VALUE; i++) {
        if (cups[i] == dest_value) {
          return i;
        }
      }
      return - 1;
    }

    private static bool IsValueInPickups(int[] pickups, int value) {
      for (int i = 0; i < pickups.Length; i++) {
        if (pickups[i] == value) return true;
      }
      return false;
    }



    private static int mod(int value)
    {
      // map to 1-9 
      int r = value % MOD_VALUE;
      while (r < 0) r += 9;
      r = value % MOD_VALUE;
      if (r == 0) return 9;
      return r;
    }
    

    
    
  }
}
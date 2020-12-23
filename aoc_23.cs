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

    public static void Run(string[] args) {
      Process.Start("clear");
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

      char[] input_chars = lines[0].ToCharArray();
      int[] cups = new int[9];
      for (int i = 0; i < Math.Min(cups.Length, lines[0].Length); i++) {
        cups[i] = (int) Char.GetNumericValue(input_chars[i]);
      }
      Console.WriteLine($"\t {Utility.ArrayToStringLine(cups)} (length {cups.Length})\n");

      int turn;
      int current_cup;
      int[] pickups = new int[3];
	  int[] p_index = new int[3];
      int dest_value = -1;
      int dest_place = -1;
      int value_after_pickup = -1;
      int place_after_pickup = -1;
      /* ############################## End Initialization ############################## */

      turn = 0;
      current_cup = -1;

      int rounds = 2;

      /* ############################## End Priming the Loop ############################## */

      while (turn < rounds) {
        turn++;
        current_cup++;

        dest_value = ValueAdd(cups[current_cup], -1);
        dest_place = GetDestPlace(cups, dest_value);


			Console.WriteLine($"doing pickup");
        for (int i = 1; i <= 3; i++) {
		  p_index[i] = ((current_cup + i) % 10) - 1 ;
		  Console.WriteLine($"current cup :{current_cup} \t p_index: {p_index[i]}"   );
		  Console.WriteLine($"seting pickups[{i-1}]");
		  Console.WriteLine($"seting pickups[{i-1}] {p_index[i]} of cups");
		  Console.WriteLine($"change from {pickups[i-1]}");
		  Console.WriteLine($"to {cups[p_index[i]]}");
         pickups[i - 1] = cups[p_index[i]];
        }
Console.WriteLine($"done pickup");


        while (IsValueInPickups(pickups, dest_value)) {
          dest_value--;
          if (dest_value == 0) dest_value = 9;
        }
        dest_place = GetDestPlace(cups, dest_value);

        DisplayTurn(turn, cups, pickups, current_cup, dest_value, dest_place);
      }

   
      Console.WriteLine($"\n\tPart 1 Solution: {0}");
    }

    private static void Part2() {
      Console.WriteLine("   Part 2");
      string[] lines = File.ReadAllLines(Part1Input);
      Console.WriteLine("$\tRead {0} inputs", lines.Length);

      Console.WriteLine($"\n\tPart 2 Solution: {0}");
    }

    private static int ValueAdd(int value, int amount) {
      value = value - 1;
      value = (value + amount) % 10;
      value++;
      return value;
    }
    private static void DisplayTurn(int turn, int[] cups, int[] pickups, int current_cup, int dest_value, int dest_place) {
      Console.WriteLine($"\t -- move {turn} --");
      Console.Write($"\t cups: ");
      for (int i = 0; i < cups.Length; i++) {
        Console.Write(" ");
        if (i == current_cup % 10) {
          Console.Write($" ({cups[i]})");
        } else {
          Console.Write($" {cups[i]}");
        }
      }
      Console.WriteLine();
      Console.Write($"\t pick up:");
      for (int i = 0; i < pickups.Length; i++) {
        Console.Write($" {pickups[i]}");
        if (i < 2) Console.Write(",");
      }
      Console.WriteLine();
      Console.WriteLine($"\t destination: value={dest_value} place={dest_place}");
      Console.WriteLine("\n");
    }

    private static int GetDestPlace(int[] cups, int dest_value) {
      for (int i = 0; i < cups.Length; i++) {
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

    private static void ModReminder(int t = 5) {
      Console.WriteLine("\n\n");

      for (int i = 0; i < 12; i++) {
        Console.WriteLine($"\t {t.ToString().PadLeft(3)} - {i.ToString().PadLeft(3)} % 10 = {(t-i).ToString().PadLeft(3)} % 10 ={((t-i)% 10).ToString().PadLeft(3)}");
      }
      Console.WriteLine("\n\n");
      for (int i = 0; i < 12; i++) {
        Console.WriteLine($"\t {t.ToString().PadLeft(3)} - {i.ToString().PadLeft(3)} % 10 = {(t-i).ToString().PadLeft(3)} % 10 ={((t-i)% 10).ToString().PadLeft(3)}");
      }
      Console.WriteLine("\n\n");

    }

  }
}
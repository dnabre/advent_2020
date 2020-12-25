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
    private const int MOD_VALUE = 10;
    
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

      char[] input_chars = lines[0].ToCharArray();
      int[] cups = new int[MOD_VALUE];
      for (int i = 0; i < input_chars.Length; i++) {
        cups[i+1] = (int) Char.GetNumericValue(input_chars[i]);
      }
      cups[0] = int.MaxValue;
      Console.WriteLine($"\t {Utility.ArrayToStringLine(cups,1)} (length {cups.Length})\n");

      int turn;
      int current_cup;
      int[] pickups = new int[3]; ;
      
      int dest_value = -1;
      int dest_place = -1;
    ;
      /* ############################## End Initialization ############################## */

      turn = 0;
      current_cup = 1;

      int rounds = 10;

      /* ############################## End Priming the Loop ############################## */

      while (turn < rounds+1) {
        turn++;
        current_cup = mod(current_cup);

        dest_value = mod(cups[current_cup]-1);
        dest_place = GetDestPlace(cups, dest_value);
        
        for (int i = 0; i < 3; i++)
        {
          int index = mod(current_cup + i + 1);
          pickups[i] = cups[index];
          cups[index] = -cups[index];
        }
        
        while (IsValueInPickups(pickups, dest_value)) {
          dest_value = mod(dest_value -1);
        }
        
        dest_place = GetDestPlace(cups, dest_value);

        DisplayTurn(turn, cups, pickups, current_cup, dest_value, dest_place);

      
        // shift to close pick spot
        for (int i = 0; i < 9; i++)
        {
          Console.WriteLine($"\t {Utility.ArrayToStringLine(cups, 1)}  i={i}");
          if (i <6)
          {
            cups[mod(current_cup + 1 + i)] = cups[mod(current_cup + 1 + i + 3)];
            cups[mod(current_cup + 1 + i + 3)] = -1;
          }
          else
          {
            cups[mod(current_cup  + i)] = -1;
           
          } Console.WriteLine($"\t {Utility.ArrayToStringLine(cups,1)}  i={i}");
        }
    
        Console.WriteLine($"\t {Utility.ArrayToStringLine(cups)} ");

        int insert_place =  1;
        for (int i = 0; i < 6; i++)
        {
          int value = cups[insert_place];
          if (value == dest_value)
          {
            break;
          }
          else
          {
            insert_place++;
          }
        }
        Console.WriteLine($"\t {Utility.ArrayToStringLine(cups,1)} \t insert place is {insert_place}");

        for (int i = MOD_VALUE-1; i > (insert_place+3); i--)
        {
          int p_index = i - 3;
          cups[i] = cups[p_index];
          cups[p_index] = -2;
          Console.WriteLine($"\t {Utility.ArrayToStringLine(cups,1)} \t insert place is {insert_place} \t i={i}");
        }
      
        
        
        //Insert pickups at destination +1;
        for (int i = 0; i < 3; i++)
        {
          int index = mod(insert_place + 1 +i);
          cups[index] = pickups[i];
        }
      
        
        
        Console.WriteLine($"\t{Utility.ArrayToStringLine(cups,1)}");
        
        current_cup = mod(current_cup + 1);
      }

      Console.WriteLine();
      Console.WriteLine($"\t-- final --");
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
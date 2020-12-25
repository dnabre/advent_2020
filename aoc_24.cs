using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;

/*
	Solutions found:
	Part 1: 382
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
    Part2();
      watch.Stop();
      long time_part_2 = watch.ElapsedMilliseconds;
      Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
    }


    
	

    private static void Part1() {
    Console.WriteLine($"   Part 1");
      string[] lines = File.ReadAllLines(Part1Input);
      Console.WriteLine($"\tRead {lines.Length} inputs");

		Dictionary<Hex,bool> map = new Dictionary<Hex,bool>();

		foreach(String l in lines) {
			Hex h = new Hex(0,0,0);
			List<String> ops = ParseLine(l);		
			foreach(String s in ops)  {
				h.Move(s);
			}
			if(map.ContainsKey(h)) {
				bool c = map[h];

				if(c==true) {
					map[h] = false;
				} else {
					map[h] = true;;
				}
			} else {
				map[h] = false;
			}
		}

		(int white_count, int black_count) = CountMap(map);

		Console.WriteLine($"\t final map  black: {black_count} white: {white_count} ");
		
      Console.WriteLine($"\n\tPart 1 Solution: {black_count}");
    }





    private static void Part2() {
      Console.WriteLine("   Part 2");
      string[] lines = File.ReadAllLines(Part1Input);

      Console.WriteLine("$\tRead {0} inputs", lines.Length);
      Console.WriteLine($"\n\tPart 2 Solution: {0}");
    }


	private static List<String> ParseLine(String current) {
		char[] ca = current.ToCharArray();
		
		
		List<String> ops = new List<String>();

		int index =0;

		while( index < ca.Length) {
			char ch;
			ch = ca[index];
			if(ch == 's') {
				index++;
				ch = ca[index];
				if(ch=='w') ops.Add("sw");
				if(ch=='e') ops.Add("se");
			} else if (ch == 'n') {
				index++;
				ch = ca[index];
				if(ch=='w') ops.Add("nw");
				if(ch=='e') ops.Add("ne");
			} else {
				if(ch=='w') ops.Add("w");
				if(ch=='e') ops.Add("e");
			}
			index++;
		}
		return ops;
	}

	private static (int,int) CountMap(Dictionary<Hex,bool> map) {

		int white_count =0;
		int black_count =0;

		foreach(Hex h in map.Keys) {
			bool c;
			c = map[h];
			if(c==true) white_count++;
			if(c==false) black_count++;
		}

		return (white_count,black_count);
	}


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
		public Hex(int x_=0, int y_=0, int z_ =0) {
			x = x_;
			y = y_;
			z = z_;
			if(x+y+z != 0) {
				throw new Exception($"Invalid Hex coord: {ToString()}");
			}
		
		}
		public override string  ToString() {
			return $"({x},{y},{z})";
		}

		public List<Hex> GetNeighbors(Dictionary<Hex,bool> map) {
			List<Hex> neigh = new List<Hex>();
			int x2,y2,z2;
		 	Hex n_h;

			x2 = x+1;
			y2 = y+0;
			z2 = z-1;
			n_h = new Hex(x2,y2,z2);
			neigh.Add(n_h);

			x2 = x+0;
			y2 = y+1;
			z2 = z-1;
						n_h = new Hex(x2,y2,z2);
			neigh.Add(n_h);
           
			x2 = x-1;
			y2 = y+1;
			z2 = z+0;
						n_h = new Hex(x2,y2,z2);
			neigh.Add(n_h);
           
			x2 = x-1;
			y2 = y+0;
			z2 = z+1;
						n_h = new Hex(x2,y2,z2);
			neigh.Add(n_h);

			x2 = x+0;
			y2 = y-1;
			z2 = z+1;
            			n_h = new Hex(x2,y2,z2);
			neigh.Add(n_h);

			x2 = x+1;
			y2 = y-1;
			z2 = z+0;
			n_h = new Hex(x2,y2,z2);
			neigh.Add(n_h);
			if(neigh.Count != 6 ) {
				Console.WriteLine($"")

			}			
			
			HashSet<Hex> test_set new HashSet<Hex>(neigh);
			



			return neigh;

		}



		public void Move(String str) {
	
    	
        switch (str) { 
              // e, se, sw, w, nw, and ne
        case "e": 
			x = x+1;
			y = y+0;
			z = z-1;
            break;
		case "se": 
			x = x+0;
			y = y+1;
			z = z-1;
            break;
		case "sw": 
			x = x-1;
			y = y+1;
			z = z+0;
            break;
		case "w":
			x = x-1;
			y = y+0;
			z = z+1;
			break;
		case "nw": 
			x = x+0;
			y = y-1;
			z = z+1;
            break;
		case "ne": 
			x = x+1;
			y = y-1;
			z = z+0;
            break;  
        default: 
            throw new Exception($"invalid direction {str} ");
           
		}
		
		if(x+y+z != 0) {
			throw new Exception($"Invalid Hex coord: {ToString()}");
		}

		}


		

	}

  
}

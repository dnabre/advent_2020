using System;
using System.Text;
using System.Collections.Generic;


/**
 *	Utility methods used in AoC 2020 Solutions.
 *	Basically anything general purpose and/or stuff that should be in the standard library
 */

namespace advent_2020
{
	static class Utility {
			public static bool Array2DEqual<T>(T[,] a, T[,] b) {				
				(int w, int h) size_a = (a.GetLength(0), a.GetLength(1));
				(int w, int h) size_b = (b.GetLength(0), b.GetLength(1));
				if(size_a.w != size_b.w) return false;
				if(size_a.h != size_b.h) return false;
				for(int y = 0; y < size_a.h; y++) {
					for(int x=0; x < size_a.w; x++) {
						if(!a[x,y].Equals(b[x,y])) return false;
					}
				}


				return true;
			}

			public static int[] StringArrayToIntArray(String[] lines)
			{
				int[] result = new int[lines.Length];
				for (int i = 0; i < lines.Length; i++)
				{
					result[i] = int.Parse(lines[i]);
				}

				return result;
			}

			public static string[] AddToArray(string[] lines, string[] o)
			{
				String[] result = new String[lines.Length + o.Length];
				for (int i = o.Length; i < lines.Length + o.Length; i++)
				{
					result[i] = lines[i - o.Length];
				}

				for (int i = 0; i < o.Length; i++)
				{
					result[i] = o[i];
				}

				return result;
			}

			public static String MaxIntOfString(String[] inputs)
			{
				int max = int.Parse(inputs[0]);
				foreach(String i in inputs)
				{
					max = Math.Max(max, int.Parse(i));
				}

				return max.ToString();
			}
			public static String MinIntOfString(String[] inputs)
			{
				int min = int.Parse(inputs[0]);
				foreach(String i in inputs)
				{
					min = Math.Max(min, int.Parse(i));
				}

				return min.ToString();
			}

			public static long IntPow(long e_base, long power)
			{
				if (power < 0)
				{
					throw new ArgumentException($"power {power} must be greater than 0");
				}

				if (power == 0) return 1;
				long a = 1;
				while (power > 1)
				{
					if (power % 2 == 0)
					{
						e_base = e_base * e_base;
						power = power / 2;
					}
					else
					{
						a = e_base * a;
						e_base = e_base * e_base;
						power = (power - 1) / 2;
					}
				}

				return e_base * a;
			}

		public static void PrintMap(Char[,] map, bool tab=true) {
				int width = map.GetLength(0);
			int height = map.GetLength(1);
			for(int y=0; y < height; y++) {
				if(tab) Console.Write("\t");
				for(int x=0; x < width; x++) {
					Console.Write(map[x,y]);
					
				}
				Console.WriteLine();
			}
			return;
		}
		public static void PrintMap(String[] lines, bool tab=true) {

			int width = lines[0].Length;
			int height = lines.Length;
			for(int y=0; y < height; y++) {
				if(tab) Console.Write("\t");
				for(int x=0; x < width; x++) {
					char c  = lines[y][x];
					Console.Write(c);
									}
				Console.WriteLine();
			}
				return;
		}
	}
}
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
	}
}
using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;


/**
 *	Utility methods used in AoC 2020 Solutions.
 *	Basically anything general purpose and/or stuff that should be in the standard library
 */

namespace advent_2020
{
    static class Utility
    {

        public static T[] HashSetToArray<T>(HashSet<T> h_set) {
            T[] arr = new T[h_set.Count];
            h_set.CopyTo(arr);
            return arr;
        }


        public static bool Array2DEqual<T>(T[,] a, T[,] b)
        {
            (int w, int h) size_a = (a.GetLength(0), a.GetLength(1));
            (int w, int h) size_b = (b.GetLength(0), b.GetLength(1));
            if (size_a.w != size_b.w) return false;
            if (size_a.h != size_b.h) return false;
            for (int y = 0; y < size_a.h; y++)
            {
                for (int x = 0; x < size_a.w; x++)
                {
                    if (!a[x, y].Equals(b[x, y])) return false;
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
            foreach (String i in inputs)
            {
                max = Math.Max(max, int.Parse(i));
            }

            return max.ToString();
        }

        public static String MinIntOfString(String[] inputs)
        {
            int min = int.Parse(inputs[0]);
            foreach (String i in inputs)
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


        public static void PrintMap(Char[,] map, bool tab = true)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            for (int y = 0; y < height; y++)
            {
                if (tab) Console.Write("\t");
                for (int x = 0; x < width; x++)
                {
                    Console.Write(map[x, y]);
                }

                Console.WriteLine();
            }

            return;
        }

        public static void PrintMap(String[] lines, bool tab = true)
        {
            int width = lines[0].Length;
            int height = lines.Length;
            for (int y = 0; y < height; y++)
            {
                if (tab) Console.Write("\t");
                for (int x = 0; x < width; x++)
                {
                    char c = lines[y][x];
                    Console.Write(c);
                }

                Console.WriteLine();
            }

            return;
        }
        
        


        
         
        
        

        public static long ModuloInverse(long a, long m)
        {
            // result is module inverse of a with respect to m
            long m0 = m;
            long t, q;
            long x0 = 0;
            long x1 = 1;
            if (m == 1)
            {
                return 0;
            }

            while (a > 1)
            {
                q = a / m;
                t = m;
                m = a % m;
                a = t;
                t = x0;
                x0 = x1 - q * x0;
                x1 = t;
            }

            if (x1 < 0)
            {
                x1 = x1 + m0;
            }

            return x1;
        }

        public static long ChineseRemainderTheorem(long[] modules, long[] b_i)
        {
            long N = 1;
            //foreach (long n in modules) prod *= n;
            for (int i = 0; i < modules.Length; i++)
            {
                N = N * modules[i];
            }

            long result = 0;
            for (int i = 0; i < modules.Length; i++)
            {
                long N_i = N / modules[i];
                result += b_i[i] * Utility.ModuloInverse(N_i, modules[i]) * N_i;
            }

            return (result % N);
        }

        public static List<T> MergeList<T>(List<T> a, List<T> b)
        {
            HashSet<T> h_set = new HashSet<T>(a.Count + b.Count);
            foreach(T aa in a)
            {
                h_set.Add(aa);
            }

            foreach (T bb in b)
            {
                h_set.Add(bb);

            }

            List<T> result = new List<T>(h_set);
            return result;
        }

	    public static String ArrayToStringLine<T>(T[] lst)
        {
            if (lst.Length == 0) return "[]";
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach(T e in lst)
            {
                sb.Append(e.ToString());
                sb.Append(",");
            }
            
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }

		public static int[] StringToIntArray(String s) {
			int length = s.Length;
			int[] result = new int[length];
			for(int i=0; i < length;i++) {
				char c = s[i];
				result[i] = (int)(Char.GetNumericValue(c));
			}
			return result;
		}



        public static String StackToStringLine<T>(Stack<T> lst)
        {
            if (lst.Count == 0) return "[]";
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach(T e in lst)
            {
                sb.Append(e.ToString());
                sb.Append(",");
            }
            
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }

        public static void SwapStackTop<T>(Stack<T> s)
        {
            T one;
            T two;

            one = s.Pop();
            two = s.Pop();
            s.Push(one);
            s.Push(two);
        }
        
        public static String HashSetToStringLine<T>(HashSet<T> lst)
        {
            if (lst.Count == 0) return "[]";
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach(T e in lst)
            {
                sb.Append(e.ToString());
                sb.Append(",");
            }
            
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }

     
        public static String QueueToStringLine<T>(Queue<T> lst)
        {
            if (lst.Count == 0) return "[]";
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach(T e in lst)
            {
                sb.Append(e.ToString());
                sb.Append(",");
            }
            
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }


        public static String ListToStringLine<T>(List<T> lst)
        {
            if (lst.Count == 0) return "[]";
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach(T e in lst)
            {
                sb.Append(e.ToString());
                sb.Append(",");
            }
            
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }
        
        public static void PrintList<T>(List<T> lines)
        {
            
            foreach (T ln in lines)
            {
                String s;
                s = ln.ToString();
                Console.WriteLine($"\t {s}");
            }
            
        }

        public static long ListProduct(List<long> l_list)
        {
            long result = 0;
            foreach (long i in l_list)
            {
                result += i;
            }

            return result;
        }

        public static int ListProduct(List<int> i_list)
        {
            int result = 0;
            foreach (int i in i_list)
            {
                result += i;
            }

            return result;
        }
        
    }
}
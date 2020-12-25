using System;
using System.Collections.Generic;
using System.Text;


/**
 *	Utility methods used in AoC 2020 Solutions.
 *	Basically anything general purpose and/or stuff that should be in the standard library
 */

namespace advent_2020
{
    internal static class Utility
    {
        public static T[] HashSetToArray<T>(HashSet<T> h_set)
        {
            var arr = new T[h_set.Count];
            h_set.CopyTo(arr);
            return arr;
        }


        public static bool Array2DEqual<T>(T[,] a, T[,] b)
        {
            (int w, int h) size_a = (a.GetLength(0), a.GetLength(1));
            (int w, int h) size_b = (b.GetLength(0), b.GetLength(1));
            if (size_a.w != size_b.w) return false;
            if (size_a.h != size_b.h) return false;
            for (var y = 0; y < size_a.h; y++)
            for (var x = 0; x < size_a.w; x++)
                if (!a[x, y].Equals(b[x, y]))
                    return false;


            return true;
        }

        public static int[] StringArrayToIntArray(string[] lines)
        {
            var result = new int[lines.Length];
            for (var i = 0; i < lines.Length; i++) result[i] = int.Parse(lines[i]);

            return result;
        }

        public static string[] AddToArray(string[] lines, string[] o)
        {
            var result = new string[lines.Length + o.Length];
            for (var i = o.Length; i < lines.Length + o.Length; i++) result[i] = lines[i - o.Length];

            for (var i = 0; i < o.Length; i++) result[i] = o[i];

            return result;
        }

        public static string MaxIntOfString(string[] inputs)
        {
            var max = int.Parse(inputs[0]);
            foreach (var i in inputs) max = Math.Max(max, int.Parse(i));

            return max.ToString();
        }

        public static string MinIntOfString(string[] inputs)
        {
            var min = int.Parse(inputs[0]);
            foreach (var i in inputs) min = Math.Max(min, int.Parse(i));

            return min.ToString();
        }

        public static long IntPow(long e_base, long power)
        {
            if (power < 0) throw new ArgumentException($"power {power} must be greater than 0");

            if (power == 0) return 1;
            long a = 1;
            while (power > 1)
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

            return e_base * a;
        }


        public static void PrintMap(char[,] map, bool tab = true)
        {
            var width = map.GetLength(0);
            var height = map.GetLength(1);
            for (var y = 0; y < height; y++)
            {
                if (tab) Console.Write("\t");
                for (var x = 0; x < width; x++) Console.Write(map[x, y]);

                Console.WriteLine();
            }
        }

        public static void PrintMap(string[] lines, bool tab = true)
        {
            var width = lines[0].Length;
            var height = lines.Length;
            for (var y = 0; y < height; y++)
            {
                if (tab) Console.Write("\t");
                for (var x = 0; x < width; x++)
                {
                    var c = lines[y][x];
                    Console.Write(c);
                }

                Console.WriteLine();
            }
        }


        public static long ModuloInverse(long a, long m)
        {
            // result is module inverse of a with respect to m
            var m0 = m;
            long t, q;
            long x0 = 0;
            long x1 = 1;
            if (m == 1) return 0;

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

            if (x1 < 0) x1 = x1 + m0;

            return x1;
        }

        public static long ChineseRemainderTheorem(long[] modules, long[] b_i)
        {
            long N = 1;
            //foreach (long n in modules) prod *= n;
            for (var i = 0; i < modules.Length; i++) N = N * modules[i];

            long result = 0;
            for (var i = 0; i < modules.Length; i++)
            {
                var N_i = N / modules[i];
                result += b_i[i] * ModuloInverse(N_i, modules[i]) * N_i;
            }

            return result % N;
        }

        public static List<T> MergeList<T>(List<T> a, List<T> b)
        {
            var h_set = new HashSet<T>(a.Count + b.Count);
            foreach (var aa in a) h_set.Add(aa);

            foreach (var bb in b) h_set.Add(bb);

            var result = new List<T>(h_set);
            return result;
        }

        public static string ArrayToStringLine<T>(T[] lst, int skip = 0, int num = -1)
        {
            if (lst.Length == 0) return "[]";
            var sb = new StringBuilder();

            sb.Append("[");
            foreach (var e in lst)
            {
                if (skip > 0)
                {
                    skip--;
                    continue;
                }

                if (num == 0)
                    break;
                num--;
                sb.Append(e);
                sb.Append(",");
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }

        public static int[] StringToIntArray(string s)
        {
            var length = s.Length;
            var result = new int[length];
            for (var i = 0; i < length; i++)
            {
                var c = s[i];
                result[i] = (int) char.GetNumericValue(c);
            }

            return result;
        }


        public static string StackToStringLine<T>(Stack<T> lst)
        {
            if (lst.Count == 0) return "[]";
            var sb = new StringBuilder();
            sb.Append("[");
            foreach (var e in lst)
            {
                sb.Append(e);
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

        public static string HashSetToStringLine<T>(HashSet<T> lst)
        {
            if (lst.Count == 0) return "[]";
            var sb = new StringBuilder();
            sb.Append("[");
            foreach (var e in lst)
            {
                sb.Append(e);
                sb.Append(",");
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }


        public static string QueueToStringLine<T>(Queue<T> lst)
        {
            if (lst.Count == 0) return "[]";
            var sb = new StringBuilder();
            sb.Append("[");
            foreach (var e in lst)
            {
                sb.Append(e);
                sb.Append(",");
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }


        public static string ListToStringLine<T>(List<T> lst)
        {
            if (lst.Count == 0) return "[]";
            var sb = new StringBuilder();
            sb.Append("[");
            foreach (var e in lst)
            {
                sb.Append(e);
                sb.Append(",");
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }

        public static void PrintList<T>(List<T> lines)
        {
            foreach (var ln in lines)
            {
                string s;
                s = ln.ToString();
                Console.WriteLine($"\t {s}");
            }
        }

        public static long ListProduct(List<long> l_list)
        {
            long result = 0;
            foreach (var i in l_list) result += i;

            return result;
        }

        public static int ListProduct(List<int> i_list)
        {
            var result = 0;
            foreach (var i in i_list) result += i;

            return result;
        }
    }
}
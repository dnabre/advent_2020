using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;

/*
	Solutions found:
	Part 1: 382
	Part 2: 3964
	
*/

namespace advent_2020
{
    static class AOC_24
    {
        private const string Part1Input = "aoc_24_input_1.txt";
        private const string Part2Input = "aoc_24_input_2.txt";
        private const string TestInput1 = "aoc_24_test_1.txt";
        private const string TestInput2 = "aoc_24_test_2.txt";
        private const bool BLACK = false;
        private const bool WHITE = true;


        public static void Run(string[] args)
        {
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


        private static void Part1()
        {
            Console.WriteLine($"   Part 1");
            string[] lines = File.ReadAllLines(Part1Input);
            Console.WriteLine($"\tRead {lines.Length} inputs");

            Dictionary<Hex, bool> map = new Dictionary<Hex, bool>();

            foreach (String l in lines)
            {
                Hex h = new Hex(0, 0, 0);
                List<String> ops = ParseLine(l);
                foreach (String s in ops)
                {
                    h.Move(s);
                }

                if (map.ContainsKey(h))
                {
                    bool c = map[h];

                    if (c == true)
                    {
                        map[h] = false;
                    }
                    else
                    {
                        map[h] = true;
                        ;
                    }
                }
                else
                {
                    map[h] = false;
                }
            }

            (int white_count, int black_count) = CountMap(map);

            Console.WriteLine($"\t final map  black: {black_count} white: {white_count} ");

            Console.WriteLine($"\n\tPart 1 Solution: {black_count}");
        }


        [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
        private static void Part2()
        {
            Console.WriteLine("   Part 2");
            string[] lines = File.ReadAllLines(Part2Input);
            Console.Write("\tRead ");
            Console.Write(lines.Length);
            Console.WriteLine(" inputs");

            Dictionary<Hex, bool> map = new Dictionary<Hex, bool>();

            foreach (String l in lines)
            {
                Hex h = new Hex(0, 0, 0);
                List<String> ops = ParseLine(l);
                foreach (String s in ops)
                {
                    h.Move(s);
                }

                if (map.ContainsKey(h))
                {
                    bool c = map[h];

                    if (c == true)
                    {
                        map[h] = false;
                    }
                    else
                    {
                        map[h] = true;
                        ;
                    }
                }
                else
                {
                    map[h] = false;
                }
            }

            (int white_count, int black_count) = CountMap(map);
            //Console.WriteLine($"\t final map  black: {black_count} white: {white_count} ");
            //Console.WriteLine($"\t map has {map.Count} total tiles\n");

            int steps = 100;
            int done_steps = 0;

            while (steps > 0)
            {
                steps--;
                done_steps++;


                //Go through all tiles in out sparse map and add their neighbors. 
                HashSet<Hex> to_add = new HashSet<Hex>();
                foreach (Hex h in map.Keys)
                {
                    List<Hex> neigh = h.GetNeighbors();
                    foreach (Hex n in neigh)
                    {
                        if (!map.ContainsKey(n))
                        {
                            to_add.Add(n);
                        }
                    }
                }

                //    Console.WriteLine($"\t\t adding {to_add.Count} new unique Neighbors");
                foreach (Hex n in to_add)
                {
                    map[n] = true;
                }

                (white_count, black_count) = CountMap(map);
                //Console.WriteLine($"\t final map  black: {black_count} white: {white_count} ");
                //Console.WriteLine($"\t map has {map.Count} total tiles\n");
                // Map read for automata

                map = Step(map);

                (white_count, black_count) = CountMap(map);
                //  Console.WriteLine($"\t final map  black: {black_count} white: {white_count} ");
                //Console.WriteLine($"\t map has {map.Count} total tiles\n");
                if ((done_steps < 4) || (done_steps > (96)))
                    Console.WriteLine($"\t Day  {done_steps.ToString().PadLeft(5)}: {black_count}");
            }


            Console.WriteLine($"\n\tPart 2 Solution: {black_count}");
        }

        [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
        private static Dictionary<Hex, bool> Step(Dictionary<Hex, bool> map)
        {
            Dictionary<Hex, bool> new_map = new Dictionary<Hex, bool>(map.Count);
            foreach (Hex tile in map.Keys)
            {
                bool b_color = map[tile];
                //  Console.Write($"\t {tile}[{(b_color ? "white" : "black")}] ");

                List<Hex> neigh = tile.GetNeighbors();
                int black = 0;
                int white = 0;
                foreach (Hex n in neigh)
                {
                    bool c;
                    if (map.ContainsKey(n))
                    {
                        c = map[n];
                    }
                    else
                    {
                        //tiles default to white (aka true)
                        c = WHITE;
                    }

                    if (c == WHITE)
                    {
                        white++;
                    }
                    else
                    {
                        black++;
                    }
                }

                //	Console.Write($"\t white={white}, black={black}");
                bool final_color;
                if (b_color)
                {
                    //white
                    if (black == 2)
                    {
                        final_color = BLACK;
                    }
                    else
                    {
                        final_color = WHITE;
                    }
                }
                else
                {
                    //black
                    if ((black == 0) || (black > 2))
                    {
                        final_color = WHITE;
                    }
                    else
                    {
                        final_color = BLACK;
                    }
                }

                //   Console.WriteLine($" setting tile to {(b_color ? "white" : "black")}");
                new_map[tile] = final_color;
            }

            return new_map;
        }


        private static bool GetTileColor(Hex h, Dictionary<Hex, bool> map)
        {
            if (map.ContainsKey(h))
            {
                return map[h];
            }
            else
            {
                map[h] = true;
                return true;
            }
        }

        private static List<String> ParseLine(String current)
        {
            char[] ca = current.ToCharArray();


            List<String> ops = new List<String>();

            int index = 0;

            while (index < ca.Length)
            {
                char ch;
                ch = ca[index];
                if (ch == 's')
                {
                    index++;
                    ch = ca[index];
                    if (ch == 'w') ops.Add("sw");
                    if (ch == 'e') ops.Add("se");
                }
                else if (ch == 'n')
                {
                    index++;
                    ch = ca[index];
                    if (ch == 'w') ops.Add("nw");
                    if (ch == 'e') ops.Add("ne");
                }
                else
                {
                    if (ch == 'w') ops.Add("w");
                    if (ch == 'e') ops.Add("e");
                }

                index++;
            }

            return ops;
        }

        private static (int, int) CountMap(Dictionary<Hex, bool> map)
        {
            int white_count = 0;
            int black_count = 0;

            foreach (Hex h in map.Keys)
            {
                bool c;
                c = map[h];
                if (c == true) white_count++;
                if (c == false) black_count++;
            }

            return (white_count, black_count);
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

        public int x, y, z;

        public Hex(int x_ = 0, int y_ = 0, int z_ = 0)
        {
            x = x_;
            y = y_;
            z = z_;
            if (x + y + z != 0)
            {
                throw new Exception($"Invalid Hex coord: {ToString()}");
            }
        }

        public override string ToString()
        {
            return $"({x},{y},{z})";
        }

        public List<Hex> GetNeighbors()
        {
            List<Hex> neigh = new List<Hex>();
            int x2, y2, z2;
            Hex n_h;

            x2 = x + 1;
            y2 = y + 0;
            z2 = z - 1;
            n_h = new Hex(x2, y2, z2);
            neigh.Add(n_h);

            x2 = x + 0;
            y2 = y + 1;
            z2 = z - 1;
            n_h = new Hex(x2, y2, z2);
            neigh.Add(n_h);

            x2 = x - 1;
            y2 = y + 1;
            z2 = z + 0;
            n_h = new Hex(x2, y2, z2);
            neigh.Add(n_h);

            x2 = x - 1;
            y2 = y + 0;
            z2 = z + 1;
            n_h = new Hex(x2, y2, z2);
            neigh.Add(n_h);

            x2 = x + 0;
            y2 = y - 1;
            z2 = z + 1;
            n_h = new Hex(x2, y2, z2);
            neigh.Add(n_h);

            x2 = x + 1;
            y2 = y - 1;
            z2 = z + 0;
            n_h = new Hex(x2, y2, z2);
            neigh.Add(n_h);
            if (neigh.Count != 6)
            {
                Console.WriteLine(
                    $"Neighbor count is off (6 expected, found {neigh.Count} {Utility.ListToStringLine(neigh)}");
                Environment.Exit(0);
            }

            HashSet<Hex> test_set = new HashSet<Hex>(neigh);
            if (test_set.Count != 6)
            {
                Console.WriteLine(
                    $"Neighbor count is off (6 expected, found {test_set.Count} unique " +
                    $"{Utility.HashSetToStringLine(test_set)}, full list: {Utility.ListToStringLine(neigh)}");
                Environment.Exit(0);
            }

            return neigh;
        }


        public void Move(String str)
        {
            switch (str)
            {
                // e, se, sw, w, nw, and ne
                case "e":
                    x = x + 1;
                    y = y + 0;
                    z = z - 1;
                    break;
                case "se":
                    x = x + 0;
                    y = y + 1;
                    z = z - 1;
                    break;
                case "sw":
                    x = x - 1;
                    y = y + 1;
                    z = z + 0;
                    break;
                case "w":
                    x = x - 1;
                    y = y + 0;
                    z = z + 1;
                    break;
                case "nw":
                    x = x + 0;
                    y = y - 1;
                    z = z + 1;
                    break;
                case "ne":
                    x = x + 1;
                    y = y - 1;
                    z = z + 0;
                    break;
                default:
                    throw new Exception($"invalid direction {str} ");
            }

            if (x + y + z != 0)
            {
                throw new Exception($"Invalid Hex coord: {ToString()}");
            }
        }
    }
}
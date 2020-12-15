using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;


/*
	Solutions found:
	Part 1: 3059488894985
	Part 2: 2900994392308
	
*/

namespace advent_2020
{
    static class AOC_14
    {
        private const string Part1Input = "aoc_14_input_1.txt";
        private const string Part2Input = "aoc_14_input_2.txt";
        private const string TestInput1 = "aoc_14_test_1.txt";
        private const string TestInput2 = "aoc_14_test_2.txt";

        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 14");
            var watch = Stopwatch.StartNew();
            Part1(args);
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = Stopwatch.StartNew();
            Part2(args);
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }

        public class Mask
        {
            public Dictionary<int, char> bits;

            private bool IsMaskNull()
            {
                return (bits.Count == 0);
            }

            public Mask(Dictionary<int, char> bits)
            {
                this.bits = new Dictionary<int, char>(bits);
            }

            public Mask(String mask = null)
            {
                if (this.bits == null)
                {
                    this.bits = new Dictionary<int, char>();
                }

                if (mask.Length > 36)
                {
                    String[] parts = mask.Split(' ');
                    mask = parts[2];
                }


                for (int i = 0; i < mask.Length; i++)
                {
                    char ch = mask[i];
                    if ((ch == '0') || (ch == '1'))
                    {
                        bits[i] = ch;
                    }
                }
            }

            public override String ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("MASK = ");
                for (int i = 0; i < 36; i++)
                {
                    if (bits.ContainsKey(i))
                    {
                        sb.Append(bits[i]);
                    }
                    else
                    {
                        sb.Append("X");
                    }
                }

                return sb.ToString();
            }

            public long Apply(long value)
            {
                if (IsMaskNull()) return value;
                char[] a_v = Convert.ToString(value, 2).PadLeft(36, '0').ToCharArray();

                for (int i = 0; i < 36; i++)
                {
                    if (bits.ContainsKey(i))
                    {
                        a_v[i] = bits[i];
                    }
                }

                String result = new String(a_v);

                long v = Convert.ToInt64(result, 2);
                return v;
                //return Convert.ToInt64(a_v.ToString(),2);
            }
        }

        public class Mask2
        {
            public char[] mask;


            public Mask2(String input)
            {
                String m = "";

                String[] parts = input.Split(' ');
                m = parts[2];

                if (m.Length != 36)
                {
                    throw new ArgumentException(m);
                }


                this.mask = m.ToCharArray();
            }

            public override String ToString()
            {
                return new String(this.mask);
            }


            public HashSet<long> Apply(long value)
            {
                Queue<(char[] str, char[] m)> to_process = new Queue<(char[], char[])>();
                char[] a = Convert.ToString(value, 2).PadLeft(36, '0').ToCharArray();
                char[] b = (char[]) mask.Clone();
                to_process.Enqueue((a, b));
                HashSet<long> addresses = new HashSet<long>();

                while (to_process.Count > 0)
                {
                    char[] a_v;
                    char[] mask;
                    (a_v, mask) = to_process.Dequeue();
                    //  Console.WriteLine($"\t processing addr: {new String(a_v)} with mask {new String(mask)} ");
                    bool addr_done = true;
                    for (int i = 0; i < 36; i++)
                    {
                        char ch = mask[i];
                        if (ch == '0')
                        {
                            // bit is unchanged
                        }
                        else if (ch == '1')
                        {
                            a_v[i] = '1';
                            mask[i] = '0';
                            //    Console.WriteLine($"\t processing addr: {new String(a_v)} with mask {new String(mask)} mask[{i}] <- 0 ");
                        }
                        else if (ch == 'X')
                        {
                            char[] a_v_1, a_v_0;
                            char[] m_0, m_1;
                            a_v_0 = (char[]) a_v.Clone();
                            a_v_1 = (char[]) a_v.Clone();
                            m_0 = (char[]) mask.Clone();
                            m_1 = (char[]) mask.Clone();

                            a_v_0[i] = '1';
                            a_v_1[i] = '0';
                            m_0[i] = '0';
                            m_1[i] = '0';

                            //     Console.WriteLine($"\t enqueing addr: {new String(a_v_0)} with mask {new String(m_0)} ");
                            to_process.Enqueue((a_v_0, m_0));
                            //    Console.WriteLine($"\t enqueing addr: {new String(a_v_1)} with mask {new String(m_1)} ");
                            to_process.Enqueue((a_v_1, m_1));
                            addr_done = false;
                            break;
                        }
                        else
                        {
                            throw new Exception($"invalid char {ch} in mask {mask}[{i}]");
                        }
                    }

                    if (addr_done)
                    {
                        DoubleCheck(mask);
                        String result = new String(a_v);
                        long v = Convert.ToInt64(result, 2);
                        addresses.Add(v);
                    }
                }

                return addresses;
            }
        }

        private static void DoubleCheck(char[] c)
        {
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] != '0')
                {
                    throw new Exception($"Mask not clear {new String(c)}");
                }
            }
        }

        class Assignment
        {
            public long index;
            public long value;


            public Assignment(String m)
            {
                (long i, long v) = ParseAssign(m);
                this.index = i;
                this.value = v;
            }

            public Assignment(long index, long value)
            {
                this.index = index;
                this.value = value;
            }

            public String ToBitString()
            {
                String num = Convert.ToString(value, 2);
                /*
                if (num.Length > 36)
                {
                    num = num.Remove(0, (num.Length - 36));
                }
                else if (num.Length < 36)
                {
                    num = num.PadRight(36);
                }
*/
                return num.PadLeft(36, '0');
            }

            public override String ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("mem[");
                sb.Append(index.ToString());
                sb.Append("] = ");
                sb.Append(value.ToString());
                return sb.ToString();
            }
        }


        private static (long, long) ParseAssign(String m)
        {
            String[] parts = m.Split(' ');
            long value = long.Parse(parts[2]);
            String index_string = parts[0];
            index_string = index_string.Remove(index_string.Length - 1, 1);
            index_string = index_string.Remove(0, 4);
            int index = int.Parse(index_string);
            return (index, value);
        }

        private static Mask current_mask = null;
        private static Mask2 current_mask2 = null;
        private static Dictionary<long, long> RAM = null;

        private static void AssignToRam(long index, long value, Mask m)
        {
            String m_string;
            long value_to_use = 0;
            if (m == null)
            {
                value_to_use = value;
                m_string = "none";
            }
            else
            {
                value_to_use = m.Apply(value);
                m_string = m.ToString();
            }

            RAM[index] = value_to_use;


            // Console.WriteLine($"\tSetting RAM[{index}] = {value_to_use} \t\t current Mask = {m_string}");


            return;
        }

        private static void AssignToRam2(long index, long value, Mask2 m)
        {
            String m_string;
            HashSet<long> indexes = new HashSet<long>(1);

            if (m == null)
            {
                indexes.Add(index);
                m_string = "none";
            }
            else
            {
                indexes = m.Apply(index);
                m_string = m.ToString();
            }

            //   Console.WriteLine($"index: {index}, value: {value}, address number: {indexes.Count} mask: {m}");
            foreach (long id in indexes)
            {
                RAM[id] = value;
                //     Console.WriteLine($"\tSetting RAM[{id}] = {value} \t\t current Mask = {m_string}");
            }

            return;
        }

        private static void Part1(string[] args)
        {
            Console.WriteLine("   Part 1");
            string[] lines = File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            RAM = new Dictionary<long, long>();
            foreach (String ln in lines)
            {
                if (ln[1] == 'e')
                {
                    Assignment a = new Assignment(ln);
                    AssignToRam(a.index, a.value, current_mask);
                }
                else
                {
                    if (ln.Contains("1") || ln.Contains("0"))
                    {
                        Mask m = new Mask(ln);
                        current_mask = m;
                    }
                    else
                    {
                        current_mask2 = null;
                    }
                }
            }

            long sum = 0;
            foreach (long k in RAM.Keys)
            {
                sum = sum + RAM[k];
            }

            Console.WriteLine($"\n\tPart 1 Solution: {sum}");
        }

        private static void Part2(string[] args)
        {
            Console.WriteLine("   Part2");
            String[] lines = File.ReadAllLines(Part2Input);

            //Console.WriteLine("\tRead {0} inputs", lines.Length);

            RAM = new Dictionary<long, long>();

            foreach (String ln in lines)
            {
                //Console.WriteLine($"\t{ln}");
                if (ln[1] == 'e')
                {
                    // Console.WriteLine("\t assign ");
                    Assignment a = new Assignment(ln);
                    AssignToRam2(a.index, a.value, current_mask2);
                    //Console.WriteLine($"\t{a.ToString()} \n\tmask-> {a.ToBitString()}");
                }
                else
                {
                    //Console.WriteLine("\t mask ");
                    if (ln.Contains("X") || ln.Contains("1"))
                    {
                        Mask2 m = new Mask2(ln);
                        current_mask2 = m;
                    }
                    else
                    {
                        current_mask2 = null;
                    }

                    //   Console.WriteLine($"\t{current_mask2}");
                }
            }

            long sum = 0;
            foreach (long k in RAM.Keys)
            {
                sum = sum + RAM[k];
            }

            Console.WriteLine($"\n\tPart 2 Solution: {sum}");
        }
    }
}
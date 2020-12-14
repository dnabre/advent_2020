using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;


/*
	Solutions found:
	Part 1:
	Part 2: 
	
*/

namespace advent_2020
{
    static class AOC_14 {
        private const string Part1Input = "aoc_14_input_1.txt";
        private const string Part2Input = "aoc_14_input_2.txt";
        private const string TestInput1 = "aoc_14_test_1.txt";
        private const string TestInput2 = "aoc_14_test_2.txt";

        public static void Run (string[] args) {
            Console.WriteLine ("AoC Problem 14");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Part1(args);
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
   //         Part2(args);
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");

        }

        public class Mask
        {
	        public Dictionary<int, int> bits;

	        public Mask(Dictionary<int, int> bits)
	        {
		        this.bits = new Dictionary<int, int>(bits);
	        }

	        public Mask(String mask)
	        {
		        if (this.bits == null)
		        {
			        this.bits = new Dictionary<int, int>();
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
				        bits[i] = int.Parse(ch.ToString());
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
	        
        }

        class Assignment
        {
	        public int index;
	        public long value;


	        public Assignment(String m)
	        {
		        (int i, long v) = ParseAssign(m);
		        this.index = i;
		        this.value = v;
	        }
	        public Assignment(int index, long value)
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
		        return num.PadLeft(36,'0');
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

	        public long ApplyMask(Mask m)
	        {


		        return value;
	        }
        }
        
        
        private static (int, long) ParseAssign(String m)
        {
	        String[] parts = m.Split(' ');
	        long value = long.Parse(parts[2]);
	        String index_string = parts[0];
	        index_string = index_string.Remove(index_string.Length - 1, 1);
	        index_string = index_string.Remove(0, 4);
	        int index = int.Parse(index_string);
	        return (index, value);
        }
        
        private static void Part1(string[] args) {
            Console.WriteLine("   Part 1");
            string[] lines =  System.IO.File.ReadAllLines(TestInput1);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            
            foreach (String ln in lines)
            {
	            Console.WriteLine($"\t{ln}");
	            if (ln[1] == 'e')
	            {
		            Console.WriteLine("\t assign ");
		            Assignment a = new Assignment(ln);     
		            Console.WriteLine($"\t{a.ToString()} \n\tmask-> {a.ToBitString()}");
	            }
	            else
	            {
		            Console.WriteLine("\t mask ");
		            Mask m = new Mask(ln);
		            Console.WriteLine($"\t{m}");
		            

	            }
	        
            }
        


        Console.WriteLine($"\n\tPart 1 Solution: {0}");	
        }

        private static void Part2(string[] args) {
            Console.WriteLine("   Part2");
            string[] lines =  System.IO.File.ReadAllLines(Part2Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
	
		Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }
}
}



using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.AccessControl;


/*
	Solutions found:
	Part 1: 731031916
	Part 2: 
	
*/

namespace advent_2020
{
    
    static class AOC_09
    {
        private const string Part1Input = "aoc_09_input_1.txt";
        private const string Part2Input = "aoc_09_input_2.txt";
        private const string TestInput1 = "aoc_09_test_1.txt";
        private const string TestInput2 = "aoc_09_test_2.txt";

      
        public static void Run(string[] args)
        {
           long part1_answer;
		   
		    Console.WriteLine("AoC Problem 09");
            part1_answer = Part1(args);
            Console.Write("\n");
            //part1_answer = 127L;
			Part2(part1_answer, args);
        }


        private static HashSet<long> getAllPartsLast5(long[] nums, int start)
        {
            HashSet<long> all_sums = new HashSet<long>();
            long[] five = new long[25];
            for (int q = 0; q <25; q++)
            {
                five[q] = nums[start + q];
            }
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j <25; j++)
                {
                    if (i == j) {
                        continue;
                    }
                    else
                    {
                        all_sums.Add(five[i] + five[j]);
                    }
                }
            }
            return all_sums;
        } 


        private static long Part1(string[] args)
        {
            Console.WriteLine("   Part 1");
            String[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
			
            long[] nums = new long[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                nums[i] = long.Parse(lines[i]);
            }

            long solution = -1;
            for (int n = 25; n < nums.Length; n++)
            {
                HashSet<long> sums = getAllPartsLast5(nums, n - 25);
                if (sums.Contains(nums[n]))
                {
                //    Console.WriteLine($"\t {nums[n]} is a sum");
                }
                else
                {
                    //Console.WriteLine($"\t {nums[n]} is NOT sum");
                    solution = nums[n];
                  
                    
                    Console.WriteLine($"\n\tPart 1 Solution: {solution}");
                  return solution;
                }
            }
            
            

            Console.WriteLine($"\n\tPart 1 Solution: {solution}");
			return solution;
        }

    
        private static void Part2(long target, string[] args)
        {
            Console.WriteLine("   Part 2");
            String[] lines = System.IO.File.ReadAllLines(Part2Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
			Console.Write($"\t Magic number from Part 1 is {target}");

			long[] numbers = new long[lines.Length];
			for(int i=0; i < lines.Length; i++) {
					numbers[i] = long.Parse(lines[i]);
					if(numbers[i] == target) {
						Console.WriteLine($" with index numbers[{i}]");
					}
					numbers[i] = target +1;
			}
			
			

			foreach(long l in sols) {
				for(int i=0; i < numbers.Length; i++){
					if(number[i] == l) {
						Console.WriteLine($"\t{i} is {l}");
						i=numbers.Length;
					}
				}
			}

			System.Environment.Exit(0)

			int start; int length;
			//(start,length) = findSequence(target, numbe	

			
			//(start,length) = bruteSequence(target, numbers);
						
			long min=numbers[start];
			long max=numbers[start];
			for(int i=start; i < start+length; i++) {
				Console.WriteLine($"\tnumbers[{i}] = {numbers[i]}");
				min=Math.Min(min,numbers[i]);
				max=Math.Max(max,numbers[i]);

			}

			(int s, int e) = (start,start+length);
			Console.WriteLine($"(start,length,end) = {(start,length, start+length-1)}")


			long test;
			test = naive_sum(numbers,start,length);

			long test2 = 0;
			

			Console.WriteLine($"\t found solution: target={target} test of sum: {test}");

			Console.WriteLine($"\t{(s,e)}\t length={length}\t{(numbers[start],numbers[start+length-1])}");
			Console.WriteLine($"\t min = {min}, max = {max} sum = {min+max}");
			// 2301111897 is too high
			// 92538328 is too low
			// answer is 93396727
          

            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }

		private static long naive_sum(long[] numbers, int start, int length) {
			long result=0;
			for(int i =0; i<length; i++ ){
				result = numbers[start+i];
			}
			return result;
		}

		
		private static (int start, int length) findSequence(long target, long[] numbers) {
			int range_start, range_length;
			long current_sum = 0L;
			range_start=0;
			do
			{

				range_length = 2; 
				current_sum = naive_sum(numbers, range_start,range_length);
				while(current_sum < target) {
			//		Console.WriteLine($"\tsum({range_start},{range_start+range_length})={current_sum}");

					range_length++;
					current_sum +=numbers[range_start+range_length-1];
				}
				if(current_sum == target) {
					Console.WriteLine($"\tsum({range_start},{range_start+range_length})={current_sum} == {target}");
					return (range_start,range_length);
				}
				range_start++;
			} while ((range_start < numbers.Length-1) && (range_start+range_length < numbers.Length));
			return (0, 0);
		}
		
		private long[] sols = {30684790, 
31929175, 
32111531, 
33239826, 
34484211, 
34933587, 
35048416, 
35733886, 
37317716, 
37982233, 
51019481, 
51993598, 
52688226, 
53564537, 
53833200, 
61755566, 
62711937,}

		private static (int start, int length) bruteSequence(long target, long[] numbers) {
			int max_length = numbers.Length;
			long sum=0;
			for(int start=0; start < numbers.Length; start++) {
				sum=0;
				for(int length =2; (length < numbers.Length) && (start+length < numbers.Length) && (sum <target); length++) {
					sum = naive_sum(numbers,start,length);
					if(sum==target){
						Console.WriteLine($"\tsum({start},{start+length}, {length})={sum} == {target}");
						return (start,length);
					}

				}
			}	
			return (0,0);
    	}
	}
}


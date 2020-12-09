using System;
using System.Text;
using System.Collections.Generic;



/*
	Solutions found:
	Part 1: 731031916
	Part 2: 93396727
	
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
            }
       
           
            
            (int start, int length) = findSequence(target, numbers);
            if ((start == 0) && (length == 0))
            {
                Console.WriteLine($"\t findSequence failed to find solution");
            }
            else
            {
                Console.WriteLine($"\t find returned {(start, length)}");
            }

            long min=numbers[start];
            long max=numbers[start];
           
            for(int i=start; i < start+length; i++)
            {
                min=Math.Min(min,numbers[i]);
                max=Math.Max(max,numbers[i]);
            }

            //Double check that range is the right sum
            long test = naive_sum(numbers,start,length);
       
			
            if(test != target)  Console.WriteLine($"\t found solution: target={target} test of sum: {test}");

            Console.Write($"\t{(start, start+length-1)}\t length={length}\t{(numbers[start],numbers[start+length-1])}");
            Console.WriteLine($"\t min = {min}, max = {max} sum = {min+max}");
            // 2301111897 is too high
            // 92538328 is too low
            // answer is 93396727
          

            Console.WriteLine($"\n\tPart 2 Solution: {min+max}");
        }

        private static long naive_sum(long[] numbers, int start, int length) {
            long result=0;
            for(int i =0; i<length; i++ ){
                result += numbers[start+i];
            }
            return result;
        }

		
        private static (int start, int length) findSequence(long target, long[] numbers) {
            int range_start, range_length;
            long current_sum = 0L;
            range_start=-1;
            do
            {
                range_start++;
                range_length = 2; 
                current_sum = naive_sum(numbers, range_start,range_length);
                while (current_sum < target)
                {
                    range_length++;
                    current_sum +=numbers[range_start+range_length-1];
                }
                if(current_sum == target) {
                    return (range_start,range_length-1);
                }
                range_start++;
            } while ((range_start < numbers.Length-1) && (range_start+range_length < numbers.Length));
            return (0, 0);
        }

   
    }
}
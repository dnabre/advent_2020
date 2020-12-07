using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;


/*
	Solutions found:
	Part 1: 911
	Part 2: 629
	
*/

namespace advent_2020
{
    static class AOC_05
    {
        private const string Part1Input = "aoc_05_input_1.txt";
        private const string Part2Input = "aoc_05_input_2.txt";
        private const string TestInput1 = "aoc_05_test_1.txt";
        private const string TestInput2 = "aoc_05_test_2.txt";
        private const int max_row = 127;
        private const int min_row = 0;
        private const int max_col = 7;
        private const int min_col = 0;

        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 05");
            Part1(args);
            Console.Write("\n");
            Part2(args);
        }

        private static (int low, int high) BinaryPartition(int min, int max, char ch)
        {
            int count = max - min + 1;
            int half = count / 2;
            if ((ch == 'F') || (ch == 'L'))
            {
                // lower half
                max = max - half;
            }
            else if ((ch == 'B') || (ch == 'R'))
            {
                //upper half
                min = min + half;
            }
            else
            {
                Console.WriteLine($"Unknown partition direction {ch}");
                System.Environment.Exit(-1);
            }


            return (min, max);
        }

        private static void DecodeSeat(String input, out int row, out int col, out int ID)
        {
			/*
		    var wording = new Dictionary<char, string>
            {
                {'F', "lower half"},
                {'B', "upper half"},
                {'R', "upper half"},
                {'L', "lower half"}
            };
            Console.WriteLine($"\tTest input: |{input}| Length: {input.Length}");
			*/
            (int lo, int hi) row_range = (min_row, max_row);
            (int lo, int hi) col_range = (min_col, max_col);

            //    Console.WriteLine($"\tStart by considering the whole range, rows {row.lo} through {row.hi}");
            for (int i = 0; i < 7; i++)
            {
                char ch = input[i];
                (int n_l, int n_h) = BinaryPartition(row_range.lo, row_range.hi, ch);
                //      Console.WriteLine($"\t{ch} means to take the {wording[ch]}, keeping rows {n_l} through {n_h} ");
                row_range = (n_l, n_h);
            }

            //Console.WriteLine($"\n\tStart by considering the whole range, columns {col.lo} through {col.hi}.");
            for (int i = 7; i < 10; i++)
            {
                char ch = input[i];
                (int n_l, int n_h) = BinaryPartition(col_range.lo, col_range.hi, ch);
                //  Console.WriteLine($"\t{ch} means to take the {wording[ch]}, keeping rows {n_l} through {n_h} ");
                col_range = (n_l, n_h);
            }

            row = row_range.lo;
            col = col_range.lo;
            ID = (row * 8) + col;
            return;
        }


        private static void Part1(string[] args)
        {
            Console.WriteLine("   Part 1");
            String[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);


            int max_ID = 0;
            foreach (String input in lines)
            {
                int my_row, my_col, my_ID;
                DecodeSeat(input, out my_row, out my_col, out my_ID);
                max_ID = Math.Max(my_ID, max_ID);
                //     Console.WriteLine($"\t- {input}: row {my_row}, column {my_col}, seat ID {my_ID}.");
            }


            Console.WriteLine($"\n\tPart 1 Solution: {max_ID}");
        }

        private static void Part2(string[] args)
        {
            Console.WriteLine("   Part 2:");
            String[] lines = System.IO.File.ReadAllLines(Part2Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            bool[,] seats = new bool[128, 8];


            foreach (String input in lines)
            {
                int my_row, my_col, my_ID;
                DecodeSeat(input, out my_row, out my_col, out my_ID);
                seats[my_row, my_col] = true;
                //     Console.WriteLine($"\t- {input}: row {my_row}, column {my_col}, seat ID {my_ID}.");
            }


            for (int r = 0; r <= max_row; r++)
            {
                if ((!seats[r, 0]) && (!seats[r, 1]))
                {
                    // row r is all empty so fill it with place holders
                    for (int c = 0; c <= max_col; c++)
                    {
                        seats[r, c] = true;
                    }
                }
            }

            // PrintSeats(seats);

            (int r, int c) my_seat = (-1, -1);
            for (int r = 0; r <= max_row; r++) 
            {
                for (int c = 0; c <= max_col; c++)
                {
                    if (!seats[r, c])
                    {
                        my_seat = (r, c);
                        break;
                    }
                }
            }

            int ID = (my_seat.r * 8) + my_seat.c;


            Console.WriteLine($"\n\tPart 2 Solution: {ID}");
        }

        private static void PrintSeats(bool[,] seats)
        {
            for (int y = 0; y < seats.GetLength(1); y++)
            {
                Console.Write("\t");
                for (int x = 0; x < seats.GetLength(0); x++)
                {
                    if (seats[x, y]) Console.Write("X");
                    else Console.Write("0");
                }

                Console.WriteLine();
            }
        }
    }
}
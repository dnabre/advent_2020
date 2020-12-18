using System;
using System.CodeDom;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;


/*
	Solutions found:
	Part 1: 301
	Part 2: 2424
	
*/

namespace advent_2020
{
    static class AOC_17
    {
        private const string Part1Input = "aoc_17_input_1.txt";
        private const string Part2Input = "aoc_17_input_2.txt";
        private const string TestInput1 = "aoc_17_test_1.txt";
        private const string TestInput2 = "aoc_17_test_2.txt";

        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 17");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Part1();
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
            Part2();
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }


        private class Point : IEquatable<Point>
        {
            public bool Equals(Point other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return x == other.x && y == other.y && z == other.z;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof(Point)) return false;
                return Equals((Point) obj);
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

            public static bool operator ==(Point left, Point right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(Point left, Point right)
            {
                return !Equals(left, right);
            }

            public readonly int x;
            public readonly int y;
            public readonly int z;

            public Point(int x, int y, int z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }


            static public int GetNeighbors(char[,,] board, int p_x, int p_y, int p_z)
            {
                int count = 0;

                //  Console.WriteLine($"-> {(p_x,p_y,p_z)}");
                for (int z = -1; z <= 1; z++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        for (int x = -1; x <= 1; x++)
                        {
                           
                            
                            if ((0 <= (p_z+z)) && ((p_z+z) < depth) &&
                                (0 <= (p_y+y)) && ((p_y+y) < height) &&
                                (0 <= (p_x+x)) && ((p_x+x) < width)  
                            )
                            {
                                if ((x == 0) && (y == 0) && (z == 0))
                                {
                                    continue;
                                }
                                else
                                {
                                    if (board[p_x + x, p_y + y, p_z + z] == '#') count++;
                                }
                            }
                        }
                    }
                }

                return count;
            }
            
            static public int GetNeighbors(char[,,,] board, int p_x, int p_y, int p_z, int p_w)
            {
                int count = 0;

                //  Console.WriteLine($"-> {(p_x,p_y,p_z)}");
                for (int w = -1; w <= 1; w++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            for (int x = -1; x <= 1; x++)
                            {


                                if ((0 <= (p_z + z)) && ((p_z + z) < depth) &&
                                    (0 <= (p_y + y)) && ((p_y + y) < height) &&
                                    (0 <= (p_x + x)) && ((p_x + x) < width) &&
                                    (0 <= (p_w + w)) && ((p_w + w) < blueth)
                                )
                                {
                                    if ((x == 0) && (y == 0) && (z == 0) && (w==0))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (board[p_x + x, p_y + y, p_z + z,p_w+w] == '#') count++;
                                    }
                                }
                            }
                        }
                    }
                }

                return count;
            }
        }


        private static int height;
        private static int width;
        private static int depth;
        private static int blueth;

        private static int slide = 10;

        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            height = 10 + lines.Length + 10;
            width = 10 + lines[0].Length + 10;
            depth = Math.Max(height, width);

            char[,,] board = new char[height + 1, width + 1, depth + 1];


            int parse_z = 0;
            for (int y = 0; y < lines.Length; y++)
            {
                char[] row = lines[y].ToCharArray();
                for (int x = 0; x < lines[0].Length; x++)
                {
                    char ch = row[x];
                    board[x + slide, y + slide, parse_z + slide] = ch;
                }
            }
         //   Console.WriteLine();

         //   PrintMap(board);
            int steps = 0;
            steps++;
            // int test = Point.GetNeighbors(board, 1 + slide, 2 + slide, -1 + slide);
            while(steps <= 6)
            {
            //    Console.WriteLine($"----------step={steps}-----------------");
                board = Step(board);
                //         PrintMap(board);
                steps++;
            }

            int answer = CountCube(board);

            Console.WriteLine($"\n\tPart 1 Solution: {answer}");
        }

        private static void Part2()
        {
            Console.WriteLine("   Part 2");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            height = 10 + lines.Length + 10;
            width = 10 + lines[0].Length + 10;
            depth = Math.Max(height, width);
            blueth = depth + 1;

            char[,,,] board = new char[height + 1, width + 1, depth + 1, blueth+1];

            int parse_w = 0;
            int parse_z = 0;
            for (int y = 0; y < lines.Length; y++)
            {
                char[] row = lines[y].ToCharArray();
                for (int x = 0; x < lines[0].Length; x++)
                {
                    char ch = row[x];
                    board[x + slide, y + slide, parse_z + slide, parse_w + slide] = ch;
                }
            }
          //  Console.WriteLine();

            //PrintMap(board);
            int steps = 0;
            steps++;
            // int test = Point.GetNeighbors(board, 1 + slide, 2 + slide, -1 + slide);
            while(steps <= 6)
            {
           //     Console.WriteLine($"----------step={steps}-----------------");
                board = Step(board);
                //         PrintMap(board);
                steps++;
            }

            int answer = CountCube(board);

            Console.WriteLine($"\n\tPart 2 Solution: {answer}");
        }

        private static int CountCube(char[,,] board)
        {
            int count = 0;
            for (int z = 0; z < depth; z++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (board[x, y, z] == '#') count++;
                    }

                }
            }

            return count;
        }

        private static int CountCube(char[,,,] board)
        {
            int count = 0;
            for(int w =0; w < blueth; w++)
            {
                for (int z = 0; z < depth; z++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            if (board[x, y, z, w] == '#') count++;
                        }

                    }
                }
            }
            return count;
        }
        
        private static char[,,] Step(char[,,] board)
        {
            char[,,] new_board = new char[height+1, width+1, depth+1];
            for (int z = 0; z < depth; z++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        char cube = board[x, y, z];
                        int neigh_count = Point.GetNeighbors(board, x, y, z);
                        if (cube == '#')
                        {
                            if ((neigh_count == 2) || (neigh_count == 3))
                            {
                                new_board[x, y, z] = '#';
                            }
                            else
                            {
                                new_board[x, y, z] = '.';
                            }
                        }
                        else // cube == '.' 
                        {
                            if (neigh_count == 3)
                            {
                                new_board[x, y, z] = '#';
                            }
                            else
                            {
                                new_board[x, y, z] = '.';
                            }
                        }
                    }
                }
            }

            return new_board;
        }

        
        
        private static char[,,,] Step(char[,,,] board)
        {
            char[,,,] new_board = new char[height+1, width+1, depth+1, blueth+1];
            for (int w = 0; w < blueth; w++)
            {
                for (int z = 0; z < depth; z++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            char cube = board[x, y, z, w];
                            int neigh_count = Point.GetNeighbors(board, x, y, z,w);
                            if (cube == '#')
                            {
                                if ((neigh_count == 2) || (neigh_count == 3))
                                {
                                    new_board[x, y, z,w] = '#';
                                }
                                else
                                {
                                    new_board[x, y, z,w] = '.';
                                }
                            }
                            else // cube == '.' 
                            {
                                if (neigh_count == 3)
                                {
                                    new_board[x, y, z,w] = '#';
                                }
                                else
                                {
                                    new_board[x, y, z,w] = '.';
                                }
                            }
                        }
                    }
                }
            }

            return new_board;
        }
        
        
        private static void PrintMap(char[,,] board)
        {
            int x_min = Int32.MaxValue;
            int y_min = Int32.MaxValue;
            int z_min = Int32.MaxValue;
            
            int x_max = Int32.MinValue;
            int y_max = Int32.MinValue;
            int z_max = Int32.MinValue;


            for (int z = 0; z < depth; z++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        
                        
                        if (board[x, y, z] == '#')
                        {
                            x_min = Math.Min(x_min, x);
                            y_min = Math.Min(y_min, y);
                            z_min = Math.Min(z_min, z);

                            x_max = Math.Max(x_max, x);
                            y_max = Math.Max(y_max, y);
                            z_max = Math.Max(z_max, z);
                        }
                        
                    }
                }
            }

            
            //     Console.WriteLine();
            //     Console.WriteLine($"\t x: {x_min},{x_max}");
            //     Console.WriteLine($"\t y: {y_min},{y_max}");
            //     Console.WriteLine($"\t z: {z_min},{z_max}");
            
            for (int z = z_min; z <= z_max; z++)
            {
                Console.WriteLine($"\tz={z}");
                for (int y = y_min; y <= y_max; y++)
                {
                    Console.Write("\t");
                    for (int x = x_min; x <= x_max; x++)
                    {
                        char ch = board[x, y, z];
                        Console.Write(ch);
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }
        
        private static void PrintMap(char[,,,] board)
        {
            int x_min = Int32.MaxValue;
            int y_min = Int32.MaxValue;
            int z_min = Int32.MaxValue;
            int w_min = Int32.MaxValue;
            
            int x_max = Int32.MinValue;
            int y_max = Int32.MinValue;
            int z_max = Int32.MinValue;
            int w_max = Int32.MinValue;


            for (int w = 0; w < blueth; w++)
            {
                for (int z = 0; z < depth; z++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {


                            if (board[x, y, z,w] == '#')
                            {
                                x_min = Math.Min(x_min, x);
                                y_min = Math.Min(y_min, y);
                                z_min = Math.Min(z_min, z);
                                w_min = Math.Min(w_min, w);
                                
                                x_max = Math.Max(x_max, x);
                                y_max = Math.Max(y_max, y);
                                z_max = Math.Max(z_max, z);
                                w_max = Math.Max(w_max, w);
                            }

                        }
                    }
                }
            }


            //     Console.WriteLine();
            //     Console.WriteLine($"\t x: {x_min},{x_max}");
            //     Console.WriteLine($"\t y: {y_min},{y_max}");
            //     Console.WriteLine($"\t z: {z_min},{z_max}");
            for (int w = w_min; w <= w_max; w++)
            {
                for (int z = z_min; z <= z_max; z++)
                {
                    Console.WriteLine($"\tz={z}, w={w}");
                    for (int y = y_min; y <= y_max; y++)
                    {
                        Console.Write("\t");
                        for (int x = x_min; x <= x_max; x++)
                        {
                            char ch = board[x, y, z, w];
                            Console.Write(ch);
                        }

                        Console.WriteLine();
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
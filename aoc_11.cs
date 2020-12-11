using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Xml;
using Microsoft.CSharp;


/*
	Solutions found:
	Part 1: 2427 aft 101 steps
	Part 2: 
	
*/

namespace advent_2020
{
    static class AOC_11 {
        private const string Part1Input = "aoc_11_input_1.txt";
        private const string Part2Input = "aoc_11_input_2.txt";
        private const string TestInput1 = "aoc_11_test_1.txt";
        private const string TestInput2 = "aoc_11_test_2.txt";

        public static void Run (string[] args) {
            Console.WriteLine ("AoC Problem 11");
            Part1(args);
            Console.Write("\n");
            Part2(args);
        }

        private static void Part1(string[] args) {
            Console.WriteLine("   Part 1");
            string[] lines =  System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            int width = lines[0].Length;
            int height = lines.Length;
	
            Console.WriteLine($"\t {(width,height)}");
		
            char[,] map = new char[width,height];
            for(int y=0; y < height; y++) {
                for(int x=0; x < width; x++) {
                    map[x,y] = lines[y][x];
                }
            }

            char[,] new_map;
          // Utility.PrintMap(map);

            int num_people = 0;
            
            int max_steps = 1000;
            
            
            for(int i=0; i < max_steps; i++) {
                new_map = StepMap(map);
              //  Console.WriteLine();
             //   Utility.PrintMap(new_map);
                if (isEqual(new_map, map))
                {
                    num_people = CountPeopleOnMap(new_map);
                    Console.WriteLine($"\t Map has reached fixed point after {Number_Steps} steps with {num_people} peoples ");
                    map = new_map;
                    break;
                }

                map = new_map;
            }
            
            
            
            
			
            Console.WriteLine($"\n\tPart 1 Solution: {0}");	
        }

        private static void Part2(string[] args) {
            Console.WriteLine("   Part 2:");
            string[] lines =  System.IO.File.ReadAllLines(Part2Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
			


		
            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }

        
        private static bool isEqual(char[,] map, char[,] other_map)
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[x, y] != other_map[x, y]) return false;
                }
            }

            return true;
        }
        
        private static char[,] MapDub(char[,] map) {
            return (char[,]) map.Clone();
        }

        private static int CountPeopleOnMap(char[,] map, char people_char = '#')
        {
            int count = 0;
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[x,y] == people_char) count++;
                }
            }
            return count;
        }
        

        private static List<char> GetNeighbors(char[,] map, int x, int y)
        {
            List<char> results = new List<char>();
      
            (int left, int right) = (-1, map.GetLength(0));
            (int up, int down) = (-1, map.GetLength(1));
            for (int n_x = x - 1; n_x <= x + 1; n_x++)
            {
                for (int n_y = y - 1; n_y <= y + 1; n_y++)
                {
                    if ((n_x == x) && (n_y == y))
                    {
                        continue;
                    }
                    else
                    {
                        if((left < n_x) && (n_x < right) && (up < n_y) && (n_y < down))
                        {
                            results.Add(map[n_x,n_y]);
                        }
                    }
                }
            }

            return results;
        }

        private static int Number_Steps = 0;
        private static char[,] StepMap(char[,] map)
        {
            Number_Steps++;
            char[,] new_map = MapDub(map);
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    char new_cell = UpdateCell(map, x, y);
                    new_map[x, y] = new_cell;
                }
            }

            return new_map;
        }

        
        private static char  UpdateCell(char[,] map,int  x, int y)
        {
            char cell = map[x,y];
            char new_cell = cell;

            if(cell == '.') {
                return '.';
            }

            List<char> neighbors = GetNeighbors(map, x, y);
           
            int count_people = 0;
            foreach (char c in neighbors)
            {
                if (c == '#') count_people++;
             
            }
  
            if(cell == 'L') {
                if (count_people == 0)
                {
                    new_cell = '#';
                }
            } else if (cell == '#') {
                if (count_people >= 4)
                {
                    new_cell = 'L';
                }
            } else {
                throw new ArgumentException($"Cell is {cell} when expected '.', 'L', '#' ");
            }

            return new_cell;
        }
    }
	

}
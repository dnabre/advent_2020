using System;
using System.CodeDom;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;

/*
	Solutions found:
	Part 1: 
	Part 2: 
	
*/

namespace advent_2020
{
    static class AOC_20
    {
        private const string Part1Input = "aoc_20_input_1.txt";
        private const string Part2Input = "aoc_20_input_2.txt";
        private const string TestInput1 = "aoc_20_test_1.txt";
        private const string TestInput2 = "aoc_20_test_2.txt";

        public static void Run(string[] args)
        {

	        Console.WriteLine("AoC Problem 20");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Part1();
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
    //        Part2();
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }

        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            string[] lines = System.IO.File.ReadAllLines(TestInput1);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            List<char[,]> square_a = new List<char[,]>();
            
            List<List<String>> tile_patchs = new List<List<string>>();
            int tile_count = 1;
            List<String> current_patch = null;
            foreach (String line in lines)
            {
                if (line.Equals(""))
                {
                    tile_count++;
                    continue;
                }
                else
                {
                    if (line.StartsWith("Tile"))
                    {
                        // int tile_id;
                        String ti = line.Replace("Tile ", "");
                        ti = ti.Replace(":", "");

                        Console.WriteLine($"\t Tile ID: |{ti}|");
                        if (current_patch != null)
                        {
                            Console.WriteLine($"\t Patch Height: {current_patch.Count}");
                            tile_patchs.Add(current_patch);
                            char[,] m = new char[current_patch[0].Length, current_patch.Count];
                            for (int y=0; y < current_patch.Count; y++)
                            {
                                char[] c_a = current_patch[y].ToCharArray();
                                for (int x=0; x < current_patch[0].Length; x++)
                                {
                                    m[x, y] = c_a[x];
                                }
                            }
                            square_a.Add(m);
            
                                current_patch = new List<String>();
                        }
                        else
                        {
                            current_patch = new List<String>();
                        }
                    

                    }
                    else
                    {
                        current_patch.Add(line);
                        Console.WriteLine($"\t {line} \t length: {line.Length}");    
                    }
                    
                        
                }
                
                
            }

            if (!tile_patchs.Contains(current_patch))
            {
                tile_patchs.Add(current_patch);
                char[,] m = new char[current_patch[0].Length, current_patch.Count];
                for (int y=0; y < current_patch.Count; y++)
                {
                    char[] c_a = current_patch[y].ToCharArray();
                    for (int x=0; x < current_patch[0].Length; x++)
                    {
                        m[x, y] = c_a[x];
                    }
                }
                square_a.Add(m);
            }
        
            Console.WriteLine($"\n\t Tiles seen: {tile_count}");
            Console.WriteLine(($"\t number of patches: {tile_patchs.Count}"));
            Console.WriteLine($"\t number of grids: {square_a.Count}");
            
            
            Console.WriteLine($"\n\tPart 1 Solution: {0}");
        }
        
        private static void Part2()
        {
            
            Console.WriteLine("   Part 2");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
           
           
            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }

        
    }
    
}

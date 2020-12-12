using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;


/*
	Solutions found:
	Part 1: 1424
	Part 2: 63447
	
*/

namespace advent_2020
{
    public class FlosserWhip
    {
        public int x;
        public int y;
        public int heading = 90;

        public void Translate(char direction, int amount)
        {
            if (direction == 'N')
            {
                y += amount;
            } else if (direction == 'S')
            {
                y -= amount;
            } else if (direction == 'E')
            {
                x += amount;
            } else if (direction == 'W')
            {
                x -= amount;
            }
            else
            {
                throw new ArgumentException($"Direction {direction} not recognized");
            }
        }

        public void Rotate(char left_or_right, int degrees)
        {
            if (left_or_right == 'L')
            {
                degrees = -degrees;
            } else if (left_or_right != 'R')
            {
                throw new ArgumentException($"Rotate direction {left_or_right} not recognized");
            }
            heading = (heading + 360 + degrees) % 360;
        }

        public void Forward(int amount)
        {
            switch (heading)
            {
                case 0:
                    y += amount;
                    break;
                case 90:
                    x += amount;
                    break;
                case 180:
                    y -= amount;
                    break;
                case 270:
                    x -= amount;
                    break;
                default:
                    Console.WriteLine($"\t heading {heading} not handled");
                    System.Environment.Exit(0);
                    break;
            }

            
        }
        
        
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Location: ({x},{y}) Heading: {heading} Distance: {getManhattanDistance()}");
            return sb.ToString();
        }

        public int getManhattanDistance(int s_x = 0, int s_y = 0)
        {
            return Math.Abs(s_x + x) + Math.Abs(s_y + y);
        }
    }
    
    public class FlosserWhip2
    {
        public int x=0;
        public int y=0;
       
        public int waypoint_x = 10;
        public int waypoint_y = 1;
        
        public void Translate(char direction, int amount)
        {
            if (direction == 'N')
            {
                waypoint_y += amount;
            } else if (direction == 'S')
            {
                waypoint_y -= amount;
            } else if (direction == 'E')
            {
                waypoint_x += amount;
            } else if (direction == 'W')
            {
                waypoint_x -= amount;
            }
            else
            {
                throw new ArgumentException($"Direction {direction} not recognized");
            }
        }

        public void Rotate(char left_or_right, int degrees)
        {
            int heading;
            if (left_or_right == 'L')
            {
                heading = 360 - degrees;
            } else if (left_or_right != 'R')
            {
                throw new ArgumentException($"Rotate direction {left_or_right} not recognized");
            }
            else
            {
                heading = degrees;
            }
            
            
            
            int w_x = waypoint_x;
            int w_y = waypoint_y;
            switch (heading)
            {
                case 0:
                    break;
                case 90:
                    waypoint_x = w_y;
                    waypoint_y = -w_x;
                    break;
                case 180:
                    waypoint_x = -w_x;
                    waypoint_y = -w_y;
                    break;
                case 270:
                    waypoint_x = -w_y;
                    waypoint_y = w_x;
                    break;
                default:
                    Console.WriteLine($"\t heading {heading} not handled");
                    System.Environment.Exit(0);
                    break;
            }
            
            
            
            
            
            
            
            
            
        }

        public void Forward(int amount)
        {
            x = amount * waypoint_x + x;
            y = amount * waypoint_y + y;
   
        }
        
        
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Location: ({x},{y})  Waypoint: ({waypoint_x},{waypoint_y})  Distance: {getManhattanDistance()}");
            return sb.ToString();
        }

        public int getManhattanDistance(int s_x = 0, int s_y = 0)
        {
            return Math.Abs(s_x + x) + Math.Abs(s_y + y);
        }
    }
  
    static class AOC_12
    {
   
        private const string Part1Input = "aoc_12_input_1.txt";
        private const string Part2Input = "aoc_12_input_2.txt";
        private const string TestInput1 = "aoc_12_test_1.txt";
        private const string TestInput2 = "aoc_12_test_2.txt";

    

        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 12");
            Part1(args);
            Console.Write("\n");
            Part2(args);
        }


        private static void Part1(string[] args)
        {
            Console.WriteLine("   Part 1");
            String[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            char letter;
            int amount;
            FlosserWhip turtle = new FlosserWhip();
            
            foreach (String ln in lines)
            {
                letter = ln[0];
                amount = int.Parse(ln.Substring(1));
                if((letter == 'R') || (letter == 'L'))
                {
                    turtle.Rotate(letter, amount);
                } else if ("NSWE".Contains(Char.ToString(letter)))
                {
                    turtle.Translate(letter, amount);
                } else if (letter == 'F')
                {
                    turtle.Forward(amount);
                }
                else
                {
                    Console.WriteLine($"\t Bad parse!  {ln} letter:{letter} amount:{amount}");
                }
                
                
              //  Console.Write($"\t {ln} \t {letter}:{amount}\n");
            }
          //  Console.WriteLine($"\t{turtle.ToString()}");
        
            Console.WriteLine($"\n\tPart 1 Solution: {turtle.getManhattanDistance()}");
        }


        private static void Part2(string[] args)
        {
            Console.WriteLine("   Part 2");
            String[] lines = System.IO.File.ReadAllLines(Part2Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            char letter;
            int amount;
            FlosserWhip2 turtle = new FlosserWhip2();
           // Console.WriteLine($"\t{turtle.ToString()}\n\n");
            foreach (String ln in lines)
            {
                letter = ln[0];
                amount = int.Parse(ln.Substring(1));
                if((letter == 'R') || (letter == 'L'))
                {
                    turtle.Rotate(letter, amount);
                } else if ("NSWE".Contains(Char.ToString(letter)))
                {
                    turtle.Translate(letter, amount);
                } else if (letter == 'F')
                {
                    turtle.Forward(amount);
                }
                else
                {
                    Console.WriteLine($"\t Bad parse!  {ln} letter:{letter} amount:{amount}");
                }
                
                
                //Console.Write($"\t {ln} \t {letter}:{amount}\n");
               // Console.WriteLine($"\t{turtle.ToString()}");
            }
              Console.WriteLine($"\t{turtle.ToString()}");


      
        
            Console.WriteLine($"\n\tPart 2 Solution: {turtle.getManhattanDistance()}");
        }
    }

}
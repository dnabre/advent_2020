using System;
using System.Collections.Generic;
using advent_2020;


class MainClass
{
    public static void Main(string[] args)
    {
        /*
        Stack<int> test = new Stack<int>();
        int[] arr = {1, 2, 3, 4, 5};
        int[] arr2 = {11, 22, 33, 44, 55};
        
        for (int i = 0; i < arr.Length; i++)
        {
            test.Push(arr[i]);
            prints(test);
        }

        Stack<int> test2 = new Stack<int>(arr2);
        prints(test2);
        Console.WriteLine($"\t{Utility.ArrayToStringLine(arr2)}");


        int[] mod;
        mod = test2.ToArray();
        Console.WriteLine($"\t{Utility.ArrayToStringLine(mod)}");
        int[] p = new int[2];
        p[0] = mod[0];
        p[1] = mod[1];
        Console.WriteLine($"\tstole p[0]={p[0]} and p[1]={p[1]} ");
        Stack<int> replacement = new Stack<int>();
        for (int i = 2; i < mod.Length; i++)
        {
            replacement.Push(mod[i]);
        }
        prints(replacement);
*/


        AOC_23.Run(args);
    }



    public static void prints(Stack<int> left)
    {
        Console.Write($"\t\t [");
        int[] l_array = left.ToArray();
        int w;
        for (int i = l_array.Length - 1; i >= 0; i--)
        {
            w = l_array[i];
            
                Console.Write($" {w} ");
            
        }
        Console.Write($" ] \t Top: {left.Peek()} \n");
    }
}
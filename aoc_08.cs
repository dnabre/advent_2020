using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;


/*
	Solutions found:
	Part 1: 2051
	Part 2: 3640
	
*/

namespace advent_2020
{



    class IntCode08
    {
        public String[] instructions;
        public int[] arguments;

        public int PC;
        public int accumulator;
        public IntCode08(String[] instructions, int[] arguments)
        {
            this.instructions = instructions;
            this.arguments = arguments;
            this.PC = 0;
            this.accumulator = 0;

        }

        public (int, String) testFlipedOp()
        {
            
            int target_pc = instructions.Length;
            for (int i = 0; i < instructions.Length; i++)
            {
                String instruct = instructions[i];
                if (i.Equals("acc")) continue;

                if (i.Equals("nop"))
                {
                    int a = arguments[i];
                    if (i + a == target_pc)
                    {
                        return (i, "jmp");
                    }
                } else if (i.Equals("jmp")){
                    int a = arguments[i];
                    if (i + 1 == target_pc)
                    {
                        return (i, "nop");
                    }
                } 
            }

            return (-1, "not found");
        }

        static public (String, int) parseInstruction(String line)
        {
            String[] p = line.Split(' ');
            String r_string;
            int r_arg;

            r_string = p[0];
            if(!isValidInstruction(r_string)) Console.WriteLine($"Invalid instruction {r_string} in {line}");

            r_arg = int.Parse(p[1]);
            return (r_string, r_arg);
            
        }

        static readonly String[] valid = { "acc","jmp","nop" };
        static bool isValidInstruction(String instruct)
        {
            foreach (String v in IntCode08.valid)
            {
                if (instruct.Equals(v))
                {
                    return true;
                }
            }
            return false;
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"\tPC={PC} \t Accumulator={accumulator}");
            for (int i = 0; i < instructions.Length; i++)
            {
                sb.Append($"\t\t{instructions[i]}: {arguments[i].ToString().PadLeft(6)}  \n");
            }
            

            return sb.ToString();
        }

        private void executeStep()
        {
            string   s = $"\tpc: {this.PC.ToString().PadLeft(3)}\t acc: {this.accumulator.ToString().PadLeft(5)}" +
                         $"|\t {instructions[PC].PadLeft(7)} {arguments[PC].ToString().PadLeft(5)}\t";
            Console.WriteLine(s);
           // Console.WriteLine(s);
            String i = instructions[PC];
            int a = arguments[PC];

            if (i.Equals("acc"))
            {
                accumulator += a;
                PC++;
            } else if (i.Equals("jmp"))
            {
                
                PC += a;
                if (PC > instructions.Length)
                {
                    Console.WriteLine("\t jmp to invalid PC={pc}");
                    PC = PC - a;
                    Console.WriteLine(
                        $"\tpc: {this.PC.ToString().PadLeft(3)} acc: {this.accumulator.ToString().PadLeft(5)}" +
                        $"\t {instructions[PC].PadLeft(7)} {arguments[PC].ToString().PadLeft(5)}");
                    PC = PC + a;
                    System.Environment.Exit(0); 
                }
            } else if (i.Equals("nop"))
            {
                PC++;
            }
            else
            {
                Console.WriteLine($"\t invalid instruction {i}");
                System.Environment.Exit(0);
            }
            s = $"\tpc: {this.PC.ToString().PadLeft(3)}\t acc: {this.accumulator.ToString().PadLeft(5)}" +
                       $"|\t {instructions[PC].PadLeft(7)} {arguments[PC].ToString().PadLeft(5)}\t";
        //    Console.WriteLine(s);
            Console.WriteLine();
            return;
        }
        
        public int RunUntilLoop()
        {
           
            PC = 0;
            bool[] ran = new bool[instructions.Length];
            for (int i = 0; i < instructions.Length; i++)
            {
                ran[i] = false;
            }

            do
            {
           
                ran[PC] = true;
                executeStep();
                
                if (PC >= instructions.Length)
                {
                    Console.WriteLine($"\tProgram Reached End (PC={PC})");
                    System.Environment.Exit(0);
                }
            } while (ran[PC] == false);


            return this.accumulator;

        }
    }
    
    
    
    static class AOC_08
{
    
    
    
    
    private const string Part1Input = "aoc_08_input_1.txt";
    private const string Part2Input = "aoc_08_input_2.txt";
    private const string TestInput1 = "aoc_08_test_1.txt";
    private const string TestInput2 = "aoc_08_test_2.txt";

    

    public static void Run(string[] args)
    {
        Console.WriteLine("AoC Problem 08");
  //      Part1(args);
          Console.Write("\n");
          Part2(args);
    }


    private static void Part1(string[] args)
    {
        Console.WriteLine("   Part 1");
        String[] lines = System.IO.File.ReadAllLines(Part1Input);
        Console.WriteLine("\tRead {0} inputs", lines.Length);

        (String s, int i) instruct;

        String[] prog = new String[lines.Length];
        int[] iargs = new int[lines.Length];
        
        
        for(int i=0; i < lines.Length; i++) {
            instruct = IntCode08.parseInstruction(lines[i]);
            prog[i] = instruct.s;
            iargs[i] = instruct.i;
        }
        
        IntCode08 machine = new IntCode08(prog,iargs);
        
       // Console.Write(machine);

        int count = machine.RunUntilLoop();
        
        Console.WriteLine($"\n\tPart 1 Solution: {count}");
    }


    private static void Part2(string[] args)
    {
        Console.WriteLine("   Part 2");
        String[] lines = System.IO.File.ReadAllLines(TestInput1);
        Console.WriteLine("\tRead {0} inputs", lines.Length);

        
        (String s, int i) instruct;

        String[] prog = new String[lines.Length];
        int[] iargs = new int[lines.Length];
        
        
        for(int i=0; i < lines.Length; i++) {
            instruct = IntCode08.parseInstruction(lines[i]);
            prog[i] = instruct.s;
            iargs[i] = instruct.i;
        }
        
        IntCode08 machine = new IntCode08(prog,iargs);
        
        // Console.Write(machine);

        (int i, String s) result = machine.testFlipedOp();
        
        Console.WriteLine(result);
        
        Console.WriteLine($"\n\tPart 2 Solution: {0}");
    }
}

}
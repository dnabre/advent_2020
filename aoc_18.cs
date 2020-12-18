using System;
using System.CodeDom;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;


/*
	Solutions found:
	Part 1: 
	Part 2: 
	
*/

namespace advent_2020
{
    static class AOC_18
    {
        private const string Part1Input = "aoc_18_input_1.txt";
        private const string Part2Input = "aoc_18_input_2.txt";
        private const string TestInput1 = "aoc_18_test_1.txt";
        private const string TestInput2 = "aoc_18_test_2.txt";

		 enum PType { op,l_paren,r_paren,number}


        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 18");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Part1();
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
     //       Part2();
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }

        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            string[] lines = System.IO.File.ReadAllLines(TestInput1);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
			bool DEBUG = false;
			Queue<(PType,String)> equation = new Queue<(PType,String)>();
			for(int i=1; i < 2; i++){
			
				String ln = lines[i];
				Console.WriteLine($"\t{ln}");
				String[] parts = ln.Split(' ');
				
				Stack<String> to_process = new Stack<String>();
				for(int j=parts.Length-1; j >=0; j--) {
					to_process.Push(parts[j]);
				}
				while(to_process.Count > 0) {
					(PType pt, String o) e; 
					String p = to_process.Pop();
					if(p.Equals(")")) {
						e = (PType.r_paren, ")");
						equation.Enqueue(e); 
						continue;
					}



					if(p.StartsWith('(')) {
						String other = p.Remove(0,1);
						if(other.Length > 0 ) to_process.Push(other);
						e = (PType.l_paren,"(");
						equation.Enqueue(e);
						
						continue;
					} else if (p.EndsWith(')')) {
						String other = p.Remove(p.Length-1);
						to_process.Push(")");
						to_process.Push(other);
						continue;
					}
					if((p.Length == 1) && ("+-*/".Contains(p[0]))) {
						e = (PType.op, p);
						equation.Enqueue(e);
			
						continue;
					}
					int num;
					bool good_num = int.TryParse(p, out num);
					if(good_num) {
						e = (PType.number,p);
						equation.Enqueue(e);
					
					} else {
						if(DEBUG) Console.WriteLine($"\t p=\"{p}\" discarding");
					}
				}
			}
			Console.Write("\n\tparse: ");
			foreach((PType t,String s) q in equation ) {
				if((q.t == PType.l_paren) || (q.t == PType.r_paren)) {
					Console.Write($" {q.s}");
				} else {
				Console.Write($" {q.s}");
				}
			}
			Console.WriteLine();

			Stack<(PType t, String s)> s_equation = new Stack<(PType t, String s)>(equation.Count);
			var  eq_a = new (PType t, String s)[equation.Count];
			int index =0;
			while(equation.Count > 0) {
				(PType t, String s) e;
				e = equation.Dequeue();
				eq_a[index] = e;
				index++;
			}
			
			for(int k=eq_a.Length-1; k >= 0; k--) {
				s_equation.Push(eq_a[k]);
			}


			int result = 0;
			result = Eval(s_equation);
			Console.WriteLine($"\t {result}");

            Console.WriteLine($"\n\tPart 1 Solution: {0}");
        }

		private static int Eval(Stack<(PType t, String s)> equation) {
			int acc = 0;
			
			(PType t, String s) term;
			while(equation.Count > 0) {

				term = equation.Pop();
				switch(term.t) {
					case PType.number:
					//	Console.WriteLine($"\t number {term}");
						acc = int.Parse(term.s);

						break;
					case PType.l_paren:
						Console.WriteLine($"\t l_paren {term}");
						int v;
						v = Eval(equation);
						equation.Push( (PType.number, v.ToString()));
						break;
					case PType.r_paren:
						Console.WriteLine($"\t r_paren {term}");
						
						return acc;
	
					case PType.op:
					//	Console.WriteLine($"\t op {term}");
						(PType t, String s) next_term = equation.Peek();
						if(next_term.t == PType.number) {
							next_term = equation.Pop();
							int value;
							value = ApplyOp(term.s, acc , next_term.s);
							//Console.WriteLine($"\t value: {value} acc: {acc} -> {value}, {acc} {term.s} {next_term.s}");
							acc = value;
						}
						break;
					
				}

			}
			return acc;
		}


		private static int ApplyOp(String op, int acc, String right) {
			

			int r_num = int.Parse(right);
			int result=-1;
			if(op.Equals("+")){
				result = acc + r_num;
			}
			if(op.Equals("-")){
				result = acc - r_num;
			}
			if(op.Equals("*")){
				result =acc * r_num;
			}
			if(op.Equals("/")){
				result = acc / r_num;
			}
			Console.WriteLine($"\tEval {acc} {op} {right} = {result}");
			return result;

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

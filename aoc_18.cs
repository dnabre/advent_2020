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

  
    public enum PType
    {
        op,
        l_paren,
        r_paren,
        number,
        undefined
    };

    public enum OType
    {
        add,
        sub,
        mult,
        div,
        left_p,
        right_p
    }
	
    static class AOC_18
    {
        private const string Part1Input = "aoc_18_input_1.txt";
        private const string Part2Input = "aoc_18_input_2.txt";
        private const string TestInput1 = "aoc_18_test_1.txt";
        private const string TestInput2 = "aoc_18_test_2.txt";



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

        private static OType ConvertPType(String op) 
        {
            if(op.Equals("+"))
            {
                return OType.add;
            }
            if(op.Equals("-"))
            {
                return OType.sub;
            }
            if(op.Equals("*"))
            {
                return OType.mult;
            }
            if(op.Equals("/"))
            {
                return OType.div;
            }
            throw new ArgumentException($"Expected PType String, received {op}");
        }


        private static String OTypeToString(OType otp)
        {
            if(otp == OType.add) return "+";
            if(otp == OType.sub) return "-";
            if(otp == OType.div) return "/";
            if(otp == OType.mult) return "*";
            return "unknown operator";
        }
        private static List<String> EquationToRPN(List<(PType t, String s)> equation)
        {
            Stack<OType> o_stack = new Stack<OType>();
            Queue<String> output_queue = new Queue<String>();
            foreach ((PType t, String s) e in equation)
            {
                if (e.t == PType.number)
                {
                    output_queue.Enqueue(e.s);
                }
                else if (e.t == PType.op)
                {
                    while ((o_stack.Count > 0) && (o_stack.Peek() != OType.left_p))
                    {
                        OType otp = o_stack.Pop();
                        output_queue.Enqueue(OTypeToString(otp));
                    }

                    o_stack.Push(ConvertPType(e.s));
                }
                else if (e.t == PType.l_paren)
                {
                    o_stack.Push(OType.left_p);
                }
                else if (e.t == PType.r_paren)
                {
                    while ((o_stack.Count > 0) && (!(o_stack.Peek() == OType.left_p)))
                    {
                        OType otp = o_stack.Pop();
                        output_queue.Enqueue(OTypeToString(otp));
                    }

                    if ((o_stack.Count > 0) && (o_stack.Peek() == OType.left_p))
                    {
                        o_stack.Pop();
                    }
                }
            }

            while (o_stack.Count > 0)
            {
                OType otp = o_stack.Pop();
                output_queue.Enqueue(OTypeToString(otp));
            }

            List<String> result = new List<String>(output_queue);
            return result;
        }
            
            
            
            
            
        

        private static Stack<(PType t, String s)> EqListToStack(List<(PType t, String s)> equation)
        {
            
            Stack<(PType t, String s)> s_equation = new Stack<(PType t, String s)>(equation.Count);
            var  eq_a = new (PType t, String s)[equation.Count];
            int index =0;
            while(equation.Count > 0) {
                (PType t, String s) e;
                e = equation[0];
                equation.RemoveAt(0);
                eq_a[index] = e;
                index++;
            }
			
            for(int k=eq_a.Length-1; k >= 0; k--) {
                s_equation.Push(eq_a[k]);
            }

            return s_equation;
        }  

        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
            long result = 0;
            
            for(int i=0; i < lines.Length; i++) {
            
                List<(PType, String)> equation = ParseToList(lines[i]);
                /*
                Console.Write("\n\tparse: ");
                foreach((PType t,String s) q in equation ) {
                    if((q.t == PType.l_paren) || (q.t == PType.r_paren)) {
                        Console.Write($" {q.s}");
                    } else {
                        Console.Write($" {q.s}");
                    }
                }
                Console.WriteLine();
                */
                List<String> rpn  = EquationToRPN(equation);
                long t = EvalRPN(rpn);
                result += t;
                Console.WriteLine($"\t {Utility.ListToStringLine(rpn)} \t = \t {t}");
                
            }
            
            //        Console.WriteLine($"\t {result}");

            Console.WriteLine($"\n\tPart 1 Solution: {result}");
        }

        private static int EvalRPN(List<String> rpn)
        {
            Stack<int> eval_stack = new Stack<int>();
            foreach (String s in rpn)
            {
                if ("+-*/".Contains(s))
                {
                    int left, right, result;
                    left = eval_stack.Pop();
                    right = eval_stack.Pop();
                    result = ApplyOp(s, left, right);
                    eval_stack.Push(result);
                }
                else
                {
                    eval_stack.Push(int.Parse(s));
                }
            }

            if (eval_stack.Count == 1)
            {
                return eval_stack.Pop();
            }
            else
            {
                int last = -1;
                Console.Write("\t");
                foreach (int i in eval_stack)
                {
                    last = i;
                    Console.Write($"i, ");
                }

                Console.WriteLine();
                return last;
            }
            
        }
        
        
        private static Term BuildTree(List<(PType t, String s)> equation)
        {
            //Stack<(PType, String )> pStack = new Stack<(PType, string)>();
            Stack<Term> pStack = new Stack<Term>();
            Term eTree = new Term("");
            pStack.Push(eTree);
            Term currentTree = eTree;
            foreach((PType t, String s) i in equation)
            {
                if (i.t == PType.l_paren)
                {
                    currentTree.insertLeft(new Term("("));
                    pStack.Push(currentTree);
                    currentTree = currentTree.left;

                } else if (i.t == PType.op)
                {
                    currentTree.type = PType.op;
                    currentTree.value = i.s;
                    currentTree.insertRight(new Term(""));
                    pStack.Push(currentTree);
                    currentTree = currentTree.right;
                } else if (i.t == PType.r_paren)
                {
                    currentTree = pStack.Pop();
                }
                else
                {
                    currentTree.value = i.s;
                    currentTree.type = PType.number;
                    Term parent = pStack.Pop();
                    currentTree = parent;
                }

                return eTree;
            }
            
            
            return null;
        }

        private static int Eval(Term parseTree)
        {
            
            Term leftC = parseTree.left;
            Term rightC = parseTree.right;
            
            
            if ((parseTree.type == PType.l_paren) ||
                (parseTree.type == PType.r_paren) || (parseTree.type == PType.undefined))
            {
                Console.WriteLine($"Got type {parseTree.type} in eval tree");
                System.Environment.Exit(0); 
            }

            if ((leftC != null) && (rightC != null))
            {
                int l, r;
                l = Eval(leftC);
                r = Eval(rightC);
                return ApplyOp(parseTree.value, l, r);
            }
            else
            {
                return int.Parse(parseTree.value);
            }
        }
        

        private static int ApplyOp(String op, int left, int right)
        {


            int r_num = right;
            int result=-1;
            if(op.Equals("+")){
                result = left + r_num;
            }
            if(op.Equals("-")){
                result = left - r_num;
            }
            if(op.Equals("*")){
                result =left * r_num;
            }
            if(op.Equals("/")){
                result = left / r_num;
            }
         //   Console.WriteLine($"\tEval {left} {op} {right} = {result}");
            return result;

        }
        
        private static void Part2()
        {
            
            Console.WriteLine("   Part 2");
            string[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
           
           
            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }
        
        
        private static List<(PType, String)> ParseToList(String ln)
        {
            List<(PType,String)> equation = new List<(PType,String)>();
	     
			
		 
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
                    equation.Add(e); 
                    continue;
                }
					
                if(p.StartsWith("(")) {
                    String other = p.Remove(0,1);
                    if(other.Length > 0 ) to_process.Push(other);
                    e = (PType.l_paren,"(");
                    equation.Add(e);
						
                    continue;
                } else if (p.EndsWith(")")) {
                    String other = p.Remove(p.Length-1);
                    to_process.Push(")");
                    to_process.Push(other);
                    continue;
                }
                if((p.Length == 1) && ("+-*/".Contains(p[0].ToString()))) {
                    e = (PType.op, p);
                    equation.Add(e);
			
                    continue;
                }
                int num;
                bool good_num = int.TryParse(p, out num);
                if(good_num) {
                    e = (PType.number,p);
                    equation.Add(e);
					
                }
            }

            return equation;
        }
        
    }
    
}
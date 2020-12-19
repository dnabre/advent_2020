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

    abstract class Term
    {
        public abstract int GetValue();
        public abstract bool IsOp();

    }
	
    class ValueTerm : Term, IEquatable<ValueTerm>
    {
        public bool Equals(ValueTerm other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return value == other.value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ValueTerm) obj);
        }

        public override int GetHashCode()
        {
            return value;
        }

        public static bool operator ==(ValueTerm left, ValueTerm right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueTerm left, ValueTerm right)
        {
            return !Equals(left, right);
        }

        public int value;

        public override int GetValue()
        {
            return value;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public ValueTerm(int value)
        {
            this.value = value;
        }
        public override bool IsOp()
        {
            return false;
        }

    }

    class OpTerm : Term, IEquatable<OpTerm>
    {
        public override bool IsOp()
        {
            return true;
        }

        public static OType ParseOp(String op) 
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
                return OType.div;
           
            throw new ArgumentException($"Invalid operation string {op}");
        }
        
        public bool Equals(OpTerm other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(left, other.left) && Equals(right, other.right) && op == other.op;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OpTerm) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (left != null ? left.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (right != null ? right.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) op;
                return hashCode;
            }
        }

        public static bool operator ==(OpTerm left, OpTerm right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(OpTerm left, OpTerm right)
        {
            return !Equals(left, right);
        }

        public Term left;
        public Term right;
        public OType op;

        public OpTerm(OType op)
        {
            this.op = op;
        }

        public OpTerm(Term left, Term right, OpTerm op_term)
        {
            this.left = left;
            this.right = right;
            this.op = op_term.op;
        }
        public OpTerm(Term left, Term right, OType op)
        {
            this.left = left;
            this.right = right;
            this.op = op;
        }

        public override int GetValue()
        {
            int l, r;
            l = left.GetValue();
            r = right.GetValue();
            switch (op)
            {
                case OType.add:
                    return l + r;
                case OType.div:
                    return l / r;
                case OType.mult:
                    return l * r;
                case OType.sub:
                    return l - r;
            }

            throw new Exception($"Invalid operator {op}");
        }

        public override string ToString()
        {
            String o_s;
            switch (op)
            {
                case OType.add:
                    o_s = "+";
                    break;
                case OType.div:
                    o_s = "/";
                    break;
                case OType.mult:
                    o_s = "*";
                    break;
                case OType.sub:
                    o_s = "-";
                    break;
                default:
                    o_s = "#";
                    break;
            }

            return $"{left} {o_s} {right}";
        }
    }
    enum PType
    {
        op,
        l_paren,
        r_paren,
        number
    };

    enum OType
    {
        add,
        sub,
        mult,
        div
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



        private static Stack<(PType t, String s)> EqQueueToStack(Queue<(PType t, String s)> equation)
        {
            
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

            return s_equation;
        }  

        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            string[] lines = System.IO.File.ReadAllLines(TestInput1);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            Queue<(PType, String)> equation = ParseToQueue(lines[1]);
            Console.Write("\n\tparse: ");
            foreach((PType t,String s) q in equation ) {
                if((q.t == PType.l_paren) || (q.t == PType.r_paren)) {
                    Console.Write($" {q.s}");
                } else {
                    Console.Write($" {q.s}");
                }
            }
            Console.WriteLine();

            Stack<(PType t, String s)> s_equation = EqQueueToStack(equation);


            Term result = Eval(s_equation);
           // int result = Eval(s_equation);
            Console.WriteLine($"\t {result}");

            Console.WriteLine($"\n\tPart 1 Solution: {0}");
        }

        private static Term Eval(Stack<(PType t, String s)> equation)
        {
            Console.WriteLine($"\t {Utility.StackToStringLine(equation)}");
	        
            Term top = null;
            Stack<Term> push_left = new Stack<Term>();
            Stack<(PType top, String s)> left_parens = new Stack<(PType top, string s)>();
            Stack<(PType top, String s)> right_parens;
            Term left, right,op;
            (PType t, String s) e;
            (PType t, String s) s_term;
            int acc;
            while(equation.Count > 0) {

                s_term = equation.Pop();
                switch(s_term.t) {
                    case PType.number:
                        //	Console.WriteLine($"\t number {term}");
                        acc = int.Parse(s_term.s);
                        left = new ValueTerm(acc);
                        push_left.Push(left);
                        top = left;
                        break;
                    case PType.l_paren:
                        Console.WriteLine($"\t l_paren {s_term}");
                        Term v;
                        v = Eval(equation);
                        //equation.Push( (PType.number, v.ToString()));
                        push_left.Push(v);
                        break;
                    case PType.r_paren:
                        Console.WriteLine($"\t r_paren {s_term}");
                        return top; 
	
                    case PType.op:
                        op = new OpTerm(OpTerm.ParseOp(s_term.s));
                                
                        /*
                        (PType t, String s) next_term = equation.Peek();
                        if(next_term.t == PType.number) {
                            next_term = equation.Pop();
                            
                            
                            value = ApplyOp(s_term.s, acc , next_term.s);
                            Console.WriteLine($"\t value: {value} acc: {acc} -> {value}, {acc} {term.s} {next_term.s}");
                          */  
                
                        break;
					
                }

            }


            return top;
        }
        
        /*
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
        */


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
        private static Queue<(PType, String)> ParseToQueue(String ln)
        {
            Queue<(PType,String)> equation = new Queue<(PType,String)>();
	     
			
		 
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
					
                if(p.StartsWith("(")) {
                    String other = p.Remove(0,1);
                    if(other.Length > 0 ) to_process.Push(other);
                    e = (PType.l_paren,"(");
                    equation.Enqueue(e);
						
                    continue;
                } else if (p.EndsWith(")")) {
                    String other = p.Remove(p.Length-1);
                    to_process.Push(")");
                    to_process.Push(other);
                    continue;
                }
                if((p.Length == 1) && ("+-*/".Contains(p[0].ToString()))) {
                    e = (PType.op, p);
                    equation.Enqueue(e);
			
                    continue;
                }
                int num;
                bool good_num = int.TryParse(p, out num);
                if(good_num) {
                    e = (PType.number,p);
                    equation.Enqueue(e);
					
                }
            }

            return equation;
        }
        
    }
    
}
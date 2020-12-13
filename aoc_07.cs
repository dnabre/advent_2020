using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;


/*
	Solutions found:
	Part 1: 289
	Part 2: 3640
	
*/

namespace advent_2020
{
    
    
    public class Node : IEquatable<Node>
    {
        public bool Equals(Node other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return id == other.id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Node) obj);
        }

        public override int GetHashCode()
        {
            return (id != null ? id.GetHashCode() : 0);
        }

        public static bool operator ==(Node left, Node right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Node left, Node right)
        {
            return !Equals(left, right);
        }

        readonly public String id;
 
        public HashSet<Node> edges;
       
        public Node(String id)
        {
            this.id = id;
            this.edges = new HashSet<Node>();
            this.edge_weights = new Dictionary<Node,int>();

        }

        public Dictionary<Node, int> edge_weights; 
        
  
        
        public override String ToString()
        {
            
            String result, end;
            result = $"{id}: {edges.Count} edges";
            foreach (Node e in edges)
            {
                if (edge_weights.ContainsKey(e))
                {
                    end = $"\n\t\t{edge_weights[e]} {e.id}";
                }
                else
                {
                    end = $"\n\t\t{0} {e.id}";
                }
                result = result + end;
            }
           
            return result;
        }

        public bool CanHoldGold()
        {
            if (id.Equals(AOC_07.GOLD)) return false;
            foreach (Node n in edges)
            {
                if (n.id.Equals(AOC_07.GOLD)) return true;
            }

            return false;
        }

        public bool Search()
        {
            if (CanHoldGold()) return true;
            HashSet<Node> visited = new HashSet<Node>();
            Queue<Node> queue = new Queue<Node>(edges);
            while (queue.Count > 0)
            {
                Node n = queue.Dequeue();
                if (visited.Contains(n)) continue;
                if (n.CanHoldGold()) return true;
                foreach (Node n_edge in n.edges)
                {
                    if (visited.Contains(n))
                    {
                        continue;
                    }
                    else
                    {
                        queue.Enqueue(n_edge);
                    }
                }
            }
            return false;
        }

        public int CountSearch()
        {
            if (edge_weights.Count == 0) return 1;
            //List<int> path = new List<int>();
            //HashSet<Node> visited = new HashSet<Node>();
            Stack<Node> stack = new Stack<Node>();
            stack.Push(this);

            
            
            
            /*             
            HashSet<Node> visited = new HashSet<Node>();
            Queue<Node> queue = new Queue<Node>(edges);
            while (queue.Count > 0)
            {
                Node n = queue.Dequeue();
                if (visited.Contains(n)) continue;
                foreach (Node n_edge in n.edges)
                {
                    if (visited.Contains(n)) continue;
                    else queue.Enqueue(n_edge);
                }
            } */

            
            
            
            
            return 0;
        }
    }

    static class AOC_07
    {
        private const string Part1Input = "aoc_07_input_1.txt";
        private const string Part2Input = "aoc_07_input_2.txt";
        private const string TestInput1 = "aoc_07_test_1.txt";
        private const string TestInput2 = "aoc_07_test_2.txt";

        public const string GOLD = "shiny gold";

        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 07");
            Part1(args);
            Console.Write("\n");
            Part2(args);
        }



        private static String NormalizeInputLine(String line)
        {
            /*
             * Normalize input:
             *     bag      -> bags        # switch single bag to bags. will catch bags->bagss
             *     bagss    -> bags        # fix the bags->bagss->bags
             *     .$       -> ""          # remove . at the end of the line
             *
             *    Discard numbers, don't care about them for Part 1
             *     ^""      -> ^"1 "       # insert "1 " at the beginning of each line
             *
             *    Resulting syntax is then
             *     Bag_ID -> "# Modifer Color"
             *     Bag_ID "bags contain" Bag_ID {, Bag_ID}* 
             *
             *
             * 
             */

            line = line.Replace("bag", "bags");
            line = line.Replace("bagss", "bags");

            if (line[line.Length - 1] == '.')
            {
                line = line.Remove(line.Length - 1, 1);
            }

            line = RemoveAllDigits(line);
            line = line.Replace(" contain", ",");
            line = line.Replace(" bags,  ", "|");
            line = line.Replace(" bags", "");
            line = line.Replace(", no other", "|null null");
            return line;
        }

        private static String NormalizeInputLine2(String line)
        {
            /*
             * Normalize input:
             *     bag      -> bags        # switch single bag to bags. will catch bags->bagss
             *     bagss    -> bags        # fix the bags->bagss->bags
             *     .$       -> ""          # remove . at the end of the line
             *
             *    Discard numbers, don't care about them for Part 1
             *     ^""      -> ^"1 "       # insert "1 " at the beginning of each line
             *
             *    Resulting syntax is then
             *     Bag_ID -> "# Modifer Color"
             *     Bag_ID "bags contain" Bag_ID {, Bag_ID}* 
             *
             *
             * 
             */

            line = line.Replace("bag", "bags");
            line = line.Replace("bagss", "bags");

            if (line[line.Length - 1] == '.')
            {
                line = line.Remove(line.Length - 1, 1);
            }

            //line = RemoveAllDigits(line);
            line = line.Replace(" contain", ",");
            line = line.Replace(" bags,  ", "|");
            line = line.Replace(" bags", "");
            line = line.Replace("no other", "null null");
            line = line.Replace(", ", "|");
            return line;
        }

        private static void Part1(string[] args)
        {
            Console.WriteLine("   Part 1");
            String[] lines = System.IO.File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = NormalizeInputLine(lines[i]);
            }


            Dictionary<String, Node> Nodes = new Dictionary<string, Node>();
            HashSet<Node> Node_Set = new HashSet<Node>();
            HashSet<String> Node_names = new HashSet<string>();


            foreach (String line in lines)
            {
                String[] parts = line.Split('|');
                String id = parts[0];
                Node_names.Add(id);
                Node n = new Node(id);
                Node_Set.Add(n);
                Nodes[id] = n;
            }

            /*
             foreach (String s in  Node_names)
             {
                 Node n = Nodes[s];
                 bool in_node_set = Node_Set.Contains(n);
                 Console.WriteLine($"Node ID: {s}, Node_Set: {in_node_set}, Node: {n}");
             }
            */

            foreach (String line in lines)
            {
                if (line.Contains("null null")) continue;
                String[] parts = line.Split('|');
                String node_id = parts[0];
                Node root_node = Nodes[node_id];
                for (int j = 1; j < parts.Length; j++)
                {
                    String leaf_id = parts[j];
                    Node leaf_node = Nodes[leaf_id];
                    root_node.edges.Add(leaf_node);
                }
            }

            HashSet<String> shiny = new HashSet<string>();
            foreach (Node n in Node_Set)
            {
                if (n.Search())
                {
                    shiny.Add(n.id);
                    //               Console.WriteLine($"\t{n.id} holds {GOLD}");
                }
            }


            Console.WriteLine($"\n\tPart 1 Solution: {shiny.Count}");
        }

        private static String RemoveAllDigits(String input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if (!Char.IsDigit(c))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        private static void Part2(string[] args)
        {
            Console.WriteLine("   Part 2");
            String[] lines = System.IO.File.ReadAllLines(TestInput1);
            Console.WriteLine("\tRead {0} inputs", lines.Length);


            Dictionary<String, Node> Nodes = new Dictionary<string, Node>();
            HashSet<Node> Node_Set = new HashSet<Node>();
            HashSet<String> Node_names = new HashSet<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = NormalizeInputLine2(lines[i]);
             //   Console.WriteLine($"\t{lines[i]}");
            }

            foreach (String line in lines)
            {
                String[] parts = line.Split('|');
                String id = parts[0];
                Node_names.Add(id);
                Node n = new Node(id);
                Node_Set.Add(n);
                Nodes[id] = n;
            }


            foreach (String s in Node_names)
            {
                Node n = Nodes[s];
                bool in_node_set = Node_Set.Contains(n);
                if (!in_node_set)
                {
                    Console.WriteLine($"Node ID: {s}, Node_Set: {in_node_set}, Node: {n}");
                }
              
            }

            
            foreach (String line in lines)
            {
               // Console.WriteLine($"\t{line}");
                if (line.Contains("null null")) continue;
                String[] parts = line.Split('|');
                foreach (String p in parts)
                {
                    Console.Write($"{p}- ");
                }
                Console.WriteLine();
                
                String node_id = parts[0];
                Node root_node = Nodes[node_id];
                
                Console.WriteLine($"\t {root_node}");
                
                
                for (int j = 1; j < parts.Length; j++)
                {
                    String[] entry_parts = parts[j].Split(' ');
                    int weight = int.Parse(entry_parts[0]);
                    String leaf_id = $"{entry_parts[1]} {entry_parts[2]}";
                    Node leaf_node = Nodes[leaf_id];
                    root_node.edges.Add(leaf_node);
                    root_node.edge_weights[leaf_node] = weight;
                    Console.WriteLine( $"\t >{parts[j]}->{entry_parts[0]}|{entry_parts[1]}|{entry_parts[2]}");
                }
                
            }

            Console.WriteLine("==========\n");
            foreach (Node n in Node_Set)
            {
                Console.WriteLine($"\t{n}");
            }

            Console.WriteLine($"\n\tPart 2 Solution: {0}");
        }
    }
}


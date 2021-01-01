using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


/*
	Solutions found:
	Part 1: 30197
	Part 2: 34031
	
*/

namespace advent_2020
{
    static class AOC_22
    {
        private const string Part1Input = "aoc_22_input_1.txt";
        private const string Part2Input = "aoc_22_input_2.txt";
        private const string TestInput1 = "aoc_22_test_1.txt";
        private const string TestInput2 = "aoc_22_test_2.txt";


        public static void Run(string[] args)
        {
            Console.WriteLine("AoC Problem 22");
            var watch = Stopwatch.StartNew();
        //    Part1();
            watch.Stop();
            long time_part_1 = watch.ElapsedMilliseconds;
            Console.Write("\n");
            watch = Stopwatch.StartNew();
            Part2();
            watch.Stop();
            long time_part_2 = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution time, Part 1: {time_part_1} ms\t Part 2: {time_part_2} ms");
        }


        private static void Part1()
        {
            Console.WriteLine("   Part 1");
            string[] lines = File.ReadAllLines(Part1Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);

            Queue<int> my_deck = new Queue<int>();
            Queue<int> crab_deck = null;

            int index = 1;
            while (index < lines.Length)
            { 
                //Console.WriteLine($"\t{lines[index]}");
                
                
                if (lines[index].Equals(""))
                {
                    index = index + 2;
                    crab_deck = new Queue<int>();
                }

                if (crab_deck == null)
                {
                    my_deck.Enqueue(int.Parse(lines[index]));                    
                }
                else
                {
                    crab_deck.Enqueue(int.Parse(lines[index]));
                }
                index++;
            }
            
//              Console.WriteLine($"\t My   Deck: {Utility.QueueToStringLine(my_deck)}");
//              Console.WriteLine($"\t Crab Deck: {Utility.QueueToStringLine(crab_deck)}");
            int round = 0;
            while((my_deck.Count > 0) && (crab_deck.Count > 0))
            {
            //    Console.WriteLine($"\t My   Deck: {Utility.QueueToStringLine(my_deck)}");
              //  Console.WriteLine($"\t Crab Deck: {Utility.QueueToStringLine(crab_deck)}");

                round++;
                int m, c;
                m = my_deck.Dequeue();
                c = crab_deck.Dequeue();
                if (m > c)
                {
                    my_deck.Enqueue(m);
                    my_deck.Enqueue(c);
                }
                else
                {
                    crab_deck.Enqueue(c);
                    crab_deck.Enqueue(m);
                }
            }
	
			var	ppair = (my_deck.Count > crab_deck.Count) ? (my_deck, "Player 1") : (crab_deck, "Player 2");
       //     Console.WriteLine($"\n\tAfter Round {round}:");
        //    Console.WriteLine($"\t My   Deck: {Utility.QueueToStringLine(my_deck)}");
        //    Console.WriteLine($"\t Crab Deck: {Utility.QueueToStringLine(crab_deck)}");
            long score = Score(ppair.Item1);
         //   Console.WriteLine($"\t{p} wins with score {score}");
            
            

            Console.WriteLine($"\n\tPart 1 Solution: {score}");
        }

		private static Queue<int> Player1;
		private static Queue<int> Player2;

        private static void Part2()
        {
            Console.WriteLine("   Part 2");
            string[] lines = File.ReadAllLines(Part2Input);
            Console.WriteLine("\tRead {0} inputs", lines.Length);
			Player1 = new Queue<int>();
			Player2 = new Queue<int>();

            Queue<int> activeQueue = Player1;

            for(int x = 1; x< lines.Length;x++)
            {
                if (lines[x] == "")
                {
                    x = x + 1;
                    activeQueue = Player2;
                }else
                {
                    activeQueue.Enqueue(int.Parse(lines[x]));
                }
            }

		//	Console.WriteLine($"\t Player 1: {Utility.QueueToStringLine(Player1)}");
			
		//	Console.WriteLine($"\t Player 2: {Utility.QueueToStringLine(Player2)}");
			

   			CombatGame game = new CombatGame(Player1, Player2);
            Winner winner = PlayUntilWin(Player1, Player2, game.SeenHandsPlayer1, game.SeenHandsPlayer2);


            int answer = 0;
			Queue<int> winning_player_hand;
			
			if(winner == Winner.Player_1) {
				winning_player_hand = game.Player1;
			} else {
				winning_player_hand = game.Player2;
			}
   
			int[] cards_r_a = winning_player_hand.Reverse().ToArray();
        
            for (int x = 0; x < cards_r_a.Length; x++)
            {
                answer += (x + 1) * cards_r_a[x];
            }

			String final_result = answer.ToString();
            Console.WriteLine($"\n\tPart 2 Solution: {final_result}");
        }

	private static String FlatJoinString(Queue<int> elems) {
			StringBuilder sb = new StringBuilder();
			foreach(int e in elems) {
				sb.Append(e.ToString());
				sb.Append(",");
			}
			sb.Remove(sb.Length-1, 1);
			return sb.ToString();
		}


 private static  Winner PlayUntilWin(Queue<int> Player1, Queue<int> Player2, HashSet<String> SeenHandsPlayer1, HashSet<String> SeenHandsPlayer2)
        {
            while ((Player1.Count > 0) && (Player2.Count > 0)) {
			          if (SeenHandsPlayer1.Contains(FlatJoinString(Player1)) 
						|| SeenHandsPlayer2.Contains(FlatJoinString(Player2)))
                {
                    // default to player 1
                    return Winner.Player_1;
                }
				
				SeenHandsPlayer1.Add(FlatJoinString(Player1));
				SeenHandsPlayer2.Add(FlatJoinString(Player2));
				
				int p1_card = Player1.Dequeue();
                int p2_card = Player2.Dequeue();

               
              
                    if (Player1.Count >= p1_card && Player2.Count >= p2_card)
                    {
                        
                    	Queue<int> player1_new_hand = new Queue<int>(Player1.Take(p1_card));
                        Queue<int> player2_new_hand = new Queue<int>(Player2.Take(p2_card));

						CombatGame new_game;
						new_game = new CombatGame(player1_new_hand, player2_new_hand);
                        Winner winner = PlayUntilWin(player1_new_hand,player1_new_hand,new_game.SeenHandsPlayer1,new_game.SeenHandsPlayer2);

                        if (winner == Winner.Player_1)
                        {
                            
                            Player1.Enqueue(p1_card);
                            Player1.Enqueue(p2_card);
							continue;
                        } else
                        {
                            Player2.Enqueue(p2_card);
                            Player2.Enqueue(p1_card);
							continue;
                        }
                    }
                

                if (p1_card > p2_card)
                {
                    Player1.Enqueue(p1_card);
                    Player1.Enqueue(p2_card);
                } else
                {
                    Player2.Enqueue(p2_card);
                    Player2.Enqueue(p1_card);
                }
            }
			if(Player1.Count > 0) {
				return Winner.Player_1;
			} else {
				return Winner.Player_2;
			}

        }



        private static long Score(Queue<int> deck)
        {
            Stack<long> rev = new Stack<long>();
            while (deck.Count > 0)
            {
                long d = deck.Dequeue();
                rev.Push(d);
            }

            long score = 0;
            long mult = 0;

            while (rev.Count > 0)
            {
                mult++;
                long top = rev.Pop();
                score = (mult * top) + score;
            }
            return score;
        }


       private  class CombatGame
    	{
        public HashSet<string> SeenHandsPlayer1 = null;
        public HashSet<string> SeenHandsPlayer2 = null;

        public Queue<int> Player1 = null;
        public Queue<int> Player2 = null;
        
        public CombatGame(Queue<int> player_1, Queue<int> player_2)
        {
           Player1 = new Queue<int>(player_1);
		   
         Player2 = new Queue<int>(player_2);
		
        
			 SeenHandsPlayer1 = new HashSet<String>();
        	 SeenHandsPlayer2 = new HashSet<String>();
		
		
		}

	


       
    }
	}
	public enum Winner {
		Player_1, Player_2
	}
}
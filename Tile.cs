using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_2020
{ 
    
    public class Tile : IEquatable<Tile>
    {
        public bool Equals(Tile other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return tile_id == other.tile_id && Equals(patch, other.patch);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Tile) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (tile_id * 397) ^ (patch != null ? patch.GetHashCode() : 0);
            }
        }

        public static bool operator ==(Tile left, Tile right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Tile left, Tile right)
        {
            return !Equals(left, right);
        }


        public static Dictionary<Direction, Direction> OppositeDirection;
        public static readonly int t_width = 10;
        public static readonly int t_height = 10;
        public static Dictionary<char, char> ToBinary;


        static Tile()
        {
            Tile.ToBinary= new Dictionary<char, char>(2);
            ToBinary['#'] = '1';
            ToBinary['.'] = '0';
            
            
            Tile.OppositeDirection  = new Dictionary<Direction, Direction>();
            Tile.OppositeDirection[Direction.LEFT] = Direction.RIGHT;
            Tile.OppositeDirection[Direction.RIGHT] = Direction.LEFT;
            Tile.OppositeDirection[Direction.UP] = Direction.DOWN;
            Tile.OppositeDirection[Direction.DOWN] = Direction.UP;
            
        }

      

        public static String[] Tile_UpperLeft_Raw =
        {
            "Tile 1613:",
            "####...#..",
            "#.#.......",
            "..........",
            "#....#####",
            "#..#....##",
            "...#..#...",
            ".....#...#",
            ".#......##",
            "##....#..#",
            "....#..###"
        };



        
        
        
        
        public int tile_id;
        public char[,] patch;
        public Dictionary<Direction, int> side_numbers;
        
        
        public int Left
        {
            get => side_numbers[Direction.LEFT];
            set => side_numbers[Direction.LEFT]= value;
        }

        public int Right
        {
         
            get => side_numbers[Direction.RIGHT];
            set => side_numbers[Direction.RIGHT]= value;
        }

        public int Down
        {
            
            get => side_numbers[Direction.DOWN];
            set => side_numbers[Direction.DOWN]= value;
        }

        public int Up
        {
            
            get => side_numbers[Direction.UP];
            set => side_numbers[Direction.UP]= value;
        }


        public Tile(int id, char[,] pp)
        {
            tile_id = id;
            patch = pp;
            side_numbers = GetSides();
        
            List<int> side_ints = GetPossibleSides();
        }

       
        
        public static int BinaryStringToInt(string s)
        {
            int n = Convert.ToInt32(s, 2);
            return n;
        }
        public static string SideNumberToString(int s)
        {
            string t = Convert.ToString(s, 2).PadLeft(t_width, '0');
            return t;
        }

       
        public static int ReverseSideNumber(int s)
        {
            string t = Convert.ToString(s, 2).PadLeft(t_width, '0');
            char[] cc = t.ToCharArray();
            Array.Reverse(cc);
            int r = Convert.ToInt32(new String(cc), 2);
            return r;
        }


        public static char[,] OrientPatch(char[,] patch, Orientation o)
        {
            return OrientPatch(patch, o.flip, o.rot);
        }

        

        public static char[,]  OrientPatch(char[,] patch, Tile_Flip flip, Tile_Rotate_Left rot)
        {
            char[,] grid = (char[,]) patch.Clone();
            

          
            if (flip == Tile_Flip.X_Flip)
            {
                char[,] new_grid = new char[t_width, t_height];
                for (int y = 0; y < t_height; y++)
                for (int x = 0; x < t_width; x++)
                    new_grid[t_width - 1 - x, y] = grid[x, y];

                grid = new_grid;
            }
          

            
            /*
            if (flip == Tile_Flip.Y_Flip || flip == Tile_Flip.XY_Flip)
            {
                char[,] new_grid = new char[t_width, t_height];
                for (int y = 0; y < t_height; y++)
                for (int x = 0; x < t_width; x++)
                    new_grid[x, t_height - 1 - y] = grid[x, y];

                grid = new_grid;
            }*/
            
            while (rot != Tile_Rotate_Left.None)
            {
                char[,] new_grid = new char[t_width, t_height];
                for (int y = 0; y < t_height; y++)
                for (int x = 0; x < t_width; x++)
                    new_grid[y, t_height - 1 - x] = grid[x, y];

                grid = new_grid;
                rot--;
            }

            
            return grid;
        }
        



        

        
        public Dictionary<Direction,int> GetSides()
        {

            char[,] pp = this.patch;
            int left, right, up, down;
            StringBuilder sb_left = new StringBuilder();
            StringBuilder sb_right = new StringBuilder();
            StringBuilder sb_up = new StringBuilder();
            StringBuilder sb_down = new StringBuilder();

            for (int y = 0; y < t_height; y++)
            {
                char l, r;
                l = pp[0, y];
                r = pp[t_width - 1, t_height - 1 - y];
                sb_left.Append(ToBinary[l]);
                sb_right.Append(ToBinary[r]);
            }

            for (int x = 0; x < t_width; x++)
            {
                char t, b;
                t = pp[t_width - 1 - x, 0];
                b = pp[x, t_height - 1];
                sb_up.Append(ToBinary[t]);
                sb_down.Append(ToBinary[b]);
            }

            Dictionary<Direction, int> sides = new Dictionary<Direction, int>(4);
            left = Convert.ToInt32(sb_left.ToString(), 2);
            right = Convert.ToInt32(sb_right.ToString(), 2);
            up = Convert.ToInt32(sb_up.ToString(), 2);
            down = Convert.ToInt32(sb_down.ToString(), 2);
            sides[Direction.LEFT] = left;
            sides[Direction.RIGHT] = right;
            sides[Direction.UP] = up;
            sides[Direction.DOWN] = down;
            return sides;
        }
        


        public void PrintPatchWithSides()
        {

            Utility.BarPrint();
            for (int y = 0; y < t_height; y++)
            {
                Console.Write("\t ");
                for (int x = 0; x < t_width; x++) Console.Write(this.patch[x, y]);
                if (y == 0)
                {
                    Console.WriteLine($"\t tile_id".PadRight(7, ' ')
                                      + $" : {Convert.ToString(this.Left, 2).PadLeft(t_width, '0')} : {this.tile_id}");

                }else  if (y == 1)
                {
                

                    Console.WriteLine($"\t left".PadRight(7, ' ')
                                      + $" : {Convert.ToString(this.Left, 2).PadLeft(t_width, '0')} : {this.Left}"
                                      + $"({ReverseSideNumber(this.Left)}".PadLeft(7, ' ') + ")");
                } else if (y == 2)
                {

                    Console.WriteLine($"\t right".PadRight(7, ' ')
                                      + $" : {Convert.ToString(this.Right, 2).PadLeft(t_width, '0')} : {this.Right}"
                                      + $"({ReverseSideNumber(this.Right)}".PadLeft(7, ' ') + ")");
                } else if (y == 3)
                {

                    Console.WriteLine($"\t up".PadRight(7, ' ')
                                      + $" : {Convert.ToString(Up, 2).PadLeft(t_width, '0')} : {this.Up}"
                                      + $"({ReverseSideNumber(this.Up)}".PadLeft(7, ' ') + ")");
                } else if (y == 4)
                {


                    Console.WriteLine($"\t down".PadRight(7, ' ')
                                      + $" : {Convert.ToString(Down, 2).PadLeft(t_width, '0')} : {Down}"
                                      + $"({ReverseSideNumber(this.Down)}".PadLeft(7, ' ') + ")");
                }
                else
                {


                    Console.WriteLine();
                }
            }
            Utility.BarPrint();
        }
        
        public static void PrintPatch(char[,] pp)
        {
            StringBuilder line; 
            for (int y = 0; y < t_height; y++)
            {
                Console.Write("");
                line = new StringBuilder();
                for (int x = 0; x < t_width; x++)
                {
                    line.Append(pp[x, y]);
                }
                Console.Write(line.ToString().PadRight(28,' '));
                
                Console.WriteLine();
            }
        }

        public void Print()
        {
            string id_tag = "id: ";

            Console.WriteLine($"{id_tag.PadRight(5)}{tile_id.ToString().PadLeft(5)}");
            PrintPatch(patch);
        }

        public int GetSideNum(char[,] patch, Direction facing)
        {
       
            StringBuilder sb = new StringBuilder(12);

            switch (facing)
            {
                case Direction.UP:
                    for (int x = 0; x < t_width; x++) sb.Append(ToBinary[patch[t_width - 1 - x, 0]]);

                    break;
                case Direction.DOWN:
                    for (int x = 0; x < t_width; x++) sb.Append(ToBinary[patch[x, t_height - 1]]);

                    break;
                case Direction.LEFT:
                    for (int y = 0; y < t_height; y++)
                    {
                        char p = patch[0, y];
                        char b = ToBinary[p];
                        sb.Append(b);
                    } 
                    //sb.Append(ToBinary[patch[0, y]]);

                    break;
                case Direction.RIGHT:
                    for (int y = 0; y < t_height; y++) sb.Append(ToBinary[patch[t_width - 1, t_height - 1 - y]]);

                    break;
                default:
                    throw new ArgumentException($"{facing} is not valid Directions");
            }

            string s = sb.ToString();
            return BinaryStringToInt(s);
        }

    
        public List<int> GetCurrentSides()
        {
            return new List<int>(side_numbers.Values);
            
        }


        public List<int> GetPossibleSides()
        {
            HashSet<int> result = new HashSet<int>(GetCurrentSides());
            HashSet<int> r_sides = new HashSet<int>();
            foreach (int s in result)
            {
                String ss = Convert.ToString(s, 2);
                ss = ss.PadLeft(t_width, '0');
                char[] rev;
                rev = ss.ToCharArray();
                Array.Reverse(rev);
                int n_s = Convert.ToInt32(new String(rev), 2);
                r_sides.Add(n_s);
            }
            result.UnionWith(r_sides);
            return new List<int>(result);
        }




        public static List<Tile> ParseTiles(String[] lines)
        {
            int last_parsed_id = -1;
            Tile current_tile = null;
            List<Tile> tile_list = new List<Tile>();
            for (int i = 0; i < lines.Length; i++)
            {
                //current_tile = new Tile();

                int new_tile_id = -1;
                String ln = lines[i];
                if (ln.StartsWith("Tile"))
                {
                    char[] id_nums = new char[4];
                    id_nums[0] = ln[5];
                    id_nums[1] = ln[6];
                    id_nums[2] = ln[7];
                    id_nums[3] = ln[8];
                    new_tile_id = int.Parse(new String(id_nums));

                    last_parsed_id = new_tile_id;
                    //	Console.WriteLine($"\t id: {current_tile.tile_id}");
                }
                else
                {
                    char[,] patch = new char[t_width, t_height];
                    for (int y = 0; y < t_height; y++)
                    {
                        char[] c_line = lines[i].ToCharArray();
                        for (int x = 0; x <t_width; x++)
                        {
                            patch[x, y] = c_line[x];
                        }

                        i++;
                    }

                    //current_tile.patch = patch;
                    //current_tile.tile_id = last_parsed_id;
                    current_tile = new Tile(last_parsed_id, patch);

                    //current_tile.Print();
                    //Console.WriteLine();

                    tile_list.Add(current_tile);
                    current_tile = null;
                }
            }

            return tile_list;
        }

        public override string ToString()
        {
            return $"|{this.tile_id}|";
        }

        static public List<Tile> SwapTile(List<Tile> tile_list, int id, Tile UpperLeft)
        {
            Tile Tile_1613 = null;
            foreach (Tile t_tile in tile_list)
            {
                if (t_tile.tile_id == 1613)
                {
                    Tile_1613 = t_tile;
                    break;
                }
            }

            if (Tile_1613 == null) return tile_list;
            tile_list.Remove(Tile_1613);
            tile_list.Add(UpperLeft);
            return tile_list;
        }

       

    }
    public  enum Direction
    {
        LEFT,RIGHT,UP,DOWN
    }

  

}
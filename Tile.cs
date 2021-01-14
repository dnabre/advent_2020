using System;
using System.Collections.Generic;
using System.Text;

namespace advent_2020
{
    public class Tile
    {
        private static readonly int t_width = 10;
        private static readonly int t_height = 10;
        public static Dictionary<char, char> ToBinary;
        public Tile_Flip flip = Tile_Flip.None;

        public int left, right, down, up;
        public char[,] patch;
        public Tile_Rotate_Left rot = Tile_Rotate_Left.None;
  
        public int tile_id;


        public Tile(int id, char[,] pp)
        {
            tile_id = id;
            patch = pp;

            SetOrient(Tile_Flip.None, Tile_Rotate_Left.None);
            List<int> side_ints = GetPossibleSides();
          //  side_nums = new HashSet<int>(side_ints);

            
        }

        private static int BinaryStringToInt(string s)
        {
            int n = Convert.ToInt32(s, 2);
            return n;
        }

        public char[,] GetOnlyPatch(Tile_Flip flip, Tile_Rotate_Left rot)
        {
            char[,] n_patch = new char[t_width - 1, t_height - 1];
            for (int y = 1; y < t_height - 1; y++)
            for (int x = 1; x < t_width - 1; x++)
                n_patch[x - 1, y - 1] = patch[x, y];

            return n_patch;
        }

        public static int ReverseSideNumber(int s)
        {
            string t = Convert.ToString(s, 2).PadLeft(t_width, '0');
            char[] cc = t.ToCharArray();
            Array.Reverse(cc);
            int r = Convert.ToInt32(new String(cc), 2);
            return r;
        }
        
        public static string SideNumberToString(int s)
        {
            string t = Convert.ToString(s, 2).PadLeft(t_width, '0');
            return t;
        }

        public char[,] GetOrient(Orientation o)
        {
            return GetOrient(o.flip, o.rot);
        }

        public char[,] GetOrient(Tile_Flip flip, Tile_Rotate_Left rot)
        {
            char[,] grid = (char[,]) patch.Clone();
            while (rot != Tile_Rotate_Left.None)
            {
                char[,] new_grid = new char[t_width, t_height];
                for (int y = 0; y < t_height; y++)
                for (int x = 0; x < t_width; x++)
                    new_grid[y, t_height - 1 - x] = grid[x, y];

                grid = new_grid;
                rot--;
            }

            if (flip == Tile_Flip.X_Flip || flip == Tile_Flip.XY_Flip)
            {
                char[,] new_grid = new char[t_width, t_height];
                for (int y = 0; y < t_height; y++)
                for (int x = 0; x < t_width; x++)
                    new_grid[t_width - 1 - x, y] = grid[x, y];

                grid = new_grid;
            }

            if (flip == Tile_Flip.Y_Flip || flip == Tile_Flip.XY_Flip)
            {
                char[,] new_grid = new char[t_width, t_height];
                for (int y = 0; y < t_height; y++)
                for (int x = 0; x < t_width; x++)
                    new_grid[x, t_height - 1 - y] = grid[x, y];

                grid = new_grid;
            }


            return grid;
        }

        public void PrintSides()
        {
            Console.WriteLine("\t ---sides----");
            Console.WriteLine($"\t left   : {Convert.ToString(left, 2).PadLeft(t_width, '0')} : {left}");
            Console.WriteLine($"\t right : {Convert.ToString(right, 2).PadLeft(t_width, '0')} : {right}");
            Console.WriteLine($"\t up    : {Convert.ToString(up, 2).PadLeft(t_width, '0')} : {up}");
            Console.WriteLine($"\t down  : {Convert.ToString(down, 2).PadLeft(t_width, '0')} : {down}");
            Console.WriteLine("\t ------------");
        }

        public void SetOrient(Orientation o)
        {
            this.SetOrient(o.flip, o.rot);
        }
        
        public void SetOrient(Tile_Flip flip, Tile_Rotate_Left rot)
        {
            patch = GetOrient(flip, rot);
            StringBuilder sb_left = new StringBuilder();
            StringBuilder sb_right = new StringBuilder();
            StringBuilder sb_up = new StringBuilder();
            StringBuilder sb_down = new StringBuilder();

            for (int y = 0; y < t_height; y++)
            {
                char l, r;
                l = patch[0, y];
                r = patch[t_width - 1, t_height - 1 - y];
                sb_left.Append(ToBinary[l]);
                sb_right.Append(ToBinary[r]);
            }

            for (int x = 0; x < t_width; x++)
            {
                char t, b;
                t = patch[t_width - 1 - x, 0];
                b = patch[x, t_height - 1];
                sb_up.Append(ToBinary[t]);
                sb_down.Append(ToBinary[b]);
            }


            left = Convert.ToInt32(sb_left.ToString(), 2);
            right = Convert.ToInt32(sb_right.ToString(), 2);
            up = Convert.ToInt32(sb_up.ToString(), 2);
            down = Convert.ToInt32(sb_down.ToString(), 2);
        }

        public static void PrintPatch(char[,] pp)
        {
            for (int y = 0; y < t_height; y++)
            {
                Console.Write("\t ");
                for (int x = 0; x < t_width; x++) Console.Write(pp[x, y]);

                Console.WriteLine();
            }
        }

        public void Print()
        {
            string id_tag = "id: ";

            Console.WriteLine($"\t {id_tag.PadRight(5)}{tile_id.ToString().PadLeft(5)}");
            PrintPatch(patch);
        }

        public int GetSideNum(char[,] patch, Directions facing)
        {
            StringBuilder sb = new StringBuilder(12);

            switch (facing)
            {
                case Directions.UP:
                    for (int x = 0; x < t_width; x++) sb.Append(ToBinary[patch[t_width - 1 - x, 0]]);

                    break;
                case Directions.DOWN:
                    for (int x = 0; x < t_width; x++) sb.Append(ToBinary[patch[x, t_height - 1]]);

                    break;
                case Directions.LEFT:
                    for (int y = 0; y < t_height; y++) sb.Append(ToBinary[patch[0, y]]);

                    break;
                case Directions.RIGHT:
                    for (int y = 0; y < t_height; y++) sb.Append(ToBinary[patch[t_width - 1, t_height - 1 - y]]);

                    break;
                default:
                    throw new ArgumentException($"{facing} is not valid Directions");
            }

            string s = sb.ToString();
            return BinaryStringToInt(s);
        }


        public bool GetOrientWhere(Directions facing, int side_id, out Orientation orient)
        {
            foreach (Orientation o in AOC_20.all_orientations)
            {
                char[,] patch;
                int n = -1;
                patch = GetOrient(o);
                n = GetSideNum(patch, facing);
                if (n == side_id)
                {
                    orient = o;
                    return true;
                }
            }

            orient = new Orientation(Tile_Flip.None, Tile_Rotate_Left.None);
            return false;
        }

        public List<int> GetCurrentSides()
        {
            List<int> result = new List<int>();
            result.Add(this.left);
            result.Add(this.right);
            result.Add(this.up);
            result.Add(this.down);
            return result;
        }
        
        
        public List<int> GetPossibleSides()
        {
            List<int> result = GetCurrentSides();


            List<int> r_sides = new List<int>();
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
   
            result.AddRange(r_sides);

           
            return result;
        }

        public List<int> GetSideNums()
        {
            return GetPossibleSides();
        }


        private static string HashDotToBinary(string s)
        {
            char[] from = s.ToCharArray();
            char[] c_to = new char[from.Length];
            for (int i = 0; i < from.Length; i++)
            {
                char ch = from[i];
                if (ch == '#')
                    c_to[i] = '1';
                else
                    c_to[i] = '0';
            }

            return new string(c_to);
        }


        private static List<int> BinaryStringListToInt(List<string> sides)
        {
            List<int> result = new List<int>(sides.Count);
            foreach (string s in sides)
            {
                int n = Convert.ToInt32(s, 2);
                result.Add(n);
            }

            return result;
        }
    }
}
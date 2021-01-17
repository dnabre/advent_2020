using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_2020
{
    public class Tile : IEquatable<Tile>,IComparable<Tile>, IComparable
    {
        public bool Equals(Tile other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!ReferenceEquals(this.adj_tiles, other.adj_tiles)) return false;
            if (!ReferenceEquals(this.match_sides, other.match_sides)) return false;
            if (!ReferenceEquals(this.unmatched_sides, other.unmatched_sides)) return false;
            if (!ReferenceEquals(this.side_for_tile, other.side_for_tile)) return false;
            return Utility.Array2DEqual(this.patch, other.patch) && (this.tile_id == other.tile_id);
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
                return ((patch != null ? patch.GetHashCode() : 0) * 397) ^ tile_id;
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

        private static readonly int t_width = 10;
        private static readonly int t_height = 10;
        public static Dictionary<char, char> ToBinary;
        
        
      

        public int left, right, down, up;
        public char[,] patch;
      
  
        public int tile_id;

        public HashSet<Tile> adj_tiles;
        public Dictionary<Tile, HashSet<int>> side_for_tile;
        
        public HashSet<int> unmatched_sides;
        public HashSet<int> match_sides;
       
        
        public Tile(int id, char[,] pp)
        {
            tile_id = id;
            patch = pp;

            SetOrient(Tile_Flip.None, Tile_Rotate_Left.None);
            List<int> side_ints = GetPossibleSides();
            adj_tiles = null;
            unmatched_sides= null;
            match_sides = null;
            side_for_tile = null;
        }

        private static int BinaryStringToInt(string s)
        {
            int n = Convert.ToInt32(s, 2);
            return n;
        }

        public char[,] GetOnlyPatch(Orientation o)
        {
            return GetOnlyPatch(o.flip, o.rot);
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
            Console.WriteLine($"\t left".PadRight(7,' ') +$" : {Convert.ToString(left, 2).PadLeft(t_width, '0')} : {left}");
            Console.WriteLine($"\t right".PadRight(7,' ') +$" : {Convert.ToString(right, 2).PadLeft(t_width, '0')} : {right}");
            Console.WriteLine($"\t up".PadRight(7,' ') +$" : {Convert.ToString(up, 2).PadLeft(t_width, '0')} : {up}");
            Console.WriteLine($"\t down".PadRight(7,' ') +$" : {Convert.ToString(down, 2).PadLeft(t_width, '0')} : {down}");
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

        public bool GetEdgeOrient(Directions m_facing, Directions e_facing, int side_id, out Orientation orient)
        {
            int m_side_num = Tile.ReverseSideNumber(side_id);
            int[] edge_nums = unmatched_sides.ToArray();
            int edge_num_1 = edge_nums[0];
            int edge_num_2 = edge_nums[1];
          

            foreach (Orientation o in AOC_20.all_orientations)
            {
                char[,] patch = this.GetOnlyPatch(o);
                int m_side;
                int e_side;
                switch (m_facing)
                {
                    case Directions.RIGHT:
                        m_side = GetSideNum(patch, Directions.LEFT);
                        e_side = GetSideNum(patch, e_facing);
                        break;
                    case Directions.LEFT:
                        m_side = GetSideNum(patch, Directions.RIGHT);
                        e_side = GetSideNum(patch, e_facing);

                        break;
                    case Directions.DOWN:
                        m_side = GetSideNum(patch, Directions.UP);
                        e_side = GetSideNum(patch, e_facing);

                        break;
                    case Directions.UP:
                        m_side = GetSideNum(patch, Directions.DOWN);
                        e_side = GetSideNum(patch, e_facing);
                        break;
                    default:
                        m_side = -1;
                        e_side = -1;
                        break;
                    
                }
                if ((m_side == m_side_num) &&
                    ((edge_num_1 == e_side) || (edge_num_2 == e_side)))
                {
                    orient = o;
                    return true;
                }
            }
            orient = new Orientation(Tile_Flip.None, Tile_Rotate_Left.None);
            return false;
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

        public List<int> GetSideNums()
        {
            return GetPossibleSides();
        }


        private static string HashDotToBinary(string s)
        {
            char[] from = s.ToCharArray();
            char[] c_to = new char[@from.Length];
            for (int i = 0; i < @from.Length; i++)
            {
                char ch = @from[i];
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

        

        public static List<Tile> ParseTiles(String[] lines) {

            int last_parsed_id = -1;
            Tile current_tile=null;
            List<Tile> tile_list = new List<Tile>();
            for(int i=0; i < lines.Length; i++) {
                //current_tile = new Tile();

                int new_tile_id=-1;
                String ln = lines[i];
                if(ln.StartsWith("Tile")) {
                    char[] id_nums = new char[4];
                    id_nums[0] = ln[5];
                    id_nums[1] = ln[6];
                    id_nums[2] = ln[7];
                    id_nums[3] = ln[8];
                    new_tile_id= int.Parse(new String(id_nums));

                    last_parsed_id = new_tile_id;
                    //	Console.WriteLine($"\t id: {current_tile.tile_id}");
                } else {
                    char[,] patch = new char[AOC_20.t_width,AOC_20.t_height];
                    for(int y=0; y < AOC_20.t_height; y++) {
                        char[] c_line = lines[i].ToCharArray();
                        for(int x=0; x < AOC_20.t_width; x++) {
                            patch[x,y] = c_line[x];
                        }
                        i++;
                    }

                    //current_tile.patch = patch;
                    //current_tile.tile_id = last_parsed_id;
                    current_tile = new Tile(last_parsed_id,patch);
					
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

        public static void PrintTileGrid(Tile[,] grid)
        {
            for (int y = 0; y < 12; y++)
            {

                for (int x = 0; x < 12; x++)
                {
                    Tile n_tile = grid[x, y];
                    String r;
                    if (n_tile == null)
                    {
                        r = "NULL";
                    }
                    else
                    {
                        r = $"{n_tile.tile_id}";
                    }

                    Console.Write($"| {r}");
                }

                Console.WriteLine();
                Console.WriteLine($"------------------------------------------------------------------------");

            }
        }

        public int CompareTo(Tile other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            int tileIdComparison = tile_id.CompareTo(other.tile_id);
            if (tileIdComparison != 0) return tileIdComparison;
            int rightComparison = right.CompareTo(other.right);
            if (rightComparison != 0) return rightComparison;
            int leftComparison = left.CompareTo(other.left);
            if (leftComparison != 0) return leftComparison;
            int downComparison = down.CompareTo(other.down);
            if (downComparison != 0) return downComparison;
            return up.CompareTo(other.up);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is Tile other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(Tile)}");
        }

        public static bool operator <(Tile left, Tile right)
        {
            return Comparer<Tile>.Default.Compare(left, right) < 0;
        }

        public static bool operator >(Tile left, Tile right)
        {
            return Comparer<Tile>.Default.Compare(left, right) > 0;
        }

        public static bool operator <=(Tile left, Tile right)
        {
            return Comparer<Tile>.Default.Compare(left, right) <= 0;
        }

        public static bool operator >=(Tile left, Tile right)
        {
            return Comparer<Tile>.Default.Compare(left, right) >= 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_2020
{
    public class Tile : IEquatable<Tile>, IComparable<Tile>, IComparable
    {
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Tile) obj);
        }

        public override int GetHashCode()
        {
            return tile_id;
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

        public static string[] Tile_UpperLeft_Raw =
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

        public Dictionary<Tile, int> Adj_Tile_Side_map;
        public HashSet<Tile> Adjacent_Tiles;

        public (int, int) grid = (-1, -1);
        public bool oriented = false;
        public char[,] patch;
        public bool pinned;
        public Dictionary<Direction, int> side_numbers;

        public int tile_id;


        static Tile()
        {
            ToBinary = new Dictionary<char, char>(2);
            ToBinary['#'] = '1';
            ToBinary['.'] = '0';


            OppositeDirection = new Dictionary<Direction, Direction>();
            OppositeDirection[Direction.LEFT] = Direction.RIGHT;
            OppositeDirection[Direction.RIGHT] = Direction.LEFT;
            OppositeDirection[Direction.UP] = Direction.DOWN;
            OppositeDirection[Direction.DOWN] = Direction.UP;
        }


        public Tile(int id, char[,] pp)
        {
            tile_id = id;
            patch = pp;
            side_numbers = GetSides();

            List<int> side_ints = GetPossibleSides();
        }


        public int Left
        {
            get => side_numbers[Direction.LEFT];
            set => side_numbers[Direction.LEFT] = value;
        }

        public int Right
        {
            get => side_numbers[Direction.RIGHT];
            set => side_numbers[Direction.RIGHT] = value;
        }

        public int Down
        {
            get => side_numbers[Direction.DOWN];
            set => side_numbers[Direction.DOWN] = value;
        }

        public int Up
        {
            get => side_numbers[Direction.UP];
            set => side_numbers[Direction.UP] = value;
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is Tile other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(Tile)}");
        }

        public int CompareTo(Tile other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return tile_id.CompareTo(other.tile_id);
        }

        public bool Equals(Tile other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return tile_id == other.tile_id;
        }

        public static void PrintCompare(Tile a_tile, Tile b_tile)
        {
            Console.Write("\t  " + a_tile.ToString().PadRight(12, ' '));
            Console.WriteLine("      " + b_tile.ToString().PadRight(12, ' '));
            Console.WriteLine("-----------------------------------------");
            Utility.PrintSideBySide2D(a_tile.patch, b_tile.patch);
            Console.WriteLine();
        }


        public Orientation OrientTile(Direction facing_a, int a_v, Direction facing_b, int b_v)
        {
            int a1 = ReverseSideNumber(a_v);
            int a2 = ReverseSideNumber(b_v);
            foreach (Orientation o in Orientation.AllOrientations)
            {
                char[,] o_patch = OrientPatch(patch, o.flip, o.rot);
                Dictionary<Direction, int> n_sides = GetSides(o_patch);
                int b1 = n_sides[facing_a];
                int b2 = n_sides[facing_b];
                if ((a1 == b1) && (a2 == b2))
                {
                    return o;
                }

            }
           
            Console.WriteLine($"No orient found for {this}.OrientTile({facing_a},{a_v}" 
                              +$",{facing_b}, {b_v}) ");
            return Orientation.InvalidOrientation;
        }
        
        
        
        public Orientation OrientTileOnEdge(Direction facing, int v, Direction edge_dir)
        {
            int side_target = ReverseSideNumber(v);
            HashSet<int> non_edges = new HashSet<int>();
            foreach (int i in Adj_Tile_Side_map.Values)
            {
                non_edges.Add(i);
                non_edges.Add(Tile.ReverseSideNumber(i));
            }


            foreach (Orientation o in Orientation.AllOrientations)
            {
                char[,] o_patch = OrientPatch(patch, o.flip, o.rot);
                Dictionary<Direction, int> n_sides = GetSides(o_patch);
                int t = n_sides[facing];
                int e = n_sides[edge_dir];
                if (t == side_target && !non_edges.Contains(e)) return o;
            }

            Console.WriteLine($"No orient found for {this}.OrientTileOnEdge({facing},{v},{edge_dir}) ");
            return Orientation.InvalidOrientation;
        }

        public void Pin(int x, int y)
        {
            if (pinned)
            {
                Console.WriteLine(
                    $"Error {this} is already pinned at [{grid}] trying to pin at ({x},{y}) instead");
                Environment.Exit(-1);
            }

            grid = (x, y);
            pinned = true;
            AOC_20.final_tile_grid[x, y] = this;
            AOC_20.Used_Tiles.Add(this);
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
            int r = Convert.ToInt32(new string(cc), 2);
            return r;
        }


        public static char[,] OrientPatch(char[,] patch, Orientation o)
        {
            return OrientPatch(patch, o.flip, o.rot);
        }

      
        public void SetOrient(Tile_Flip flip, Tile_Rotate_Left rot)
        {
            // Set grid to a new rotated version of it
  
            patch = OrientPatch(patch, flip, rot);
      
            // Update Side
            side_numbers = GetSides();
            
        }

        public static char[,] OrientPatch(char[,] patch, Tile_Flip flip, Tile_Rotate_Left rot)
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


        public Dictionary<Direction, int> GetSides(char[,] pp)
        {
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


        public Dictionary<Direction, int> GetSides()
        {
            char[,] pp = patch;
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
                for (int x = 0; x < t_width; x++) Console.Write(patch[x, y]);
                if (y == 0)
                    Console.WriteLine("\t tile_id".PadRight(7, ' ')
                                      + $" : {Convert.ToString(Left, 2).PadLeft(t_width, '0')} : {tile_id}");
                else if (y == 1)
                    Console.WriteLine("\t left".PadRight(7, ' ')
                                      + $" : {Convert.ToString(Left, 2).PadLeft(t_width, '0')} : {Left}"
                                      + $"({ReverseSideNumber(Left)}".PadLeft(7, ' ') + ")");
                else if (y == 2)
                    Console.WriteLine("\t right".PadRight(7, ' ')
                                      + $" : {Convert.ToString(Right, 2).PadLeft(t_width, '0')} : {Right}"
                                      + $"({ReverseSideNumber(Right)}".PadLeft(7, ' ') + ")");
                else if (y == 3)
                    Console.WriteLine("\t up".PadRight(7, ' ')
                                      + $" : {Convert.ToString(Up, 2).PadLeft(t_width, '0')} : {Up}"
                                      + $"({ReverseSideNumber(Up)}".PadLeft(7, ' ') + ")");
                else if (y == 4)
                    Console.WriteLine("\t down".PadRight(7, ' ')
                                      + $" : {Convert.ToString(Down, 2).PadLeft(t_width, '0')} : {Down}"
                                      + $"({ReverseSideNumber(Down)}".PadLeft(7, ' ') + ")");
                else
                    Console.WriteLine();
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
                for (int x = 0; x < t_width; x++) line.Append(pp[x, y]);
                Console.Write(line.ToString().PadRight(28, ' '));

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

        public bool IsAdjacent(Tile a_tile)
        {
            return Adjacent_Tiles.Contains(a_tile);
        }

        public List<int> GetPossibleSides()
        {
            List<int> c_sides = GetCurrentSides();
            HashSet<int> r_sides = new HashSet<int>();
            foreach (int s in c_sides)
            {
                string ss = Convert.ToString(s, 2);
                ss = ss.PadLeft(t_width, '0');
                char[] rev;
                rev = ss.ToCharArray();
                Array.Reverse(rev);
                int n_s = Convert.ToInt32(new string(rev), 2);
                r_sides.Add(Math.Max(s, n_s));
            }

            return new List<int>(r_sides);
        }


        public static List<Tile> ParseTiles(string[] lines)
        {
            int last_parsed_id = -1;
            Tile current_tile = null;
            List<Tile> tile_list = new List<Tile>();
            for (int i = 0; i < lines.Length; i++)
            {
                //current_tile = new Tile();

                int new_tile_id = -1;
                string ln = lines[i];
                if (ln.StartsWith("Tile"))
                {
                    char[] id_nums = new char[4];
                    id_nums[0] = ln[5];
                    id_nums[1] = ln[6];
                    id_nums[2] = ln[7];
                    id_nums[3] = ln[8];
                    new_tile_id = int.Parse(new string(id_nums));

                    last_parsed_id = new_tile_id;
                    //	Console.WriteLine($"\t id: {current_tile.tile_id}");
                }
                else
                {
                    char[,] patch = new char[t_width, t_height];
                    for (int y = 0; y < t_height; y++)
                    {
                        char[] c_line = lines[i].ToCharArray();
                        for (int x = 0; x < t_width; x++) patch[x, y] = c_line[x];

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
            return $"|{tile_id}|";
        }


        public static Tile GetCornerTile(Tile a_tile)
        {
            foreach (Tile c_tile in AOC_20.corner_tiles)
            {
                if (c_tile.pinned) continue;
                if (c_tile.IsAdjacent(a_tile)) return c_tile;
            }

            throw new Exception($"No corner list found adjacent to {a_tile}");
        }

        public static Tile GetTileAdjacentToBoth(Tile a_tile, Tile b_tile)
        {
            HashSet<Tile> a_set = new HashSet<Tile>(a_tile.Adjacent_Tiles);

            a_set.IntersectWith(b_tile.Adjacent_Tiles);
            if (a_set.Count == 0)
                throw new Exception(
                    $"Can't find tile adjancent. Intersection: {Utility.HashSetToStringLine(a_set)}" +
                    $" from {a_tile}:{Utility.HashSetToStringLine(a_tile.Adjacent_Tiles)} and " +
                    $"{b_tile}:{Utility.HashSetToStringLine(b_tile.Adjacent_Tiles)}");

            foreach (Tile n_tile in a_set)
            {
                if (n_tile.pinned) continue;
                return n_tile;
            }

            throw new Exception(
                $"Can't find tile adjancent. Intersection: {Utility.HashSetToStringLine(a_set)}" +
                $" from {a_tile}:{Utility.HashSetToStringLine(a_tile.Adjacent_Tiles)} and " +
                $"{b_tile}:{Utility.HashSetToStringLine(b_tile.Adjacent_Tiles)}");
        }


        public static Tile GetTileAdjacentToTileAndEdge(Tile a_tile)
        {
            foreach (Tile c_tile in a_tile.Adjacent_Tiles)
            {
                if (AOC_20.Used_Tiles.Contains(c_tile)) continue;
                if (c_tile.Adjacent_Tiles.Count == 3) return c_tile;
            }

            throw new Exception($"Tile adjacent to {a_tile}:{Utility.HashSetToArray(a_tile.Adjacent_Tiles)} "
                                + "and border not found.");
        }

        public static void PlaceTilesInGrid()
        {
            {
                AOC_20.UpperLeft.Pin(0, 0);
                Tile r_tile = AOC_20.IdLookup[1361];
                Tile d_tile = AOC_20.IdLookup[3769];
                r_tile.Pin(1, 0);
                d_tile.Pin(0, 1);
            }

            for (int x = 2; x < 12 - 1; x++)
            {
                Tile n_tile;
                Tile l_tile = AOC_20.final_tile_grid[x - 1, 0];
                n_tile = GetTileAdjacentToTileAndEdge(l_tile);
                n_tile.Pin(x, 0);
            }

            GetCornerTile(AOC_20.final_tile_grid[12 - 1 - 1, 0]).Pin(11, 0);


            for (int y = 2; y < 12 - 1; y++)
            {
                Tile a_tile = AOC_20.final_tile_grid[0, y - 1];
                Tile n_tile = GetTileAdjacentToTileAndEdge(a_tile);
                n_tile.Pin(0, y);
            }

            GetCornerTile(AOC_20.final_tile_grid[0, 12 - 1 - 1]).Pin(0, 11);

            for (int y = 1; y < 12; y++)
            for (int x = 1; x < 12; x++)
            {
                Tile up_tile = AOC_20.final_tile_grid[x, y - 1];
                Tile left_tile = AOC_20.final_tile_grid[x - 1, y];
                Tile n_tile = GetTileAdjacentToBoth(up_tile, left_tile);
                n_tile.Pin(x, y);
            }
        }

        public static void SetupAdjacents(List<Tile> tile_list)
        {
            HashSet<int>[] PossibleSides = new HashSet<int>[tile_list.Count];
            Tile[] tile_array = tile_list.ToArray();
            for (int i = 0; i < tile_array.Length; i++)
            {
                Tile c_tile = tile_array[i];
                HashSet<int> c_possible_side_set = new HashSet<int>(c_tile.GetPossibleSides());
                PossibleSides[i] = c_possible_side_set;
            }


            for (int i = 0; i < tile_array.Length; i++)
            {
                Tile c_tile = tile_array[i];
                c_tile.Adjacent_Tiles = new HashSet<Tile>(4);
                c_tile.Adj_Tile_Side_map = new Dictionary<Tile, int>();

                for (int j = 0; j < tile_array.Length; j++)
                {
                    if (i == j) continue;
                    Tile o_tile = tile_array[j];
                    HashSet<int> c_sides = new HashSet<int>(PossibleSides[i]);
                    c_sides.IntersectWith(PossibleSides[j]);

                    if (c_sides.Count == 0)
                    {
                        //tiles aren't related 
                    }
                    else
                    {
                        if (c_sides.Count != 1)
                        {
                            Console.WriteLine($" When building adjaceny set between {c_tile} and {o_tile} got more"
                                              + $" than one matching side {Utility.HashSetToStringLine(c_sides)}");
                        }
                        else
                        {
                            int side = c_sides.ToArray()[0];
                            c_tile.Adjacent_Tiles.Add(o_tile);
                            c_tile.Adj_Tile_Side_map[o_tile] = side;
                        }
                    }
                }

                if (c_tile.Adjacent_Tiles.Count == 2) AOC_20.corner_tiles.Add(c_tile);
            }
        }


        public static List<Tile> SwapTile(List<Tile> tile_list, int id, Tile UpperLeft)
        {
            Tile Tile_1613 = null;
            foreach (Tile t_tile in tile_list)
                if (t_tile.tile_id == 1613)
                {
                    Tile_1613 = t_tile;
                    break;
                }

            if (Tile_1613 == null) return tile_list;
            tile_list.Remove(Tile_1613);
            tile_list.Add(UpperLeft);
            return tile_list;
        }

      
    }

    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
}
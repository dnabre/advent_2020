namespace advent_2020
{
    public struct Orientation
    {
        public Tile_Flip flip;
        public Tile_Rotate_Left rot;
        
        public Orientation(Tile_Flip f, Tile_Rotate_Left r)
        {
            flip = f;
            rot = r;
        }

        public override string ToString()
        {
            return $"({flip},{rot})";
        }
        public static readonly Orientation GroundTile = new Orientation(Tile_Flip.None,Tile_Rotate_Left.None);
    }
    
    public enum Tile_Flip {
        None, X_Flip, Y_Flip, XY_Flip
    }
    public enum Tile_Rotate_Left{
        None, One, Two, Three
    }

    public enum Directions
    {
        LEFT,RIGHT,UP,DOWN
    }
}
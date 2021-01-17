using System;
using System.Collections.Generic;

namespace advent_2020
{
    public struct Orientation
    {
        public readonly Tile_Flip flip;
        public readonly Tile_Rotate_Left rot;

        private Orientation(Tile_Flip f, Tile_Rotate_Left r, bool bad)
        {
            if (bad)
            {
                this.flip = (Tile_Flip) (-1);
                this.rot = (Tile_Rotate_Left) (-1);
            }
            else
            {
                if (f < 0)
                {
                    throw new ArgumentException($"Orientation Tile_Flip invalid, received: {f}");
                }
            
                if (r < 0)
                {
                    throw new ArgumentException($"Orientation Tile_Rotate_Left invalid, received: {r}");
                }
            
                flip = f;
                rot = r;
            }
           
        }
        
        public Orientation(Tile_Flip f, Tile_Rotate_Left r)
        {
            if (f < 0)
            {
                throw new ArgumentException($"Orientation Tile_Flip invalid, received: {f}");
            }
            
            if (r < 0)
            {
                throw new ArgumentException($"Orientation Tile_Rotate_Left invalid, received: {r}");
            }
            
            flip = f;
            rot = r;
        }

        public override string ToString()
        {
            if ((int)flip == -1)
            {
                return "(Null Orient)";
            }
            return $"({flip},{rot})";
        }
     public static readonly List<Orientation> AllOrientations ;
        public static readonly Orientation InvalidOrientation;
        
        static Orientation()
        {
            InvalidOrientation = new Orientation((Tile_Flip)(-1),(Tile_Rotate_Left)(-1),true);
            
            AllOrientations = new List<Orientation>();
            foreach (Tile_Flip f in Enum.GetValues(typeof(Tile_Flip)))
            {
                foreach (Tile_Rotate_Left r in Enum.GetValues(typeof(Tile_Rotate_Left)))
                {
                    Orientation.AllOrientations.Add(new Orientation(f, r));
                }
            }
        }
    }
    
    public enum Tile_Flip {
        None, X_Flip, Y_Flip, XY_Flip
    }
    public enum Tile_Rotate_Left{
        None, One, Two, Three
    }
    
   
}
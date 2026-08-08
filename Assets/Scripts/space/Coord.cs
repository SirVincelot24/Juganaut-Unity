using System;
using UnityEngine;

namespace space
{
    public record Coord(int X, int Y)
    {
        public int X { get; } = X;
        public int Y { get; } = Y;

        public Coord Move(Direction direction)
        {
            return direction switch
            {
                Direction.Up => new Coord(X, Y - 1),
                Direction.Down => new Coord(X, Y + 1),
                Direction.Left => new Coord(X - 1, Y),
                Direction.Right => new Coord(X + 1, Y),
                _ => throw new ArgumentException("Invalid direction")
            };
        }
        
        public Vector2 ToVector2(float scale)
        {
            return  new Vector2(X * scale, Y * scale);
        }
    }
}
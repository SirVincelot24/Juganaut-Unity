using System;
using UnityEngine;

namespace space
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class DirectionExtensions
    {
        public static Direction RandomDirection()
        {
            return (Direction)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Direction)).Length);
        }

        public static Vector2 Snap(Vector2 direction)
        {
            var absX = Mathf.Abs(direction.x);
            var absY = Mathf.Abs(direction.y);

            if (absX >= absY)
                return direction.x > 0 ? Vector2.right : Vector2.left;
            if (absX <= absY)
                return direction.y > 0 ? Vector2.up : Vector2.down;
            return Vector2.zero;
        }
        
        public static Direction DirectionFromVector(Vector2 vector)
        {
            vector = Snap(vector);
            if (vector ==  Vector2.up)
                return Direction.Up;
            if (vector ==  Vector2.down)
                return Direction.Down;
            if (vector ==  Vector2.left)
                return Direction.Left;
            if (vector ==  Vector2.right)
                return Direction.Right;
            throw new ArgumentOutOfRangeException();
        }
        
        public static Vector2 DirectionToVector(Direction direction)
        {
            return direction switch
            {
                Direction.Up => Vector2.up,
                Direction.Down => Vector2.down,
                Direction.Left => Vector2.left,
                Direction.Right => Vector2.right,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}
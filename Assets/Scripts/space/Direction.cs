using System;

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
    }
}
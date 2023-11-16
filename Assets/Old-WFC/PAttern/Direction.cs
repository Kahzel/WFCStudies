﻿using System;

namespace WaveFunctionCollapse
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class DirectionHelper
    {
        public static Direction GetOpposite(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, "Not a valid Direction");
            }

        }
    }
}
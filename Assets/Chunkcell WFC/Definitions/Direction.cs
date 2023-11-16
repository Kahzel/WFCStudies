using System.ComponentModel;

namespace Chunkcell_WFC.Definitions
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public class DirectionUtils
    {
        public static Direction GetOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Up;
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
            }
            return Direction.Right;
        }
    }
}
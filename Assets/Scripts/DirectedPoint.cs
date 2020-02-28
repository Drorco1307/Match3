using System.Drawing;

public class DirectedPoint
{
    public Point Point;
    public Direction Direction;
    public int Length;

}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

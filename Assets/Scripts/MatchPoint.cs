using System.Drawing;

/// <summary>
/// A class to represent a point in a match and it's direction in relation to the origin point.
/// </summary>
public class MatchPoint
{
    /// <summary>
    /// x, and y position values
    /// </summary>
    public Point PointPosition;

    /// <summary>
    /// the direction of the match
    /// </summary>
    public Direction Direction;

    /// <summary>
    /// the length of the match
    /// </summary>
    public int MatchLength;

}

/// <summary>
/// a direction for detected matches
/// </summary>
public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

/// <summary>
/// Defines a maze using a dictionary. The dictionary is provided by the
/// user when the Maze object is created. The dictionary will contain the
/// following mapping:
///
/// (x,y) : [left, right, up, down]
///
/// 'x' and 'y' are integers and represents locations in the maze.
/// 'left', 'right', 'up', and 'down' are boolean are represent valid directions
///
/// If a direction is false, then we can assume there is a wall in that direction.
/// If a direction is true, then we can proceed.  
///
/// If there is a wall, then throw an InvalidOperationException with the message "Can't go that way!".  If there is no wall,
/// then the 'currX' and 'currY' values should be changed.
/// </summary>
public class Maze
{
    private readonly Dictionary<ValueTuple<int, int>, bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<ValueTuple<int, int>, bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    // TODO Problem 4 - ADD YOUR CODE HERE
    /// <summary>
    /// Check to see if you can move left.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveLeft()
    {
        // Made a new variable that looks at the current cell's movement options using the characters position. Every other function has the same variable used
        var moves = _mazeMap[(_currX, _currY)];
        // 0 means left. If the moves do not = 0, that means there's a wall to the left and that is why the exception is thrown
        if (!moves[0])
            throw new InvalidOperationException("Can't go that way!");

        // if the path is open to the left, the character moves to the left once
        _currX -= 1;
    }

    /// <summary>
    /// Check to see if you can move right.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveRight()
    {
        var moves = _mazeMap[(_currX, _currY)];
        // Just like before, 1 means right. If the moves do not = 1, that means there's a wall to the right and that is why the exception is thrown
        if (!moves[1])
            throw new InvalidOperationException("Can't go that way!");

        // Just like before, if the path is open to the right, the character moves to the right once
        _currX += 1;
    }

    /// <summary>
    /// Check to see if you can move up.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveUp()
    {
        var moves = _mazeMap[(_currX, _currY)];
        // Just like before, 2 means up. If the moves do not = 2, that means there's a wall above and that is why the exception is thrown
        if (!moves[2])
            throw new InvalidOperationException("Can't go that way!");

        // Just like before, if the path is open above, the character moves up once
        _currY -= 1;
    }

    /// <summary>
    /// Check to see if you can move down.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveDown()
    {
        var moves = _mazeMap[(_currX, _currY)];
        // Just like before, 3 means down. If the moves do not = 3, that means there's a wall below and that is why the exception is thrown
        if (!moves[3])
            throw new InvalidOperationException("Can't go that way!");

        // Just like before, if the path is open below, the character moves down once
        _currY += 1;
    }

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}
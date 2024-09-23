using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeLib.state
{
    public enum SnakeHeadingStatesType
    {
        NORTH,
        EAST,
        WEST,
        SOUTH
    };

    public enum InputType
    {
        LEFT,
        RIGHT,
        FORWARD
    };

    public record Move(int row, int col);


    public interface IState
    {
        Move NextMove(InputType input);
    }
}

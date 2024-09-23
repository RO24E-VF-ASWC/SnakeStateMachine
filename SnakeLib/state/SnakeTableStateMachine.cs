using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeLib.state
{
    class StateAction
    {
        public SnakeHeadingStatesType HeadingState { get; set; } // next state
        public SnakeHeadingStatesType Action { get; set; } // direction the snake should move

    }

    public class SnakeTableStateMachine:IState
    {
        // internal table as StateMachine
        private StateAction[,] _stateMachine;
        private SnakeHeadingStatesType _currentHeadingState;

        public SnakeTableStateMachine()
        {
            _currentHeadingState = SnakeHeadingStatesType.NORTH;

            // initialize table
            _stateMachine = new StateAction[4,3];
            _stateMachine[(int)SnakeHeadingStatesType.NORTH, (int)InputType.LEFT] = new StateAction() { HeadingState = SnakeHeadingStatesType.WEST, Action = SnakeHeadingStatesType.WEST };
            _stateMachine[(int)SnakeHeadingStatesType.NORTH, (int)InputType.RIGHT] = new StateAction() { HeadingState = SnakeHeadingStatesType.EAST, Action = SnakeHeadingStatesType.EAST };
            _stateMachine[(int)SnakeHeadingStatesType.NORTH, (int)InputType.FORWARD] = new StateAction() { HeadingState = SnakeHeadingStatesType.NORTH, Action = SnakeHeadingStatesType.NORTH };

            _stateMachine[(int)SnakeHeadingStatesType.EAST, (int)InputType.LEFT] = new StateAction() { HeadingState = SnakeHeadingStatesType.NORTH, Action = SnakeHeadingStatesType.NORTH };
            _stateMachine[(int)SnakeHeadingStatesType.EAST, (int)InputType.RIGHT] = new StateAction() { HeadingState = SnakeHeadingStatesType.SOUTH, Action = SnakeHeadingStatesType.SOUTH };
            _stateMachine[(int)SnakeHeadingStatesType.EAST, (int)InputType.FORWARD] = new StateAction() { HeadingState = SnakeHeadingStatesType.EAST, Action = SnakeHeadingStatesType.EAST };

            _stateMachine[(int)SnakeHeadingStatesType.WEST, (int)InputType.LEFT] = new StateAction() { HeadingState = SnakeHeadingStatesType.SOUTH, Action = SnakeHeadingStatesType.SOUTH };
            _stateMachine[(int)SnakeHeadingStatesType.WEST, (int)InputType.RIGHT] = new StateAction() { HeadingState = SnakeHeadingStatesType.NORTH, Action = SnakeHeadingStatesType.NORTH };
            _stateMachine[(int)SnakeHeadingStatesType.WEST, (int)InputType.FORWARD] = new StateAction() { HeadingState = SnakeHeadingStatesType.WEST, Action = SnakeHeadingStatesType.WEST };

            _stateMachine[(int)SnakeHeadingStatesType.SOUTH, (int)InputType.LEFT] = new StateAction() { HeadingState = SnakeHeadingStatesType.EAST, Action = SnakeHeadingStatesType.EAST };
            _stateMachine[(int)SnakeHeadingStatesType.SOUTH, (int)InputType.RIGHT] = new StateAction() { HeadingState = SnakeHeadingStatesType.WEST, Action = SnakeHeadingStatesType.WEST };
            _stateMachine[(int)SnakeHeadingStatesType.SOUTH, (int)InputType.FORWARD] = new StateAction() { HeadingState = SnakeHeadingStatesType.SOUTH, Action = SnakeHeadingStatesType.SOUTH };
        }


        public Move NextMove(InputType input)
        {
            // Find next move from current state and input
            SnakeHeadingStatesType nextMove = _stateMachine[(int) _currentHeadingState, (int) input].Action;

            // Find next state from current state and input
            _currentHeadingState = _stateMachine[(int) _currentHeadingState, (int) input].HeadingState;
            return ConvertDirection2Move(nextMove);
        }

        private Move ConvertDirection2Move(SnakeHeadingStatesType nextMove)
        {
            switch (nextMove)
            {
                case SnakeHeadingStatesType.NORTH: return MoveObjects.GoNorth;
                case SnakeHeadingStatesType.EAST: return MoveObjects.GoEast;
                case SnakeHeadingStatesType.SOUTH: return MoveObjects.GoSouth;

                default: return MoveObjects.GoWest;
            }
        }
    }
}

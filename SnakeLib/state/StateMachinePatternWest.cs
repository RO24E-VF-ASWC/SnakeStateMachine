using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeLib.state
{
    public class StateMachinePatternWest:IStateMachinePattern
    {
        private static readonly IStateMachinePattern NORTH = StateObjects.North;
        private static readonly IStateMachinePattern SOUTH = StateObjects.South;


        public IStateMachinePattern NextState(InputType input)
        {
            switch (input)
            {
                case InputType.FORWARD: return this;
                case InputType.LEFT: return SOUTH;
                case InputType.RIGHT: return NORTH;
            }

            return this;
        }

        public Move NextAction(InputType input)
        {
            switch (input)
            {
                case InputType.FORWARD: return MoveObjects.GoWest;
                case InputType.LEFT: return MoveObjects.GoSouth;
                case InputType.RIGHT: return MoveObjects.GoNorth;
            }

            return MoveObjects.GoWest;
        }

        public string Retning => "West";
    }
}

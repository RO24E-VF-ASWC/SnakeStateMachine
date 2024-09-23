using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeLib.state
{
    public interface IStateMachinePattern
    {
        IStateMachinePattern NextState(InputType input);
        Move NextAction(InputType input);

        string Retning { get; }

    }
}

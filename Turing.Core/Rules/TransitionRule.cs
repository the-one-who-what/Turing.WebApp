using System.Diagnostics.CodeAnalysis;
using Turing.Core.Machine;
using Turing.Core.MachineTape;

namespace Turing.Core.Rules
{
    public class TransitionRule
    {
        public MachineState FromState { get; }
        public TapeSymbol Input { get; }
        public MachineState ToState { get; set; }
        public TapeSymbol Output { get; set; }
        public Direction Direction { get; set; }

        public TransitionRule(
            MachineState fromState,
            TapeSymbol input,
            MachineState toState,
            TapeSymbol output,
            Direction direction)
        {
            FromState = fromState;
            Input = input;
            ToState = toState;
            Output = output;
            Direction = direction;
        }

        public TransitionRule(
            MachineState fromState,
            TapeSymbol input,
            Direction direction = 0,
            MachineState? toState = null,
            TapeSymbol? output = null)
        {
            FromState = fromState;
            Input = input;
            Direction = direction;

            ToState = toState ?? fromState;
            Output = output ?? input;
        }
    }
}

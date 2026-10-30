using Turing.Core.Machine;
using Turing.Core.MachineTape;
using Turing.Core.Rules;
using Turing.WebApp.Client.ViewModel;

namespace Turing.WebApp.Client
{
    public static class Samples
    {
        public static RulesTableVm? BinaryIncrement { get; private set; }

        static Samples()
        {
            SetBinaryIncrement();
        }

        static void SetBinaryIncrement()
        {
            BinaryIncrement = new();

            MachineState q0 = BinaryIncrement.FromStates.FirstOrDefault()!.State;
            MachineState q1 = new("q1");
            MachineState q2 = new("q2");

            TapeSymbol s0 = new("0");
            TapeSymbol s1 = new("1");

            BinaryIncrement.AddFromState(q1);
            BinaryIncrement.AddFromState(q2);

            BinaryIncrement.AddInput(s0);
            BinaryIncrement.AddInput(s1);

            TransitionRule r00 = BinaryIncrement.GetRule(q0, s0)!.Rule;
            TransitionRule r01 = BinaryIncrement.GetRule(q0, s1)!.Rule;
            TransitionRule r0b = BinaryIncrement.GetRule(q0, TapeSymbol.Blank)!.Rule;

            TransitionRule r10 = BinaryIncrement.GetRule(q1, s0)!.Rule;
            TransitionRule r11 = BinaryIncrement.GetRule(q1, s1)!.Rule;
            TransitionRule r1b = BinaryIncrement.GetRule(q1, TapeSymbol.Blank)!.Rule;

            TransitionRule r20 = BinaryIncrement.GetRule(q2, s0)!.Rule;
            TransitionRule r21 = BinaryIncrement.GetRule(q2, s1)!.Rule;
            TransitionRule r2b = BinaryIncrement.GetRule(q2, TapeSymbol.Blank)!.Rule;

            r00.Direction = Direction.Right;

            r01.Direction = Direction.Right;

            r0b.Output = TapeSymbol.Blank;
            r0b.Direction = Direction.Left;
            r0b.ToState = q1;

            r10.Output = s1;
            r10.Direction = Direction.None;
            r10.ToState = q2;

            r11.Output = s0;
            r11.Direction = Direction.Left;
            r11.ToState = q1;

            r1b.Output = s1;
            r1b.Direction = Direction.None;
            r1b.ToState = q0;

            r20.Direction = Direction.Left;
            r21.Direction = Direction.Left;

            r2b.Output = TapeSymbol.Blank;
            r2b.Direction = Direction.Right;
            r2b.ToState = q0;

            BinaryIncrement.Initial = q0;
        }
    }
}

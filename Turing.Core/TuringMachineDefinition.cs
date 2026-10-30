using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using Turing.Core.Machine;
using Turing.Core.MachineTape;
using Turing.Core.Rules;

namespace Turing.Core
{
    public class TuringMachineDefinition
    {
        public TransitionRuleSet Rules { get; }
        public MachineState InitialState { get; }

        public TuringMachineDefinition(TransitionRuleSet rules, MachineState initialState)
        {
            Rules = rules;
            InitialState = initialState;
        }
    }
}

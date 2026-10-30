using System.Collections.ObjectModel;
using Turing.Core.Machine;
using Turing.Core.MachineTape;
using Turing.Core.Rules;

namespace Turing.WebApp.Client.ViewModel
{
    public class RulesTableVm
    {
        public ReadOnlyCollection<RuleVm> Rules { get; private set; }
        public ReadOnlyCollection<InputVm> Inputs { get; private set; }
        public ReadOnlyCollection<FromStateVm> FromStates { get; private set; }
        public MachineState Initial { get; set; }

        List<RuleVm> _rules = new();
        List<FromStateVm> _fromStates = new();
        List<InputVm> _inputs = new();

        public RulesTableVm()
        {
            AddInput(TapeSymbol.Blank);
            MachineState first = new MachineState("q0");
            AddFromState(first);
            Initial = first;
        }
        public void AddFromState(MachineState state)
        {
            _fromStates.Add(new FromStateVm(state));
            foreach (InputVm input in _inputs)
            {
                _rules.Add(new RuleVm(_fromStates, _inputs, new TransitionRule(state, input.Input, Direction.None, state, input.Input)));
            }
            FromStates = new(_fromStates);
            Rules = new(_rules);
        }
        public void RemoveFromState(MachineState state)
        {
            _fromStates.RemoveAll(f => f.State == state);
            _rules.RemoveAll(r => r.Rule.FromState == state);

            foreach (RuleVm rule in _rules)
            {
                if (rule.Rule.ToState == state)
                    rule.Rule.ToState = rule.Rule.FromState;
            }

            if (state == Initial)
                Initial = _fromStates[0].State;

            FromStates = new(_fromStates);
            Rules = new(_rules);
        }
        public void AddInput(TapeSymbol symbol)
        {
            _inputs.Add(new InputVm(symbol));
            foreach (FromStateVm fromState in _fromStates)
            {
                _rules.Add(new RuleVm(_fromStates, _inputs, new TransitionRule(fromState.State, symbol, Direction.None, fromState.State, symbol)));
            }
            Inputs = new(_inputs);
            Rules= new(_rules);
        }
        public void RemoveInput(TapeSymbol symbol)
        {
            _inputs.RemoveAll(i => i.Input == symbol);
            _rules.RemoveAll(r => r.Rule.Input == symbol);

            foreach (RuleVm rule in _rules)
            {
                if (rule.Rule.Output == symbol)
                    rule.Rule.Output = rule.Rule.Input;
            }

            Inputs = new(_inputs);
            Rules = new(_rules);
        }
        public RuleVm? GetRule(MachineState fromsState, TapeSymbol input)
        {
            return _rules.FirstOrDefault(r => r.Rule.Input == input && r.Rule.FromState == fromsState);
        }
    }
}

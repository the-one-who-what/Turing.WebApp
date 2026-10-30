using System;
using System.Collections.Generic;
using System.Text;
using Turing.Core.MachineTape;
using Turing.Core.Rules;
using Turing.Core.Snapshots;

namespace Turing.Core.Machine
{
    public class TuringMachine
    {
        readonly TuringMachineDefinition _definition;
        readonly Stack<TransitionRule> _history;

        MachineState _currentState;
        Tape _tape;
        TapeSnapshot _initialTape;

        public Tape Tape => _tape;

        public TuringMachine(TuringMachineDefinition definition, Tape tape)
        {
            _definition = definition;
            _history = new();
            _currentState = definition.InitialState;
            _tape = tape;
            _initialTape = tape.Snapshot();
        }

        public TuringMachine(TuringMachineDefinition definition)
        {
            _definition = definition;
            _history = new();
            _currentState = definition.InitialState;
            _tape = new Tape();
            _initialTape = _tape.Snapshot();
        }

        public IReadOnlyCollection<TransitionRule> History => _history;
        public void SetTape(Tape tape)
        {
            _tape = tape;
            _initialTape = tape.Snapshot();
        }

        public StepResult Step()
        {
            StepResult result = StepResult.Continue;

            if (_currentState.Final)
                result = StepResult.Halt;

            TapeSymbol input = _tape.Read();
            bool hasRuleFor = _definition.Rules.TryGetRule(_currentState, input, out TransitionRule? rule);

            if (hasRuleFor)
            {
                Apply(rule!);
                _history.Push(rule!);
            }
            else
            {
                result = StepResult.NoRule;
            }

            return result;
        }

        void Apply(TransitionRule rule)
        {
            _tape.Write(rule.Output);
            if (rule.Direction == Direction.Right)
                _tape.MoveRight();
            if (rule.Direction == Direction.Left)
                _tape.MoveLeft();
            _currentState = rule.ToState;
        }

        public bool CanUndo => _history.Count > 0;
        public void Undo()
        {
            if (!CanUndo)
                return;

            TransitionRule rule = _history.Pop();
            ApplyReverse(rule);
        }

        void ApplyReverse(TransitionRule rule)
        {
            if (rule.Direction == Direction.Right)
                _tape.MoveLeft();
            if (rule.Direction == Direction.Left)
                _tape.MoveRight();

            _tape.Write(rule.Input);
            _currentState = rule.FromState;
        }

        public void Reset()
        {
            _history.Clear();
            _currentState = _definition.InitialState;
            SetTape(Tape.Restore(_initialTape));
        }
    }
}

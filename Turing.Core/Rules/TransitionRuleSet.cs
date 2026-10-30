using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Turing.Core.Machine;
using Turing.Core.MachineTape;

namespace Turing.Core.Rules
{
    public class TransitionRuleSet : IReadOnlyCollection<TransitionRule>
    {
        List<TransitionRule> _rules;
        public TransitionRuleSet(List<TransitionRule> rules)
        {
            _rules = rules;
        }

        public int Count => _rules.Count;

        public bool TryGetRule(MachineState state, TapeSymbol input, [NotNullWhen(true)] out TransitionRule? rule)
        {
            rule = _rules.FirstOrDefault(r => r.FromState == state && r.Input == input);
            return rule != null;
        }
         
        public IEnumerator<TransitionRule> GetEnumerator() =>
            _rules.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}

using Turing.Core.Machine;
using Turing.Core.Rules;

namespace Turing.WebApp.Client.ViewModel
{
    public class RuleVm
    {
        public Guid ToStateId
        {
            get => FromStates.FirstOrDefault(f => f.State == Rule.ToState)!.Id;
            set => Rule.ToState = FromStates.FirstOrDefault(f => f.Id == value)?.State!;
        }
        public Guid OutputId
        {
            get => Inputs.FirstOrDefault(i => i.Input == Rule.Output)!.Id;
            set => Rule.Output = Inputs.FirstOrDefault(i => i.Id == value)?.Input!;
        }
        public List<FromStateVm> FromStates { get; }
        public List<InputVm> Inputs { get; }
        public TransitionRule Rule { get; }

        public RuleVm(List<FromStateVm> fromStates, List<InputVm> inputs, TransitionRule rule)
        {
            FromStates = fromStates;
            Inputs = inputs;
            Rule = rule;
        }
    }
}

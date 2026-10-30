using Turing.Core.MachineTape;
using Turing.Core.Rules;

namespace Turing.WebApp.Client.ViewModel
{
    public class InputVm
    {
        public TapeSymbol Input { get; }
        public Guid Id { get; }

        public InputVm(TapeSymbol input)
        {
            Input = input;
            Id = Guid.NewGuid();
        }
    }
}

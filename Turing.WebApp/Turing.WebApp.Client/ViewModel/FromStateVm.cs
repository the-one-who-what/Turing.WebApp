using Turing.Core.Machine;

namespace Turing.WebApp.Client.ViewModel
{
    public class FromStateVm
    {
        public MachineState State { get; }
        public Guid Id { get; }

        public FromStateVm(MachineState state)
        {
            State = state;
            Id = Guid.NewGuid();
        }
    }
}

using System.Diagnostics.CodeAnalysis;

namespace Turing.Core.Machine
{
    public class MachineState
    {
        public string Name { get; set; }
        public bool Final { get; set; }

        public MachineState(string name, bool final = false)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name), $"{nameof(Name)} cannot be null");
            Name = name;
            Final = final;
        }
    }
}

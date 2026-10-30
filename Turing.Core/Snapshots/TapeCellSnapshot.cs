using Turing.Core.MachineTape;

namespace Turing.Core.Snapshots
{
    public class TapeCellSnapshot
    {
        public TapeSymbol Symbol { get; }

        public TapeCellSnapshot(TapeSymbol symbol)
        {
            Symbol = symbol;
        }
    }
}

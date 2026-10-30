using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace Turing.Core.Snapshots
{
    public class TapeSnapshot
    {
        public ImmutableList<TapeCellSnapshot> Cells { get; }
        public int CurrentIndex { get; }
        public TapeSnapshot(IEnumerable<TapeCellSnapshot> cells, int currentIndex)
        {
            Cells = ImmutableList.CreateRange(cells);
            CurrentIndex = currentIndex;
        }
    }
}

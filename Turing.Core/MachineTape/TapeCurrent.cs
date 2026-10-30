namespace Turing.Core.MachineTape
{
    internal readonly struct TapeCurrent
    {
        public TapeCell Cell { get; }
        public TapePosition Position { get; }

        public TapeCurrent(TapeCell cell, TapePosition position)
        {
            Cell = cell;
            Position = position;
        }
    }
}

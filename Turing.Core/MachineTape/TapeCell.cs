using System;
using System.Collections.Generic;
using System.Text;

namespace Turing.Core.MachineTape
{
    public class TapeCell
    {
        public TapeSymbol Symbol { get; internal set; } = TapeSymbol.Blank;

        TapeCell? _previous;
        internal TapeCell GetPrevious()
        {
            if (_previous == null)
                _previous = new() { _next = this };
            return _previous;
        }
        TapeCell? _next;
        internal TapeCell GetNext()
        {
            if (_next == null)
                _next = new() { _previous = this };
            return _next;
        }

        public TapeCell? PeekNext() =>
            _next;
        public TapeCell? PeekPrevious() =>
            _previous;
        
    }
}

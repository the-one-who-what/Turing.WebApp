using Turing.Core.Snapshots;

namespace Turing.Core.MachineTape
{
    public class Tape
    {
        public TapeCell Current => _current.Cell;
        TapeCurrent _current;

        TapeCell _leftmostWritten;
        TapeCell _rightmostWritten;

        public Tape()
        {
            _current = new TapeCurrent(new TapeCell(), TapePosition.Between);
            _leftmostWritten = _current.Cell;
            _rightmostWritten = _current.Cell;
        }

        public void Write(TapeSymbol symbol)
        {
            if (_current.Position == TapePosition.BehindLeftmost)
                _leftmostWritten = _current.Cell;
            if (_current.Position == TapePosition.AheadRightmost)
                _rightmostWritten= _current.Cell;

            _current.Cell.Symbol = symbol;
        }

        public TapeSymbol Read() =>
            _current.Cell.Symbol;

        public void WriteAt(TapeSymbol symbol, int offset)
        {
            TapeCurrent initial = _current;
            try
            {
                MoveBy(offset);
                Write(symbol);
            }
            finally
            {
                _current = initial;
            }
        }
        public TapeSymbol ReadAt(int offset)
        {
            TapeCurrent initial = _current;
            try
            {
                MoveBy(offset);
                TapeSymbol result = Read();
                return result;
            }
            finally
            {
                _current = initial;
            }
        }
        public TapeCell? PeekAt(int offset)
        {
            TapeCell? cell = _current.Cell;
            if (offset > 0)
            {
                for (int i = 0; i < offset; i++)
                {
                    if (cell == null)
                        return null;

                    cell = cell.PeekNext();
                }
            }
            else if (offset < 0)
            {
                for (int i = 0; i > offset; i--)
                {
                    if (cell == null)
                        return null;

                    cell = cell.PeekPrevious();
                }
            }
            return cell;
        }
        public void MoveRight()
        {
            TapePosition newPosition = _current.Position;

            if (_current.Cell == _leftmostWritten)
                newPosition = TapePosition.Between;

            if (_current.Cell == _rightmostWritten)
                newPosition = TapePosition.AheadRightmost;

            _current = new TapeCurrent(_current.Cell.GetNext(), newPosition);
        }

        public void MoveLeft()
        {
            TapePosition newPosition = _current.Position;

            if (_current.Cell == _rightmostWritten)
                newPosition = TapePosition.Between;

            if (_current.Cell == _leftmostWritten)
                newPosition = TapePosition.BehindLeftmost;

            _current = new TapeCurrent(_current.Cell.GetPrevious(), newPosition);
        }

        public void MoveBy(int offset)
        {
            if (offset > 0)
            {
                for (int i = 0; i < offset; i++)
                {
                    MoveRight();
                }
            }
            else if (offset < 0)
            {
                for (int i = 0; i > offset; i--)
                {
                    MoveLeft();
                }
            }
        }

        public TapeSnapshot Snapshot()
        {
            TapeCell first = _current.Position == TapePosition.BehindLeftmost ? _current.Cell : _leftmostWritten;
            int currentIndexCounter = 0;
            int currentIndex = 0;

            List<TapeCellSnapshot> cellSnapshots = new();

            TapeCell? currentCell = first;
            while (currentCell != null)
            {
                cellSnapshots.Add(new TapeCellSnapshot(currentCell.Symbol));
                if (currentCell == _current.Cell)
                    currentIndex = currentIndexCounter;

                currentIndexCounter += 1;
                currentCell = currentCell.PeekNext();
            }

            return new TapeSnapshot(cellSnapshots, currentIndex);
        }

        public static Tape Restore(TapeSnapshot snapshot)
        {
            Tape tape = new();
            TapeCurrent current = tape._current;

            for (int i = 0; i < snapshot.Cells.Count; i++)
            {
                TapeCellSnapshot cellSnapshot = snapshot.Cells[i];
                tape.Write(cellSnapshot.Symbol);

                if (i == snapshot.CurrentIndex)
                    current = tape._current;

                if (i < snapshot.Cells.Count - 1)
                    tape.MoveRight();
            }
            tape._current = current;
            return tape;
        }
    }
}

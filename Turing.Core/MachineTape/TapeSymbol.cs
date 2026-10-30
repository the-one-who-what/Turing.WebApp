namespace Turing.Core.MachineTape
{
    public class TapeSymbol
    {
        public string? Value { get; set; }
        public bool IsBlank { get; }

        public static readonly TapeSymbol Blank = new TapeSymbol(null, true);

        TapeSymbol(string? value, bool isBlank)
        {
            Value = value;
            IsBlank = isBlank;
        }

        public TapeSymbol(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Value cannot be null for not-blank Symbol.");

            Value = value;
            IsBlank = false;
        }
    }
}

namespace Google.Protobuf;

public sealed partial class DecimalValue
{
    private const decimal NanoFactor = 1_000_000_000;

    public DecimalValue(long units, int nanos)
    {
        Units = units;
        Nanos = nanos;
    }

    public static implicit operator decimal(DecimalValue decimalValue)
    {
        return decimalValue.Units + decimalValue.Nanos / NanoFactor;
    }

    public static implicit operator decimal?(DecimalValue decimalValue)
    {
        if (decimalValue == null) return null;
        return (decimal)decimalValue;
    }

    public static implicit operator DecimalValue(decimal value)
    {
        var units = decimal.ToInt64(value);
        var nanos = decimal.ToInt32((value - units) * NanoFactor);
        return new DecimalValue(units, nanos);
    }

    public static implicit operator DecimalValue(decimal? value)
    {
        if (value == null) return null;
        return (DecimalValue)value.Value;
    }
}

using CsvHelper.Configuration.Attributes;

namespace Ingots.Parsers.Operations;

public readonly record struct KeytradeOperation : IRawOperation
{
    [Index( 1 )] public DateTime Date { get; init; }
    [Index( 4 )] public string Description { get; init; }
    [Index( 5 )][TypeConverter(typeof(KeytradeDecimalConverter))] public decimal Value { get; init; }

    public RawOperation ToRawOperation()
        => new RawOperation { Date = Date , Description = Description , Value = Value , Counterpart = string.Empty };
}
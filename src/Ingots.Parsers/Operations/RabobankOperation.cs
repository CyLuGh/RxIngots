using CsvHelper.Configuration.Attributes;

namespace Ingots.Parsers.Operations;

public readonly record struct RabobankOperation : IRawOperation
{
    [Index( 0 )] public DateTime Date { get; init; }
    [Index( 3 )][TypeConverter(typeof(RabobankDecimalConverter))] public decimal Value { get; init; }
    [Index(5)] public string Counterpart { get; init; }
    [Index(7)] public string Communication { get; init; }

    public RawOperation ToRawOperation()
    {
        throw new NotImplementedException();
    }
}
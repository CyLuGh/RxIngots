using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Ingots.Parsers.Operations;

internal class KeytradeDecimalConverter : DefaultTypeConverter
{
    private DecimalConverter Converter { get; } = new();
    public override object? ConvertFromString( string? text , IReaderRow row , MemberMapData memberMapData )
    {
        var fixedText = text?.Replace( " " , "" ) ?? string.Empty;
        return Converter.ConvertFromString( fixedText, row, memberMapData );
    }
}
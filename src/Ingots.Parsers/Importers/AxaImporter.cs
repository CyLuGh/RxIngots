using CsvHelper;
using CsvHelper.Configuration;
using Ingots.Parsers.Operations;
using System.Globalization;
using System.Text;

namespace Ingots.Parsers.Importers;

public class AxaImporter : IOperationImporter<AxaOperation>
{
    public string Name => "AXA";
    
    public IEnumerable<AxaOperation> Parse( string filePath )
    {
        var config = new CsvConfiguration( CultureInfo.GetCultureInfo( "fr-fr" ) )
        {
            Delimiter = ";",
            HasHeaderRecord = true,
            Encoding = Encoding.Latin1
        };
        
        using var reader = new StreamReader( filePath );
        using var csv = new CsvReader( reader , config);

        return csv.GetRecords<AxaOperation>().ToArray();
    }
}
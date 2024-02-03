using CsvHelper;
using CsvHelper.Configuration;
using Ingots.Parsers.Operations;
using System.Globalization;
using System.Text;

namespace Ingots.Parsers.Importers;

public class FintroImporter : IOperationImporter<FintroOperation>
{
    public string Name => "Fintro";
    
    public IEnumerable<FintroOperation> Parse( string filePath )
    {
        var config = new CsvConfiguration( CultureInfo.GetCultureInfo( "en-gb" ) )
        {
            Delimiter = ";",
            HasHeaderRecord = true,
            Encoding = Encoding.UTF32
        };
        
        using var reader = new StreamReader( filePath );
        using var csv = new CsvReader( reader , config);

        return csv.GetRecords<FintroOperation>().ToArray();
    }
}

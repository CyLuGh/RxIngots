using CsvHelper;
using CsvHelper.Configuration;
using Ingots.Parsers.Operations;
using System.Globalization;
using System.Text;

namespace Ingots.Parsers.Importers;

public class RabobankImporter : IOperationImporter<RabobankOperation>
{
    public string Name => "Rabobank";
    
    public IEnumerable<RabobankOperation> Parse( string filePath )
    {
        var config = new CsvConfiguration( CultureInfo.GetCultureInfo( "fr-BE" ) )
        {
            Delimiter = ";",
            HasHeaderRecord = true,
            Encoding = Encoding.UTF8
        };
        
        using var reader = new StreamReader( filePath );
        using var csv = new CsvReader( reader , config);

        return csv.GetRecords<RabobankOperation>().ToArray();
    }
}
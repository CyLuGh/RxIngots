namespace Ingots.Parsers;

public interface IOperationImporter
{
    IEnumerable<RawOperation> Parse( string filePath );
}
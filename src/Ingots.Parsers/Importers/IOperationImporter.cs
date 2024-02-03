using Ingots.Parsers.Operations;

namespace Ingots.Parsers.Importers;

public interface IOperationImporter<out T> where T : IRawOperation
{
    string Name { get; }
    IEnumerable<T> Parse( string filePath );
}
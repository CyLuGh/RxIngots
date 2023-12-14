namespace Ingots.Data;

public interface IDataManager
{
    string FilePath { get; set; }
    string ConnectionString { get; }
    
    Task CreateDatabaseAsync( CancellationToken token = default );
}
using System.Data.SQLite;

namespace Ingots.Data.Sqlite;

public class DataManager : IDataManager
{
    public string FilePath { get; set; } =
        Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ) ,
            "DefaultIngots.db3"); 
    public string ConnectionString => $"Data Source ={FilePath}; Version=3; Compress=true;";
    
    public async Task CreateDatabaseAsync( CancellationToken token = default )
    {
        await using var cn = new SQLiteConnection( ConnectionString + " New=true;");
        await cn.OpenAsync( token );

        await using var tr = cn.BeginTransaction();
        await using var cm = cn.CreateCommand();

        try
        {
            cm.Transaction = tr;

            await CreateOwnersAsync( cm , token );
            await CreateAccountsAsync( cm , token );
            await CreateAccountsOwnersAsync( cm , token );
            await CreateTransactionsAsync( cm , token );
            await CreateTransfersAsync( cm , token );
            
            await tr.CommitAsync( token );
        }
        catch ( Exception )
        {
            await tr.RollbackAsync(token);
            throw;
        }
    }

    private static async Task CreateOwnersAsync( SQLiteCommand cm , CancellationToken token = default )
    {
        cm.CommandText = """
                         CREATE TABLE Owners( 
                             [id] INTEGER NOT NULL PRIMARY KEY,
                             [Name] VARCHAR(50) NOT NULL,
                             [IsSelf] BIT
                         )
                         """;
        await cm.ExecuteNonQueryAsync( token );
    }

    private static async Task CreateAccountsAsync( SQLiteCommand cm , CancellationToken token = default )
    {
        cm.CommandText = """
                         CREATE TABLE Accounts(
                             id INTEGER NOT NULL PRIMARY KEY,
                             iban VARCHAR(50) NOT NULL UNIQUE,
                             bic VARCHAR(20),
                             description VARCHAR(50),
                             startValue DECIMAL(16,2),
                             isDeleted BIT NOT NULL DEFAULT 0,
                             kind INTEGER NOT NULL DEFAULT 0,
                             stash VARCHAR(20) NOT NULL DEFAULT 'None',
                             bank VARCHAR(50) NOT NULL DEFAULT 'Unknown'
                         )
                         """;
        await cm.ExecuteNonQueryAsync( token );
    }

    private static async Task CreateAccountsOwnersAsync( SQLiteCommand cm , CancellationToken token = default )
    {
        cm.CommandText = """
                         CREATE TABLE AccountsOwners(
                             [OwnerId] INTEGER NOT NULL,
                             [AccountId] INTEGER NOT NULL,
                             PRIMARY KEY ([OwnerId],[AccountId]),
                             FOREIGN KEY ([OwnerId]) REFERENCES Owners([id]),
                             FOREIGN KEY ([AccountId]) REFERENCES Accounts([id])
                         )
                         """;
        await cm.ExecuteNonQueryAsync( token );
    }

    private static async Task CreateTransactionsAsync( SQLiteCommand cm , CancellationToken token = default )
    {
        cm.CommandText = """
                         CREATE TABLE Transactions(
                             id BIGINT NOT NULL PRIMARY KEY,
                             id_account INTEGER,
                             date DATETIME,
                             description VARCHAR(128),
                             value DECIMAL(16,2),
                             category VARCHAR(64),
                             subCat VARCHAR(64),
                             shop VARCHAR(64),
                             executed BIT,
                             FOREIGN KEY ([id_account]) REFERENCES Accounts([id])
                         )
                         """;
        await cm.ExecuteNonQueryAsync( token );
    }

    private static async Task CreateTransfersAsync( SQLiteCommand cm , CancellationToken token = default )
    {
        cm.CommandText = """
                         CREATE TABLE Transfers(
                             id BIGINT NOT NULL PRIMARY KEY,
                             id_account INTEGER,
                             date DATETIME,
                             description VARCHAR(128),
                             value DECIMAL(16,2),
                             id_target INTEGER,
                             executed BIT,
                             FOREIGN KEY ([id_account]) REFERENCES Accounts([id])
                         )
                         """;
        await cm.ExecuteNonQueryAsync( token ); 
    }

}
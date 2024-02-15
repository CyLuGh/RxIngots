﻿using Ingots.Core;

namespace Ingots.Data;

public interface IDataManager
{
    string FilePath { get; set; }
    string ConnectionString { get; }
    
    Task CreateDatabaseAsync( CancellationToken token = default );

    Task<Account> AddAccountAsync( Account account , CancellationToken token = default );
}
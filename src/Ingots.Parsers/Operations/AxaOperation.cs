using CsvHelper.Configuration.Attributes;

namespace Ingots.Parsers.Operations;

// extrait; date opération; date valeur; date écriture; montant; solde; description; compte bénéficiare; contrepartie;

public readonly record struct AxaOperation : IRawOperation
{
    [Index( 1 )] public DateTime Date { get; init; }
    [Index( 4 )] public decimal Value { get; init; }
    [Index( 6 )] public string Description { get; init; }
    [Index( 7 )] public string TargetAccount { get; init; }
    [Index( 8 )] public string TargetDescription { get; init; }
    [Index( 12 )] public string Communication { get; init; }
    [Index( 14 )] public string Details { get; init; }

    public RawOperation ToRawOperation() =>
        new RawOperation()
        {
            Date = Date ,
            Value = Value ,
            Counterpart = TargetAccount ,
            Description = string.Join( " " , Communication , Details )
        };
}
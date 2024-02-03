using CsvHelper.Configuration.Attributes;

namespace Ingots.Parsers.Operations;

// Nº de séquence;Date d'exécution;Date valeur;Montant;Devise du compte;Contrepartie;Détails;Numéro de compte;Communication;Nom de la contrepartie

public readonly record struct FintroOperation : IRawOperation
{
    [Index( 1 )] public DateTime Date { get; init; }
    [Index( 3 )] public decimal Value { get; init; }
    [Index( 7 )] public string Counterpart { get; init; }
    [Index( 9 )] public string Communication { get; init; }
    [Index( 10 )] public string Details { get; init; }

    
    public RawOperation ToRawOperation()
    {
        throw new NotImplementedException();
    }
}
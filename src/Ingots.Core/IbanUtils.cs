using System.Text;

namespace Ingots.Core;

public static class IbanUtils
{
    public static string Format( string input )
    {
        try
        {
            var sb = new StringBuilder();
            int i = 0;
            do
            {
                sb.Append( input.AsSpan( i , (int) Math.Min( 4 , input.Length - i ) ) );
                sb.Append( ' ' );
                i += 4;
            } while ( i < input.Length );

            return sb.ToString().Trim();
        }
        catch ( Exception )
        {
            return input;
        }
    }

    // Valid example: BE68539007547034
    // DE89 3704 0044 0532 0130 00
    // AT483200000012345864
    // SC52BAHL01031234567890123456USD
    public static bool IsValid( string? iban )
    {
        if ( string.IsNullOrWhiteSpace( iban ) ) return false;

        try
        {
            var sb = new StringBuilder();

            foreach ( string s in iban.Split( ' ' ) ) sb.Append( s.ToUpper() );

            if ( sb.Length < 10 ) return false;

            string buffer = sb.ToString();

            var reversed = new StringBuilder( buffer[4..] ).Append( buffer[..4] );
            var transformed = new StringBuilder();

            for ( int i = 0 ; i < reversed.Length ; i++ )
            {
                char c = reversed[i];

                if ( !char.IsDigit( c ) )
                    transformed.Append( ValueOf( c ) );
                else
                    transformed.Append( c );
            }

            long mod = 0;

            while ( transformed.Length > 8 )
            {
                mod = Mod97( transformed.ToString()[..8] );
                transformed = new StringBuilder( mod.ToString() ).Append( transformed.ToString()[8..] );
            }

            mod = Mod97( transformed.ToString() );

            return mod == 1;
        }
        catch ( Exception )
        {
            return false;
        }
    }

    public static string UnFormat( string input )
    {
        var sb = new StringBuilder();
        foreach ( var c in input )
        {
            if ( c != ' ' ) sb.Append( c );
        }
        return sb.ToString();
    }

    private static long Mod97( string s ) =>
        Convert.ToInt64( s ) % 97;

    private static int ValueOf( char c ) =>
        10 + (int) c - (int) 'A';
}
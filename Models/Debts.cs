namespace Namiokai.Models;

public class Debts
{
    public string MantelisToKlaidui { get; set; }
    public string MantelisToKlaideliui { get; set; }
    public string KlaidasToMantui { get; set; }
    public string KlaidasToKlaideliui { get; set; }
    public string KlaidelisToMantui { get; set; }
    public string KlaidelisToKlaidui { get; set; }

    public Debts(string mantelisToKlaidui, string mantelisToKlaideliui, string klaidasToMantui, string klaidasToKlaideliui, string klaidelisToMantui, string klaidelisToKlaidui)
    {
        MantelisToKlaidui = mantelisToKlaidui;
        MantelisToKlaideliui = mantelisToKlaideliui;
        KlaidasToMantui = klaidasToMantui;
        KlaidasToKlaideliui = klaidasToKlaideliui;
        KlaidelisToMantui = klaidelisToMantui;
        KlaidelisToKlaidui = klaidelisToKlaidui;
    }
    public Debts()
    {

    }

    public Debts(string debtsLiteral)
    {
        var split = debtsLiteral.Split(';', StringSplitOptions.RemoveEmptyEntries);
        if (split.Length != 6) return;
            
        MantelisToKlaidui = split[0];
        MantelisToKlaideliui = split[1];
        KlaidasToMantui = split[2];
        KlaidasToKlaideliui = split[3];
        KlaidelisToMantui = split[4];
        KlaidelisToKlaidui = split[5];
    }

    public override string ToString()
    {
        return $"{MantelisToKlaidui};{MantelisToKlaideliui};{KlaidasToMantui};{KlaidasToKlaideliui};{KlaidelisToMantui};{KlaidelisToKlaidui}";
    }
}
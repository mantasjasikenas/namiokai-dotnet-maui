namespace Namiokai.Models;

public class Fuel
{
    public bool IsValid { get; set; }
    public DateTime Date { get; set; }
    public string Passengers { get; set; }
    public bool TripToHome { get; set; }
    public bool TripToKaunas { get; set; }

    public Fuel(bool isValid, DateTime date, string passengers, bool tripToHome, bool tripToKaunas)
    {
        IsValid = isValid;
        Date = date;
        Passengers = passengers;
        TripToHome = tripToHome;
        TripToKaunas = tripToKaunas;
    }
    public Fuel()
    {
            
    }
        
        
}
namespace Namiokai.Models;

public class ShoppingItem
{
    public string Date { get; set; }
    public string WhoPaid { get; set; }
    public string ShoppingList { get; set; }
    public bool MantelisSplit { get; set; }
    public string TotalPrice { get; set; }
    public bool KlaidasSplit { get; set; }
    public bool KlaidelisSplit { get; set; }
    public double? TotalPerson { get; set; }


    public ShoppingItem(string date, string whoPaid, string shoppingList, string totalPrice, string mantelisSplit,
        string klaidasSplit, string klaidelisSplit, string totalSplit = default)
    {
        Date = date;
        WhoPaid = whoPaid;
        ShoppingList = shoppingList;
        TotalPrice = totalPrice?.Trim('€').Replace('.', ',');
        MantelisSplit = bool.Parse(mantelisSplit);
        KlaidasSplit = bool.Parse(klaidasSplit);
        KlaidelisSplit = bool.Parse(klaidelisSplit);
        TotalPerson = double.Parse(totalSplit?.Trim('€'));
    }

    public ShoppingItem(string date, string whoPaid, string shoppingList, string totalPrice, bool mantelisSplit,
        bool klaidasSplit, bool klaidelisSplit)
    {
        Date = date;
        WhoPaid = whoPaid;
        ShoppingList = shoppingList;
        TotalPrice = totalPrice?.Trim('€').Replace('.', ',');
        MantelisSplit = mantelisSplit;
        KlaidasSplit = klaidasSplit;
        KlaidelisSplit = klaidelisSplit;
    }
}
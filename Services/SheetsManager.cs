
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace Namiokai.Services;

public class SheetsManager
{
    private readonly string credentials;
    private SheetsService sheetsService;
    private readonly string spreadsheetId = "10rRpIasdAhIUE8w8WJwC6K8AQhY1RQiNygE2iz3UH-8";

    private readonly string rangeShopping = "Shopping!A6:H";
    private readonly string rangeFuel = "Fuel!A7:E7";

    private readonly string rangeFuelDebts = "Fuel!K7:K8";
    private readonly string rangeSummaryDebts = "Summary!H6:H15";
    private readonly string rangeSummaryPaid = "Summary!G6:G15";

    private readonly string rangeClearShopping = "Shopping!A6:G";
    private readonly string rangeClearFuel = "Fuel!A7:E";

    private const int shoppingId = 1754341746;
    private const int fuelId = 2087465977;
    private const int summaryID = 485072624;

    public enum SheetType
    {
        Shopping = shoppingId,
        Fuel = fuelId,
        Summary = summaryID
    }

    public SheetsManager(string credentials)
    {
        this.credentials = credentials;
        Init();
    }

    private void Init()
    {
        if (sheetsService != null)
            return;


        string[] scopes = { SheetsService.Scope.Spreadsheets };
        var credential = GoogleCredential.FromJson(credentials).CreateScoped(scopes);
        var applicationName = "Namiokai";
        
        sheetsService = new SheetsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = applicationName
        });
    }

    public async Task<Debts> GetSummaryDebtsAsync() => await GetDebtsAsync(rangeSummaryDebts);

    public async Task<Debts> GetSummaryPaidAsync() => await GetDebtsAsync(rangeSummaryPaid);

    private async Task<Debts> GetDebtsAsync(string range)
    {
        Init();
        Debts debts = null;

        try
        {
            var request = sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);
            var response = await request.ExecuteAsync();
            var rows = response.Values;
            var temp = new List<string>();

            foreach (var row in rows)
            {
                if (row.Count > 0)
                {
                    temp.Add(row[0].ToString());
                }
            }

            debts = new Debts(temp[0], temp[1], temp[2], temp[3], temp[4], temp[5]);
        }
        catch
        {
            // ignored
        }

        return debts;
    }

    public async Task<Dictionary<User, string>> GetFuelDebtsAsync()
    {
        Init();
        var debts = new Dictionary<User, string>();

        try
        {
            var request = sheetsService.Spreadsheets.Values.Get(spreadsheetId, rangeFuelDebts);
            var response = await request.ExecuteAsync();
            var rows = response.Values;

            debts.Add(User.Klaidas, rows[0][0].ToString());
            debts.Add(User.Klaidelis, rows[1][0].ToString());

        }
        catch
        {
            // ignored
        }

        return debts;
    }

    public async Task<List<ShoppingItem>> GetShoppingItemsAsync()
    {
        Init();
        List<ShoppingItem> items = new();

        try
        {
            var request = sheetsService.Spreadsheets.Values.Get(spreadsheetId, rangeShopping);
            var response = await request.ExecuteAsync();
            var rows = response.Values;


            foreach (var row in rows)
            {
                if (row.Count != 8) continue;
                
                var item = new ShoppingItem((string)row[0], (string)row[1], (string)row[2], (string)row[3], (string)row[4], (string)row[5], (string)row[6], (string)row[7]);
                items.Add(item);
            }

        }
        catch
        {
            // ignored
        }

        return items;

    }

    public async Task<bool> AppendShoppingItemAsync(ShoppingItem billItem)
    {
        try
        {
            Init();
            ValueRange body = new()
            {
                Values = new List<IList<object>>
                    {new List<object> {billItem.Date, billItem.WhoPaid, billItem.ShoppingList, billItem.TotalPrice, billItem.MantelisSplit, billItem.KlaidasSplit, billItem.KlaidelisSplit}}
            };
            
            var result = sheetsService.Spreadsheets.Values.Append(body, spreadsheetId, rangeShopping);
            result.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            
            await result.ExecuteAsync();
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> AppendFuelAsync(Fuel fuel)
    {
        try
        {
            Init();
            ValueRange body = new()
            {
                Values = new List<IList<object>>
                    {new List<object> {fuel.IsValid, fuel.Date.ToString("yyyy/MM/dd"), fuel.Passengers, fuel.TripToHome, fuel.TripToKaunas}}
            };


            var result = sheetsService.Spreadsheets.Values.Append(body, spreadsheetId, rangeFuel);
            result.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            
            
            await result.ExecuteAsync();
        }
        catch (Exception ex)
        {
            await Shell.Current.CurrentPage.DisplayAlert("", $"{ex.Message}", "");
            return false;
        }

        return true;
    }

    public async Task CopySheetAsync(SheetType sheet)
    {
        var title = sheet + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        var body = new BatchUpdateSpreadsheetRequest
        {
            Requests = new List<Request> {
                new()
                {
                    DuplicateSheet = new DuplicateSheetRequest
                    {
                        SourceSheetId = (int)sheet,
                        NewSheetName = title
                    }

                }
            }
        };

        try
        {
            await sheetsService.Spreadsheets.BatchUpdate(body, spreadsheetId).ExecuteAsync();
        }
        catch
        {
            await Shell.Current.DisplayAlert(Resources.Error, string.Format(Resources.Unable_to_create_copy_of, title), Resources.OK);
        }

    }

    public async Task ClearSheetsAsync()
    {

        var timeLiteral = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var shopping = SheetType.Shopping + " " + timeLiteral;
        var fuel = SheetType.Fuel + " " + timeLiteral;


        var duplicateBody = new BatchUpdateSpreadsheetRequest
        {
            Requests = new List<Request> {
                new()
                {
                    DuplicateSheet = new DuplicateSheetRequest
                    {
                        SourceSheetId = (int)SheetType.Shopping,
                        NewSheetName = shopping
                    }
                },
                new()
                {
                    DuplicateSheet = new DuplicateSheetRequest
                    {
                        SourceSheetId = (int)SheetType.Fuel,
                        NewSheetName = fuel
                    }

                }
            }
        };
        var clearBody = new BatchClearValuesRequest { Ranges = new List<string> { rangeClearShopping, rangeClearFuel } };


        try
        {
            await sheetsService.Spreadsheets.BatchUpdate(duplicateBody, spreadsheetId).ExecuteAsync();
            await sheetsService.Spreadsheets.Values.BatchClear(clearBody, spreadsheetId).ExecuteAsync();
        }
        catch
        {
            await Shell.Current.DisplayAlert(Resources.Error, Resources.ClearSheets_Error, Resources.OK);
        }
        finally
        {
            await Shell.Current.DisplayAlert(Resources.Status, Resources.ClearSheets_success, Resources.OK);
        }

    }


}













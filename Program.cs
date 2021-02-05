using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net.Http;

class Program
{
    static async System.Threading.Tasks.Task Main()
    {
        var response = await new HttpClient().GetAsync("https://www.siepomaga.pl/maja");
        var exchangeRateResponse = await new HttpClient().GetAsync("https://api.exchangeratesapi.io/latest?base=USD");
        var exchangeRate = Convert.ToDecimal(Regex.Match(await exchangeRateResponse.Content.ReadAsStringAsync(), "(?<=PLN\":)\\d+(\\.\\d+)").Value, new CultureInfo("en-US"));
        var amount = Convert.ToDecimal(Regex.Match(await response.Content.ReadAsStringAsync(), "(?<=amount-left\' data-value=\')\\d+(\\.\\d+)").Value, new CultureInfo("en-US"));
        Console.WriteLine($"Maja potrzebuje jeszcze {amount.ToString("C", new CultureInfo("pl-PL"))} lub {(amount / exchangeRate).ToString("C", new CultureInfo("en-US"))}");
    }
}
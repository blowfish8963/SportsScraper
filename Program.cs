using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace SportsScraper;

class Program
{
    static void Main(string[] args)
    {
        var web = new HtmlWeb();
        var doc = web.Load("https://www.basketball-reference.com/boxscores/");

        Scraper.Scrape(doc);

        var builder = new ConfigurationBuilder().AddJsonFile("appSettingsDevelopment.json");
        var recipient = builder.Build().GetSection("Email")["Recipient"];
        Console.WriteLine(recipient);
    }
}
using HtmlAgilityPack;

namespace SportsScraper;

class Program
{
    static void Main(string[] args)
    {
        var web = new HtmlWeb();
        var doc = web.Load("https://www.basketball-reference.com/boxscores/");

        Scraper.Scrape(doc);
    }
}
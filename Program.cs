using HtmlAgilityPack;

namespace SportsScraper;

class Program
{
    static void Main(string[] args)
    {
        var web = new HtmlWeb();
        var doc = web.Load("https://www.basketball-reference.com/boxscores/");
        string body = Scraper.Scrape(doc);

        Mailer.NewMail($"{DateTime.Now.ToString("yyyy-mm-dd")} NBA Report", body);
        Console.WriteLine("Mail sent");
    }
}
using HtmlAgilityPack;

namespace SportsScraper;

class Program
{
    static void Main(string[] args)
    {
        var web = new HtmlWeb();
        var doc = web.Load("https://www.basketball-reference.com/boxscores/");

        var title = doc.DocumentNode.SelectNodes("//div/h1").First().InnerText;
        var teams = doc.DocumentNode.Descendants("table")
        .Where(x => x.GetAttributeValue("class", "")
        .Contains("teams")).ToList();

        Console.WriteLine(title+"\n");
        int count = 1;
        foreach (var team in teams)
        {
            string? winnerName = string.Empty;
            string? winnerScore = string.Empty;
            string? loserName = string.Empty;
            string? loserScore = string.Empty;

            var rows = team.Descendants("tr").ToList();
            foreach (var r in rows)
            {
                var className = r.GetAttributeValue("class", "");
                if (className.Contains("winner"))
                {
                    winnerName = r.Descendants("td").FirstOrDefault()?.InnerText.Trim();
                    winnerScore = r.Descendants("td").Skip(1).FirstOrDefault()?.InnerText.Trim();
                }
                else if (className.Contains("loser"))
                {
                    loserName = r.Descendants("td").FirstOrDefault()?.InnerText.Trim();
                    loserScore = r.Descendants("td").Skip(1).FirstOrDefault()?.InnerText.Trim();
                }
            }
            Console.WriteLine($"Game {count}");  
            Console.WriteLine($"Winner: {winnerName}\t{winnerScore}");
            Console.WriteLine($"Loser: {loserName}\t{loserScore}\n");
            count++;
        }
    }
}
using HtmlAgilityPack;

namespace SportsScraper;

public class Scraper()
{
    public static string Scrape(HtmlDocument doc)
    {
        var title = doc.DocumentNode.SelectNodes("//div/h1").First().InnerText;
        var teams = doc.DocumentNode.Descendants("table")
        .Where(x => x.GetAttributeValue("class", "")
        .Contains("teams")).ToList();

        int count = 1;
        string response = title+"<br>";
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
            response += $"Game {count}<br>Winner: {winnerName}\t{winnerScore}<br>Loser: {loserName}\t{loserScore}<br><br>";
            count++;
        }
        return response;
    }
}
using HtmlAgilityPack;

namespace warren_analysis_desk;

public class ExtractHtml
{
    public async Task<(HtmlNode firstLink, HtmlNode firstTitle)> GetLinksAndTitles(string rk)
    {
        try
        {
            // var htmlDoc = await new WebFetcher()
            //     .FetchNewsHtml($"https://news.google.com/search?q={rk}%20when%3A1h&hl=pt-BR&gl=BR&ceid=BR%3Apt-419");

            // var firstLink = htmlDoc.DocumentNode
            //     .Descendants("a")
            //     .FirstOrDefault(node => node.GetAttributeValue("class", "")
            //     .Contains("WwrzSb"));

            // var firstTitle = htmlDoc.DocumentNode
            //     .Descendants("button")
            //     .FirstOrDefault(node => node.GetAttributeValue("class", "")
            //     .Contains("VfPpkd-Bz112c-LgbsSe yHy1rc eT1oJ mN1ivc hUJSud"));  

            var htmlDoc = await new WebFetcher()
                .FetchNewsHtml($"https://www.bing.com/news/search?q={rk}&qft=interval%3d%224%22&form=PTFTNR");
            
            var firstLink = htmlDoc.DocumentNode
                .Descendants("a")
                .FirstOrDefault(node => node.GetAttributeValue("class", "")
                .Contains("title"));

            var firstTitle = htmlDoc.DocumentNode
                .Descendants("a")
                .FirstOrDefault(node => node.GetAttributeValue("class", "")
                .Contains("title")); 
            
            return (firstLink, firstTitle);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao encurtar URL: {ex.Message}");
            return (null, null);
        }
    }
}
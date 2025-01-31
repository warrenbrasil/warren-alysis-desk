using HtmlAgilityPack;

namespace warren_analysis_desk
{
    public class WebFetcher
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<HtmlDocument> FetchNewsHtml(string url)
        {
            try 
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(responseBody);

                return htmlDoc;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}

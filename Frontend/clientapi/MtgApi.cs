
namespace Frontend.ClientApi
{
    public class MtgApi
    {
        private readonly HttpClient _httpClient;

        public MtgApi(HttpClient httpClient)
        {
            this._httpClient = httpClient; //new HttpClient();
            this._httpClient.BaseAddress = new Uri("https://api.magicthegathering.io/v1/");
        }

        public async Task<string?> GetSetsFromMtgApi()
        {
            try
            {
                string endpoint = $"sets";
                HttpResponseMessage response = await this._httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return data;
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string?> GetCardsFromSetMtgApi(string setCode)
        {
            try
            {
                string endpoint = $"cards?set={setCode}";
                HttpResponseMessage response = await this._httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return data;
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
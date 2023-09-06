using System.Net.Http;


namespace Frontend.ClientApi
{
    public class ClientApi
    {
        private readonly HttpClient httpClient;

        public ClientApi()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("https://localhost:5001/");
        }

        public async Task<string> GetDataForUser(int userId)
        {
            try
            {
                string endpoint = $"Mtg/user/{userId}";
                HttpResponseMessage response = await this.httpClient.GetAsync(endpoint);

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

        public async Task<string> GetUserByUsernameAndPassword(string username, string password)
        {
            try
            {
                string endpoint = $"Mtg/user/login/{username}/{password}";

                Console.WriteLine(endpoint);
                Console.WriteLine(httpClient.BaseAddress + endpoint);

                HttpResponseMessage response = await this.httpClient.GetAsync(endpoint);
                Console.WriteLine(response);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("1");
                    string data = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(data);
                    //Console.WriteLine("2" + data);
                    return data;
                }
                else
                {
                    //Console.WriteLine("Error: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}

/*
    private readonly HttpClient client = new HttpClient();

    public ClientApi()
    {
        client.BaseAddress = new Uri("https://localhost:5001/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<User> GetUserAsync(string path)
    {
        User user = null;
        HttpResponseMessage response = await client.GetAsync(path);
        if (response.IsSuccessStatusCode)
        {
            user = await response.Content.ReadAsAsync<User>();
        }
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        HttpResponseMessage response = await client.PutAsJsonAsync(
            $"api/users/{user.UserId}", user);
        response.EnsureSuccessStatusCode();

        // Deserialize the updated product from the response body.
        user = await response.Content.ReadAsAsync<User>();
        return user;
    }

    public async Task<HttpStatusCode> DeleteUserAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync(
            $"api/users/{id}");
        return response.StatusCode;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync(
            "api/users", user);
        response.EnsureSuccessStatusCode();

        // Return the URI of the created resource.
        return await response.Content.ReadAsAsync<User>();
    }   
*/
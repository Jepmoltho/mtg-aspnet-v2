using System.Net.Http;
using System.Text;
using Newtonsoft.Json;


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


        public async Task<string?> PostUser(string username, string password)
        {
            try
            {
                string endpoint = $"Mtg/user";
                UserDto userDto = new UserDto(username, password);
                HttpResponseMessage response = await this.httpClient.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8, "application/json"));
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

        public async Task<string?> GetDataForUser(int userId)
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

        public async Task<string?> GetUserByUsernameAndPassword(string username, string password)
        {
            try
            {
                string endpoint = $"Mtg/user/login/{username}/{password}";

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

        public async Task<string?> GetCardsByUserId(int userId)
        {
            try
            {
                string endpoint = $"Mtg/cardsfromuser/{userId}";
                HttpResponseMessage response = await this.httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(data);
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

        public async Task<string?> GetCommandersByUserId(int userId)
        {
            try
            {
                string endpoint = $"Mtg/commanders/{userId}";
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


        public async Task<string?> PostCardToUser(int userId, string title)
        {
            try
            {
                string endpoint = $"Mtg/card/{title}/{userId}";
                HttpResponseMessage response = await this.httpClient.PostAsync(endpoint, new StringContent(title, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Succesfully posted card: " + data);
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

        public async Task<string?> PostCardsToUser(int userId, List<string> cardNames)
        {
            try
            {
                CardsInputDto cards = new CardsInputDto(userId, cardNames);
                var jsonContent = JsonConvert.SerializeObject(cards);

                HttpResponseMessage response = await this.httpClient.PostAsync($"Mtg/cards/{userId}", new StringContent(jsonContent, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Succesfully posted cards: " + data);
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

        public class CardsInputDto
        {
            public CardsInputDto(int userId, List<string> cardNames)
            {
                this.UserId = userId;
                this.CardNames = cardNames;
            }
            public int UserId { get; set; }
            public List<string> CardNames { get; set; }
        }


        public class UserDto
        {
            public string UserName { get; set; }
            public string Password { get; set; }

            public UserDto(string username, string password)
            {
                this.UserName = username;
                this.Password = password;
            }
        }


        // public class CardWithInfo
        // {

        //     public int CardId { get; set; }
        //     public string MtgCardId { get; set; }
        //     public string Title { get; set; }
        //     public string Set { get; set; }
        //     public string SuperType { get; set; }
        //     public string ImgUrl { get; set; }
        //     public int UserId { get; set; }
        // }

        //post a user using this method
        //         curl -X 'POST' \
        //   'https://localhost:5001/Mtg/user' \
        //   -H 'accept: text/plain' \
        //   -H 'Content-Type: application/json' \
        //   -d '{
        //   "userId": 0,
        //   "userName": "Hus",
        //   "password": "Hans"
        // }'
        // Request URL
        // https://localhost:5001/Mtg/user

        //get card objects with info
        // public async Task
        // public async Task<List<Card>> GetCardObjectsByUserId(int userId)
        // {
        //     try
        //     {
        //         string endpoint = $"Mtg/cardsfromuser/{userId}";
        //         HttpResponseMessage response = await this.httpClient.GetAsync(endpoint);
        //         if (response.IsSuccessStatusCode)
        //         {
        //             //List<Card> cards = await response.Content.ReadFromJsonAsync<List<Card>>();
        //             string cards = await response.Content.ReadAsStringAsync();

        //             //Console.WriteLine(cards);

        //             List<Card> cardsList = JsonConvert.DeserializeObject<List<Card>>(cards);

        //             foreach (Card card in cardsList)
        //             {
        //                 Console.WriteLine(card.Title);
        //                 Console.WriteLine(card.ImgUrl);
        //                 Console.WriteLine(card.Set);
        //                 Console.WriteLine(card.Name);
        //             }
        //             // foreach (CardWithInfo card in cardsList)
        //             // {
        //             //     Console.WriteLine(card.Title);
        //             //     Console.WriteLine(card.ImgUrl);
        //             // }
        //             // Console.WriteLine("Fetching card 3");
        //             // foreach (Card card in cardsList)
        //             // {
        //             //     Console.WriteLine(card.Set);
        //             //     Console.WriteLine(card.Id);
        //             // }
        //             //Console.WriteLine(cards);
        //             // foreach (Card card in cards)
        //             // {
        //             //     Console.WriteLine(card.Name);
        //             //     Console.WriteLine(card.ImageUrl);
        //             // }
        //             //string data = await response.Content.ReadAsStringAsync();
        //             //Console.WriteLine(data);
        //             //List<Card> cards = JsonConvert.DeserializeObject<List<Card>>(data);
        //             return cardsList;
        //         }
        //         else
        //         {
        //             Console.WriteLine("Error: " + response.StatusCode);
        //             return null;
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //         return null;
        //     }
        // }
        //post card to user

        // public async Task<string> PostCardsToUser(List<string> cardnames, int userId)
        // {
        //     try
        //     {
        //         string endpoint = $"Mtg/cards/{userId}";
        //         string[] cardNames = cardnames.ToArray();
        //         //HttpResponseMessage response = await this.httpClient.PostAsync(endpoint, 
        //           //await this.httpClient.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(cardnames), Encoding.UTF8, "application/json"));

        //         if (response.IsSuccessStatusCode)
        //         {
        //             string data = await response.Content.ReadAsStringAsync();
        //             Console.WriteLine("Succesfully posted cards: " + data);
        //             return data;
        //         }
        //         else
        //         {
        //             Console.WriteLine("Error: " + response.StatusCode);
        //             return null;
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //         return null;
        //     }
        // }


        //post card to user
        // public async Task<string> PostCardToUser(int userId, string title)
        // {
        //     try
        //     {
        //         string endpoint = $"Mtg/card/{userId}";
        //         HttpResponseMessage response = await this.httpClient.PostAsync(endpoint, new StringContent(title, Encoding.UTF8, "application/json"));

        //         if (response.IsSuccessStatusCode)
        //         {
        //             string data = await response.Content.ReadAsStringAsync();
        //             //Console.WriteLine(data);
        //             return data;
        //         }
        //         else
        //         {
        //             Console.WriteLine("Error: " + response.StatusCode);
        //             return null;
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //         return null;
        //     }
        // }

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
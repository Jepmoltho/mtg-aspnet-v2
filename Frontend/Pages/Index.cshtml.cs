using Microsoft.AspNetCore.Mvc.RazorPages;
using Frontend.ClientApi;
using System.Text.Json;
using Newtonsoft.Json;


namespace mtg_aspnet_v2.Pages
{

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ClientApi _clientApi;
        private readonly MtgApi _mtgApi;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            _clientApi = new ClientApi();
            _mtgApi = new MtgApi();
        }

        public string User { get; set; }
        public int? UserId { get; set; }
        public string? UserName { get; set; }

        public List<string>? OwnedCards { get; set; }

        //Refactor to List<Set> where set is a class with name and code properties
        public List<Set> Sets { get; set; }

        public string CurrSetCode { get; set; }

        public List<Card> Cards { get; set; }

        public async Task OnGetAsync()
        {
            this.Sets = await FetchSets();
            this.OwnedCards = await DisplayOwnedCards();
            this.Cards = await FetchCardsFromSet(this.CurrSetCode); //2ED
        }

        public async Task<List<Set>> FetchSets()
        {
            string setsData = await _mtgApi.GetSetsFromMtgApi();
            string trimFirstSetsData = setsData.Replace("{\"sets\":", "");
            string trimLastSetsData = trimFirstSetsData.Remove(trimFirstSetsData.Length - 1);
            //Console.WriteLine(trimLastSetsData);
            if (!string.IsNullOrEmpty(setsData))
            {
                var setsArray = JsonConvert.DeserializeObject<List<Set>>(trimLastSetsData);
                var setsArrayFiltered = setsArray.Where(s => s.OnlineOnly == false && s.Type == "core" || s.Type == "expansion");
                //this.Sets = setsArrayFiltered.Select(s => s.Name).ToList();
                //return (List<Set>)setsArrayFiltered;
                return setsArray;
                //return setsArrayFiltered.Select(s => s.Name).ToList();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Card>> FetchCardsFromSet(string setCode)
        {
            string cardsData = await _mtgApi.GetCardsFromSetMtgApi(setCode);
            //Console.WriteLine(cardsData);
            string trimFirstCardsData = cardsData.Replace("{\"cards\":", "");
            string trimLastCardsData = trimFirstCardsData.Remove(trimFirstCardsData.Length - 1);
            if (!string.IsNullOrEmpty(cardsData))
            {
                var cardsArray = JsonConvert.DeserializeObject<List<Card>>(trimLastCardsData);
                // foreach (var card in cardsArray)
                // {
                //     Console.WriteLine(card.Name);
                // }
                return cardsArray;
                //return new List<Card>();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<string>> DisplayOwnedCards()
        {
            if (HttpContext.Session.GetInt32("UserId") != null && HttpContext.Session.GetString("UserName") != null)
            {
                this.UserId = HttpContext.Session.GetInt32("UserId"); //(int)HttpContext.Session.GetInt32("UserId");
                this.UserName = HttpContext.Session.GetString("UserName");
                try
                {
                    string cardsData = await _clientApi.GetCardsByUserId((int)this.UserId);
                    if (!string.IsNullOrEmpty(cardsData))
                    {
                        //Console.WriteLine(cardsData);
                        var cardList = JsonConvert.DeserializeObject<List<OwnedCard>>(cardsData);
                        List<string> cardTitles = cardList.Select(c => c.Title).ToList();
                        return cardTitles;
                    }
                    else
                    {
                        return new List<string>();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
            }
            return null;
        }

        public class UserResponse
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public List<string> Cards { get; set; }
        }

        public class OwnedCard
        {
            public string Title { get; set; }
        }

        public class Set
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }

            [JsonIgnore]
            public List<string> Booster { get; set; }
            public DateTime ReleaseDate { get; set; }
            public string Block { get; set; }
            public bool OnlineOnly { get; set; }
        }

        public class Card
        {
            public string Name { get; set; }
            public string ManaCost { get; set; }
            [JsonIgnore]
            public double Cmc { get; set; }
            [JsonIgnore]
            public List<string> Colors { get; set; }
            [JsonIgnore]
            public List<string> ColorIdentity { get; set; }
            public string Type { get; set; }
            [JsonIgnore]
            public List<string> Types { get; set; }
            [JsonIgnore]
            public List<string> Subtypes { get; set; }
            public string Rarity { get; set; }
            public string Set { get; set; }
            public string SetName { get; set; }
            [JsonIgnore]
            public string Text { get; set; }
            [JsonIgnore]
            public string Artist { get; set; }
            public string Number { get; set; }
            public string Power { get; set; }
            public string Toughness { get; set; }
            public string Layout { get; set; }
            public string MultiverseId { get; set; }
            public string ImageUrl { get; set; }
            [JsonIgnore]
            public List<string> Variations { get; set; }
            [JsonIgnore]
            public List<string> ForeignNames { get; set; }
            public List<string> Printings { get; set; }
            public string OriginalText { get; set; }
            public string OriginalType { get; set; }
            [JsonIgnore]
            public List<string> Legalities { get; set; }
            public string Id { get; set; }
        }
    }
}

// public class ForeignName
// {
//     public string Name { get; set; }
//     public string Text { get; set; }
//     public string Type { get; set; }
//     public string Flavor { get; set; }
//     public string ImageUrl { get; set; }
//     public string Language { get; set; }
//     public int MultiverseId { get; set; }
// }

// public class Legality
// {
//     public string Format { get; set; }
//     public string Legality { get; set; }
// }


// public class SetsResponse
// {
//     public List<string> Sets { get; set; }
// }


// public class SetsResponse
// {
//     public List<Set> Sets { get; set; }
// }

// public class Set
// {
//     public string Code { get; set; }
//     public string Name { get; set; }
//     public string Type { get; set; }
//     public List<string> Booster { get; set; }
//     public DateTime ReleaseDate { get; set; }
//     public string Block { get; set; }
//     public bool OnlineOnly { get; set; }
// }



// //Refactor to own function
// if (HttpContext.Session.GetInt32("UserId") != null && HttpContext.Session.GetString("UserName") != null)
// {
//     this.UserId = HttpContext.Session.GetInt32("UserId"); //(int)HttpContext.Session.GetInt32("UserId");
//     this.UserName = HttpContext.Session.GetString("UserName");
//     try
//     {
//         string cardsData = await _clientApi.GetCardsByUserId((int)this.UserId);
//         if (!string.IsNullOrEmpty(cardsData))
//         {
//             //Console.WriteLine(cardsData);
//             var cardList = JsonConvert.DeserializeObject<List<Card>>(cardsData);
//             List<string> cardTitles = cardList.Select(c => c.Title).ToList();
//             this.Cards = cardTitles;
//         }
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine(ex.Message);
//     }
// }

// public void OnGet()
// {
//     if (HttpContext.Session.GetInt32("UserId") != null && HttpContext.Session.GetString("UserName") != null)
//     {
//         this.UserId = HttpContext.Session.GetInt32("UserId"); //(int)HttpContext.Session.GetInt32("UserId");
//         this.UserName = HttpContext.Session.GetString("UserName");
//     }
// }

// public async Task OnGetAsync()
// {
//     User = await _clientApi.GetDataForUser(36);

//     if (User != null)
//     {
//         string username = JsonConvert.DeserializeObject<UserResponse>(User).UserName;
//         Console.WriteLine(username);
//         this.UserName = username;
//     }
// }
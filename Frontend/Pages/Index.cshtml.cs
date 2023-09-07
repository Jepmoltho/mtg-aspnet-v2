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

        public List<string>? Cards { get; set; }

        public List<string> Sets { get; set; }

        public async Task OnGetAsync()
        {
            this.Sets = await FetchSets();
            this.Cards = await DisplayOwnedCards();
        }

        public async Task<List<string>> FetchSets()
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
                return setsArrayFiltered.Select(s => s.Name).ToList();
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
                        var cardList = JsonConvert.DeserializeObject<List<Card>>(cardsData);
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

        public class Card
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
    }
}


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
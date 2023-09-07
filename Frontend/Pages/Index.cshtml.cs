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

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            _clientApi = new ClientApi();
        }

        public string User { get; set; }
        public int? UserId { get; set; }
        public string? UserName { get; set; }

        public List<string>? Cards { get; set; }

        public async Task OnGetAsync()
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
                        Console.WriteLine(cardsData);
                        var cardList = JsonConvert.DeserializeObject<List<Card>>(cardsData);
                        List<string> cardTitles = cardList.Select(c => c.Title).ToList();
                        this.Cards = cardTitles;
                        // foreach (string card in Cards)
                        // {
                        //     Console.WriteLine(card);
                        // }
                        //List<string> cards = JsonConvert.DeserializeObject<List<string>>(cardsData);
                        //this.Cards = cards;
                        //Console.WriteLine("Succes: " + cards);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }




        }

        // public void OnGet()
        // {
        //     if (HttpContext.Session.GetInt32("UserId") != null && HttpContext.Session.GetString("UserName") != null)
        //     {
        //         this.UserId = HttpContext.Session.GetInt32("UserId"); //(int)HttpContext.Session.GetInt32("UserId");
        //         this.UserName = HttpContext.Session.GetString("UserName");
        //     }
        // }
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
}

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
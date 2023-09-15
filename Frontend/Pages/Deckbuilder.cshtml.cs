using Microsoft.AspNetCore.Mvc.RazorPages;
using Frontend.ClientApi;
using Newtonsoft.Json;


namespace mtg_aspnet_v2.Pages
{
    public class DeckbuilderModel : PageModel
    {
        public readonly ClientApi _clientApi;

        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Commander { get; set; }
        // public string DeckName { get; set; }

        public List<CardWithInfo>? Commanders { get; set; }

        public List<CardWithInfo>? Cards { get; set; }



        public DeckbuilderModel(ClientApi clientApi)
        {
            _clientApi = clientApi; //new ClientApi();
        }


        public async Task OnGetAsync()
        {
            int userid = HttpContext.Session.GetInt32("UserId") ?? 0;
            string username = HttpContext.Session.GetString("UserName") ?? "";
            if (userid != 0)
            {
                this.UserId = userid;
                this.UserName = username;
            }
            this.Cards = await FetchCardsForUser(this.UserId);
            this.Commanders = await FetchCommandersForUser(this.UserId);
        }

        public void OnPost()
        {
            string? commanderValue = Request.Form["Commander"];
            if (commanderValue != null)
            {
                this.Commander = commanderValue;
            }
            //this.Commander = Request.Form["Commander"];
        }

        public async Task<List<CardWithInfo>?> FetchCommandersForUser(int userId)
        {
            try
            {
                string? commanderData = await _clientApi.GetCommandersByUserId(userId);
                if (commanderData != null)
                {
                    List<CardWithInfo>? commanders = JsonConvert.DeserializeObject<List<CardWithInfo>>(commanderData);
                    return commanders;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<CardWithInfo>?> FetchCardsForUser(int userId)
        {
            try
            {
                string? cardsData = await _clientApi.GetCardsByUserId(userId);
                if (cardsData != null)
                {
                    List<CardWithInfo>? cards = JsonConvert.DeserializeObject<List<CardWithInfo>>(cardsData);
                    return cards;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public class CardWithInfo
        {
            public CardWithInfo(string cardid, string title, string set, string supertype, string imgurl, int userid)
            {
                this.MtgCardId = cardid;
                this.Title = title;
                this.Set = set;
                this.SuperType = supertype;
                this.ImgUrl = imgurl;
                this.UserId = userid;
            }
            public int CardId { get; set; }
            public string MtgCardId { get; set; }
            public string Title { get; set; }
            public string Set { get; set; }
            public string SuperType { get; set; }
            public string ImgUrl { get; set; }
            public int UserId { get; set; }
        }

    }
}
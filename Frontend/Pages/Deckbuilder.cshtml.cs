using Microsoft.AspNetCore.Mvc.RazorPages;
using Frontend.ClientApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace mtg_aspnet_v2.Pages
{
    public class DeckbuilderModel : PageModel
    {
        public readonly ClientApi _clientApi;

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Commander { get; set; }
        public string DeckName { get; set; }

        public List<CardWithInfo> Commanders { get; set; }

        public List<CardWithInfo> Cards { get; set; }



        public DeckbuilderModel()
        {
            _clientApi = new ClientApi();
        }


        public async Task OnGetAsync()
        {
            int userid = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (userid != 0)
            {
                this.UserId = userid;
                this.UserName = HttpContext.Session.GetString("UserName");
            }
            this.Cards = await FetchCardsForUser(this.UserId);
            this.Commanders = await FetchCommandersForUser(this.UserId);
        }

        public void OnPost()
        {
            this.Commander = Request.Form["Commander"];
        }

        public async Task<List<CardWithInfo>> FetchCommandersForUser(int userId)
        {
            try
            {
                string commanderData = await _clientApi.GetCommandersByUserId(userId);
                List<CardWithInfo> commanders = JsonConvert.DeserializeObject<List<CardWithInfo>>(commanderData);
                return commanders;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<CardWithInfo>> FetchCardsForUser(int userId)
        {
            try
            {
                string cardsData = await _clientApi.GetCardsByUserId(userId);

                List<CardWithInfo> cards = JsonConvert.DeserializeObject<List<CardWithInfo>>(cardsData);

                return cards;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            //convert string of cards to list<Card>
            //List<Card> cards = JsonConvert.DeserializeObject<List<Card>>(cardsData);
            // foreach (Card card in cards)
            // {
            //     Console.WriteLine(card.Name);
            //     Console.WriteLine(card.ImageUrl);
            // }
        }

        public class CardWithInfo
        {
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
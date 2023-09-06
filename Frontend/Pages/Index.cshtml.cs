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
        public int userId { get; set; }
        public string UserName { get; set; }
        // public string Password { get; set; }
        // public List<string> cards { get; set; }

        public async Task OnGetAsync()
        {
            User = await _clientApi.GetDataForUser(36);

            if (User != null)
            {
                string username = JsonConvert.DeserializeObject<UserResponse>(User).UserName;
                Console.WriteLine(username);
                this.UserName = username;
            }
        }
    }

    public class UserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<string> Cards { get; set; }
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Frontend.ClientApi;
using Newtonsoft.Json;

namespace mtg_aspnet.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ClientApi _clientApi;

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string NewUsername { get; set; }
        [BindProperty]
        public string NewPassword { get; set; }

        public LoginModel(ClientApi clientApi)
        {
            _clientApi = clientApi; //new ClientApi();
            this.Username = "";
            this.Password = "";
            this.NewUsername = "";
            this.NewPassword = "";
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            try
            {
                string username = this.Username;
                string password = this.Password;

                string? userData = await _clientApi.GetUserByUsernameAndPassword(username, password);

                if (!string.IsNullOrEmpty(userData))
                {
                    UserResponse? userResponse = JsonConvert.DeserializeObject<UserResponse>(userData);
                    if (userResponse != null)
                    {
                        int userId = userResponse.UserId;
                        string userName = userResponse.UserName;
                        HttpContext.Session.SetInt32("UserId", userId);
                        HttpContext.Session.SetString("UserName", userName);
                        Console.WriteLine("Success! " + userName + " logged in");
                    }
                    else
                    {
                        Console.WriteLine("Failed to login!");
                        return RedirectToPage("/Login");
                    }
                    return RedirectToPage("/Deckbuilder");
                }
                else
                {
                    Console.WriteLine("Failed to login!");
                    return RedirectToPage("/Login");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToPage("/Login");
            }
        }

        public IActionResult OnPostLogoutAsync()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }

        public async Task<IActionResult> OnPostRegisterAsync()
        {
            try
            {
                string username = this.NewUsername;
                string password = this.NewPassword;
                string? userData = await _clientApi.PostUser(username, password);
                Console.WriteLine(userData);
                if (!string.IsNullOrEmpty(userData))
                {
                    //Console.WriteLine(userData);
                    UserResponse? userResponse = JsonConvert.DeserializeObject<UserResponse>(userData);
                    //Console.WriteLine(userResponse);
                    if (userResponse != null)
                    {
                        int userId = userResponse.UserId;
                        string userName = userResponse.UserName;
                        HttpContext.Session.Clear();
                        HttpContext.Session.SetInt32("UserId", userId);
                        HttpContext.Session.SetString("UserName", userName);
                        Console.WriteLine("Success! " + userName + " logged in");
                    }
                    else
                    {
                        Console.WriteLine("Failed to register!");
                        return RedirectToPage("/Login");
                    }
                    return RedirectToPage("/Index");  //return Page();
                }
                else
                {
                    Console.WriteLine("Failed to login!");
                    return RedirectToPage("/Login");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToPage("/Login");
            }
        }
    }

    public class UserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<string?> Cards { get; set; }
        public UserResponse(string userName, string password, List<string?> cards)
        {
            this.UserName = userName;
            this.Password = password;
            this.Cards = cards;
        }
    }
}

//test
// public async Task OnGetAsync()
// {
//     string username = await _clientApi.GetDataForUser(36);

//     if (username != null)
//     {
//         //string username = JsonConvert.DeserializeObject<UserResponse>(User).UserName;
//         //Console.WriteLine(username);
//         this.Usernametest = username;
//     }
// }

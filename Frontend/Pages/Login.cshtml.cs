using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Frontend.ClientApi;
using System.Linq.Expressions;

namespace mtg_aspnet.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ClientApi _clientApi;

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        //test
        public string Usernametest { get; set; }

        public LoginModel()
        {
            _clientApi = new ClientApi();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                string username = this.Username;
                string password = this.Password;

                string userData = await _clientApi.GetUserByUsernameAndPassword(username, password);
                Console.WriteLine(userData);

                if (!string.IsNullOrEmpty(userData))
                {
                    Console.WriteLine("Success!");
                    return RedirectToPage("/Index");
                }
                else
                {
                    //Console.WriteLine("Failure!");
                    return RedirectToPage("/Login");
                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return RedirectToPage("/Login");
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

    }
}
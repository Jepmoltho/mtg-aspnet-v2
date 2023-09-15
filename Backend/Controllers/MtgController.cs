//Import packages (libraries of methods) to use in your controller  
using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{

    [ApiController]
    [Route("[controller]")]

    //My controller class that inherits (:) from ControllerBase from the Microsoft.AspNetCore.Mvc namespace
    public class MtgController : ControllerBase
    {

        private readonly MtgContext _context;

        public MtgController(MtgContext context)
        {
            _context = context;
        }

        [HttpGet("users")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _context.Users.Include(u => u.Cards).ToList();
            return Ok(users);
        }

        // Return single user with nullable
        [HttpGet("user/{userId}")]
        public ActionResult<User?> GetUser(int userId)
        {
            var users = _context.Users
            .Include(u => u.Cards)
            .FirstOrDefault(u => u.UserId == userId);

            return users;
            //return Ok(users);
        }

        [HttpGet("user/name/{userName}")]
        public ActionResult<User?> GetUserByName(string userName)
        {
            var users = _context.Users
            .Include(u => u.Cards)
            .FirstOrDefault(u => u.UserName == userName);

            return users;
        }

        [HttpGet("user/login/{userName}/{password}")]
        public ActionResult<User?> GetUserByLogin(string userName, string password)
        {
            var users = _context.Users
            .FirstOrDefault(u => u.UserName == userName && u.Password == password);
            //.Include(u => u.Cards)

            return users;
        }

        [HttpPost("user")]
        public ActionResult<User> PostUser(User user)
        {
            _context.Entry(user).State = EntityState.Added;
            _context.SaveChanges();

            return Ok(user);
        }

        [HttpGet("cardsfromuser/{userId}")]
        public ActionResult<IEnumerable<Card>> GetCardsFromUser(int userId)
        {
            var cards = _context.Cards.Where(c => c.UserId == userId).ToArray(); //Skal måske være ToArray(); 
            return Ok(cards);
        }

        //https://localhost:5001/Mtg/card/37
        //post card to a userid
        [HttpPost("card/{cardname}/{userId}")]
        public ActionResult<int> PostCard(string cardname, int userId)
        {
            _context.Cards.Add(new Card { Title = cardname, UserId = userId });
            _context.SaveChanges();
            return Ok(userId);
        }

        //post multiple cards to the same userid
        [HttpPost("cards/{userId}")]
        public ActionResult<int> PostCards([FromBody] CardsInputDto input)
        {
            if (input.CardNames != null)
            {
                foreach (string cardname in input.CardNames)
                {
                    _context.Cards.Add(new Card { Title = cardname, UserId = input.UserId });
                }
                _context.SaveChanges();
                return Ok(input.UserId);
            }
            else
            {
                return BadRequest("No cardnames provided");
            }
        }

        //post multiple cards to the same userid
        [HttpPost("cardswithinfo/{userId}")]
        public ActionResult<int> PostCardsWithInfo([FromBody] CardsInfo cardsInfo)
        {
            if (cardsInfo.Cards != null)
            {
                foreach (Card card in cardsInfo.Cards)
                {
                    _context.Cards.Add(new Card { Title = card.Title, UserId = cardsInfo.UserId, MtgCardId = card.MtgCardId, Set = card.Set, SuperType = card.SuperType, imgUrl = card.imgUrl });
                }
                _context.SaveChanges();
                return Ok(cardsInfo.UserId);
            }
            else
            {
                return BadRequest("No cards provided");
            }
        }

        [HttpGet("commanders/{userId}")]
        public ActionResult<IEnumerable<Card>> GetCommandersFromUser(int userId)
        {
            var commanders = from c in _context.Cards
                             where c.UserId == userId && c.SuperType == "Legendary"
                             select c;
            return Ok(commanders);
        }

        public class CardsInputDto
        {
            public int UserId { get; set; }
            public List<string>? CardNames { get; set; }
        }

        public class CardsInfo
        {
            public int UserId { get; set; }
            public List<Card>? Cards { get; set; }
        }

    }
}

//This works
// [HttpPost("cards/{userId}")]
// public ActionResult<int> PostCards(string[] cardnames, int userId)
// {
//     foreach (string cardname in cardnames)
//     {
//         _context.Cards.Add(new Card { Title = cardname, UserId = userId });
//     }
//     _context.SaveChanges();
//     return Ok(userId);
// }

//    [HttpPost("card/{userId}")]
//     public ActionResult<int> PostCard(Card card, int userId)
//     {
//         card.UserId = userId;
//         _context.Entry(card).State = EntityState.Added;
//         _context.SaveChanges();
//         return Ok(card.CardId);
//     }

// Return list of users
// [HttpGet("user/{userId}")]
// public ActionResult<IEnumerable<User>> GetUser(int userId){
//     var users = _context.Users
//     .Include(u => u.Cards)
//     .FirstOrDefault(u => u.UserId == userId);

//     return Ok(users);
// }

// Return single user
// [HttpGet("user/{userId}")]
// public ActionResult<User> GetUser(int userId){
//     var users = _context.Users
//     .Include(u => u.Cards)
//     .FirstOrDefault(u => u.UserId == userId);

//     return Ok(users);
// }

/*
ASP.NET MVC architectural pattern

Model: The model represents the data, and does nothing else. The model does NOT depend on the controller or the view. My Model is defined by C# classes that represent data tables in my SQL database.
View: The view displays the model data. The view in your application is your components. The view also sends user actions (e.g. button clicks) to the controller, which then sends it to the controller. The view is typically defined by Razor pages (.cshtml files) that are rendered by the controller or tsx components like in your applicaiton.
Controller: The controller acts as an intermediary between the model and the view. When a user interacts with your view, we excecute methods that calls controller methods through the controller API object, which sends requests to the model, and ultimately selects a view to render that displays UI. In an API, the controller handles the incoming HTTP request, validates the input, and sends the data to the model. The controller is defined by the controller classes in your application.
*/
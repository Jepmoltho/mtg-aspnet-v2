// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.EntityFrameworkCore;
// using RazorCountry.Models;
// using RazorCountry.Data;
// using System.Linq; 

// namespace RazorCountry.Pages.Countries
// {
//   //The data model for the index page defined by class/object logic and syntax 
//   public class IndexModel : PageModel{

//     //Field holding the _context variable which represents a session with the DB for an instance of the page being rendered 
//     private readonly CountryContext _context;

//     //Contstructor of the data model (represented by a class) for the index page, Sets the _context field upon creation. 
//     public IndexModel(CountryContext context){
//       _context = context;
//     }
    
//     //A property on the model. A request to the index page has a list of countries stored in the model 
//     public List<Country> Countries { get; set; }

//     //We bind the property search string from the view page
//     [BindProperty(SupportsGet = true)]
//     public string SearchString { get; set; }

//     //This gets called when a page renders. Other get methods can be defined if a user can take other get actions on the same page
//     public async Task OnGetAsync(){
//       //Gets the data from the db context session 
//       var countries = from c 
//                       in _context.Countries
//                       select c; 

//       //filters what data to be stored in the model (this class) relevant for the the page/view. 
//       if (!string.IsNullOrEmpty(SearchString)){
//         countries = countries.Where(c => c.Name.Contains(SearchString));      
//        } 

//        //Sets the property of the oject to the result. Note, must be called with ToListAsync in the end. This propertu is what is called by the View with @Model.Countries
//        this.Countries = countries.ToListAsync();
//     }

//     //Post method that can be called with an instance of the class. This is what gets called if a user submits a form for example. Remember a Task represents a promise, and <IActionResult> represents that it is iterrable. 
//     public async Task<IActionResult> OnPostAsync(string id){
//       if (id == null)
//       {
//         return NotFound();
//       }
      
//       //Connecting to the specific row in the DB
//       Country Country = await _context.Countries.FindAsync(id);

//       if (Country != null)
//       {
//         //remove the specific row 
//         _context.Countries.Remove(Country);
//       }
      
//       //Persist the data 
//       await _context.SaveChangesAsync();

//       //What page do you go to after the data is persisted? You refresh current page in this exmaple. 
//       return RedirectToPage("./Index");
//     }
//   }
// }
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Template.RazorWebApp.Models;

namespace Template.RazorWebApp.Pages
{
    
    public class AddStudentModel : PageModel
    {
        [BindProperty]
        public Student? Student { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Console.WriteLine(Student!.Name);
            return Page();
        }
    }
    
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace WebApplication3.Pages
{
    // can use this instead of decorating each individual property
    [BindProperties]
    public class EnquiryModel : PageModel
    {
        //[BindProperty]
        // non nullable therefore inferred to be [Required]
        // have to force compiler to say Message will never be null and framework validations will take care of it 
        public string Message { get; set; } = null!;

        //[BindProperty]
        public int BudgetAmount { get; set; }

        // Make sure property is bound for when server side validation errors happen
        // it re-populates
        //[BindProperty]
        // force compiler to say I will handle nulls and make sure they don't happen
        public Thing Thing { get; set; } = null!;

        public void OnGet()
        {
            // simulated db call
            BudgetAmount = 123;

            // populate thing
            var thing = new Thing(2, "Dave");
            Thing = thing;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // fake an application error
            if (BudgetAmount == 999)
            {
                ModelState.AddModelError(string.Empty, "Can't update again today");
            }

            if (ModelState.IsValid)
            {
                // write back to db
                // we know Message is not null
                await Task.Delay(10);

                return LocalRedirect("/");
            }

            return Page();
        }
    }

    public record Thing(int ThingId, string Name);
}

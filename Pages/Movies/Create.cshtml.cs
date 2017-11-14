using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorTutorial.Models;

namespace RazorTutorial.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly RazorTutorial.Models.MovieContext _context;

        public CreateModel(RazorTutorial.Models.MovieContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //Page()返回的是PageResult
            return Page();
        }

        //通过BindProperty实施模型绑定，在controller中的action上是默认模型绑定，这里是显式说明
        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            //导向到同一个目录下的Index.cshtml页面
            return RedirectToPage("./Index");
        }
    }
}
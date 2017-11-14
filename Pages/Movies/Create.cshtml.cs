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
            //Page()���ص���PageResult
            return Page();
        }

        //ͨ��BindPropertyʵʩģ�Ͱ󶨣���controller�е�action����Ĭ��ģ�Ͱ󶨣���������ʽ˵��
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

            //����ͬһ��Ŀ¼�µ�Index.cshtmlҳ��
            return RedirectToPage("./Index");
        }
    }
}
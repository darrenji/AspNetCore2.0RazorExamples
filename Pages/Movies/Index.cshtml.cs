using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorTutorial.Models;

namespace RazorTutorial.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorTutorial.Models.MovieContext _context;

        public IndexModel(RazorTutorial.Models.MovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }

        //当请求过来，这里的方法被调用
        public async Task OnGetAsync(string searchString)
        {
            var movies = from m in _context.Movie
                         select m;
            if(!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            Movie = await movies.ToListAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorTutorial.Models;
using Microsoft.EntityFrameworkCore;

namespace RazorTutorial.Pages.Schedules
{
    public class DeleteModel : PageModel
    {
        private readonly RazorTutorial.Models.MovieContext _context;
        public DeleteModel(RazorTutorial.Models.MovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Schedule Schedule { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Schedule = await _context.Schedule.SingleOrDefaultAsync(x => x.ID == id);
            if(Schedule==null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            Schedule = await _context.Schedule.FindAsync(id);

            if(Schedule!=null)
            {
                _context.Schedule.Remove(Schedule);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}
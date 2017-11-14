using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorTutorial.Models;
using Microsoft.EntityFrameworkCore;
using RazorTutorial.Utilites;

namespace RazorTutorial.Pages.Schedules
{
    public class IndexModel : PageModel
    {
        private readonly RazorTutorial.Models.MovieContext _context;

        public IndexModel(RazorTutorial.Models.MovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FileUpload FileUpload { get; set; }

        public IList<Schedule> Schedule { get; private set; }

        public async Task OnGetAsync()
        {
            Schedule = await _context.Schedule.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // Perform an initial check to catch FileUpload class
                // attribute violations.
                if (!ModelState.IsValid)
                {
                    Schedule = await _context.Schedule.AsNoTracking().ToListAsync();
                    return Page();
                }

                var publicScheduleData =
               await FileHelpers.ProcessFormFile(FileUpload.UploadPublicSchedule, ModelState);

                var privateScheduleData =
                    await FileHelpers.ProcessFormFile(FileUpload.UploadPrivateSchedule, ModelState);



                // Perform a second check to catch ProcessFormFile method
                // violations.
                if (!ModelState.IsValid)
                {
                    Schedule = await _context.Schedule.AsNoTracking().ToListAsync();
                    return Page();
                }

                var schedule = new Schedule()
                {
                    PublicSchedule = publicScheduleData,
                    PublicScheduleSize = FileUpload.UploadPublicSchedule.Length,
                    PrivateSchedule = privateScheduleData,
                    PrivateScheduleSize = FileUpload.UploadPrivateSchedule.Length,
                    Title = FileUpload.Title,
                    UploadDT = DateTime.UtcNow
                };

                _context.Schedule.Add(schedule);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorTutorial.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieContext(serviceProvider.GetRequiredService<DbContextOptions<MovieContext>>()))
            {
                if(context.Movie.Any())
                {
                    return;
                }

                context.Movie.AddRange(
                        new Movie
                        {
                            Title="When Harry Met Sally",
                            ReleaseDate = DateTime.Parse("1989-2-12"),
                            Genre="Romantic Comedy",
                            Price=7.99M
                        },
                        new Movie
                        {
                            Title="Ghostbustomers",
                            ReleaseDate=DateTime.Parse("1984-3-13"),
                            Genre="Comedy",
                            Price=8.99M
                        },
                        new Movie
                        {
                            Title="Ghostbusters 2",
                            ReleaseDate=DateTime.Parse("1986-2-23"),
                            Price=9.99M,
                            Genre="Comedy"
                        },
                        new Movie
                        {
                            Title="Rio Bravo",
                            ReleaseDate = DateTime.Parse("1959-4-15"),
                            Price=3.99M,
                            Genre="Western"
                        }
                    );
                context.SaveChanges();
            }
        }
    }
}

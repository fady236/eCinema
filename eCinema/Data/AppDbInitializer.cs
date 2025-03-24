using eCinema.Data.Static;
using eCinema.Models;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace eCinema.Data
{
    public class AppDbInitializer
    {
        public static async Task SeedUsersAndRoleAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                // 🛑 Create roles if they don't exist
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                // ✅ Add admin accounts
                var adminUsers = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        FullName = "Admin One",
                        UserName = "admin1",
                        Email = "admin1@ecinema.com",
                        EmailConfirmed = true
                    },
                    new ApplicationUser
                    {
                        FullName = "Admin Two",
                        UserName = "admin2",
                        Email = "admin2@ecinema.com",
                        EmailConfirmed = true
                    }
                };

                foreach (var adminUser in adminUsers)
                {
                    var existingAdmin = await userManager.FindByEmailAsync(adminUser.Email);
                    if (existingAdmin == null)
                    {
                        var createAdminResult = await userManager.CreateAsync(adminUser, "Admin@1234?");
                        if (createAdminResult.Succeeded)
                            await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
                    }
                }

                // ✅ Add user accounts
                var appUsers = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        FullName = "User One",
                        UserName = "user1",
                        Email = "user1@ecinema.com",
                        EmailConfirmed = true
                    },
                    new ApplicationUser
                    {
                        FullName = "User Two",
                        UserName = "user2",
                        Email = "user2@ecinema.com",
                        EmailConfirmed = true
                    }
                };

                foreach (var appUser in appUsers)
                {
                    var existingUser = await userManager.FindByEmailAsync(appUser.Email);
                    if (existingUser == null)
                    {
                        var createUserResult = await userManager.CreateAsync(appUser, "User@1234?");
                        if (createUserResult.Succeeded)
                            await userManager.AddToRoleAsync(appUser, UserRoles.User);
                    }
                }
            }
        }

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                if (context != null)
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    // Adding Cinemas
                    if (!context.Cinemas.Any())
                    {
                        context.Cinemas.AddRange(new List<Cinema>()
                        {
                            new Cinema()
                            {
                                Name = "Cinema Radio",
                                Logo = "https://al-ismaelia.com/wp-content/uploads/2020/07/cover-11-scaled.jpg",
                                Description = "Located in Cairo, known for its historical significance."
                            },
                            new Cinema()
                            {
                                Name = "Galaxy Cinema",
                                Logo = "https://galaxy-cinema.com/nextjs-assets/branches/64bb3b402b613a54c30cf933.png",
                                Description = "Modern cinema with multiple screens in Cairo."
                            },
                            new Cinema()
                            {
                                Name = "IMAX Cinema",
                                Logo = "https://www.cairo4hotel.com/uploads/editor_images/1519823247_1.jpg",
                                Description = "Features the latest IMAX technology in 6th of October City."
                            },
                            new Cinema()
                            {
                                Name = "Renaissance Cinema",
                                Logo = "https://www.newcairoproperty.com/uploads/posts/2020/01/1595792392-WhatsApp-Image-2020-07-26-at-8.49.51-PM.jpeg",
                                Description = "Popular cinema chain with branches across Egypt."
                            },
                            new Cinema()
                            {
                                Name = "VOX Cinema",
                                Logo = "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/0e/0c/99/61/photo0jpg.jpg?w=1200&h=-1&s=1",
                                Description = "Luxury cinema experience in various locations."
                            },
                            new Cinema()
                            {
                                Name = "Cairo Festival Cinema",
                                Logo = "https://safarin.net/wp-content/uploads/2018/09/18-09-02_20-21-31.jpg",
                                Description = "Located inside Cairo Festival City Mall, offering premium movie experiences."
                            },
                            new Cinema()
                            {
                                Name = "Mall of Egypt Cinema",
                                Logo = "https://assets.voxcinemas.com/content/MAX-banner-880x440-B%20(1)_1483954471.jpg",
                                Description = "A large cinema complex inside Mall of Egypt, featuring IMAX and 4DX screens."
                            },
                            new Cinema()
                            {
                                Name = "City Stars Cinema",
                                Logo = "https://www.citystars-heliopolis.com.eg/public/images/store_facade_image/SklAMXfnfx-main-v2-v2.jpg?1497370825773",
                                Description = "A well-known cinema in City Stars Mall, famous for blockbuster movies."
                            }
                        });
                        context.SaveChanges();
                    }

                    // Adding Actors
                    if (!context.Actors.Any())
                    {
                        context.Actors.AddRange(new List<Actor>()
                        {
                            new Actor()
                            {
                                FullName = "Adel Emam",
                                Biography = "One of Egypt's most iconic actors, known for his comedic and dramatic roles.",
                                ProfilePicUrl = "https://www.egypttoday.com/siteimages/Larg/62798.jpg"
                            },
                            new Actor()
                            {
                                FullName = "Yasmin Abdel Aziz",
                                Biography = "Renowned Egyptian singer and actress.",
                                ProfilePicUrl = "https://misrconnect.com/storage/profiles/November2024/henAoWiVFbJ7Tv0v9yy2.png"
                            },
                            new Actor()
                            {
                                FullName = "Ahmed Ezz",
                                Biography = "Popular actor known for his action and drama roles.",
                                ProfilePicUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRHDz9kUZPpJ74ow0VNTzcmAHUutY2fwJ3XrA&s"
                            },
                            new Actor()
                            {
                                FullName = "Mona Zaki",
                                Biography = "Acclaimed actress with a diverse portfolio in film and theater.",
                                ProfilePicUrl = "https://scenenow.com/Content/Admin/Uploads/Articles/ArticlesMainPhoto/62111/d286816d-f89e-4bce-9657-7e2ef3daed9f.jpg"
                            },
                            new Actor()
                            {
                                FullName = "Karim Abdel Aziz",
                                Biography = "One of Egypt's most beloved actors, known for action and comedy roles.",
                                ProfilePicUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSL05ThgcWVSdyQwLO8edRp-caF0jBjSpNsYg&s"
                            },
                            new Actor()
                            {
                                FullName = "Hend Sabry",
                                Biography = "Tunisian-Egyptian actress famous for her diverse roles in Arab cinema.",
                                ProfilePicUrl = "https://deadline.com/wp-content/uploads/2023/05/hend-sabry-1.jpg"
                            },
                            new Actor()
                            {
                                FullName = "Mohamed Ramadan",
                                Biography = "Famous Egyptian actor and singer known for his bold and unique style.",
                                ProfilePicUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQWRnAxgRSSo2lgRH5I3bvw_zOCA4bPbbkIAg&s"
                            },
                            new Actor()
                            {
                                FullName = "Dina El Sherbiny",
                                Biography = "Talented actress known for her strong performances in drama series.",
                                ProfilePicUrl = "https://i0.wp.com/top50women.com/wp-content/uploads/2024/05/dina-el-sherbiny.jpg?fit=800%2C500&ssl=1"
                            }
                        });
                        context.SaveChanges();
                    }

                    // Adding Producers
                    if (!context.Producers.Any())
                    {
                        context.Producers.AddRange(new List<Producer>()
                        {
                            new Producer()
                            {
                                FullName = "Mohamed Hefzy",
                                Biography = "Prominent film producer and screenwriter.",
                                ProfilePicUrl = "https://www.at-media-group.com/content/images/size/w1920/2023/10/IMG_0645.jpeg"
                            },
                            new Producer()
                            {
                                FullName = "Gamal El Adl",
                                Biography = "Known for producing numerous successful Egyptian films.",
                                ProfilePicUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS97H1XEcipRyR-GGiD7ZnkyBI1iPPEQ3Oilg&s"
                            },
                            new Producer()
                            {
                                FullName = "Marwan Hamed",
                                Biography = "Acclaimed director and producer with several box office hits.",
                                ProfilePicUrl = "https://www.broadcastprome.com/wp-content/uploads/2021/01/Marwan-close-up-web.jpg"
                            },
                            new Producer()
                            {
                                FullName = "Sherif Arafa",
                                Biography = "One of Egypt's top directors, known for his action and comedy films.",
                                ProfilePicUrl = "https://mad-distribution.film/assets/img/mad-films/movies/directors/shreefarfa.jpg"
                            },
                            new Producer()
                            {
                                FullName = "Tarek Alarian",
                                Biography = "Renowned filmmaker known for high-budget action films.",
                                ProfilePicUrl = "https://cdn.sbisiali.com/news/images/842f262c-2f4a-47cd-9308-a33296b74d7d.jpg"
                            },
                            new Producer()
                            {
                                FullName = "Kamla Abu Zekry",
                                Biography = "Award-winning director famous for tackling social issues.",
                                ProfilePicUrl = "https://cairogossip.com/app/uploads/2024/03/kamla-abou-zikry-615x375.jpeg"
                            },
                            new Producer()
                            {
                                FullName = "Amr Salama",
                                Biography = "Talented director known for indie films and unique storytelling.",
                                ProfilePicUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTTZTFkZghu2fVgh1zeyjDvd7sv5zrwZhBMhA&s"
                            }
                        });
                        context.SaveChanges();
                    }

                    // Adding Movies
                    if (!context.Movies.Any())
                    {
                        context.Movies.AddRange(new List<Movie>()
    {
        new Movie()
        {
            Name = "The Blue Elephant",
            Description = "A psychological thriller about a psychiatrist revisiting his past.",
            Price = 60.00,
            ImageUrl = "https://upload.wikimedia.org/wikipedia/ar/6/66/%D9%85%D9%84%D8%B5%D9%82_%D9%81%D9%8A%D9%84%D9%85_%D8%A7%D9%84%D9%81%D9%8A%D9%84_%D8%A7%D9%84%D8%A3%D8%B2%D8%B1%D9%82_2_%282019%29.jpg",
            StartDate = DateTime.ParseExact("2025-04-01", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2025-04-15", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            CinemaId = 1,
            ProducerId = 1,
            MovieCategory = MovieCategory.Thriller,
            Actor_Movies = new List<Actor_Movie>()
            {
                new Actor_Movie() { ActorId = 1 }, // Adel Emam
                new Actor_Movie() { ActorId = 3 }  // Ahmed Ezz
            }
        },
        new Movie()
        {
            Name = "Welad Rizk",
            Description = "A crime thriller about a gang of brothers.",
            Price = 55.00,
            ImageUrl = "https://img.youm7.com/ArticleImgs/2024/5/31/401755-%D8%A8%D9%88%D8%B3%D8%AA%D8%B1-%D9%88%D9%84%D8%A7%D8%AF-%D8%B1%D8%B2%D9%82-3.jpg",
            StartDate = DateTime.ParseExact("2025-04-05", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2025-04-20", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            CinemaId = 2,
            ProducerId = 2,
            MovieCategory = MovieCategory.Action,
            Actor_Movies = new List<Actor_Movie>()
            {
                new Actor_Movie() { ActorId = 2 }, // Yasmin Ali
                new Actor_Movie() { ActorId = 4 }  // Mona Zaki
            }
        },
        new Movie()
        {
            Name = "Sheikh Jackson",
            Description = "A drama about a religious man obsessed with Michael Jackson.",
            Price = 50.00,
            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSd1CpgeeddHB5tfPj6dctTac12Lj9HJRR8fQ&s",
            StartDate = DateTime.ParseExact("2025-04-10", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2025-04-25", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            CinemaId = 3,
            ProducerId = 3,
            MovieCategory = MovieCategory.Drama,
            Actor_Movies = new List<Actor_Movie>()
            {
                new Actor_Movie() { ActorId = 1 }, // Adel Emam
                new Actor_Movie() { ActorId = 5 }  // Karim Abdel Aziz
            }
        },
        new Movie()
        {
            Name = "Al Asleyeen",
            Description = "A sci-fi mystery film exploring the deep state.",
            Price = 45.00,
            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFNa6Md-vNGr9m80ZwgVeSl_Pg5tnoymxgYQ&s",
            StartDate = DateTime.ParseExact("2025-04-01", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2025-04-15", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            CinemaId = 1,
            ProducerId = 1,
            MovieCategory = MovieCategory.SciFi,
            Actor_Movies = new List<Actor_Movie>()
            {
                new Actor_Movie() { ActorId = 3 }, // Ahmed Ezz
                new Actor_Movie() { ActorId = 6 }  // Hend Sabry
            }
        },
        // New Movies (8 additional movies)
        new Movie()
        {
            Name = "Harly",
            Description = "A classic Egyptian film about tomb robbers in the 19th century.",
            Price = 40.00,
            ImageUrl = "https://img.youm7.com/ArticleImgs/2023/4/28/338580-%D9%81%D9%8A%D9%84%D9%85-%D9%87%D8%A7%D8%B1%D9%84%D9%8A.jpg",
            StartDate = DateTime.ParseExact("2025-04-16", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2025-04-30", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            CinemaId = 4,
            ProducerId = 4,
            MovieCategory = MovieCategory.Drama,
            Actor_Movies = new List<Actor_Movie>()
            {
                new Actor_Movie() { ActorId = 7 }, // Mohamed Ramadan
                new Actor_Movie() { ActorId = 8 }  // Dina El Sherbiny
            }
        },
        new Movie()
        {
            Name = "Kira & El Gin",
            Description = "A fantasy action film set in an alternate reality Egypt.",
            Price = 65.00,
            ImageUrl = "https://upload.wikimedia.org/wikipedia/ar/1/15/%D9%85%D9%84%D8%B5%D9%82_%D9%83%D9%8A%D8%B1%D8%A9_%D9%88%D8%A7%D9%84%D8%AC%D9%86_%28%D9%81%D9%8A%D9%84%D9%85%29.jpeg",
            StartDate = DateTime.ParseExact("2025-05-01", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2025-05-15", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            CinemaId = 5,
            ProducerId = 5,
            MovieCategory = MovieCategory.Action,
            Actor_Movies = new List<Actor_Movie>()
            {
                new Actor_Movie() { ActorId = 5 }, // Karim Abdel Aziz
                new Actor_Movie() { ActorId = 6 }  // Hend Sabry
            }
        },
        new Movie()
        {
            Name = "Zahaimar",
            Description = "A comedy about a group of people stranded on an island.",
            Price = 50.00,
            ImageUrl = "https://upload.wikimedia.org/wikipedia/ar/c/c0/%D9%85%D9%84%D8%B5%D9%82_%D9%81%D9%8A%D9%84%D9%85_%D8%B2%D9%87%D8%A7%D9%8A%D9%85%D8%B1_%282010%29.jpg",
            StartDate = DateTime.ParseExact("2025-05-05", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2025-05-20", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            CinemaId = 6,
            ProducerId = 6,
            MovieCategory = MovieCategory.Comedy,
            Actor_Movies = new List<Actor_Movie>()
            {
                new Actor_Movie() { ActorId = 1 }, // Adel Emam
                new Actor_Movie() { ActorId = 2 }  // Yasmin Ali
            }
        },
        new Movie()
        {
            Name = "The Yacoubian Building",
            Description = "A drama following the lives of residents in a Cairo apartment building.",
            Price = 55.00,
            ImageUrl = "https://upload.wikimedia.org/wikipedia/ar/3/33/The_Yacoubian_Building_%28Movie_Poster%29.jpg",
            StartDate = DateTime.ParseExact("2025-05-10", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2025-05-25", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            CinemaId = 7,
            ProducerId = 7,
            MovieCategory = MovieCategory.Drama,
            Actor_Movies = new List<Actor_Movie>()
            {
                new Actor_Movie() { ActorId = 1 }, // Ahmed Ezz
                new Actor_Movie() { ActorId = 4 }  // Mona Zaki
            }
        },
        new Movie()
        {
            Name = "Elbo3bo3",
            Description = "A comedy about two men searching for hidden treasure.",
            Price = 45.00,
            ImageUrl = "https://media.almashhad.com/twitter/1689506806134_KFhlx.webp",
            StartDate = DateTime.ParseExact("2025-06-01", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2025-06-15", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            CinemaId = 8,
            ProducerId = 1,
            MovieCategory = MovieCategory.Comedy,
            Actor_Movies = new List<Actor_Movie>()
            {
                new Actor_Movie() { ActorId = 5 }, // Karim Abdel Aziz
                new Actor_Movie() { ActorId = 7 }  // Mohamed Ramadan
            }
        },
        new Movie()
        {
            Name = "Eldashash",
            Description = "A coming-of-age story about a Christian boy attending a Muslim school.",
            Price = 50.00,
            ImageUrl = "https://assets.voxcinemas.com/posters/P_HO00011848_1735562555824.jpg",
            StartDate = DateTime.ParseExact("2025-06-05", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2025-06-20", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            CinemaId = 1,
            ProducerId = 2,
            MovieCategory = MovieCategory.Drama,
            Actor_Movies = new List<Actor_Movie>()
            {
                new Actor_Movie() { ActorId = 6 }, // Hend Sabry
                new Actor_Movie() { ActorId = 8 }  // Dina El Sherbiny
            }
        },
        new Movie()
        {
            Name = "19 B",
            Description = "A horror film about a group trapped in a haunted underground passage.",
            Price = 60.00,
            ImageUrl = "https://media.filfan.com/NewsPics/FilfanNew/large/505609_0.jpg",
            StartDate = DateTime.ParseExact("2025-06-10", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2025-06-25", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            CinemaId = 2,
            ProducerId = 3,
            MovieCategory = MovieCategory.Horror,
            Actor_Movies = new List<Actor_Movie>()
            {
                new Actor_Movie() { ActorId = 2 }, // Yasmin Ali
                new Actor_Movie() { ActorId = 4 }  // Mona Zaki
            }
        },
        new Movie()
        {
            Name = "Elserb",
            Description = "A crime drama about the original gangsters of Cairo.",
            Price = 55.00,
            ImageUrl = "https://static.sayidaty.net/styles/900_scale/public/2024-05/343363.jpg.webp",
            StartDate = DateTime.ParseExact("2025-07-01", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2025-07-15", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            CinemaId = 3,
            ProducerId = 4,
            MovieCategory = MovieCategory.Crime,
            Actor_Movies = new List<Actor_Movie>()
            {
                new Actor_Movie() { ActorId = 1 }, // Adel Emam
                new Actor_Movie() { ActorId = 3 }  // Ahmed Ezz
            }
        }
    });
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
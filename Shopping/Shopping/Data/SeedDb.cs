using Shopping.Data.Entities;
using Shopping.Enums;
using Shopping.Helpers.Interfaces;

namespace Shopping.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriesAsync();
            await CheckCountriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Henrique", "Schraiber", "henriquescunha123@gmail.com", "27 995815978", "Rua airton Senna", UserType.Admin);
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }



        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    CountryName = "Colombia",
                    States = new List<State>()
                    {
                        new State()
                        {
                            StateName = "Antioquia",
                            Cities = new List<City>() {
                                new City() { CityName = "Medellín" },
                                new City() { CityName = "Itagüí" },
                                new City() { CityName = "Envigado" },
                                new City() { CityName = "Bello" },
                                new City() { CityName = "Rionegro" },
                            }
                        },
                        new State()
                        {
                            StateName = "Bogotá",
                            Cities = new List<City>() {
                                new City() { CityName = "Usaquen" },
                                new City() { CityName = "Champinero" },
                                new City() { CityName = "Santa fe" },
                                new City() { CityName = "Useme" },
                                new City() { CityName = "Bosa" },
                            }
                        },
                    }
                });
                _context.Countries.Add(new Country
                {
                    CountryName = "Estados Unidos",
                    States = new List<State>()
                    {
                        new State()
                        {
                            StateName = "Florida",
                            Cities = new List<City>() {
                                new City() { CityName = "Orlando" },
                                new City() { CityName = "Miami" },
                                new City() { CityName = "Tampa" },
                                new City() { CityName = "Fort Lauderdale" },
                                new City() { CityName = "Key West" },
                            }
                        },
                        new State()
                        {
                            StateName = "Texas",
                            Cities = new List<City>() {
                                new City() { CityName = "Houston" },
                                new City() { CityName = "San Antonio" },
                                new City() { CityName = "Dallas" },
                                new City() { CityName = "Austin" },
                                new City() { CityName = "El Paso" },
                            }
                        },
                    }
                });
            }

            await _context.SaveChangesAsync();
        }
        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { CategoryName = "Tecnologia" });
                _context.Categories.Add(new Category { CategoryName = "Roupa" });
                _context.Categories.Add(new Category { CategoryName = "Calçado" });
                _context.Categories.Add(new Category { CategoryName = "Beleza" });
                _context.Categories.Add(new Category { CategoryName = "Nutrição" });
                _context.Categories.Add(new Category { CategoryName = "Esportes" });
                _context.Categories.Add(new Category { CategoryName = "Apple" });
                _context.Categories.Add(new Category { CategoryName = "Mascotes" });
                await _context.SaveChangesAsync();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Data.Entities;
using Shopping.Models;

namespace Shopping.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountriesController : Controller
    {
        private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return View(await _context.Countries
                  .Include(x => x.States)
                  .ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .Include(x => x.States)
                .ThenInclude(x => x.Cities)
                .FirstOrDefaultAsync(m => m.IdCountry == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }
        public async Task<IActionResult> DetailsState(int? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            State state = await _context.States
                .Include(c => c.Country)
                .Include(c => c.Cities)
                .FirstOrDefaultAsync(m => m.IdState == id);

            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }
        public async Task<IActionResult> DetailsCity(int? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            City city = await _context.Cities
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.IdCity == id);

            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }
        public IActionResult Create()
        {
            Country country = new()
            {
                States= new List<State>(0)
            };
            return View(country);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Já existe um país com o mesmo nome.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(country);
        }
		public async Task<IActionResult> AddState(int? idCountry)
		{
			if (idCountry == null)
			{
				return NotFound();
			}

            Country country = await _context.Countries.FirstOrDefaultAsync(x => x.IdCountry == idCountry);
            if(country == null)
            {
				return NotFound();
			}
            StateViewModel stateModel = new()
            {
                IdCountry = country.IdCountry
            };
			return View(stateModel);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddState(StateViewModel stateModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
                    State state = new()
                    {
                        Cities = new List<City>(),
                        Country = await _context.Countries.FindAsync(stateModel.IdCountry),
                        StateName= stateModel.StateName
                    };
					_context.Add(state);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Details), new {Id = stateModel.IdCountry});
				}
				catch (DbUpdateException dbUpdateException)
				{
					if (dbUpdateException.InnerException.Message.Contains("duplicate"))
					{
						ModelState.AddModelError(string.Empty, "Já existe um estado com o mesmo nome nesse país.");
					}
					else
					{
						ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
					}
				}
				catch (Exception ex)
				{

					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View(stateModel);
		}
		public async Task<IActionResult> AddCity(int? idCity)
		{
			if (idCity == null)
			{
				return NotFound();
			}

			State state = await _context.States.FindAsync(idCity);
			if (state == null)
			{
				return NotFound();
			}
			CityViewModel cityModel = new()
			{
				IdState = state.IdState
			};
			return View(cityModel);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddCity(CityViewModel cityModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					City city = new()
					{
						State = await _context.States.FindAsync(cityModel.IdState),
						CityName = cityModel.CityName
					};
					_context.Add(city);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(DetailsState), new { Id = cityModel.IdState });
				}
				catch (DbUpdateException dbUpdateException)
				{
					if (dbUpdateException.InnerException.Message.Contains("duplicate"))
					{
						ModelState.AddModelError(string.Empty, "Já existe uma cidade com o mesmo nome nesse estado.");
					}
					else
					{
						ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
					}
				}
				catch (Exception ex)
				{

					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View(cityModel);
		}


		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Country country = await _context.Countries
                .Include(x => x.States)
                .FirstOrDefaultAsync(x => x.IdCountry == id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Country country)
        {
            if (id != country.IdCountry)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Já existe um país com o mesmo nome.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(country);
        }
        public async Task<IActionResult> EditState(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            State state = await _context.States
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.IdState == id);
            if (state == null)
            {
                return NotFound();
            }
            StateViewModel stateModel = new()
            {
                IdCountry = state.Country.IdCountry,
                IdState= state.IdState,
                StateName = state.StateName
            };
            return View(stateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditState(int id, StateViewModel stateModel)
        {
            if (id != stateModel.IdState)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    State state = new()
                    {
                        IdState= stateModel.IdState,
                        StateName = stateModel.StateName
                    };
                    _context.Update(state);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new {Id = stateModel.IdCountry});
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Já existe um estado com o mesmo nome nesse país.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(stateModel);
        }
        public async Task<IActionResult> EditCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            City city = await _context.Cities
                .Include(x => x.State)
                .FirstOrDefaultAsync(x => x.IdCity == id);
            if (city == null)
            {
                return NotFound();
            }
            CityViewModel cityModel = new()
            {
                IdState = city.State.IdState,
                IdCity= city.IdCity,
                CityName = city.CityName
            };
            return View(cityModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCity(int id, CityViewModel cityModel)
        {
            if (id != cityModel.IdCity)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    City city = new()
                    {
                        IdCity = cityModel.IdCity,
                        CityName = cityModel.CityName

                    };
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DetailsState), new { Id = cityModel.IdState });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Já existe uma cidade com o mesmo nome nesse estado.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(cityModel);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .Include(x => x.States)
                .FirstOrDefaultAsync(m => m.IdCountry == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Country country = await _context.Countries
                .FirstOrDefaultAsync(c => c.IdCountry == id);
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
		public async Task<IActionResult> DeleteState(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			State state = await _context.States
                .Include(x => x.Country)
				.FirstOrDefaultAsync(m => m.IdState == id);

			if (state == null)
			{
				return NotFound();
			}

			return View(state);
		}

		[HttpPost, ActionName("DeleteState")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmedState(int id)
		{
			State state = await _context.States
                .Include(x => x.Country)
                .FirstOrDefaultAsync(c => c.IdState == id);
			_context.States.Remove(state);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Details), new {Id = state.Country.IdCountry });
		}
        public async Task<IActionResult> DeleteCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            City city = await _context.Cities
                .Include(x => x.State)
                .FirstOrDefaultAsync(m => m.IdCity == id);

            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        [HttpPost, ActionName("DeleteCity")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedCity(int id)
        {
            City city = await _context.Cities
                .Include(x => x.State)
                .FirstOrDefaultAsync(c => c.IdCity == id);
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DetailsState), new { Id = city.State.IdState });
        }

    }
}

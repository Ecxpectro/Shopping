using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Data.Entities;
using Shopping.Helpers.Interfaces;

namespace Shopping.Helpers
{
	public class CombosHelper : ICombosHelper
	{
		private readonly DataContext _context;

		public CombosHelper(DataContext context)
        {
			_context = context;
		}
		public async Task<IEnumerable<SelectListItem>> GetComboCountriesAsync()
		{
			List<SelectListItem> contries = await _context.Countries.Select(c => new SelectListItem
			{
				Text = c.CountryName,
				Value = c.IdCountry.ToString()
			}).OrderBy(x => x.Text).ToListAsync();

			contries.Insert(0, new SelectListItem { Text = "Selecione ume país...", Value = "0" });

			return contries;
		}

		public async Task<IEnumerable<SelectListItem>> GetCombosCategoriesAsync()
		{
			List<SelectListItem> categories = await _context.Categories.Select(c => new SelectListItem
			{
				Text = c.CategoryName,
				Value = c.IdCategory.ToString()
			}).OrderBy(x => x.Text).ToListAsync();

			categories.Insert(0, new SelectListItem { Text = "Selecione uma categoria...", Value = "0" });

			return categories;
		}
		public async Task<IEnumerable<SelectListItem>> GetComboCitiesAsync(long stateId)
		{
			List<SelectListItem> cities = await _context.Cities.Where(x => x.State.IdState == stateId).Select(c => new SelectListItem
			{
				Text = c.CityName,
				Value = c.IdCity.ToString()
			}).OrderBy(x => x.Text).ToListAsync();

			cities.Insert(0, new SelectListItem { Text = "Selecione uma cidade...", Value = "0" });

			return cities;
		}
		public async Task<IEnumerable<SelectListItem>> GetComboStatesAsync(long countryId)
		{
			List<SelectListItem> states = await _context.States.Where(x => x.Country.IdCountry == countryId).Select(c => new SelectListItem
			{
				Text = c.StateName,
				Value = c.IdState.ToString()
			}).OrderBy(x => x.Text).ToListAsync();

			states.Insert(0, new SelectListItem { Text = "Selecione um Estado...", Value = "0" });

			return states;
		}
	}
}

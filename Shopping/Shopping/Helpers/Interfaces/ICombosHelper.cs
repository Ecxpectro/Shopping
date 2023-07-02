using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shopping.Helpers.Interfaces
{
	public interface ICombosHelper
	{
		Task<IEnumerable<SelectListItem>> GetCombosCategoriesAsync();
		Task<IEnumerable<SelectListItem>> GetComboCountriesAsync();
		Task<IEnumerable<SelectListItem>> GetComboStatesAsync(long countryId);
		Task<IEnumerable<SelectListItem>> GetComboCitiesAsync(long stateId);
	}
}

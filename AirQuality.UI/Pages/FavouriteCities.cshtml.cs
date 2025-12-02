using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Dto;
using Models.Requests.UserFavouriteCities;

namespace AirQuality.UI.Pages
{
    public class FavouriteCitiesModel : PageModel
    {
        private readonly IFavouriteCitiesService _service;
        public List<FavouriteCitiesDto>? Cities { get; set; }
        public FavouriteCitiesModel(IFavouriteCitiesService service)
        {
            _service = service;
        }
        public async Task OnGet([FromQuery] FavouriteCitiesSearchRequest request)
        {
            Cities = await _service.GetFavouriteCities(new FavouriteCitiesSearchRequest() { CityId = request.CityId });
        }

        public async Task<IActionResult> OnPostAddAsync([FromBody] FavouriteCitiesUpsertRequest request)
        {
            if (request == null) return BadRequest();

            var result = await _service.Insert(request);
            return new JsonResult(result);
        }
    }
}

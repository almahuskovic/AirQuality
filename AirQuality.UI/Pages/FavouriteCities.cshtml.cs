using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Dto;
using Models.IServices;
using Models.Requests.UserFavouriteCities;
using System.Net.WebSockets;

namespace AirQuality.UI.Pages
{
    public class FavouriteCitiesModel : PageModel
    {
        private readonly IFavouriteCities _service;
        public List<AirQualityMeasurementDto>? Cities { get; set; }
        public FavouriteCitiesModel(IFavouriteCities service)
        {
            _service = service;
        }
        public async Task OnGet()
        {
            var result = await _service.GetFavouriteCities();
            Cities = result?.Cities;
        }
        
        public async Task<IActionResult> OnPostAddAsync([FromBody] FavouriteCitiesUpsertRequest request)
        {
            if (request == null) return BadRequest();

            var result = await _service.Insert(request);  
            return new JsonResult(result);                 
        }

    }
}

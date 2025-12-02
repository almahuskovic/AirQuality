using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AirQuality.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public bool IsInitialLoad { get; set; }
        private readonly IAirQualityService _service;

        public IndexModel(ILogger<IndexModel> logger, IAirQualityService airQualityService)
        {
            _logger = logger;
            _service = airQualityService;
        }

        public async Task OnGetAsync()
        {
            var response = await _service.GetAQIByCities("BA");
            IsInitialLoad = response != null && response.Count < 70;
        }
    }
}

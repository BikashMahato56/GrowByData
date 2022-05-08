using GrowByData.Data;
using GrowByData.Data.Entities;
using GrowByData.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrowByData.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly IGrowByDataService _service;
        public ProductList ProductList { get; set; }
        public IEnumerable<ProductDto> products { get; set; }
        public IndexModel(IGrowByDataService service)
        {
            _service = service;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            products = await _service.GetAll();
            return Page();
        }
    }
}

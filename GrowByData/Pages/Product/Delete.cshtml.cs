using GrowByData.Data;
using GrowByData.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrowByData.Pages
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IGrowByDataService _service;

        public ProductDto Product { get; set; }
        public DeleteModel(IGrowByDataService service)
        {
            _service = service;
        }
        public async void OnGet(int id)
        {
            Product = await _service.GetById(id);
        }
        public async Task<IActionResult> OnPost(ProductDto product)
        {
            var status = await _service.Delete(product);
            return RedirectToPage("Index");

        }
    }
}

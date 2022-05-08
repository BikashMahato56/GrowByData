using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrowByData.Data;
using GrowByData.Services;

namespace GrowByData.Pages
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IGrowByDataService _service;

        public ProductDto Product { get; set; }
        public CreateModel(IGrowByDataService service)
        {
            _service = service;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(ProductDto product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var status = await _service.Insert(product);
                }
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                return RedirectToPage("Index");
            }
            
        }
    }
}

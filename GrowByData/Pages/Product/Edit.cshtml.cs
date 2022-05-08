using GrowByData.Data;
using GrowByData.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrowByData.Pages
{
    [BindProperties]
    public class EditModel : PageModel
    {

        private readonly IGrowByDataService _service;

        public ProductDto Product { get; set; }
        public EditModel(IGrowByDataService service)
        {
            _service = service;
        }
        public async void OnGet(int id) 
        {
            Product = await _service.GetById(id);
           
        }
        public async Task<IActionResult> OnPost(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var status = await _service.Update(product);
                }
                catch (Exception ex)
                {
                    return RedirectToPage("Index");
                }
                
            }
            return RedirectToPage("Index");
        }
    }
}

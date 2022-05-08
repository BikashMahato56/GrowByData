using GrowByData.Data;
using GrowByData.Data.Entities;
using GrowByData.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GrowByData.Pages
{
    public class ECommerceProductsModel : PageModel
    {
        private readonly IGrowByDataService _service;

        public ProductList ProductList { get; set; }
        public IEnumerable<ProductDto> products { get; set; } 
        public ECommerceProductsModel(IGrowByDataService service)
        {
            _service = service;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            //var model = await _service.ECommerceApi();
            var model = await _service.GetAll();
            products = model;
            return Page();
        }
       
    }

  
   
}

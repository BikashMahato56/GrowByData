using Microsoft.EntityFrameworkCore;

namespace GrowByData.Data.Entities
{
    [Keyless]
    public class ProductList
    {
        public IEnumerable<Product> Data { get; set; }
    }
}

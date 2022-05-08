using GrowByData.Data.Entities;

namespace GrowByData.Data
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public string Rating { get; set; }
        public string Link { get; set; }
        public DateTime AddedOn { get; set; }
    }
}


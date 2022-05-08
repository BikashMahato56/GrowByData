using System.ComponentModel.DataAnnotations;

namespace GrowByData.Data.Entities
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public string Rating { get; set; }
        public Href Href { get; set; }
    }
}

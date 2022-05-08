using System.ComponentModel.DataAnnotations;

namespace GrowByData.Data.Entities
{

    public class Href
    {
        [Key]
        public int LinkID { get; set; }
        public int ProductID { get; set; }
        public string Link { get; set; }
    }
}

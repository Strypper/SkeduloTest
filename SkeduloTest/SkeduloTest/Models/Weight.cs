using SQLite;

namespace SkeduloTest.Models
{
    [Table("Weight")]
    public class Weight
    {
        public string imperial { get; set; }
        public string metric { get; set; }
    }
}

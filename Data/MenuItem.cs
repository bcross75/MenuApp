using System.ComponentModel.DataAnnotations.Schema;

namespace MenuApp.Data
{
    [Table("MenuItem")]
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CalorieCount { get; set; }
        public decimal Price { get; set; }

        [Column("CategoryId")]
        public Categories Category { get; set; }
        public bool Active { get; set; }
    }
}

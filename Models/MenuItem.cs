namespace MenuApp.Models
{
    public class MenuItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CalorieCount { get; set; }
        public string Price { get; set; }
        public Categories Category { get; set; }
        public bool Active { get; set; }
    }
}

namespace MenuApp.Data
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CalorieCount { get; set; }
        public decimal Price { get; set; }
        public Categories Category { get; set; }
        public bool Active { get; set; }
    }
}

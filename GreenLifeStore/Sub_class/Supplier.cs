namespace GreenLifeStore.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string DistanceType { get; set; } // LONG or SHORT
    }
}
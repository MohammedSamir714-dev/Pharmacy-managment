namespace Pharmacy_managment.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Medicene> medicenes { get; set; }
    }
}

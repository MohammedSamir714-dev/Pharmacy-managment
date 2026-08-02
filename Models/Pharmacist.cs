namespace Pharmacy_managment.Models
{
    public class Pharmacist
    {
        public int Id { get; set; }
        public string Licensenumber { get; set; }
        public decimal Salary { get; set; }
        public DateOnly HireDate { get; set; }
        public PharmacistStatus Status { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Invoice> inovices { get; set; } = [];
    }
}

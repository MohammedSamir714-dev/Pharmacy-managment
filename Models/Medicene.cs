using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;

namespace Pharmacy_managment.Models
{
    public class Medicene
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<PurchaseOrderMedicene> PurchaseOrderMedicenes { get; set; }
        public ICollection<InvoiceMedicenedetails> InvoiceMedicenedetails { get; set; }
        public ICollection<MedicineBatch> medicineBatches { get; set; }
        [NotMapped]
        public int TotalQuntityInStock => medicineBatches?.Sum(b => b.Quntityremaining) ?? 0;

    }
}

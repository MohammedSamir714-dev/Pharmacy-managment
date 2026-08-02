using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy_managment.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
        public PaymentMethod Paymentmethod { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int PharmacistId { get; set; }
        public Pharmacist Pharmacist { get; set; }
        public ICollection<InvoiceMedicenedetails> InvoiceMedicenedetails { get; set; }
        [NotMapped]
        public decimal TotalAmount => InvoiceMedicenedetails.Sum(d => d.Quantity * d.UnitPrice);
    }
}

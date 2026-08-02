namespace Pharmacy_managment.Models
{
    public class InvoiceMedicenedetails
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int InvoiceId { get; set; }
        public int MediceneId { get; set; }
        public Invoice Invoice { get; set; }
        public Medicene Medicene { get; set; }
    }
}

namespace Pharmacy_managment.Models
{
    public class PurchaseOrderMedicene
    {
        public int Quantity { get; set; }
        public int MediceneId { get; set; }
        public int PurchaseOrderId { get; set; }
        public decimal UnitPrice { get; set; }
        public PurChaseOrder PurChaseOrder { get; set; }
        public Medicene Medicene { get; set; }
    }
}

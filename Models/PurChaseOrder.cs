namespace Pharmacy_managment.Models
{
    public class PurChaseOrder
    {
        public int Id { get; set; }
        public DateTime orderDate { get; set; }= DateTime.UtcNow;
        public PurchaseOrderStatus PurchaseOrderStatus { get; set; }=PurchaseOrderStatus.Pending;
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<PurchaseOrderMedicene> PurchaseOrderMedicenes { get; set; }
        public ICollection<MedicineBatch> MedicineBatches { get; set; } = [];

    }
}

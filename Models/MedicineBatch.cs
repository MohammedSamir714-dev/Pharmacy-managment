using System.ComponentModel.DataAnnotations;

namespace Pharmacy_managment.Models
{
    public class MedicineBatch
    {
        public int Id { get; set; }
        public string BatchName { get; set; }
        public DateOnly ManufactureDate { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public int QuntityReceived { get; set; }
        public int Quntityremaining { get; set; }
        public int MediceneId { get; set; }
        public Medicene Medicene { get; set; }
        public int? PurchaseOrderId { get; set; }
        public PurChaseOrder PurchaseOrder { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}

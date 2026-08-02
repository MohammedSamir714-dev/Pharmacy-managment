namespace Pharmacy_managment.Errors
{
    public class PurchaseOrderErrors
    {
        public static readonly Error NotFound =
       new(
           "PurchaseOrder.NotFound",
           "Purchase order was not found.",
           StatusCodes.Status404NotFound);

        public static readonly Error InvalidStatus =
            new(
                "PurchaseOrder.InvalidStatus",
                "The purchase order status is invalid.",
                StatusCodes.Status400BadRequest);

        public static readonly Error AlreadyReceived =
            new(
                "PurchaseOrder.AlreadyReceived",
                "The purchase order has already been received.",
                StatusCodes.Status409Conflict);

        public static readonly Error AlreadyCancelled =
            new(
                "PurchaseOrder.AlreadyCancelled",
                "The purchase order has already been cancelled.",
                StatusCodes.Status409Conflict);
        public static readonly Error EmptyOrder = 
            new("PurchaseOrder.EmptyOrder", "Purchase order must contain at least one medicine", StatusCodes.Status400BadRequest);
        public static readonly Error InvalidQuantity = new("PurchaseOrder.InvalidQuantity", "Quantity must be greater than zero", StatusCodes.Status400BadRequest);
        public static readonly Error InvalidStatusTransition = new("PurchaseOrder.InvalidStatusTransition", "This status transition is not allowed", StatusCodes.Status400BadRequest);
        public static readonly Error EmptyReceipt = new("PurchaseOrder.EmptyReceipt", "Received batches list cannot be empty", StatusCodes.Status400BadRequest);
        public static readonly Error MedicineNotInOrder = new("PurchaseOrder.MedicineNotInOrder", "This medicine is not part of the purchase order", StatusCodes.Status400BadRequest);
        public static readonly Error CannotCancelReceivedOrder = new("PurchaseOrder.CannotCancelReceivedOrder", "Cannot cancel an order that has already been received", StatusCodes.Status400BadRequest);
    }
}

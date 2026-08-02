using Pharmacy_managment.Contracts.PurchaseOrderDTO;

namespace Pharmacy_managment.Services.PurChaseOrderServices
{
    public interface IPurChaseOrderService
    {
        Task<Result<IEnumerable<PurchaseOrderResponse>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<PurchaseOrderResponse>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<PurchaseOrderResponse>> AddAsync(PurchaseOrderRequest request, CancellationToken cancellationToken);
        Task<Result> ApproveAsync(int id, CancellationToken cancellationToken);
        Task<Result> ReceiveAsync(int id, ReceivePurchaseOrderRequest request, CancellationToken cancellationToken);
        Task<Result> CancelAsync(int id, CancellationToken cancellationToken);
    }
}

namespace Pharmacy_managment.Services.MedicineServices
{
    public interface IMedicineService
    {
        Task<Result<IEnumerable<MediceneResponse>>>GetAllAsync(CancellationToken cancellation);
        Task<Result<MediceneResponse>> GetById(int id,CancellationToken cancellation);
        Task<Result<MediceneResponse>>AddAsync(MediceneRequest request,CancellationToken cancellation);
        Task<Result<IEnumerable<MediceneResponse>>> GetLowStockAsync(int minimumStock = 10);
        Task<Result<IEnumerable<MediceneResponse>>>SearchAsync(string Name,CancellationToken cancellation);
        Task<Result>UpdateAync(int id,UpdateMedicene request ,CancellationToken cancellation);
        Task<Result>DeleteAsync(int id, CancellationToken cancellation);
    }
}

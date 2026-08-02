using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_managment.Services.MedicineBatchServices;

namespace Pharmacy_managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineBatchController(IMedicineBatchService medicineBatch) : ControllerBase
    {
        private readonly IMedicineBatchService medicineBatch = medicineBatch;
        [HttpGet("GetAllMedicineBatch")]
        public async Task<IActionResult>GetAll(CancellationToken cancellationToken)
        {
            var MedicinesBatches=await medicineBatch.GetAllAsync(cancellationToken);
            return MedicinesBatches.IsSuccess? Ok(MedicinesBatches.Value): MedicinesBatches.ToProblem();
        }
        [HttpGet("GetById{id}")]
        public async Task<IActionResult> GetByID([FromRoute]int id,CancellationToken cancellation)
        {
            var MedicineBatch=await medicineBatch.GetByIdAsync(id,cancellation);
            return MedicineBatch.IsSuccess ? Ok(MedicineBatch.Value) : MedicineBatch.ToProblem();
        }
        [HttpPut("UpdateBatch{id}")]
        public async Task<IActionResult> Updatebatch([FromRoute]int id, [FromBody]MediceneBatchRequest request,CancellationToken cancellation)
        {
            var UpdateMedicineBatch = await medicineBatch.UpdateAsync(id, request, cancellation);
            return UpdateMedicineBatch.IsSuccess ? NoContent() : UpdateMedicineBatch.ToProblem();
        }
        [HttpDelete("deletebatch{id}")]
        public async Task<IActionResult> Deletebatch([FromRoute]int id,CancellationToken cancellation)
        {
            var deletebatch=await medicineBatch.DeleteAsync(id, cancellation);
            return deletebatch.IsSuccess ? NoContent() : deletebatch.ToProblem();
        }
        [HttpGet("GetExpiring")]
        public async Task<IActionResult>Getexpire(CancellationToken cancellation)
        {
            var Expire=await medicineBatch.GetExpireAsync(cancellation);
            return Expire.IsSuccess?Ok(Expire.Value) : Expire.ToProblem();
        }
        [HttpGet("ExpireSoon")]
        public async Task<IActionResult> GetExpirSoon([FromQuery]int days=30,CancellationToken cancellation=default)
        {
            var result=await medicineBatch.GetExpiringSoonAsync(days, cancellation);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpGet("available{medicineId}")]
        public async Task<IActionResult>GetAvailableBatches([FromRoute]int medicineId,CancellationToken cancellation)
        {
            var result= await medicineBatch.GetAvailbleBatchesAsync(medicineId, cancellation);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Pharmacy_managment.Services.MedicineServices;

namespace Pharmacy_managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController(IMedicineService medicineService) : ControllerBase
    {
        private readonly IMedicineService medicineService = medicineService;
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var medicine = await medicineService.GetAllAsync(cancellationToken);
            return Ok(medicine.Value);
        }
        [HttpGet("GetLowStock")]
        public async Task<IActionResult> GetLowStocks(int minimumStock=10)
        {
            var Result=await medicineService.GetLowStockAsync(minimumStock);
            return Ok(Result.Value);
        }
        [HttpGet("Search")]
        public async Task<IActionResult>Seaech(string Name,CancellationToken cancellation)
        {
            var Result=await medicineService.SearchAsync(Name,cancellation);
            return Ok(Result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById([FromRoute]int id,CancellationToken cancellation)
        {
            var medicine = await medicineService.GetById(id, cancellation);
            return Ok(medicine.Value);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]MediceneRequest request,CancellationToken cancellation)
        {
            var newmedicine=await medicineService.AddAsync(request, cancellation);
            return newmedicine.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = newmedicine.Value.Id }, newmedicine.Value) : newmedicine.ToProblem();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateMedicine([FromRoute]int id,[FromBody]UpdateMedicene request,CancellationToken cancellation)
        {
            var UpdateMedicine=await medicineService.UpdateAync(id, request, cancellation);
            return UpdateMedicine.IsSuccess ? NoContent() : UpdateMedicine.ToProblem();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>Deletemedicine(int id,CancellationToken cancellationToken)
        {
            var DeleteMedicine=await medicineService.DeleteAsync(id, cancellationToken);
            return DeleteMedicine.IsSuccess?NoContent() : DeleteMedicine.ToProblem();
        }
    }
}

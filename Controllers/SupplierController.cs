using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_managment.Contracts.SupplierDTO;
using Pharmacy_managment.Services.SupplierServices;

namespace Pharmacy_managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController(ISupplierService supplierService) : ControllerBase
    {
        private readonly ISupplierService supplierService = supplierService;
        [HttpGet]
        public async Task<IActionResult>GetAll(CancellationToken cancellationToken)
        {
            var Suppliers=await supplierService.GetAllAsync(cancellationToken);
            return Ok(Suppliers.Value);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id,CancellationToken cancellation)
        {
            var Supplier= await supplierService.GetByIdAsync(id,cancellation);
            return Ok(Supplier.Value);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]SupplierRequest request,CancellationToken cancellationToken)
        {
            var NewSupplier=await supplierService.AddAsync(request,cancellationToken);
            return NewSupplier.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = NewSupplier.Value.Id }, NewSupplier.Value) : NewSupplier.ToProblem();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier([FromRoute]int id, [FromBody]UpdateSupplier request,CancellationToken cancellation)
        {
            var UpdateSupplier=await supplierService.UpdateAsync(id,request,cancellation);
            return UpdateSupplier.IsSuccess?NoContent():UpdateSupplier.ToProblem();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier([FromRoute]int id,CancellationToken cancellation)
        {
            var DeleteSupplier=await supplierService.DeleteAsync(id, cancellation);
            return DeleteSupplier.IsSuccess?NoContent():DeleteSupplier.ToProblem();
        }
    }
}

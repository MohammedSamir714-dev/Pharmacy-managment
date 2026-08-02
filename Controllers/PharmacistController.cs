using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_managment.Abstractions.Consts;
using Pharmacy_managment.Contracts.PharmacistDTO;
using Pharmacy_managment.Services.PharmacistServices;

namespace Pharmacy_managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = DefaultRoles.Admin)]
    public class PharmacistController(IPharmacistService pharmacistService) : ControllerBase
    {
        private readonly IPharmacistService pharmacistService = pharmacistService;

        [HttpGet]
        public async Task<IActionResult>GetAll(CancellationToken cancellationToken)
        {
            var result= await pharmacistService.GetAllAsync(cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id,CancellationToken cancellation)
        {
            var result= await pharmacistService.GetByIdAsync(id,cancellation);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]PharmacistRequest request, CancellationToken cancellationToken)
        {
            var result = await pharmacistService.AddAsync(request, cancellationToken);

            return result.IsSuccess
                ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
                : result.ToProblem();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePharmacist request, CancellationToken cancellationToken)
        {
            var result = await pharmacistService.UpdateAsync(id, request, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await pharmacistService.DeleteAsync(id, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> ChangeStatus(int id, UpdatePharmacistStatus request, CancellationToken cancellationToken)
        {
            var result = await pharmacistService.ChangeStatusAsync(id, request, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}

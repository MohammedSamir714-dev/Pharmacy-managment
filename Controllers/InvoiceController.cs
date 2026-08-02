using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_managment.Abstractions.Consts;
using Pharmacy_managment.Contracts.InvoiceDTO;
using Pharmacy_managment.Services.InvoiceServices;

namespace Pharmacy_managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController(InvoiceServices invoiceServices) : ControllerBase
    {
        private readonly InvoiceServices invoiceServices = invoiceServices;
        [HttpGet]
        [Authorize(Roles = DefaultRoles.Admin)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await invoiceServices.GetAllAsync(cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{DefaultRoles.Admin},{DefaultRoles.Pharmacist}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await invoiceServices.GetByIdAsync(id, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost]
        [Authorize(Roles = $"{DefaultRoles.Admin},{DefaultRoles.Pharmacist}")]
        public async Task<IActionResult> Add(InvoiceRequest request, CancellationToken cancellationToken)
        {
            var result = await invoiceServices.AddAsync(request, cancellationToken);

            return result.IsSuccess
                ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
                : result.ToProblem();
        }
    }
}





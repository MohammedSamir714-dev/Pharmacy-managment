using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_managment.Abstractions.Consts;
using Pharmacy_managment.Contracts.PurchaseOrderDTO;
using Pharmacy_managment.Services.PurChaseOrderServices;

namespace Pharmacy_managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = DefaultRoles.Admin)]
    public class PurchaseOrderController(IPurChaseOrderService purChaseOrderService) : ControllerBase
    {
        private readonly IPurChaseOrderService purChaseOrder = purChaseOrderService;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await purChaseOrder.GetAllAsync(cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await purChaseOrder.GetByIdAsync(id, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PurchaseOrderRequest request, CancellationToken cancellationToken)
        {
            var result = await purChaseOrder.AddAsync(request, cancellationToken);

            return result.IsSuccess
                ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
                : result.ToProblem();
        }

        [HttpPut("{id}/approve")]
        public async Task<IActionResult> Approve(int id, CancellationToken cancellationToken)
        {
            var result = await purChaseOrder.ApproveAsync(id, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpPut("{id}/receive")]
        public async Task<IActionResult> Receive(int id, ReceivePurchaseOrderRequest request, CancellationToken cancellationToken)
        {
            var result = await purChaseOrder.ReceiveAsync(id, request, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id, CancellationToken cancellationToken)
        {
            var result = await purChaseOrder.CancelAsync(id, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}

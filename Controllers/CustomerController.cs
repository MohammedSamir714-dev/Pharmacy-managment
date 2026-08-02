using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_managment.Abstractions.Consts;
using Pharmacy_managment.Contracts.CustomerDTO;

namespace Pharmacy_managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService customerService = customerService;
        [HttpGet("GEtAllCustpmers")]
        [Authorize(Roles = $"{DefaultRoles.Admin},{DefaultRoles.Pharmacist}")]
        public async Task<IActionResult> GetAll(CancellationToken cancellation)
        {
            var result = await customerService.GetAllAsync(cancellation);
            return result.IsSuccess? Ok(result.Value):result.ToProblem();
        }
        [HttpGet("{id}")]
        [Authorize(Roles = $"{DefaultRoles.Admin},{DefaultRoles.Pharmacist}")]
        public async Task<IActionResult> GetById([FromRoute]int id,CancellationToken cancellation)
        {
            var result= await customerService.GetByIdAsync(id,cancellation);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpGet("Search")]
        [Authorize(Roles = $"{DefaultRoles.Admin},{DefaultRoles.Pharmacist}")]
        public async Task<IActionResult> Search([FromQuery]string Name,CancellationToken cancellation)
        {
            var result = await customerService.SearchAsync(Name, cancellation);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();

        }
        [HttpPost]
        [Authorize(Roles = $"{DefaultRoles.Admin},{DefaultRoles.Pharmacist}")]
        public async Task<IActionResult> Add([FromBody]CustomerRequest request,CancellationToken cancellation)
        {
            var result=await customerService.AddAsync(request,cancellation);
            return result.IsSuccess ? CreatedAtAction(nameof(GetById), new {id=result.Value.Id},result.Value) : result.ToProblem();
        }
        [HttpPut("{id}")]
        [Authorize(Roles = DefaultRoles.Admin)]
        public async Task<IActionResult>Update(int id,[FromBody]UpdateCustomer request,CancellationToken cancellation)
        {
            var result = await customerService.UpdateAsync(id,request, cancellation);
           return  result.IsSuccess?NoContent() : result.ToProblem(); 

        }
        [HttpDelete("{id}")]
        [Authorize(Roles = DefaultRoles.Admin)]
        public async Task<IActionResult>Delete(int id,CancellationToken cancellation)
        {
            var result= await customerService.DeleteAsync(id,cancellation);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}

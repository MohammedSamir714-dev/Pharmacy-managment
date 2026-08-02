using Microsoft.AspNetCore.Mvc;
using Pharmacy_managment.Contracts.CategoryDTO;
using Pharmacy_managment.Services.CategoryServices;

namespace Pharmacy_managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService categoryService = categoryService;
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var Categories = await categoryService.GetAllAsync(cancellationToken);
            return Ok(Categories.Value);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
        var Category= await categoryService.GetByIdAsync(id, cancellationToken);
            return Ok(Category.Value);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CategoryRequest request,CancellationToken cancellationToken)
        {
            var Newcategory=await categoryService.AddAsync(request,cancellationToken);
            return Newcategory.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = Newcategory.Value.Id }, Newcategory.Value) : Newcategory.ToProblem();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>Update(int id,UpdateCategory request,CancellationToken cancellationToken)
        {
            var UpdateCategory = await categoryService.UpdateAsync(id,request, cancellationToken);
            return UpdateCategory.IsSuccess ? NoContent() : UpdateCategory.ToProblem();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id,CancellationToken cancellationToken)
        {
            var DeleteCategory=await categoryService.DeleteAsync(id,cancellationToken);
            return DeleteCategory.IsSuccess?NoContent() :DeleteCategory.ToProblem();
        }
    }
}

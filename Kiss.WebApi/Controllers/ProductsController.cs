using Microsoft.AspNetCore.Mvc;
using $ext_projectname$.Application.Interfaces;
using $ext_projectname$.Application.Parameters;
using $ext_projectname$.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace $safeprojectname$.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// <summary>
        /// SELECT records
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductsParameter filter)
        {
            var data = await unitOfWork.Products.GetAllAsync(filter.PageNumber, filter.PageSize);
            return Ok(data);
        }
        /// <summary>
        /// SELECT a record by id
        /// </summary>
        /// <param name="id">product unique id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await unitOfWork.Products.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        /// <summary>
        /// INSERT a record
        /// </summary>
        /// <param name="product">Data fields for insert</param>
        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            var data = await unitOfWork.Products.AddAsync(product);
            return Ok(data);
        }
        /// <summary>
        /// UPDATE a record
        /// </summary>
        /// <param name="product">Data fields for update</param>
        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            var data = await unitOfWork.Products.UpdateAsync(product);
            return Ok(data);
        }
        /// <summary>
        /// DELETE a record by id
        /// </summary>
        /// <param name="id">product unique id</param>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await unitOfWork.Products.DeleteAsync(id);
            return Ok(data);
        }
    }
}
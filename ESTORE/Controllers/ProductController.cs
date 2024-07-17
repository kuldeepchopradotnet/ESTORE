using DAL.Entities;
using DAL.Repositories.ProductRepository;
using Microsoft.AspNetCore.Mvc;

namespace ESTORE.Controllers
{
    [Route("[controller]")]
    [ApiController]
   /* [Authorize]*/
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
                _productRepository = productRepository; 
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _productRepository.get(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

       /* [HttpGet]
        public ActionResult Get(int id)
        {
            return Ok(new { id });
        }*/

        [HttpGet("all")]
        public ActionResult GetAll()
        {
            return Ok("all product");
        }

        [HttpPost]
        public async Task<ActionResult> save(Product product)
        {
            await _productRepository.AddAsync(product);
            await _productRepository.saveAsync();
            return Ok(product.Id);
        }

        [HttpPut]
        public ActionResult update()
        {
            return Ok("updated");
        }


        [HttpDelete]
        public ActionResult delete(string id) { 
            return Ok("deleted");
        }

      /*  [HttpDelete]
        public ActionResult delete(int id)
        {
            return Ok("deleted");
        }*/


        [HttpDelete("all")]
        public ActionResult deleteAll()
        {
            return Ok("all deleted");
        }

       
    }
}

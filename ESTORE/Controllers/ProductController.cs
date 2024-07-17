using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESTORE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {

        public ProductController()
        {
                
        }

        [HttpGet]
        public ActionResult Get(string id)
        {
            return Ok(new { id });
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
        public ActionResult save()
        {
            return Ok("saved");
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

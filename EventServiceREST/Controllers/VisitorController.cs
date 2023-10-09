using EventService.BL.Managers;
using EventService.BL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventServiceREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private VisitorManager VM;
        public VisitorController(VisitorManager vm) { VM = vm; }
        [HttpGet]
        public ActionResult<List<Visitor>> GetAll()
        {
            try
            {
                return Ok(VM.GetAllVisitors());
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("{id}")]
        public ActionResult<Visitor> Get(int id)
        {
            try
            {
                return Ok(VM.GetVisitor(id));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPost]
        public ActionResult<Visitor> Post([FromBody] Visitor visitor) 
        { 
            if (visitor == null) { return BadRequest("invalid visitor"); }
            try
            {
                visitor=VM.RegisterVisitor(visitor);
                VM.SubscribeVisitor(visitor);
                return CreatedAtAction(nameof(Get),new {id=visitor.Id},visitor);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!VM.ExistsVisitor(id)) return NotFound();
                VM.UnSubscribeVisitor(VM.GetVisitor(id));
                return NoContent();
            }
            catch(Exception ex) { return BadRequest($"Unable to delete {id}"); }
        }
    }
}

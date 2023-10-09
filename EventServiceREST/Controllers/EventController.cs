using EventService.BL.Managers;
using EventService.BL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventServiceREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private EventManager EM;
        private VisitorManager VM;

        public EventController(EventManager eM,VisitorManager vM)
        {
            EM = eM;
            VM = vM;
        }

        [HttpGet("{name}")]
        public ActionResult<Event> Get(string name) 
        {
            try
            {
                return Ok(EM.GetEvent(name));
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet]
        [Route("location")]
        public ActionResult<List<Event>> GetWithLocation([FromQuery] string location)
        {
            try
            {
                return Ok(EM.GetEventsForLocation(location));
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet]
        [Route("date")]
        public ActionResult<List<Event>> GetWithDate([FromQuery] string dateString)
        {
            try
            {
                return Ok(EM.GetEventsForDate(DateTime.Parse(dateString)));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPost]
        [Route("{name}/visitor")]
        public ActionResult<Event> SubscribeVisitor(string name,[FromBody] int visitorId)
        {
            try
            {
                Visitor visitor=VM.GetVisitor(visitorId);
                Event ev = EM.GetEvent(name);
                EM.SubscribeVisitor(visitor, ev);
                return CreatedAtAction(nameof(Get), new { name = name }, ev);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("{name}/visitor/{id}")]
        public ActionResult<Event> UnSubscribeVisitor(string name, int id)
        {
            try
            {
                Visitor visitor = VM.GetVisitor(id);
                Event ev = EM.GetEvent(name);
                EM.UnsubscribeVisitor(visitor, ev);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

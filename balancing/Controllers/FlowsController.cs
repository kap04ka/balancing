using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using balancing.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace balancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FlowsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Flows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flow>>> GetFlows()
        {
          if (_context.Flows == null)
          {
              return NotFound();
          }
            return await _context.Flows.ToListAsync();
        }

        // GET: api/Flows/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flow>> GetFlow(int id)
        {
          if (_context.Flows == null)
          {
              return NotFound();
          }
            var flow = await _context.Flows.FindAsync(id);

            if (flow == null)
            {
                return NotFound();
            }

            return flow;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlow(int id, UpdateFlowDto updateFlow)
        {
            var flow = await _context.Flows.FindAsync(id);
            if (flow == null)
            {
                return NotFound();
            }

            if(updateFlow.Name != null) flow.Name = updateFlow.Name;
            if(updateFlow.Value != null) flow.Value = (double)updateFlow.Value;
            if(updateFlow.Tols != null) flow.Tols = (double)updateFlow.Tols;
            if(updateFlow.IsUsed != null) flow.IsUsed = (bool)updateFlow.IsUsed;

            flow.LowerBound = updateFlow.LowerBound;
            flow.UpperBound = updateFlow.UpperBound;
            flow.SourceNode = updateFlow.SourceNode;
            flow.DestNode = updateFlow.DestNode;

            _context.Entry(flow).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Flow>> PostFlow(AddFlowDto addFlow)
        {
            Flow flow = new Flow();

            flow.Name = addFlow.Name;
            flow.Value = addFlow.Value;
            flow.Tols = addFlow.Tols;
            flow.IsUsed = addFlow.IsUsed;
            flow.LowerBound = addFlow.LowerBound;
            flow.UpperBound = addFlow.UpperBound;
            flow.SourceNode = addFlow.SourceNode;
            flow.DestNode = addFlow.DestNode;

            _context.Flows.Add(flow);
            await _context.SaveChangesAsync();

            return flow;
        }

        // DELETE: api/Flows/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlow(int id)
        {
            if (_context.Flows == null)
            {
                return NotFound();
            }
            var flow = await _context.Flows.FindAsync(id);
            if (flow == null)
            {
                return NotFound();
            }

            _context.Flows.Remove(flow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllFlows()
        {
            if (_context.Flows == null)
            {
                return NotFound();
            }
            var flows =  await _context.Flows.ToListAsync();
            if (flows == null)
            {
                return NotFound();
            }
            foreach (var flow in flows) {
                _context.Flows.Remove(flow);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FlowExists(int id)
        {
            return (_context.Flows?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

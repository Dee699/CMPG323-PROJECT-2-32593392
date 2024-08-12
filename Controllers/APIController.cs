using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_32593392.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace API_32593392.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly MasterContext _context;

        public APIController(MasterContext context)
        {
            _context = context;
        }

        //  GET method that retrieves all Telemetry entries from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobTelemetry>>> GetJobTelemetries()
        {
            return await _context.JobTelemetries.ToListAsync();
        }

        // GET: GET method that will retrieve one Telemetry from the database based on the ID parsed through
        [HttpGet("{id}")]
        public async Task<ActionResult<JobTelemetry>> GetJobTelemetry(int id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);

            if (jobTelemetry == null)
            {
                return NotFound();
            }

            return jobTelemetry;
        }

        // PUT: api/API/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobTelemetry(int id, JobTelemetry jobTelemetry)
        {
            if (id != jobTelemetry.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobTelemetry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTelemetryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: POST method that will create a new Telemetry entry on the database

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobTelemetry>> PostJobTelemetry(JobTelemetry jobTelemetry)
        {
            _context.JobTelemetries.Add(jobTelemetry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobTelemetry", new { id = jobTelemetry.Id }, jobTelemetry);
        }

        // PATCH: api/API/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchJobTelemetry(int id, [FromBody] JsonPatchDocument<JobTelemetry> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Patch document is null.");
            }

            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);
            if (jobTelemetry == null)
            {
                return NotFound();
            }

            // Apply the patch document to the entity
            patchDoc.ApplyTo(jobTelemetry, (error) =>
            {
                // Add error details to ModelState
                ModelState.AddModelError(error.Operation.path, $"Operation error: {error.Operation.op}");
            });

            // Check for model state errors
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTelemetryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE method that will delete an existing Telemetry entry on the database
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobTelemetry(int id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);
            if (jobTelemetry == null)
            {
                return NotFound();
            }

            _context.JobTelemetries.Remove(jobTelemetry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobTelemetryExists(int id)
        {
            return _context.JobTelemetries.Any(e => e.Id == id);
        }

        [HttpGet("savings", Name = "GetSavingsByProject")]
        public async Task<IActionResult> GetSavings(int projectId, DateTime startDate, DateTime endDate)
        {
            var telemetries = await _context.JobTelemetries
                .Where(t => t.ProjectId == projectId && t.EntryDate >= startDate && t.EntryDate <= endDate)
                .ToListAsync();

            if (telemetries == null || !telemetries.Any())
            {
                return NotFound("No telemetry data found for the specified project and date range.");
            }

            var cumulativeTimeSaved = telemetries.Sum(t => t.TimeSaved);
            var cumulativeCostSaved = telemetries.Sum(t => t.CostSaved);

            var result = new
            {
                ProjectId = projectId,
                StartDate = startDate,
                EndDate = endDate,
                CumulativeTimeSaved = cumulativeTimeSaved,
                CumulativeCostSaved = cumulativeCostSaved
            };

            return Ok(result);
        }
        
        [HttpGet("Savings2", Name = "GetSavingsbyclient")]
        public async Task<IActionResult> GetSavings2(int clientId, DateTime startDate, DateTime endDate)
        {
            var telemetries = await _context.JobTelemetries
                .Where(t => t.ClientId == clientId && t.EntryDate >= startDate && t.EntryDate <= endDate)
                .ToListAsync();

            if (!telemetries.Any())
            {
                return NotFound("No telemetry data found for the specified client and date range.");
            }

            var cumulativeTimeSaved = telemetries.Sum(t => t.TimeSaved);
            var cumulativeCostSaved = telemetries.Sum(t => t.CostSaved);

            var result = new
            {
                ClientId = clientId,
                StartDate = startDate,
                EndDate = endDate,
                CumulativeTimeSaved = cumulativeTimeSaved,
                CumulativeCostSaved = cumulativeCostSaved
            };

            return Ok(result);
        }
    }
}

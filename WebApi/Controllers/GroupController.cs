using Data;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly PersonManagementContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupssController" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GroupController(PersonManagementContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// DELETE: api/Groups/5.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Group>> DeleteGroup(long id)
        {
            var group = await context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            context.Groups.Remove(group);
            await context.SaveChangesAsync();

            return group;
        }

        /// <summary>
        /// GET: api/Groups/5.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(long id)
        {
            var group = await context.Groups.FindAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            return group;
        }

        /// <summary>
        /// GET: api/Groups.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            return await context.Groups.ToListAsync();
        }

        /// <summary>
        /// POST: api/Groups.
        /// </summary>
        /// <param name="group">The Group.</param>
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(Group group)
        {
            context.Groups.Add(group);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = group.Id }, group);
        }

        /// <summary>
        /// PUT: api/Groups/5.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="group">The Group.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(long id, Group group)
        {
            if (id != group.Id)
            {
                return BadRequest();
            }

            context.Entry(group).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        private bool GroupExists(long id)
        {
            return context.Groups.Any(e => e.Id == id);
        }
    }
}
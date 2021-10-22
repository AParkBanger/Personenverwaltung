using AutoMapper;
using Data;
using Data.Models.DAO;
using Data.Models.DTO;
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
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupssController" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GroupController(PersonManagementContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// DELETE: api/Groups/5.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<GroupDTO>> DeleteGroup(long id)
        {
            var group = await context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            context.Groups.Remove(group);
            await context.SaveChangesAsync();

            return mapper.Map<GroupDTO>(group);
        }

        /// <summary>
        /// GET: api/Groups/5.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDTO>> GetGroup(long id)
        {
            var group = await context.Groups.FindAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            return mapper.Map<GroupDTO>(group);
        }

        /// <summary>
        /// GET: api/Groups.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDTO>>> GetGroups()
        {
            var groups = await context.Groups.ToListAsync();

            return mapper.Map<List<GroupDTO>>(groups);
        }

        /// <summary>
        /// POST: api/Groups.
        /// </summary>
        /// <param name="group">The Group.</param>
        [HttpPost]
        public async Task<ActionResult<GroupDTO>> PostGroup(GroupDTO group)
        {
            var groupDAO = mapper.Map<GroupDAO>(group);

            var groupInDb = (await context.Groups.AddAsync(groupDAO)).Entity;
            await context.SaveChangesAsync();
            var createResult = CreatedAtAction(nameof(GroupController.GetGroup), new { id = groupInDb.Id }, mapper.Map<GroupDTO>(groupInDb));
            return createResult;
        }

        /// <summary>
        /// PUT: api/Groups/5.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="group">The Group.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(long id, GroupDTO group)
        {
            if (id != group.Id)
            {
                return BadRequest();
            }

            var groupDAO = mapper.Map<GroupDAO>(group);
            context.Entry(groupDAO).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
                var createResult = CreatedAtAction(nameof(GroupController.GetGroup), new { id = id }, mapper.Map<GroupDTO>(groupDAO));
                return createResult;
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
        }

        private bool GroupExists(long id)
        {
            return context.Groups.Any(e => e.Id == id);
        }
    }
}
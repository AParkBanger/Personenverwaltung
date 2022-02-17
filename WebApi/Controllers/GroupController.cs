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
    /// <summary>
    /// GroupController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly PersonManagementContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupController" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
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
            var groups = await context.Groups.Include(x => x.Persons).ToListAsync();

            return mapper.Map<List<GroupDTO>>(groups);
        }

        /// <summary>
        /// Posts the group.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="personIds">The person ids.</param>
        [HttpPost]
        public async Task<ActionResult<GroupDTO>> PostGroup(string name, long[] personIds)
        {
            var persons = new List<PersonDAO>();

            foreach (long personId in personIds)
            {
                persons.Add(context.Persons.Find(personId));
            }

            var groupDAO = new GroupDAO()
            {
                Name = name,
                Persons = persons,
            };

            var groupInDb = (await context.Groups.AddAsync(groupDAO)).Entity;
            await context.SaveChangesAsync();
            var createResult = CreatedAtAction(nameof(GroupController.GetGroup), new { id = groupInDb.Id }, mapper.Map<GroupDTO>(groupInDb));
            return createResult;
        }

        /// <summary>
        /// Puts the group.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="personIds">The person ids.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(long id, string groupName, long[] personIds)
        {
            var groupDAO = context.Groups.Include(x => x.Persons).FirstOrDefault(x => x.Id == id);

            if (groupDAO == null)
            {
                return NotFound();
            }

            groupDAO.Name = groupName;

            foreach (var person in groupDAO.Persons)
            {
                groupDAO.Persons.Remove(person);
            }

            foreach (long personId in personIds)
            {
                groupDAO.Persons.Add(context.Persons.Find(personId));
            }

            try
            {
                await context.SaveChangesAsync();
                var createResult = CreatedAtAction(nameof(GroupController.GetGroup), new { id = id }, mapper.Map<GroupDTO>(groupDAO));
                return createResult;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
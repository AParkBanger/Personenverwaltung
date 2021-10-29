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
    public class PersonsController : ControllerBase
    {
        private readonly PersonManagementContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonssController" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PersonsController(PersonManagementContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// DELETE: api/Persons/5.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonDTO>> DeletePerson(long id)
        {
            var person = await context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            context.Persons.Remove(person);
            await context.SaveChangesAsync();

            return mapper.Map<PersonDTO>(person);
        }

        /// <summary>
        /// GET: api/Persons/5.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDTO>> GetPerson(long id)
        {
            var person = await context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return mapper.Map<PersonDTO>(person);
        }

        /// <summary>
        /// GET: api/Persons.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetPersons()
        {
            var persons = await context.Persons.Include(x => x.Groups).ToListAsync();

            return mapper.Map<List<PersonDTO>>(persons);
        }

        /// <summary>
        /// POST: api/Persons.
        /// </summary>
        /// <param name="person">The person.</param>
        [HttpPost]
        public async Task<ActionResult<PersonDTO>> PostPerson(PersonDTO person)
        {
            var personDAO = mapper.Map<PersonDAO>(person);

            var personInDB = (await context.Persons.AddAsync(personDAO)).Entity;
            await context.SaveChangesAsync();
            var createResult = CreatedAtAction(nameof(PersonsController.GetPerson), new { id = personInDB.Id }, mapper.Map<PersonDTO>(personInDB));
            return createResult;
        }

        /// <summary>
        /// PUT: api/Persons/5.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="person">The person.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(long id, PersonDTO person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            var personDAO = mapper.Map<PersonDAO>(person);
            context.Entry(personDAO).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
                var createResult = CreatedAtAction(nameof(PersonsController.GetPerson), new { id = id }, mapper.Map<PersonDTO>(personDAO));
                return createResult;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool PersonExists(long id)
        {
            return context.Persons.Any(e => e.Id == id);
        }
    }
}